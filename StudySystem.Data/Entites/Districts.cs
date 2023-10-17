using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    [Table("districts")]
    public class Districts
    {
        [Key]
        [Column("code", TypeName = "varchar(20)")]
        public string Code { get; set; } = null!;
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;
        [Column("name_en", TypeName = "varchar(255)")]
        public string NameEn { get; set; } = string.Empty;
        [Column("full_name", TypeName = "varchar(255)")]
        public string FullName { get; set; } = string.Empty;
        [Column("full_name_en", TypeName = "varchar(255)")]
        public string FullNameEn { get; set; } = string.Empty;
        [Column("code_name", TypeName = "varchar(255)")]
        public string CodeName { get; set; } = string.Empty;
        [Column("province_code", TypeName = "varchar(20)")]
        public string ProvinceCode { get; set; } = string.Empty;
        [Column("administrative_unit_id")]
        public int AdministrativeUnitId { get; set; }

        public Provinces Provinces { get; set; } = null!;
        public AdministrativeUnits AdministrativeUnits { get; set; } = null!;
        public ICollection<Ward> Wards { get; set; } = new List<Ward>();
        public ICollection<AddressUser> AddressUsers { get; set; } = new List<AddressUser>();
    }
}
