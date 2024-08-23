using AutoMapper;
using Product.Api.Model;
using Product.Api.Model.ProductDto;

namespace Product.Api.Mapper
{
    public class Mapping
    {
        public static MapperConfiguration register()
        {
            var mappingConfig = new MapperConfiguration(opt =>
            {
                opt.CreateMap<Products, ProductsDto>();
                opt.CreateMap<ProductsDto, Products>();
            });

            return mappingConfig;
        }
    }
}
