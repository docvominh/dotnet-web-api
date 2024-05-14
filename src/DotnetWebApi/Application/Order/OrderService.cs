using Infrastructure;
using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Web.Exceptions;

namespace Application.Order;

public interface IOrderService
{
    public Task<List<OrderView>> GetOrdersAsync();

    public Task<OrderView> GetOrderAsync(int id);

    public Task DeleteOrderAsync(int id);

    public Task<OrderView> SaveOrderAsync(OrderModel model);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly IOrderMapper _mapper;

    public OrderService(AppDbContext dbContext, IOrderMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<OrderView>> GetOrdersAsync()
    {
        List<OrderEntity> entities = await _dbContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
            .ToListAsync();

        return _mapper.ToViews(entities);
    }

    public async Task<OrderView> GetOrderAsync(int id)
    {
        OrderEntity entity = await _dbContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderProducts)
                .ThenInclude(orderProduct => orderProduct.Product)
                .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new OrderNotFoundException($"Couldn't find product with id {id}");

        return _mapper.ToView(entity);
    }

    public async Task DeleteOrderAsync(int id)
    {
        OrderEntity? entity = await _dbContext.Orders.FindAsync(id);

        if (entity != null)
        {
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<OrderView> SaveOrderAsync(OrderModel model)
    {
        OrderEntity entity = _mapper.ToEntity(model);
        entity.State = OrderState.Create;
        entity.OrderProducts = [];

        foreach (OrderItemModel item in model.OrderItems)
        {
            entity.OrderProducts.Add(
                new OrderProductEntity
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }
            );
        }

        List<int> ids = model.OrderItems.Select(x => x.ProductId).ToList();
        List<ProductEntity> products = _dbContext.Products.Where(x => ids.Contains(x.Id)).ToList();

        decimal total = 0;

        foreach (OrderProductEntity orderProduct in entity.OrderProducts)
        {
            total += products.FirstOrDefault(x => x.Id == orderProduct.ProductId)!.Price * orderProduct.Quantity;
        }

        entity.Total = total;

        entity = (await _dbContext.Orders.AddAsync(entity)).Entity;
        await _dbContext.SaveChangesAsync();

        return await GetOrderAsync(entity.Id);
    }
}
