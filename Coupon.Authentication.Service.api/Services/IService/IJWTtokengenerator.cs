using Coupon.Authentication.Service.api.Models;

namespace Coupon.Authentication.Service.api.Services.IService
{
    public interface IJWTtokengenerator
    {
        public string GenerateTokenJWT(ApplicationUser applicationUser, IList<string> roles);
    }
}
