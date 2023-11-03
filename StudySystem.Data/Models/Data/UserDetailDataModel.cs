using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Models.Data
{
    public class UserDetailDataModel
    {
        public string? UserFullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Gender { get; set; }
        public string? RankUser { get; set; }
        public string? JoinDateAt { get; set; }
        public decimal PriceBought { get; set; }
    }
}
