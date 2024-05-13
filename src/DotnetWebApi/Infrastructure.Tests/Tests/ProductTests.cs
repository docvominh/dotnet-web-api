using FluentAssertions;
using Infrastructure.Entity;

namespace Infrastructure.Tests.Tests;

public class ProductTests
{
    private readonly AppDbContext _dbContext = DbContextFactory.CreateDbContext(nameof(ProductTests));

    [Fact]
    public void SaveProduct_Should_Success()
    {
        // Arrange
        TagEntity phoneTag = _dbContext.Tags.FirstOrDefault(x => x.Name.Equals("Phone"))
            ?? throw new InvalidOperationException();

        ProductDetailEntity details = new ()
        {
            Height = 20,
            Width = 20,
            Depth = 20,
            Weight = (decimal)0.5
        };

        ProductEntity product = new ()
        {
            Url = "iphone-14",
            Name = "IPhone 14",
            Price = 800,
            Tags = [phoneTag],
            Details = details
        };

        // Act
        product = _dbContext.Products.Add(product).Entity;
        _dbContext.SaveChanges();

        // Assert
        product.Id.Should().NotBe(null);
        product.Name.Should().Be("IPhone 14");

        details.ProductId = product.Id;
        product.Details.Should().NotBe(null);
        product.Details.Should().Be(details);

        product.Tags.Should().NotBeEmpty();
        product.Tags.Should().Contain(phoneTag);
    }

    [Fact]
    public void SaveProduct_WithNoDetail_Should_Success()
    {
        // Arrange
        TagEntity phoneTag = _dbContext.Tags.FirstOrDefault(x => x.Name.Equals("Phone"))
            ?? throw new InvalidOperationException();

        ProductEntity product = new ()
        {
            Url = "iphone-14",
            Name = "IPhone 14",
            Price = 800,
            Tags = [phoneTag],
        };

        // Act
        product = _dbContext.Products.Add(product).Entity;
        _dbContext.SaveChanges();

        // Assert
        product.Id.Should().NotBe(null);
        product.Details.Should().Be(null);
    }
}
