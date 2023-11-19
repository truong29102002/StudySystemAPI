using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class News : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? TitleHeader { get; set; }
        public string? ImageNew { get; set; }
        public string? ContentHeader { get; set; }
        public string? TitleBody { get; set; }
        public string? ContentBody { get; set; }
        public string? TitleFooter { get; set; }
        public string? ContentFooter { get; set; }

    }
}
