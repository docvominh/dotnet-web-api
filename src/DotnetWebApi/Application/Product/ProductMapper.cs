using Infrastructure.Entity;
using Riok.Mapperly.Abstractions;

namespace Application.Product;

public interface IProductMapper
{
    public ProductView ToView(ProductEntity entity);

    public List<ProductView> ToViews(List<ProductEntity> entities);

    public ProductEntity ToEntity(ProductModel model);
}

[Mapper]
public partial class ProductMapper : IProductMapper
{
    
    [UseMapper]
    private readonly IProductDetailMapper _detailMapper;

    public ProductMapper(IProductDetailMapper detailMapper)
    {
        _detailMapper = detailMapper;
    }

    public partial ProductView ToView(ProductEntity entity);

    public partial List<ProductView> ToViews(List<ProductEntity> entities);

    public partial ProductEntity ToEntity(ProductModel model);
}
