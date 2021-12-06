using NerdStore.Order.API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Queries.Order
{
    public interface IOrderQuery
    {
        Task<OrderDto> GetLastOrderByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<OrderDto> GetAuthorizedOrdersAsync();
    }
}
