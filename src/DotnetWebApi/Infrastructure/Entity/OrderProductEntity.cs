namespace Infrastructure.Entity;

public class OrderProductEntity
{
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public virtual ProductEntity Product { get; init; } = null!;
}
