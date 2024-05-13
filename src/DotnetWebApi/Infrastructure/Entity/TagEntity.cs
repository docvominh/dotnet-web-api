namespace Infrastructure.Entity;

public class TagEntity
{
    public int Id { get; set; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public virtual List<ProductEntity> Products { get; init; } = [];
}
