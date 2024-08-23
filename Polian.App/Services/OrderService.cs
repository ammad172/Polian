using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;

namespace Polian.App.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseServices _baseServices;
        public OrderService(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }

        public async Task<ResponseDTO> CreateOrder(ShoppingCartDto shoppingCartDto)
        {
            return await _baseServices.SendAsync(new RequestDTO
            {
                Url = Utils.OrderApiBase + "/api/Order/CreateOrder",
                Data = shoppingCartDto,
                ApiType = Utils.AppiType.Post
            }, true);
        }

        public async Task<ResponseDTO> GetOrdersById(int? orderId)
        {
            return await _baseServices.SendAsync(new RequestDTO
            {
                Url = Utils.OrderApiBase + $"/api/Order/GetOrdersById/{orderId}",
                ApiType = Utils.AppiType.Get
            }, true);
        }

        public async Task<ResponseDTO> GetOrdersByUserId(string UserId)
        {
            return await _baseServices.SendAsync(new RequestDTO
            {
                Url = Utils.OrderApiBase + $"/api/Order/GetOrdersByUserId/{UserId}",
                ApiType = Utils.AppiType.Get
            }, true);
        }

        public async Task<ResponseDTO> UpdateOrderStatus(int? OrderId, string Status)
        {
            return await _baseServices.SendAsync(new RequestDTO
            {
                Url = Utils.OrderApiBase + $"/api/Order/UpdateOrderStatus/{OrderId}",
                Data = Status,
                ApiType = Utils.AppiType.Post
            }, true);
        }
    }
}
