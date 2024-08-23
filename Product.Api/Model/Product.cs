using System.ComponentModel.DataAnnotations;

namespace Product.Api.Model
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? ImageLocalPath { get; set; }
    }
}
