using Web.Data.Entity;

namespace Infrastructure.Entity;

public class OrderEntity
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public OrderState State { get; set; }

    public int CustomerId { get; init; }

    public virtual CustomerEntity Customer { get; init; }

    public virtual List<OrderProductEntity> OrderProducts { get; set; } = [];

    public required string ShippingAddress { get; init; }

    public required bool GstInvoice { get; init; }

    public string? StripePaymentIntentId { get; set; }

    public string? NabTransactionId { get; set; }

    public string? ParcelTrackingNumber { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public bool Deleted { get; set; }
}

public enum OrderState
{
    Create,
    Paid,
    Shiped,
    Done,
    Cancel
}
