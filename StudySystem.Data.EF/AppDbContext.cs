using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> _logger;
        private readonly string _currentUser;
        public AppDbContext(DbContextOptions<AppDbContext> options, UserResoveSerive userResoveSerive, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;
            _currentUser = userResoveSerive.GetUser();
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<UserDetail> UserDetails => Set<UserDetail>();
        public DbSet<VerificationOTP> VerificationOTPs => Set<VerificationOTP>();
        public DbSet<ApplicationUserToken> UserTokens => Set<ApplicationUserToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region UserDetail
            modelBuilder.Entity<UserDetail>(cfg =>
            {
                cfg.HasKey(x => x.UserID);
            });
            #endregion

            #region VerificationOTP
            modelBuilder.Entity<VerificationOTP>(cfg =>
            {
                cfg.HasKey(cfg => cfg.UserID);
            });
            #endregion

            #region User token
            modelBuilder.Entity<ApplicationUserToken>(cfg =>
            {
                cfg.HasKey(cfg => cfg.UserID);
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

    public class UserResoveSerive
    {
        private readonly IHttpContextAccessor _context;
        private readonly ILogger<UserResoveSerive> _logger;
        public UserResoveSerive(IHttpContextAccessor context, ILogger<UserResoveSerive> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string GetUser()
        {
            try
            {
                var user = _context.HttpContext?.User.Claims.FirstOrDefault(x=>x.Type == "UserID")?.Value;
                return user ?? "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "";
            }
        }
    }
}
