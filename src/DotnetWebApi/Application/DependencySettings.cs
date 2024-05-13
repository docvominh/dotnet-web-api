using Application.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencySettings
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductDetailMapper, ProductDetailMapper>();
        services.AddSingleton<IProductMapper, ProductMapper>();
        services.AddScoped<IProductService, ProductService>();
    }
}
