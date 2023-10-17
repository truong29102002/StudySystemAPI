using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    [Table("administrative_regions")]
    public class AdministrativeRegions
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;
        [Column("name_en", TypeName = "varchar(255)")]
        public string NameEn { get; set; } = string.Empty;
        [Column("code_name", TypeName = "varchar(255)")]
        public string CodeName { get; set; } = string.Empty;
        [Column("code_name_en", TypeName = "varchar(255)")]
        public string CodeNameEn { get; set; } = string.Empty;
        public ICollection<Provinces> Provinces { get; set; } = new List<Provinces>();

    }
}
