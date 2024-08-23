namespace Coupon.Authentication.Service.api
{
    public class ResponseDto
    {
        public object? Data  { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string? Message { get; set; }
    }
}
