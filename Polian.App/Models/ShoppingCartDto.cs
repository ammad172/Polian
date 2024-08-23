namespace Polian.App.Models
{
    public class ShoppingCartDto
    {
        public ShoppingCartHeaderDto? ShoppingCartHeader { get; set; }

        public IEnumerable<ShoppingCartDetailDto>? shoppingCartDetail { get; set; }
    }
}
