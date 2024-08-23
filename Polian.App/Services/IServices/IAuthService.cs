using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDTO?> RegisterUser(RegistrationDto registrationDto);

        Task<ResponseDTO?> Login(LoginRequestDto loginRequestDto);

        Task<ResponseDTO?> AssingRole(RegistrationDto registrationDto );
    }
}
