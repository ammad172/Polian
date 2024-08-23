using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Services.Api.Models
{
    public class ShoppingCartDetail
    {
        [Key]
        public int CardDetailId { get; set; }

        public int ShoppingCartId { get; set; }

        [ForeignKey("ShoppingCartId")]
        public ShoppingCartHeader ShoppingCartHeader { get; set; }

        public int ProductId { get; set; }

        [NotMapped]
        public ProductsDto product { get; set; }
        public int Count { get; set; }
    }
}
