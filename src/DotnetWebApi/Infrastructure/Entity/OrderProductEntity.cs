using System.Text.Json.Serialization;

namespace Infrastructure.Entity;

public class OrderProductEntity
{
    public int OrderId { get; init; }

    public int ProductId { get; init; }

    public int Quantity { get; init; }

    [JsonIgnore]
    public virtual OrderEntity Order { get; init; }
    [JsonIgnore]
    public virtual ProductEntity Product { get; init; }
}
