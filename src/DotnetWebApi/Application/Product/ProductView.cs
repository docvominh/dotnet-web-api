namespace Application.Product;

public class ProductView
{
    public int Id { get; set; }

    public required string Url { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public ProductDetails? Details { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
