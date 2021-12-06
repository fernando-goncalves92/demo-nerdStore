using System;
using System.Collections.Generic;

namespace NerdStore.Order.API.Application.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public Guid CustomerId { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPurchase { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Status { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public static OrderDto MapToOrderDto(Domain.Order.Order order)
        {
            var orderDto = new OrderDto
            {
                Id = order.Id,
                Code = order.Code,
                Status = (int)order.OrderStatus,
                RegistrationDate = order.RegistrationDate,
                TotalPurchase = order.TotalPurchase, 
                Discount = order.Discount,
                VoucherUsed = order.VoucherUsed, 
                OrderItems = new List<OrderItemDto>(),
                Address = new AddressDto()                
            };

            foreach (var item in order.OrderItems)
            {
                orderDto.OrderItems.Add(new OrderItemDto
                {
                    ProductName = item.ProductName,
                    ProductImage = item.ProductImage,
                    Amount = item.Amount,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    OrderId = item.OrderId
                });
            }

            orderDto.Address = new AddressDto
            {
                Street = order.Address.Street,
                Number = order.Address.Number,
                Complement = order.Address.Complement,
                District = order.Address.District,
                ZipCode = order.Address.ZipCode,
                City = order.Address.City,
                State = order.Address.State,
            };

            return orderDto;
        }
    }
}
