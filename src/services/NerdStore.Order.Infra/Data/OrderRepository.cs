using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Order.Domain.Order;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Order.Infra.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Order.Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Domain.Order.Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<Domain.Order.Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<OrderItem> GetOrdemItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetOrdemItemByOrderAndProductId(Guid orderId, Guid productId)
        {
            return await _context
                .OrderItems
                .FirstOrDefaultAsync(oi => oi.ProductId == productId && oi.OrderId == orderId);
        }

        public async Task<IEnumerable<Domain.Order.Order>> GetOrdersByClientId(Guid customerId)
        {
            return await _context
                .Orders
                .Include(p => p.OrderItems)
                .AsNoTracking()
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
