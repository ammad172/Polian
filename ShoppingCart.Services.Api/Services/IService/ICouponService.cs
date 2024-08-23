using ShoppingCart.Services.Api.Models;

namespace ShoppingCart.Services.Api.Services.IService
{
    public interface ICouponService
    {    public Task<CouponDTO> GetCoupon(string token, string CoupnCOde);
    }
}
