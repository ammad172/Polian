using Newtonsoft.Json;
using ShoppingCart.Services.Api.Models;
using ShoppingCart.Services.Api.Services.IService;

namespace ShoppingCart.Services.Api.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory? _httpClient;
        public CouponService(IHttpClientFactory? httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CouponDTO> GetCoupon(string token, string CoupnCOde)
        {
            var client = _httpClient.CreateClient("Coupon");


            client.DefaultRequestHeaders.Add("Accept", "application/json");
            //token
            client.DefaultRequestHeaders.Add("Authorization", $"{token}");


            var response = await client.GetAsync($"/api/Coupon/GetByCode/{CoupnCOde}");
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(content);

            if (resp.IsSuccess)
            {
                return JsonConvert.
                     DeserializeObject<CouponDTO>(resp.Data.ToString());
            }

            return new CouponDTO();
        }
    }
}
