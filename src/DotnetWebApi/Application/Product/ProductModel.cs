namespace Application.Product;

public class ProductModel
{
    public int Id { get; set; }

    public required string Url { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public ProductDetails? Details { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}

public class ProductDetails
{
    public int Width { get; set; }

    public int Height { get; set; }

    public int Depth { get; set; }

    public decimal Weight { get; set; }
}
