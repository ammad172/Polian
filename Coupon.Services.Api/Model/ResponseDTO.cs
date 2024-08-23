namespace Coupon.Services.Api.Model
{
    public class ResponseDTO
    {
        public object? Data  { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string? Message { get; set; }
    }
}
