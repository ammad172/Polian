using Coupon.Authentication.Service.api.Models.Model;

namespace Coupon.Authentication.Service.api.Services.IService
{
    public interface IAuth
    {
        Task<string> RegisterUser(RegistrationDto registrationDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssingRole(string email, string role);
    }
}
