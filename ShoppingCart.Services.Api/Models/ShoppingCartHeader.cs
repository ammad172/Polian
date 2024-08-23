using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Services.Api.Models
{
    public class ShoppingCartHeader
    {
        [Key]
        public int ShoppingCartId { get; set; }

        [Required]
        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string? CouponCode { get; set; }

        [NotMapped]
        public double? Discount { get; set; }

        [NotMapped]
        public double? CartTotal { get; set; }
    }
}
