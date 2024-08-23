namespace Coupon.Authentication.Service.api.Models.Model
{
    public class RegistrationDto
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? RoleName { get; set; }
    }
}
