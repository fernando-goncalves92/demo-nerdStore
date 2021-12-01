using System.Collections.Generic;

namespace NerdStore.BFF.Shopping.Models
{
    public class ShoppingCartDto
    {
        public decimal TotalPurchase { get; set; }
        public decimal Discount { get; set; }
        public VoucherDto Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
