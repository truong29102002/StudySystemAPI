using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Infrastructure.Extensions
{
    public static class StringUtils
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static string NewUserRegisterAds()
        {
            return "Cảm ơn bạn đã đăng ký dịch vụ tin tức mới của chúng tôi";
        }

        public static string RankUser(decimal point)
        {
            if (point < 3000000)
            {
                return "T-NULL";
            }else if(point < 15000000)
            {
                return "T-New";
            }else if( point < 50000000)
            {
                return "T-Mem";
            }
            else
            {
                return "VIP";
            }
        }

    }
}
