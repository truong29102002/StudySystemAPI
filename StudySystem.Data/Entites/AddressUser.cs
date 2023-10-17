using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class AddressUser : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Descriptions { get; set; } = string.Empty;

        [Column("ward_code", TypeName = "varchar(20)")]
        public string WardCode { get; set; } = string.Empty;
        [Column("districts_code", TypeName = "varchar(20)")]
        public string DistrictCode { get; set; } = string.Empty;
        [Column("province_code", TypeName = "varchar(20)")]
        public string ProvinceCode { get; set; } = string.Empty;
        [MaxLength(12)]
        public string UserID { get; set; } = null!;
        public UserDetail? UserDetail { get; set; }
        public Ward? Ward { get; set; }
        public Districts? District { get; set; }
        public Provinces? Province { get; set; }
    }
}
