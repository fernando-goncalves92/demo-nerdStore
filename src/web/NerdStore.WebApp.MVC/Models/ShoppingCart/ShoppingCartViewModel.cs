using NerdStore.WebApp.MVC.Models.Voucher;
using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public decimal TotalPurchase { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();
    }
}
