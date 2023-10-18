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
        public DbSet<UserToken> UserTokens => Set<UserToken>();
        public DbSet<AdministrativeRegion> AdministrativeRegions => Set<AdministrativeRegion>();
        public DbSet<AdministrativeUnit> AdministrativeUnits => Set<AdministrativeUnit>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<Ward> Wards => Set<Ward>();
        public DbSet<AddressUser> AddressUsers => Set<AddressUser>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region UserDetail
            modelBuilder.Entity<UserDetail>(cfg =>
            {
                cfg.HasKey(x => x.UserID);
            });
            // Create an index for the 'UserID' column in the 'UserDetail' table
            modelBuilder.Entity<UserDetail>()
                .HasIndex(ud => ud.UserID)
                .HasName("IX_UserDetail_UserID");
            #endregion

            #region VerificationOTP
            modelBuilder.Entity<VerificationOTP>(cfg =>
            {
                cfg.HasKey(cfg => cfg.UserID);
            });
            #endregion

            #region User token
            modelBuilder.Entity<UserToken>(cfg =>
            {
                cfg.HasKey(cfg => cfg.UserID);
            });
            #endregion

            #region administrative_regions
            modelBuilder.Entity<AdministrativeRegion>(cfg =>
            {
                cfg.HasKey(x => x.Id);
            });

            #endregion

            #region administrative_units
            modelBuilder.Entity<AdministrativeUnit>(cfg =>
            {
                cfg.HasKey(x => x.Id);
            });

            #endregion

            #region Provinces
            // define pk, fk
            modelBuilder.Entity<Province>().HasKey(x => x.Code);
            // AdministrativeRegions
            modelBuilder.Entity<Province>()
           .HasOne(p => p.AdministrativeRegion)
           .WithMany(p => p.Provinces)
           .HasForeignKey(p => p.AdministrativeRegionId).OnDelete(DeleteBehavior.Restrict);
            // AdministrativeUnits
            modelBuilder.Entity<Province>()
                .HasOne(p => p.AdministrativeUnit)
                .WithMany(p => p.Provinces)
                .HasForeignKey(p => p.AdministrativeUnitId).OnDelete(DeleteBehavior.Restrict);
            // define index
            modelBuilder.Entity<Province>()
                .HasIndex(x => new { x.AdministrativeRegionId, x.AdministrativeUnitId });
            #endregion

            #region Districts
            modelBuilder.Entity<District>().HasKey(x => x.Code);
            modelBuilder.Entity<District>()
           .HasOne(d => d.AdministrativeUnit)
           .WithMany(d => d.Districts)
           .HasForeignKey(d => d.AdministrativeUnitId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<District>()
                .HasOne(d => d.Province)
                .WithMany(d => d.Districts)
                .HasForeignKey(d => d.ProvinceCode).OnDelete(DeleteBehavior.Restrict);
            // define index
            modelBuilder.Entity<District>()
                .HasIndex(x => new { x.ProvinceCode, x.AdministrativeUnitId });
            #endregion

            #region Wards
            modelBuilder.Entity<Ward>().HasKey(x => x.Code);
            modelBuilder.Entity<Ward>()
            .HasOne(w => w.AdministrativeUnit)
            .WithMany(w => w.Wards)
            .HasForeignKey(w => w.AdministrativeUnitId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ward>()
                .HasOne(w => w.District)
                .WithMany(w => w.Wards)
                .HasForeignKey(w => w.DistrictCode).OnDelete(DeleteBehavior.Restrict);
            // define index
            modelBuilder.Entity<Ward>()
                .HasIndex(x => new { x.DistrictCode, x.AdministrativeUnitId });
            #endregion

            #region Address user
            modelBuilder.Entity<AddressUser>().HasKey(x => x.Id);
            // Define foreign key relationships 1 - 1 user - address
            modelBuilder.Entity<UserDetail>()
                .HasOne(x => x.AddressUser)
                .WithOne(x => x.UserDetail)
                .HasForeignKey<AddressUser>(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relationships n - 1 : addressUsers - ward
            modelBuilder.Entity<AddressUser>()
                .HasOne(x => x.Ward)
                .WithMany(x => x.AddressUsers)
                .HasForeignKey(x => x.WardCode).OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relationships n - 1 : addressUsers - district
            modelBuilder.Entity<AddressUser>()
                .HasOne(x => x.District)
                .WithMany(x => x.AddressUsers)
                .HasForeignKey(x => x.DistrictCode).OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relationships n - 1 : addressUsers - province
            modelBuilder.Entity<AddressUser>()
                .HasOne(x => x.Province)
                .WithMany(x => x.AddressUsers)
                .HasForeignKey(x => x.ProvinceCode).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressUser>()
                .HasIndex(x => new { x.UserID, x.WardCode, x.DistrictCode, x.ProvinceCode });
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
                var user = _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value;
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
