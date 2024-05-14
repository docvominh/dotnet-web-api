using Application.Product;
using Infrastructure.Entity;
using Riok.Mapperly.Abstractions;

namespace Application.Order;

public interface IOrderItemMapper
{
    public OrderItemView ToView(OrderProductEntity entity);

    public List<OrderItemView> ToViews(List<OrderProductEntity> entities);
}

[Mapper]
public partial class OrderItemMapper : IOrderItemMapper
{
    [UseMapper]
    private readonly IProductMapper _productMapper;

    public OrderItemMapper(IProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    public partial OrderItemView ToView(OrderProductEntity entity);

    public partial List<OrderItemView> ToViews(List<OrderProductEntity> entities);
}
