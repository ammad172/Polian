namespace Polian.App.Models
{
    public class OrderDetailsDto
    {

        public int OrderDetailId { get; set; }

        public int OrderHeaderId { get; set; }
        public OrderHeaderDto? ShoppingCartHeader { get; set; }

        public int ProductId { get; set; }

        public ProductsDto? product { get; set; }
        public int Count { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

    }
}
