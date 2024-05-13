using Application.Product;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/products")]
public class ProductController
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<ProductView>> GetAllProducts()
    {
        return await _service.GetProducts();
    }

    [HttpGet("{id:int}")]
    public async Task<ProductView> Get(int id)
    {
        return await _service.GetProduct(id);
    }

    [HttpPost]
    public async Task<ProductView> AddProduct([FromBody] ProductModel product)
    {
        return await _service.SaveProduct(product);
    }

    [HttpDelete("{id:int}")]
    public async Task DeleteProduct(int id)
    {
        await _service.DeleteProduct(id);
    }
}
