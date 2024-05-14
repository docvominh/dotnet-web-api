using Infrastructure.Entity;
using Riok.Mapperly.Abstractions;

namespace Application.Customer;

public interface ICustomerMapper
{
    public CustomerView ToView(CustomerEntity entity);

    public List<CustomerView> ToViews(List<CustomerEntity> entities);

    public CustomerEntity ToEntity(CustomerModel model);
}

[Mapper]
public partial class CustomerMapper : ICustomerMapper
{
    public partial CustomerView ToView(CustomerEntity entity);

    public partial List<CustomerView> ToViews(List<CustomerEntity> entities);

    public partial CustomerEntity ToEntity(CustomerModel model);
}
