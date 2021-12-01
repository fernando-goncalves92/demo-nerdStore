using NerdStore.ShoppingCart.API.Entities.Enums;

namespace NerdStore.ShoppingCart.API.Entities
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DisccountType DiscountType { get; set; }
    }
}
