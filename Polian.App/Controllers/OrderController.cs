using Microsoft.AspNetCore.Mvc;
using Polian.App.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using Polian.App.Models;
using Newtonsoft.Json;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Polian.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService? _orderService;
        private ResponseDTO? responseDTO = new ResponseDTO();
        private readonly INotyfService? _notyfService;
        public OrderController(IOrderService? orderService, INotyfService? notyfService)
        {
            _orderService = orderService;
            _notyfService = notyfService;
        }
        public async Task<IActionResult> OrderIndex()
        {

            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeaderDto> list = new List<OrderHeaderDto>();
            try
            {
                var userId = User.Claims.Where(s => s.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value;
                responseDTO = _orderService.GetOrdersByUserId(userId).GetAwaiter().GetResult();

                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(responseDTO.Data.ToString());

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return Json(new { data = list });
        }
    }
}
