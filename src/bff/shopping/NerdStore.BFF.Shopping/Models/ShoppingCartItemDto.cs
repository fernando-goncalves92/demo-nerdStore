using System;

namespace NerdStore.BFF.Shopping.Models
{
    public class ShoppingCartItemDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
