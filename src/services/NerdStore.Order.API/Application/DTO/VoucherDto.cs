namespace NerdStore.Order.API.Application.DTO
{
    public class VoucherDto
    {
        public string Code { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int DiscountType { get; set; }
    }
}
