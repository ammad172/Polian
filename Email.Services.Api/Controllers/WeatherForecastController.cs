using Email.Services.Api.Model;
using Email.Services.Api.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Email.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEmailService? _emailService;
        public WeatherForecastController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public void Get()
        {



        }
    }
}
