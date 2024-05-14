namespace Infrastructure.Entity;

public class OrderEntity
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public OrderState State { get; set; }

    public int CustomerId { get; init; }

    public virtual CustomerEntity Customer { get; init; } = null!;

    public virtual List<OrderProductEntity> OrderProducts { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }
}

public enum OrderState
{
    Create,
    Paid,
    Shipped,
    Done,
    Cancel
}
