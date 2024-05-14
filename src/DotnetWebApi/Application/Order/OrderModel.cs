using System.ComponentModel.DataAnnotations;
using Application.Customer;

namespace Application.Order;

public class OrderModel
{
    [Required]
    public required CustomerModel Customer { get; set; }

    [Required]
    public required List<OrderItemModel> OrderItems { get; init; }
}

public class OrderItemModel
{
    public int Quantity { get; init; }

    public int ProductId { get; init; }
}
