using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class UserDetail
    {
        [Key, MaxLength(12)]
        public string UserID { get; set; } = null!;
        [Required]
        public string UserFullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        // 0 male, 1 female
        [Range(0,1)]
        public int Gender { get; set; }
        // 0 user, 1 admin
        [Range(0,1)]
        public int Role { get; set; }
        public bool IsActive { get; set; }

        public AddressUser? AddressUser { get; set; }

    }
}
