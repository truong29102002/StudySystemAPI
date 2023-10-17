using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    [Table("administrative_units")]
    public class AdministrativeUnits
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("full_name", TypeName = "varchar(255)")]
        public string FullName { get; set; } = string.Empty;
        [Column("full_name_en", TypeName = "varchar(255)")]
        public string FullNameEn { get; set; } = string.Empty;
        [Column("short_name", TypeName = "varchar(255)")]
        public string ShortName { get; set; } = string.Empty;
        [Column("short_name_en", TypeName = "varchar(255)")]
        public string ShortNameEn { get; set; } = string.Empty;
        [Column("code_name", TypeName = "varchar(255)")]
        public string CodeName { get; set; } = string.Empty;
        [Column("code_name_en", TypeName = "varchar(255)")]
        public string CodeNameEn { get; set; } = string.Empty;
        public ICollection<Provinces> Provinces { get; set; } = new List<Provinces>();
        public ICollection<Districts> Districts { get; set; } = new List<Districts>();
        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
