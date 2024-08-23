using System.ComponentModel.DataAnnotations;

namespace Order.Services.Api.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }

        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string? CouponCode { get; set; }

        public double? Discount { get; set; }

        public double? OrderTotal { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime OrderDateTime { get; set; }

        public string? Status { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? StripeSessionId { get; set; }

        public IEnumerable<OrderDetails>? orderDetails { get; set; }
    }
}
