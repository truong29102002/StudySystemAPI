using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Entites
{
    public class Order : BaseEntity
    {
        [Key] 
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        // 0: Đã thanh toán, 1 chưa thanh toán, 2 đã hủy
        public string? Status { get; set; }
        public DateTime OrderDateAt { get; set; }
        public UserDetail? UserDetail { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
