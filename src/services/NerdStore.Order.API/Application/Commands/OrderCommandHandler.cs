using FluentValidation.Results;
using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Order.API.Application.DTO;
using NerdStore.Order.API.Application.Events;
using NerdStore.Order.Domain.Order;
using NerdStore.Order.Domain.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Commands
{
    public class OrderCommandHandler : CommandHandler, IRequestHandler<AddOrderCommand, ValidationResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherRepository _voucherRepository;

        public OrderCommandHandler(IOrderRepository orderRepository, IVoucherRepository voucherRepository)
        {
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<ValidationResult> Handle(AddOrderCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid()) 
                return command.ValidationResult;

            var order = MapCommandToOrderEntity(command);

            if (!await ApplyVoucher(command, order)) 
                return ValidationResult;

            if (!ValidateOrder(order)) 
                return ValidationResult;

            if (!await ProcessPayment(order, command)) 
                return ValidationResult;

            order.AuthorizeOrder();

            order.AddEvent(new OrderPlacedEvent(order.Id, order.CustomerId));

            _orderRepository.Add(order);

            return await Commit(_orderRepository.UnitOfWork);
        }

        private Domain.Order.Order MapCommandToOrderEntity(AddOrderCommand command)
        {
            var order = new Domain.Order.Order(
                command.CustomerId, 
                command.TotalPurchase, 
                command.OrderItems.Select(OrderItemDto.MapToOrderItemEntity).ToList(),
                command.VoucherUsed, 
                command.Discount);

            order.AssignAddress(new Address
            {
                Street = command.Address.Street,
                Number = command.Address.Number,
                Complement = command.Address.Complement,
                District = command.Address.District,
                ZipCode = command.Address.ZipCode,
                City = command.Address.City,
                State = command.Address.State
            });

            return order;
        }

        private async Task<bool> ApplyVoucher(AddOrderCommand command, Domain.Order.Order order)
        {
            if (!command.VoucherUsed) 
                return true;

            var voucher = await _voucherRepository.GetVoucherByCode(command.VoucherCode);
            if (voucher == null)
            {
                AddError("O voucher informado não existe!");
                
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AddError(m.ErrorMessage));
                
                return false;
            }

            order.AssignVoucher(voucher);
            
            voucher.DebitAvailableAmount();

            _voucherRepository.Update(voucher);

            return true;
        }

        private bool ValidateOrder(Domain.Order.Order order)
        {
            var orderTotalPurchase = order.TotalPurchase;
            var orderDiscount = order.Discount;

            order.CalculateShoppingCartFinalPrice();

            if (order.TotalPurchase != orderTotalPurchase)
            {
                AddError("O valor total do pedido não está igual ao valor calculado pelo sistema");
                
                return false;
            }

            if (order.Discount != orderDiscount)
            {
                AddError("O valor total do desconto não está igual ao valor calculado pelo sistema");
                
                return false;
            }

            return true;
        }

        public async Task<bool> ProcessPayment(Domain.Order.Order order, AddOrderCommand command)
        {
            return await Task.FromResult(true);
        }
    }
}
