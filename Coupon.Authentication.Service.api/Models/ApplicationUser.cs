using Microsoft.AspNetCore.Identity;

namespace Coupon.Authentication.Service.api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
