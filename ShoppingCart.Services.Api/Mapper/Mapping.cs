using AutoMapper;
using ShoppingCart.Services.Api.Models;


namespace ShoppingCart.Api.Mapper
{
    public class Mapping
    {
        public static MapperConfiguration register()
        {
            var mappingConfig = new MapperConfiguration(opt =>
            {
                opt.CreateMap<ShoppingCartDetail, ShoppingCartDetailDto>();
                opt.CreateMap<ShoppingCartDetailDto, ShoppingCartDetail>();

                opt.CreateMap<ShoppingCartHeader, ShoppingCartHeaderDto>();
                opt.CreateMap<ShoppingCartHeaderDto, ShoppingCartHeader>();
            });

            return mappingConfig;
        }
    }
}
