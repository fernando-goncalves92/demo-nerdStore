using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Core.Data.Interfaces;
using System.Data.Common;

namespace NerdStore.Order.Domain.Order
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetOrdersByClientId(Guid customerId);
        void Add(Order order);
        void Update(Order order);
        
        DbConnection GetConnection();

        Task<OrderItem> GetOrdemItemById(Guid id);
        Task<OrderItem> GetOrdemItemByOrderAndProductId(Guid orderId, Guid productId);
    }
}