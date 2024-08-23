namespace Order.Services.Api.Models.Dto
{
    public class ShoppingCartHeaderDto
    {

        public int ShoppingCartId { get; set; }

        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string? CouponCode { get; set; }

        public double? Discount { get; set; }

        public double? CartTotal { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
