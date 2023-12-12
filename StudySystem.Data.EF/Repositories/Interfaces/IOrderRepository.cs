using StudySystem.Data.Entites;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<bool> CreatedOrder(Order order);
        Task<OrdersAllResponseModel> GetOrders();
        Task<bool> UpdateStatusOrder(string orderId, string orderStatus);
        Task<bool> DeleteOrder(string orderId);
    }
}
