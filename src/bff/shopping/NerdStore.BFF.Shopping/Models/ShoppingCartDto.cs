using System.Collections.Generic;

namespace NerdStore.BFF.Shopping.Models
{
    public class ShoppingCartDto
    {
        public decimal TotalPurchase { get; set; }
        public decimal Discount { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
