using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;

namespace Polian.App.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseServices _baseServices;
        public CartService(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }

        public async Task<ResponseDTO?> ApplyCoupon(ShoppingCartDto? shoppingCartDto)
        {
            return await _baseServices.
                SendAsync(new RequestDTO
                {
                    ApiType = Utils.AppiType.Post,
                    Data = shoppingCartDto,
                    Url = Utils.CartApiBase + "/api/ShoppingCart/ApplyCoupon"
                }, true);
        }

        public async Task<ResponseDTO?> CartUpsert(ShoppingCartDto? shoppingCartDto)
        {
            return await _baseServices.
               SendAsync(new RequestDTO
               {
                   ApiType = Utils.AppiType.Post,
                   Data = shoppingCartDto,
                   Url = Utils.CartApiBase + "/api/ShoppingCart"
               }, true);
        }

        public async Task<ResponseDTO?> GetCartData(string? id)
        {
            return await _baseServices.
               SendAsync(new RequestDTO
               {
                   ApiType = Utils.AppiType.Get,
                   Data = null,
                   Url = Utils.CartApiBase + $"/api/ShoppingCart/GetCartData/{id}"
               }, true);
        }

        public async Task<ResponseDTO?> RemoveCart(int? carDetailId)
        {
            return await _baseServices.
               SendAsync(new RequestDTO
               {
                   ApiType = Utils.AppiType.Delete,
                   Data = null,
                   Url = Utils.CartApiBase + $"/api/ShoppingCart/RemoveCart/{carDetailId}"
               }, true);
        }



        public async Task<ResponseDTO?> RemoveCoupon(ShoppingCartDto? shoppingCartDto)
        {
            return await _baseServices.
               SendAsync(new RequestDTO
               {
                   ApiType = Utils.AppiType.Post,
                   Data = shoppingCartDto,
                   Url = Utils.CartApiBase + "/api/ShoppingCart/RemoveCoupon"
               }, true);
        }
    }
}
