using Coupon.Authentication.Service.api.Models;
using Coupon.Authentication.Service.api.Models.Model;
using Coupon.Authentication.Service.api.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Coupon.Authentication.Service.api.Services
{
    public class Auth : IAuth
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTtokengenerator _jWTtokengenerator;
        public Auth(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJWTtokengenerator jWTtokengenerator)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _jWTtokengenerator = jWTtokengenerator;
        }
        public async Task<string> RegisterUser(RegistrationDto registrationDto)
        {
            ApplicationUser applicationUser = new()
            {

                UserName = registrationDto.Email,
                Email = registrationDto.Email,
                NormalizedEmail = registrationDto.Email.ToUpper(),
                Name = registrationDto.Name,
                PhoneNumber = registrationDto.PhoneNumber
            };
            try
            {
                var user = await _userManager.CreateAsync(applicationUser, registrationDto.Password);

                if (user.Succeeded)
                {

                    return "";
                }
                else
                {
                    return string.Join(", ", user.Errors.Select(error => error.Description));
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

            var user = _db.ApplicationUser.FirstOrDefault((data) => (data.Email.ToLower() ?? "") == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {

                return new LoginResponseDto()
                {
                    User = null,
                    Token = string.Empty
                };

            }
            var roles = await _userManager.GetRolesAsync(user);
            string token = _jWTtokengenerator.GenerateTokenJWT(user, roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;

        }


        public async Task<bool> AssingRole(string email, string role)
        {
            var user = await _db.ApplicationUser.FirstOrDefaultAsync((data) => data.Email.ToLower() == email.ToLower());

            if (user != null)
            {

                if (_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return true;
                }
                else
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));

                    await _userManager.AddToRoleAsync(user, role);
                    return true;
                }

            }
            return false;
        }
    }
}
