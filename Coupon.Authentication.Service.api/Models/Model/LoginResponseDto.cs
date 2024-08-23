namespace Coupon.Authentication.Service.api.Models.Model
{
    public class LoginResponseDto
    {
        public UserDto User{ get; set; }

        public string Token{ get; set; }
    }
}
