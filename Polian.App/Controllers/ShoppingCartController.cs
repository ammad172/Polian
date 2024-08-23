using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;
using System.IdentityModel.Tokens.Jwt;

namespace Polian.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartService? _cartService;
        private readonly INotyfService? _notyfService;
        private readonly IOrderService? _orderService;
        public ShoppingCartController(ICartService cartService, INotyfService notyfService, IOrderService orderService)
        {
            _cartService = cartService;
            _notyfService = notyfService;
            _orderService = orderService;
        }



        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            if (TempData["msg"] != null)
            {
                _notyfService.Success(TempData["msg"].ToString());
                TempData.Remove("msg");

            }

            return View(await LoadCartDtoByUser());
        }


        [Authorize]
        public async Task<IActionResult> Checkout()
        {


            return View(await LoadCartDtoByUser());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(ShoppingCartDto shoppingCartDto)
        {
            ShoppingCartDto shoppingCartDto1 = await LoadCartDtoByUser();
            if (ModelState.IsValid)
            {
                try
                {
                    shoppingCartDto1.ShoppingCartHeader.Name = shoppingCartDto.ShoppingCartHeader.Name;
                    shoppingCartDto1.ShoppingCartHeader.Email = shoppingCartDto.ShoppingCartHeader.Email;
                    shoppingCartDto1.ShoppingCartHeader.PhoneNumber = shoppingCartDto.ShoppingCartHeader.PhoneNumber;

                    var rec = await _orderService.CreateOrder(shoppingCartDto1);

                    if (rec.IsSuccess && rec != null)
                    {

                    }
                    else
                    {

                        _notyfService.Warning(rec.Message);
                    }
                }
                catch (Exception ex)
                {
                    _notyfService.Error(ex.Message);
                }

            }
            return View(shoppingCartDto1);
        }


        public async Task<ShoppingCartDto> LoadCartDtoByUser()
        {
            ShoppingCartDto shoppingCartDto = new ShoppingCartDto();
            try
            {
                var userId = User.Claims.Where(s => s.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value;
                ResponseDTO response = await _cartService.GetCartData(userId);

                if (response.IsSuccess)
                {
                    shoppingCartDto = JsonConvert.DeserializeObject<ShoppingCartDto>(response.Data.ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return shoppingCartDto;
        }


        [Authorize]
        public async Task<IActionResult> Remove(int CardDetailId)
        {
            try
            {
                var userId = User.Claims.Where(s => s.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value;
                ResponseDTO response = await _cartService.RemoveCart(CardDetailId);

                if (response.IsSuccess)
                {
                    TempData["msg"] = "Cart Updated Successfully!";
                    return RedirectToAction(nameof(CartIndex));
                }

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return RedirectToAction(nameof(CartIndex));
        }


        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var userId = User.Claims.Where(s => s.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value;
                ResponseDTO response = await _cartService.ApplyCoupon(shoppingCartDto);

                if (response.IsSuccess)
                {
                    TempData["msg"] = "Coupn Applied Successfully!";
                    return RedirectToAction(nameof(CartIndex));
                }
                else
                {
                    TempData["msg"] = response.Message;
                    return RedirectToAction(nameof(CartIndex));
                }

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return RedirectToAction(nameof(CartIndex));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var userId = User.Claims.Where(s => s.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value;
                ResponseDTO response = await _cartService.RemoveCoupon(shoppingCartDto);

                if (response.IsSuccess)
                {
                    TempData["msg"] = "Coupn Applied Successfully!";
                    return RedirectToAction(nameof(CartIndex));
                }
                else
                {
                    TempData["msg"] = response.Message;
                    return RedirectToAction(nameof(CartIndex));
                }

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return RedirectToAction(nameof(CartIndex));
        }
    }
}
