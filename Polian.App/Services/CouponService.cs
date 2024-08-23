using Polian.App.Models;
using Polian.App.Services.IServices;
using Polian.App.Utility;

namespace Polian.App.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseServices _baseServices;
        public CouponService(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }
        public async Task<ResponseDTO?> GetCoupncodeASync()
        {
            return await _baseServices.SendAsync(
                new RequestDTO()
                {
                    ApiType = Utils.AppiType.Get,
                    Url = Utility.Utils.CouponBase + "/api/Coupon"
                }, true);
        }
        public async Task<ResponseDTO?> GetCoupncodeASyncBycode(string coupncode)
        {
            return await _baseServices.SendAsync(
              new RequestDTO()
              {
                  ApiType = Utils.AppiType.Get,
                  Url = Utility.Utils.CouponBase + "/api/Coupon/apiGetByCode/" + coupncode
              }, true);

        }
        public async Task<ResponseDTO?> GetCoupncodeASyncById(int id)
        {
            return await _baseServices.SendAsync(
                 new RequestDTO()
                 {
                     ApiType = Utils.AppiType.Get,
                     Url = Utility.Utils.CouponBase + "/api/Coupon/" + id
                 }, true);
        }
        public async Task<ResponseDTO?> CreateCoupncodeASync(CouponDTO dt)
        {
            return await _baseServices.SendAsync(
                   new RequestDTO()
                   {
                       ApiType = Utils.AppiType.Post,
                       Url = Utility.Utils.CouponBase + "/api/Coupon",
                       Data = dt
                   }, true);
        }
        public async Task<ResponseDTO?> UpdateCoupncodeASync(int id, CouponDTO dt) {

            return await _baseServices.SendAsync(
                     new RequestDTO()
                     {
                         ApiType = Utils.AppiType.Put,
                         Url = Utility.Utils.CouponBase + "/api/Coupon/" + id,
                         Data = dt
                     }, true);
        }

        public async Task<ResponseDTO?> DeleteCoupncodeASync(int id) {

            return await _baseServices.SendAsync(
                    new RequestDTO()
                    {
                        ApiType = Utils.AppiType.Delete,
                        Url = Utility.Utils.CouponBase + "/api/Coupon/" + id,
                    }, true);
        }
    }
}
