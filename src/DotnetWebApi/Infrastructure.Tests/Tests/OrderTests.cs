using FluentAssertions;
using Infrastructure.Entity;

namespace Infrastructure.Tests.Tests;

public class OrderTests
{
    private readonly AppDbContext _dbContext = DbContextFactory.CreateDbContext(nameof(OrderTests));

    [Fact]
    public async Task SaveOrder_Should_Success()
    {
        // Arrange
        List<ProductEntity> createdProducts = await CreateProductsAsync();

        CustomerEntity customer =  new()
        {
            FirstName = "Minh",
            LastName = "Pham",
            Fullname = "Minh Pham",
            Phone = "+84 777 888 999",
            Email = "minh@123.com",
            Address = "123 Noname street",
            Location = "Da Nang",
            State = "Da Nang",
            PostCode = 550000
        };
        
        OrderEntity order = new ()
        {
            Customer = customer,
            OrderProducts =
            [
                new OrderProductEntity
                {
                    Quantity = 5,
                    ProductId = createdProducts[0].Id
                },
                new OrderProductEntity
                {
                    Quantity = 1,
                    ProductId = createdProducts[1].Id
                }
            ],
            Total = 5 * 800 + 900,
            State = OrderState.Create
        };

        // Act
        order = (await _dbContext.Orders.AddAsync(order)).Entity;
        await _dbContext.SaveChangesAsync();

        // Assert
        order.Id.Should().NotBe(null);
        order.Customer.Should().BeEquivalentTo(customer);
        order.Total.Should().Be(5 * 800 + 900);

        List<ProductEntity> products = order.OrderProducts
            .Select(x => x.Product)
            .OrderBy(p => p.Id)
            .ToList();
        
        products.Should().ContainInOrder(createdProducts);
    }


    private async Task<List<ProductEntity>> CreateProductsAsync()
    {
        TagEntity phoneTag = _dbContext.Tags.FirstOrDefault(x => x.Name.Equals("Phone"))
            ?? throw new InvalidOperationException();


        ProductEntity product1 = new ()
        {
            Url = "iphone-14",
            Name = "IPhone 14",
            Price = 800,
            Tags = [phoneTag]
        };

        ProductEntity product2 = new ()
        {
            Url = "iphone-15",
            Name = "IPhone 14",
            Price = 900,
            Tags = [phoneTag]
        };

        List<ProductEntity> products =
        [
            (await _dbContext.Products.AddAsync(product1)).Entity,
            (await _dbContext.Products.AddAsync(product2)).Entity
        ];

        await _dbContext.SaveChangesAsync();

        return products;
    }
}
