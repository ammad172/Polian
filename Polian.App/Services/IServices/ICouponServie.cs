using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetCoupncodeASync();
        Task<ResponseDTO?> GetCoupncodeASyncBycode(string coupncode);
        Task<ResponseDTO?> GetCoupncodeASyncById(int id);
        Task<ResponseDTO?> CreateCoupncodeASync(CouponDTO dt);
        Task<ResponseDTO?> UpdateCoupncodeASync(int id, CouponDTO dt);

        Task<ResponseDTO?> DeleteCoupncodeASync(int id);

    }
}
