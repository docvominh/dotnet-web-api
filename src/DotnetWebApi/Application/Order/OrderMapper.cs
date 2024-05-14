using Application.Customer;
using Infrastructure.Entity;
using Riok.Mapperly.Abstractions;

namespace Application.Order;

public interface IOrderMapper
{
    public OrderView ToView(OrderEntity entity);

    public List<OrderView> ToViews(List<OrderEntity> entities);

    public OrderEntity ToEntity(OrderModel model);
}

[Mapper]
public partial class OrderMapper : IOrderMapper
{
    [UseMapper]
    private readonly IOrderItemMapper _itemMapper;

    [UseMapper]
    private readonly ICustomerMapper _customerMapper;

    public OrderMapper(IOrderItemMapper itemMapper, ICustomerMapper customerMapper)
    {
        _itemMapper = itemMapper;
        _customerMapper = customerMapper;
    }

    [MapProperty(nameof(OrderEntity.OrderProducts), nameof(OrderView.OrderItems))]
    public partial OrderView ToView(OrderEntity entity);

    public partial List<OrderView> ToViews(List<OrderEntity> entities);

    public partial OrderEntity ToEntity(OrderModel model);
}
