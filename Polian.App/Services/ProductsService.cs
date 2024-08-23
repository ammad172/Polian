using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;
using static Polian.App.Utility.Utils;

namespace Polian.App.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IBaseServices? _baseServices;
        private readonly ITokenProvider? _tokenpro;
        public ProductsService(IBaseServices baseServices, ITokenProvider tokenpro)
        {
            _baseServices = baseServices;
            _tokenpro = tokenpro;
        }
        public async Task<ResponseDTO?> GetProducts()
        {
            return await _baseServices.SendAsync(
                new RequestDTO()
                {
                    ApiType = AppiType.Get,
                    Url = Utils.ProductsBase + "/api/Product",
                    AccessToken = _tokenpro.GetToken()
                }, true);

        }
        public async Task<ResponseDTO?> GetProductById(int id)
        {

            return await _baseServices.SendAsync(
                   new RequestDTO()
                   {
                       ApiType = AppiType.Get,
                       Url = Utils.ProductsBase + "/api/Product/" + id,
                       AccessToken = _tokenpro.GetToken()
                   }, true);
        }
        public async Task<ResponseDTO?> DeleteProduct(int id)
        {

            return await _baseServices.SendAsync(
                     new RequestDTO()
                     {
                         ApiType = AppiType.Delete,
                         Url = Utils.ProductsBase + "/api/Product/" + id,
                         AccessToken = _tokenpro.GetToken()
                     }, true);
        }
        public async Task<ResponseDTO?> CreateProduct(ProductsDto productsDto)
        {

            return await _baseServices.SendAsync(
             new RequestDTO()
             {
                 ApiType = AppiType.Post,
                 Url = Utils.ProductsBase + "/api/Product",
                 AccessToken = _tokenpro.GetToken(),
                 Data = productsDto , 
                 ContentType = Contenttype.MultiPartFormData
             }, true);

        }
        public async Task<ResponseDTO?> UpdateProduct(ProductsDto productsDto)
        {

            return await _baseServices.SendAsync(
            new RequestDTO()
            {
                ApiType = AppiType.Put,
                Url = Utils.ProductsBase + "/api/Product",
                AccessToken = _tokenpro.GetToken(),
                Data = productsDto,
                ContentType = Contenttype.MultiPartFormData
            }, true);
        }
    }
}
