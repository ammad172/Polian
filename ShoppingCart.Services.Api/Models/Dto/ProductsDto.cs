using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Services.Api.Models
{
    public class ProductsDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? Count { get; set; }
    }
}
