using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class OrderItem : BaseEntity
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; } 
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
