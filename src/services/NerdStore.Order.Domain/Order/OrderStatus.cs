namespace NerdStore.Order.Domain.Order
{
    public enum OrderStatus
    {
        Authorized = 1,
        Payed = 2,
        Refused = 3,
        Delivered = 4,
        Canceled = 5
    }
}
