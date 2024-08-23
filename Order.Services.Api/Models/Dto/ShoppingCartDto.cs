namespace Order.Services.Api.Models.Dto
{
    public class ShoppingCartDto
    {
        public ShoppingCartHeaderDto? ShoppingCartHeader { get; set; }

        public IEnumerable<ShoppingCartDetailDto>? shoppingCartDetail { get; set; }
    }
}
