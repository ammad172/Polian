using Newtonsoft.Json;
using Order.Services.Api.Models.Dto;
using Order.Services.Api.Services.IService;

namespace Order.Services.Api.Services
{
    public class ProductService : IProductSerivce
    {
        private readonly IHttpClientFactory? _httpClient;
        public ProductService(IHttpClientFactory? httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductsDto>> GetProducts(string token)
        {
            var client = _httpClient.CreateClient("Product");


            client.DefaultRequestHeaders.Add("Accept", "application/json");
            //token
            client.DefaultRequestHeaders.Add("Authorization", $"{token}");


            var response = await client.GetAsync($"/api/Product");
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(content);

            if (resp.IsSuccess)
            {

                return JsonConvert.
                     DeserializeObject<IEnumerable<ProductsDto>>(resp.Data.ToString());
            }

            return new List<ProductsDto>();
        }
    }
}
