﻿
namespace Order.Services.Api.Models.Dto
{
    public class ShoppingCartDetailDto
    {
        public int CardDetailId { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCartHeaderDto? ShoppingCartHeader { get; set; }

        public int ProductId { get; set; }

        public ProductsDto? product { get; set; }
        public int Count { get; set; }
    }
}
