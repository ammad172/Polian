using ShoppingCart.Services.Api.Models;

namespace ShoppingCart.Services.Api.Services.IService
{
    public interface IProductSerivce
    {
        public Task<IEnumerable<ProductsDto>> GetProducts(string token);
    }
}
