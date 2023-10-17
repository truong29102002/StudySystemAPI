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
        public DbSet<AdministrativeRegions> AdministrativeRegions => Set<AdministrativeRegions>();
        public DbSet<AdministrativeUnits> AdministrativeUnits => Set<AdministrativeUnits>();
        public DbSet<Provinces> Provinces => Set<Provinces>();
        public DbSet<Districts> Districts => Set<Districts>();
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
            modelBuilder.Entity<AdministrativeRegions>(cfg =>
            {
                cfg.HasKey(x => x.Id);
            });

            #endregion

            #region administrative_units
            modelBuilder.Entity<AdministrativeUnits>(cfg =>
            {
                cfg.HasKey(x => x.Id);
            });

            #endregion

            #region Provinces
            modelBuilder.Entity<Provinces>().HasKey(x => x.Code);
            // Define foreign key relationships
            modelBuilder.Entity<Provinces>()
                .HasOne(p => p.AdministrativeUnits)
                .WithMany()
                .HasForeignKey(p => p.AdministrativeUnitId);

            modelBuilder.Entity<Provinces>()
                .HasOne(p => p.AdministrativeRegions)
                .WithMany()
                .HasForeignKey(p => p.AdministrativeRegionId);

            // Define indexes
            modelBuilder.Entity<Provinces>()
                .HasIndex(p => p.AdministrativeRegionId)
                .HasName("idx_provinces_region");

            modelBuilder.Entity<Provinces>()
                .HasIndex(p => p.AdministrativeUnitId)
                .HasName("idx_provinces_unit");
            #endregion

            #region Districts
            modelBuilder.Entity<Districts>().HasKey(x => x.Code);
            // Define foreign key relationships
            modelBuilder.Entity<Provinces>()
                .HasMany(p => p.Districts)
                .WithOne(d => d.Provinces)
                .HasForeignKey(d => d.ProvinceCode)
                .OnDelete(DeleteBehavior.Restrict); // Modify the delete behavior as needed

            modelBuilder.Entity<AdministrativeUnits>()
                .HasMany(a => a.Districts)
                .WithOne(d => d.AdministrativeUnits)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Restrict); // Modify the delete behavior as needed

            // Define indexes
            modelBuilder.Entity<Districts>()
                .HasIndex(d => d.ProvinceCode)
                .HasName("idx_districts_province");

            modelBuilder.Entity<Districts>()
                .HasIndex(d => d.AdministrativeUnitId)
                .HasName("idx_districts_unit");
            #endregion

            #region Wards
            modelBuilder.Entity<Ward>().HasKey(x => x.Code);
            // Define foreign key relationships
            modelBuilder.Entity<Districts>()
                .HasMany(d => d.Wards)
                .WithOne(w => w.Districts)
                .HasForeignKey(w => w.DistrictCode)
                .OnDelete(DeleteBehavior.Restrict); // Modify the delete behavior as needed

            modelBuilder.Entity<AdministrativeUnits>()
                .HasMany(a => a.Wards)
                .WithOne(w => w.AdministrativeUnits)
                .HasForeignKey(w => w.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Restrict); // Modify the delete behavior as needed

            // Define indexes
            modelBuilder.Entity<Ward>()
                .HasIndex(w => w.DistrictCode)
                .HasName("idx_wards_district");

            modelBuilder.Entity<Ward>()
                .HasIndex(w => w.AdministrativeUnitId)
                .HasName("idx_wards_unit");
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
                .WithMany()
                .HasForeignKey(x => x.WardCode);

            // Define foreign key relationships n - 1 : addressUsers - district
            modelBuilder.Entity<AddressUser>()
                .HasOne(x => x.District)
                .WithMany()
                .HasForeignKey(x => x.DistrictCode);

            // Define foreign key relationships n - 1 : addressUsers - province
            modelBuilder.Entity<AddressUser>()
                .HasOne(x => x.Province)
                .WithMany()
                .HasForeignKey(x => x.ProvinceCode);

            // Define index at userId in table AddressUser
            modelBuilder.Entity<AddressUser>()
            .HasIndex(a => a.UserID)
            .HasName("IX_AddressUser_UserID");


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
