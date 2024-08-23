using AutoMapper;
using Order.Services.Api.Models;
using Order.Services.Api.Models.Dto;


namespace Order.Services.Api.Mapper
{
    public class Mapping
    {
        public static MapperConfiguration register()
        {
            var mappingConfig = new MapperConfiguration(opt =>
            {

                opt.CreateMap<OrderHeaderDto, ShoppingCartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();


                opt.CreateMap<ShoppingCartDetailDto, OrderDetailsDto>()
               .ForMember(dest => dest.Price, u => u.MapFrom(src => src.product.Price))
               .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.product.Name));


                opt.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
                opt.CreateMap<OrderHeaderDto, OrderHeader>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
