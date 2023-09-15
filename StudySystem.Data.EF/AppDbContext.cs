using Microsoft.EntityFrameworkCore;
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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<UserDetail> UserDetails => Set<UserDetail>();
        public DbSet<VerificationOTP> VerificationOTPs => Set<VerificationOTP>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region UserDetail
            modelBuilder.Entity<UserDetail>(cfg =>
            {
                cfg.HasKey(x => x.Username);
            });
            #endregion

            #region VerificationOTP
            modelBuilder.Entity<VerificationOTP>(cfg =>
            {
                cfg.HasKey(cfg => cfg.Username);
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

    public class UserResoveSerive
    {

    }
}
