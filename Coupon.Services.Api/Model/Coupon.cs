using System.ComponentModel.DataAnnotations;

namespace Coupon.Services.Api.Model
{
    public class CouponModel
    {
        [Key]
        public int CouponId { get; set; }

        [Required]
        public string CouponCode { get; set; }

        [Required]
        public double DiscoutAmount { get; set; }

        public double MinAmount { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
