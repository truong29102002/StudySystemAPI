using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudySystem.Application.Service;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Infrastructure.Configuration;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region config jwt, AppDbContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserResoveSerive>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

#region config allows web permission
builder.Services.AddCors(cors => cors.AddPolicy(name: "StudySystemPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

#endregion

#region register service Add Transient
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient<IUserRegisterService, UserRegisterService>();
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddTransient<ILoginUserService, LoginUserService>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region config allows web permission
app.UseCors("StudySystemPolicy");
#endregion

app.UseHttpsRedirection();
app.UseAuthentication();

app.Use(async (context, next) =>
{
    await next();

});

app.UseAuthorization();

app.MapControllers();

app.Run();
