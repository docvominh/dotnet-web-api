using Infrastructure.Entity;
using Riok.Mapperly.Abstractions;

namespace Application.Product;

public interface IProductDetailMapper
{
    public ProductDetails ToView(ProductDetailEntity entity);

    public ProductDetailEntity ToEntity(ProductDetails model);
}

[Mapper]
public partial class ProductDetailMapper : IProductDetailMapper
{
    public partial ProductDetails ToView(ProductDetailEntity entity);

    public partial ProductDetailEntity ToEntity(ProductDetails model);
}
