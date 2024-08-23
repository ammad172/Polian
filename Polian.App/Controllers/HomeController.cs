using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Polian.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService? _productsService;
        private readonly INotyfService? _notyfService;
        private readonly ICartService? _cartService;
        public HomeController(IProductsService productsService, INotyfService notyfService, ICartService cartService)
        {
            _productsService = productsService;
            _notyfService = notyfService;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication", new { area = "" });
            }

            if (TempData["message"] != null)
            {

                _notyfService.Success(TempData["message"].ToString());
                TempData.Remove("message");

            }

            List<ProductsDto?> lst = new List<ProductsDto>();
            try
            {
                ResponseDTO? response = await _productsService.GetProducts();

                if (response != null && response.IsSuccess)
                {
                    lst = JsonConvert.DeserializeObject<List<ProductsDto?>>(response.Data.ToString());
                }
                else
                {
                    _notyfService.Error(response.Message);
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return View(lst);
        }

        public async Task<IActionResult> CountRecord()
        {
            List<ProductsDto?> lst = new List<ProductsDto>();
            try
            {
                ResponseDTO? response = await _productsService.GetProducts();

                if (response != null && response.IsSuccess)
                {
                    lst = JsonConvert.DeserializeObject<List<ProductsDto?>>(response.Data.ToString());
                }
                else
                {
                    _notyfService.Error(response.Message);
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return View(lst);
        }

        [HttpPost]
        [Authorize]
        [ActionName("ProductDetails")]
        public async Task<ActionResult> ProductDetails(ProductsDto productsDto)
        {
            try
            {

                ShoppingCartDto shoppingCartDto = new()
                {
                    ShoppingCartHeader = new()
                    {
                        UserId = User.Claims.Where(data => data.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value,
                    }
                };

                ShoppingCartDetailDto shoppingCartDetailDto = new()
                {
                    Count = productsDto.Count ?? 0,
                    ProductId = productsDto.ProductId
                };

                List<ShoppingCartDetailDto> shoppingCartDetails = new() { shoppingCartDetailDto };

                shoppingCartDto.shoppingCartDetail = shoppingCartDetails;

                var data = await _cartService.CartUpsert(shoppingCartDto);
                if (data.IsSuccess)
                {
                    TempData["message"] = "Item has been added to the shopping cart";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyfService.Error(data.Message);
                    return View(productsDto);

                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {

            try
            {

                var data = await _productsService.GetProductById(id);
                if (data.IsSuccess)
                {

                    var obj = JsonConvert.DeserializeObject<ProductsDto>(data.Data.ToString());
                    return View(obj);
                }
                else
                {
                    _notyfService.Error(data.Message);
                    return View();

                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}