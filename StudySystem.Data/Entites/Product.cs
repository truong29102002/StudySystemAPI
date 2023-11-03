using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class Product : BaseEntity
    {
        [Key]
        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set;}
        public double? ProductPrice { get; set;}
        public string? ProductQuantity { get; set;}
        public string? ProductImage { get; set;}
        public string? ProductManufacturer { get;set;}
        public List<ProductCategory>? ProductCategories { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public List<WishList>? WishLists { get; set; }
    }
}
