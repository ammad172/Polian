using AspNetCoreHero.ToastNotification.Abstractions;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;
using System.Collections.Generic;

namespace Polian.App.Controllers
{

    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly INotyfService _notyfService;
        public CouponController(ICouponService couponService, INotyfService notyfService)
        {
            _couponService = couponService;
            _notyfService = notyfService;
        }
        public async Task<IActionResult> Index()
        {
            List<CouponDTO>? lst = new();

            try
            {
                ResponseDTO? response = await _couponService.GetCoupncodeASync();

                if (response != null && response.IsSuccess)
                {
                    lst = JsonConvert.DeserializeObject<List<CouponDTO>>(response.Data.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View(lst);
        }


        public IActionResult CreateCoupon()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDTO DTO)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    ResponseDTO? response = await _couponService.CreateCoupncodeASync(DTO);
                    if (response.IsSuccess)
                    {
                        Response.Redirect("Index");
                    }
                    else
                    {
                        _notyfService.Warning(response.Message);
                        return View(DTO);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {

                return View(DTO);
            }
            return View();

        }


        public async Task<IActionResult> DeleteCoupon(int CouponId)
        {

            await _couponService.DeleteCoupncodeASync(CouponId);

            return RedirectToAction("Index");

        }

    }
}
