namespace Infrastructure.Entity;

public class ProductDetailEntity
{
    public int ProductId { get; set; }

    public int Width { get; init; }

    public int Height { get; init; }

    public int Depth { get; init; }

    public decimal Weight { get; init; }
}
