using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class BaseEntity
    {
        [Required]
        public string CreateUser { get; set; } = null!;
        [Required]
        public string UpdateUser { get; set; } = null!;
        [Required]
        public DateTime CreateDateAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdateDateAt { get; set; } = DateTime.UtcNow;
    }
}
