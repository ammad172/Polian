using Order.Services.Api.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Services.Api.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeader? ShoppingCartHeader { get; set; }

        public int ProductId { get; set; }

        [NotMapped]
        public ProductsDto? product { get; set; }
        public int Count { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

    }
}
