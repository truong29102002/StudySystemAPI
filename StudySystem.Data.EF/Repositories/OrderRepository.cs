using Microsoft.EntityFrameworkCore;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatedOrder(Order order)
        {
            await _context.AddAsync(order).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<OrdersAllResponseModel> GetOrders()
        {
            var rs = (from o in _context.Orders.AsNoTracking()
                      join u in _context.UserDetails.AsNoTracking() on o.UserId equals u.UserID
                      join a in _context.AddressBooks.AsNoTracking() on o.OrderId equals a.OrderId into addressGroup
                      from address in addressGroup.DefaultIfEmpty()
                      join orderItem in _context.OrderItems on o.OrderId equals orderItem.OrderId into orderItemGroup
                      from oi in orderItemGroup.DefaultIfEmpty()
                      join product in _context.Products.AsNoTracking() on oi.ProductId equals product.ProductId into productGroup
                      from p in productGroup.DefaultIfEmpty()
                      select new
                      {
                          Order = o,
                          User = u,
                          Address = address,
                          OrderItem = oi,
                          Product = p
                      })
                      .GroupBy(o => o.Order.OrderId)
                      .Select(group => new OrdersResponseDataModel
                      {
                          OrderId = group.Key,
                          CustomerName = group.First().User.UserFullName,
                          CustomerPhone = group.First().User.PhoneNumber,
                          CustomerEmail = group.First().User.Email,
                          ReciveType = group.First().Order.ReceiveType,
                          AddressReceive = group.First().Address != null ? $"{group.First().Address.AddressReceive} {group.First().Address.District} {group.First().Address.Province}" : "",
                          Note = group.First().Order.Note,
                          StatusOrder = group.First().Order.Status,
                          MethodPayment = group.First().Order.Payment,
                          TotalAmount = group.First().Order.TotalAmount,
                          ProductOrderListDataModels = group
                              .Select(oi => new ProductOrderListDataModel
                              {
                                  ProductName = oi.Product.ProductName,
                                  Quantity = oi.OrderItem.Quantity,
                                  Price = oi.OrderItem.Price
                              })
                              .ToList()
                      })
                      .ToList();


            OrdersAllResponseModel orders = new OrdersAllResponseModel();
            orders.Orders = rs;
            return orders;
        }
    }
}
