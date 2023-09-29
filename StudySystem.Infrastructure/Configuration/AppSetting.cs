using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudySystem.Infrastructure.Configuration
{
    public static class AppSetting
    {
        public static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true).AddEnvironmentVariables().Build();

        public static string ConnectionString
        {
            get { return configuration["ConnectionStrings:DefaultConnection"]; }
        }
        #region configuration for jwt
        public static string JwtSub
        {
            get { return configuration["Jwt:Sub"]; }
        }
        public static string Salt
        {
            get { return configuration["Salt"]; }
        }
        public static string Issuer
        {
            get { return configuration["Jwt:Issuer"]; }
        }
        public static string Audience
        {
            get { return configuration["Jwt:Audience"]; }
        }
        public static string SecretKey
        {
            get { return configuration["Jwt:SecretKey"]; }
        }
        public static int JwtExpireTime
        {
            get { return Convert.ToInt32(configuration["Jwt:ExpireTime"]); }
        }
        #endregion

        #region configuration for mail
        public static string Mail
        {
            get { return configuration["MailSetting:Mail"]; }
        }
        public static string MailName
        {
            get { return configuration["MailSetting:Name"]; }
        }
        public static string MailPassword
        {
            get { return configuration["MailSetting:Password"]; }
        }
        public static string MailHost
        {
            get { return configuration["MailSetting:Host"]; }
        }
        public static int MailPort
        {
            get { return Convert.ToInt32(configuration["MailSetting:Port"]); }
        }
        #endregion

        // set time user online (minutes)
        public static int SettimeUserOnl
        {
            get { return Convert.ToInt32(configuration["SetTimeOnlUser2"]); }
        }
    }
}
