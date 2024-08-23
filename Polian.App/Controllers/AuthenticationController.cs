using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Polian.App.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService? _authService;
        private readonly ITokenProvider? _tokenProvider;
        private readonly INotyfService? _notyfService;
        public AuthenticationController(IAuthService authService, ITokenProvider? tokenProvider, INotyfService notyfService)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
            _notyfService = notyfService;
        }
        public IActionResult Registeration()
        {
            var drpdownlist = new List<SelectListItem>()
            {
                new SelectListItem{Text = Utils.RoleAdmin , Value = Utils.RoleAdmin},
                new SelectListItem{Text = Utils.RoleCustomer , Value = Utils.RoleCustomer},
            };

            ViewBag.roleList = drpdownlist;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registeration(RegistrationDto req)
        {
            if (ModelState.IsValid)
            {
                var data = await _authService.RegisterUser(req);

                if (data.IsSuccess)
                {
                    TempData["success"] = "User Registered Successfully";

                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    _notyfService.Error(data.Message);
                    TempData.Remove("success");
                }
            }
            return View();
        }


        public IActionResult Login()
        {
            if (TempData["success"] != null)
            {
                _notyfService.Success(TempData["success"].ToString());
                TempData.Remove("success");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var data = await _authService.Login(loginRequestDto);

                if (data.IsSuccess)
                {
                    var cnvrted = JsonConvert.DeserializeObject<LoginResponseDto>(data.Data.ToString());

                    _tokenProvider.SetToken(cnvrted.Token);

                    await GetSignin(cnvrted);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", data.Message);
                }
            }


            return View(loginRequestDto);
        }

        private async Task GetSignin(LoginResponseDto loginResponseDto)
        {
            try
            {
                var tokenhandler = new JwtSecurityTokenHandler();

                var val = tokenhandler.ReadJwtToken(loginResponseDto.Token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);


                List<Claim> list = new() {

                    new Claim(JwtRegisteredClaimNames.Email,
                    val.Claims.FirstOrDefault((c)=> c.Type == JwtRegisteredClaimNames.Email).Value),

                    new Claim(JwtRegisteredClaimNames.Sub,
                    val.Claims.FirstOrDefault((c)=> c.Type == JwtRegisteredClaimNames.Sub).Value),

                    new Claim(JwtRegisteredClaimNames.Name,
                    val.Claims.FirstOrDefault((c)=> c.Type == JwtRegisteredClaimNames.Name).Value),

                      new Claim(ClaimTypes.Name,
                    val.Claims.FirstOrDefault((c)=> c.Type == JwtRegisteredClaimNames.Email).Value),

                         new Claim(ClaimTypes.Role,
                    val.Claims.FirstOrDefault((c)=> c.Type == "role").Value)

                };

                identity.AddClaims(list);

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IActionResult AssignRole()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();

            return RedirectToAction(nameof(Login));
        }
    }
}
