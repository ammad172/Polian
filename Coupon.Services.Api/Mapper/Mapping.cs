using AutoMapper;
using Coupon.Services.Api.Model;
using Coupon.Services.Api.Model.CouponDto;

namespace Coupon.Services.Api.Mapper
{
    public class Mapping
    {
        public static MapperConfiguration register()
        {
            var mappingConfig = new MapperConfiguration(opt =>
            {

                opt.CreateMap<CouponModel, CouponDTO>();
                opt.CreateMap<CouponDTO, CouponModel>();

            });

            return mappingConfig;
        }
    }
}
