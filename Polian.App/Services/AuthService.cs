using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;
using static Polian.App.Utility.Utils;

namespace Polian.App.Services
{
    public class AuthService : IAuthService
    {

        private readonly IBaseServices _baseServices;

        public AuthService(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }
        public async Task<ResponseDTO?> RegisterUser(RegistrationDto registrationDto)
        {

            return await _baseServices.SendAsync(
                new RequestDTO()
                {
                    ApiType = AppiType.Post,
                    Url = Utils.AuthApiBase + "/api/auth/Register",
                    Data = registrationDto
                }, false);
        }

        public async Task<ResponseDTO?> Login(LoginRequestDto loginRequestDto)
        {
            return await _baseServices.SendAsync(
                new RequestDTO()
                {
                    ApiType = AppiType.Post,
                    Url = Utils.AuthApiBase + "/api/auth/Login",
                    Data = loginRequestDto,

                }, false);
        }

        public async Task<ResponseDTO?> AssingRole(RegistrationDto registrationDto)
        {

            return await _baseServices.SendAsync(
               new RequestDTO()
               {
                   ApiType = AppiType.Post,
                   Url = Utils.AuthApiBase + "api/auth/AssingRole",
                   Data = registrationDto
               }, true);
        }
    }
}
