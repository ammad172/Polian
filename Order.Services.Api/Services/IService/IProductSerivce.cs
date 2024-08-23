using Order.Services.Api.Models;
using Order.Services.Api.Models.Dto;

namespace Order.Services.Api.Services.IService
{
    public interface IProductSerivce
    {
        public Task<IEnumerable<ProductsDto>> GetProducts(string token);
    }
}
