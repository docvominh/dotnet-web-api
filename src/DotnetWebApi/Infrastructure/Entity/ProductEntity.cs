namespace Infrastructure.Entity;

public class ProductEntity
{
    public int Id { get; set; }
    public required string Url { get; init; }
    public required string Name { get; init; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public virtual ProductDetailEntity? Details { get; set; }
    public virtual List<TagEntity> Tags { get; init; } = null!;
}
