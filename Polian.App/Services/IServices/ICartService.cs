using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface ICartService
    {
        Task<ResponseDTO?> CartUpsert(ShoppingCartDto? shoppingCartDto);
        Task<ResponseDTO?> RemoveCart(int? carDetailId);
        Task<ResponseDTO?> GetCartData(string? id);
        Task<ResponseDTO?> ApplyCoupon(ShoppingCartDto? shoppingCartDto);

        Task<ResponseDTO?> RemoveCoupon(ShoppingCartDto? shoppingCartDto);

    }
}
