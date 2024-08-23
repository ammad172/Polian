using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDTO?> CreateOrder(ShoppingCartDto? shoppingCartDto);

        Task<ResponseDTO?> GetOrdersById(int? orderId);

        Task<ResponseDTO?> GetOrdersByUserId(string? UserId);

        Task<ResponseDTO?> UpdateOrderStatus(int? OrderId, string? Status);


    }
}
