using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Infrastructure.CommonConstant
{
    public class CommonConstant
    {
        public const string PathFolderCsv = @"..\StudySystem.Data.EF\Seed Data\FileCSV\";
        public const string TypeFileCsv = @".csv";
        public const string CsvFileUserDetails = "_UserDetails_";
    }
    public static class Router
    {
        public const string RegisterUser = "~/api/register-user";
        public const string LoginUser = "~/api/login";
        public const string SendMail = "~/api/send-mail";
        public const string LogOut = "~/api/logout";
        public const string VerificationEmail = "~/api/verify-email";
        public const string IsUserOnl = "~/api/user-onl";
    }
}
