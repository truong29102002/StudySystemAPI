using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using StackExchange.Profiling.Storage;
using StudySystem.Application.Service;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.Configuration;
using StudySystem.Infrastructure.Resources;
using StudySystem.Middlewares;
using System.IO.Compression;
using System.Net;
using System.Text;
#region log info
System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// logger
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");
#endregion
var builder = WebApplication.CreateBuilder(args);
#region Nlog: setup Nlog
builder.Logging.ClearProviders();
builder.Host.UseNLog();
#endregion
#region add miniprofiler
// Add services to the container.
builder.Services.AddMemoryCache(); // add the memory cache to the service collection
// Add miniprofilter : write speed time query sql
builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler";
}).AddEntityFramework();
#endregion
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});



#region config jwt, AppDbContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserResoveSerive>();
builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AppSetting.Issuer,
        ValidAudience = AppSetting.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.SecretKey))
    };
});
#endregion

#region config allows web permission, response compression
builder.Services.AddCors(cors => cors.AddPolicy(name: "StudySystemPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
//Optimize data traffic transmitted between server and client
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // enables https is a secure risk
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

#endregion

#region register service Add Transient
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddTransient<IUserTokenService, UserTokenService>();
#endregion

#region configure connect to db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(AppSetting.ConnectionString, cfg =>
    {
        cfg.MigrationsAssembly("StudySystem.Data.EF");
        cfg.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
    });
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


#region seed data

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiniProfiler();
    app.UseHsts();
}

#region config allows web permission
app.UseCors("StudySystemPolicy");
#endregion

app.UseHttpsRedirection();
app.UseAuthentication();
#region custom middleware
app.UseMiddleware<AuthTokenMiddleware>();
app.ConfigureExceptionHandler();
#endregion
#region config status codes error response
app.Use(async (context, next) =>
{
    await next();
    dynamic responseError = new System.Dynamic.ExpandoObject();
    if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest) // 400
    {
        logger.Error(responseError);
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.BadRequest,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status400BadRequest, Message._400))
        });
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized) // 401
    {
        logger.Error(responseError);
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.Unauthorized,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status401Unauthorized, Message.Unauthorize))
        });
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden) // 403
    {
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.Forbidden,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status403Forbidden, Message._403))
        });
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.NotFound) // 404
    {
        logger.Error(responseError);
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.NotFound,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status404NotFound, Message._404))
        });
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed) // 405
    {
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.MethodNotAllowed,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status405MethodNotAllowed, Message._405))
        });
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError) // 405
    {
        await context.Response.WriteAsJsonAsync(new StudySystemAPIResponse<StudySystemErrorResponseModel>
        {
            Code = (int)HttpStatusCode.InternalServerError,
            Response = new Response<StudySystemErrorResponseModel>(false, new StudySystemErrorResponseModel(StatusCodes.Status500InternalServerError, Message._500))
        });
    }
});
#endregion

// use Response compression
app.UseResponseCompression();
app.UseAuthorization();

app.MapControllers();

app.Run();
