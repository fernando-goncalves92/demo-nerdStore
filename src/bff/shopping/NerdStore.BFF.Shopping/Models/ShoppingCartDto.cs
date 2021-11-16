using System.Collections.Generic;

namespace NerdStore.BFF.Shopping.Models
{
    public class ShoppingCartDto
    {
        public decimal TotalPurchase { get; set; }
        public decimal Discount { get; set; }
        public List<ShoppingCartItemDto> Items { get; private set; } = new List<ShoppingCartItemDto>();
        public object Itens { get; internal set; }
    }
}
