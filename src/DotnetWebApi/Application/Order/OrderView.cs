using Application.Customer;
using Application.Product;

namespace Application.Order;

public class OrderView
{
    public CustomerView Customer { get; set; } = null!;

    public List<OrderItemView> OrderItems { get; set; } = null!;
}

public class OrderItemView
{
    public int Quantity { get; set; }

    public ProductView Product { get; set; } = null!;
}
