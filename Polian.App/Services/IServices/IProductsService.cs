using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface IProductsService
    {
        public Task<ResponseDTO?> GetProducts();
        public Task<ResponseDTO?> GetProductById(int id);
        public Task<ResponseDTO?> DeleteProduct(int id);
        public Task<ResponseDTO?> CreateProduct(ProductsDto productsDto);
        public Task<ResponseDTO?> UpdateProduct(ProductsDto productsDto);
    }
}
