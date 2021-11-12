using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public decimal TotalPurchase { get; set; }
        public List<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();
    }
}
