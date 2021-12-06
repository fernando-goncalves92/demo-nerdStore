using Dapper;
using NerdStore.Order.API.Application.DTO;
using NerdStore.Order.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Queries.Order
{
    public class OrderQuery : IOrderQuery
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQuery(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = await _orderRepository.GetOrdersByClientId(customerId);

            return orders.Select(OrderDto.MapToOrderDto);
        }

        public Task<OrderDto> GetAuthorizedOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> GetLastOrderByCustomerIdAsync(Guid customerId)
        {
            const string sql = @"SELECT
                                     O.Id AS 'OrderId'
                                    ,O.Code
                                    ,O.VoucherUsed
                                    ,O.Discount
                                    ,O.TotalPurchase
                                    ,O.OrderStatus
                                    ,O.Street
                                    ,O.Number
                                    ,O.District
                                    ,O.ZipCode
                                    ,O.Complement
                                    ,O.City
                                    ,O.State
                                    ,OI.Id AS 'OrderItemId'
                                    ,OI.ProductName
                                    ,OI.Amount
                                    ,OI.ProductImage
                                    ,OI.UnitPrice 
                                FROM 
                                    [Order] O 
                                INNER JOIN 
                                    OrderItem OI ON O.Id = OI.OrderId 
                                WHERE 
                                    O.CustomerId = @customerId 
                                AND O.RegistrationDate between DATEADD(minute, -3,  GETDATE()) and GETDATE()
                                AND O.OrderStatus = 1 
                                ORDER BY 
                                    O.RegistrationDate DESC";

            return MapOrderToOrderDto(await _orderRepository.GetConnection().QueryAsync<dynamic>(sql, new { customerId }));
        }

        private OrderDto MapOrderToOrderDto(dynamic order)
        {
            var orderDto = new OrderDto
            {
                Code = order[0].Code,
                Status = order[0].OrderStatus,
                TotalPurchase = order[0].TotalPurchase,
                Discount = order[0].Discount,
                VoucherUsed = order[0].VoucherUsed,
                OrderItems = new List<OrderItemDto>(),
                Address = new AddressDto
                {
                    Street = order[0].Street,
                    District = order[0].District,
                    ZipCode = order[0].ZipCode,
                    City = order[0].City,
                    Complement = order[0].Complement,
                    State = order[0].State,
                    Number = order[0].Number
                }
            };

            foreach (var item in order)
            {
                var orderItem = new OrderItemDto
                {
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Amount = item.Amount,
                    ProductImage = item.ProductImage
                };

                orderDto.OrderItems.Add(orderItem);
            }

            return orderDto;
        }
    }
}
