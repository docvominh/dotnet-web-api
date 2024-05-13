using Infrastructure;
using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Web.Exceptions;

namespace Application.Product;

public interface IProductService
{
    public Task<List<ProductView>> GetProducts();

    public Task<ProductView> GetProduct(int id);

    public Task DeleteProduct(int id);

    public Task<ProductView> SaveProduct(ProductModel model);
}

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;
    private readonly IProductMapper _mapper;

    public ProductService(AppDbContext dbContext, IProductMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ProductView>> GetProducts()
    {
        List<ProductEntity> entities = await _dbContext.Products
            .Include(p => p.Details)
            .ToListAsync();

        return _mapper.ToViews(entities);
    }

    public async Task<ProductView> GetProduct(int id)
    {
        ProductEntity entity = await _dbContext.Products
                .Include(p => p.Details)
                .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new ProductNotFoundException($"Couldn't find product with id {id}");

        return _mapper.ToView(entity);
    }

    public async Task DeleteProduct(int id)
    {
        ProductEntity? entity = await _dbContext.Products.FindAsync(id);

        if (entity != null)
        {
            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ProductView> SaveProduct(ProductModel model)
    {
        ProductEntity entity = _mapper.ToEntity(model);

        if (model.Details != null)
        {
            entity.Details = new ProductDetailEntity
            {
                Height = model.Details.Height,
                Width = model.Details.Width,
                Depth = model.Details.Depth,
                Weight = model.Details.Weight
            };
        }

        entity.CreatedDate = DateTimeOffset.Now;

        entity = _dbContext.Products.Add(entity).Entity;
        await _dbContext.SaveChangesAsync();

        return _mapper.ToView(entity);
    }
}
