
using System.ComponentModel.DataAnnotations;

namespace Polian.App.Models
{
    public class ShoppingCartHeaderDto
    {

        public int ShoppingCartId { get; set; }

        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string? CouponCode { get; set; }

        public double? Discount { get; set; }

        public double? CartTotal { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
