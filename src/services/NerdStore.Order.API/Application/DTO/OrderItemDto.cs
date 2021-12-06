using NerdStore.Order.Domain.Order;
using System;

namespace NerdStore.Order.API.Application.DTO
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }

        public static OrderItem MapToOrderItemEntity(OrderItemDto orderItem)
        {
            return new OrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.Amount, orderItem.UnitPrice, orderItem.ProductImage);
        }
    }
}
