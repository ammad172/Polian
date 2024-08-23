using Authentication.Service.api.RabbitMqSender;
using Coupon.Authentication.Service.api.Models.Model;
using Coupon.Authentication.Service.api.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Authentication.Service.api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuth _auth;
        private readonly IRabbitMQAuthsender _rabbitMQAuthsender;

        private readonly ResponseDto _responseDto = new ResponseDto();

        public AuthController(IAuth auth, IRabbitMQAuthsender rabbitMQAuthsender)
        {
            _auth = auth;
            _responseDto = new ResponseDto();
            _rabbitMQAuthsender = rabbitMQAuthsender;
        }


        [HttpPost("Register")]
        public async Task<ResponseDto> Register([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                var res = await _auth.RegisterUser(registrationDto);

                if (string.IsNullOrEmpty(res))
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Data = "";

                    _rabbitMQAuthsender.SendMessage(registrationDto.Email, "PolainQue");
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = res;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }


            return _responseDto;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {

            var result = await _auth.Login(loginRequestDto);

            if (result.User == null && result.Token == string.Empty)
            {
                return NotFound("User Not Found");
            }

            _responseDto.IsSuccess = true;
            _responseDto.Data = result;

            return Ok(_responseDto);
        }

        [Authorize]
        [HttpPost("AssingRole")]
        public async Task<IActionResult> AssingRole([FromBody] RegistrationDto registrationDto)
        {

            var result = await _auth.AssingRole(registrationDto.Email, registrationDto.RoleName.ToUpper());

            if (result)
            {
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);

            }

            return BadRequest("Error");
        }
    }
}
