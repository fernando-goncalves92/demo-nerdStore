using System;

namespace NerdStore.WebApp.MVC.Models.Order
{
    public partial class OrderViewModel
    {
        public class OrderItemViewModel
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public int Amount { get; set; }
            public decimal Price { get; set; }
            public string Image { get; set; }
        }
    }
}
