using System;
using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Models.Order
{
    public partial class OrderViewModel
    {
        public int Code { get; set; }

        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5
        public int Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal Discount { get; set; }
        public bool VoucherUsed { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
        public AddressViewModel Address { get; set; }
    }
}
