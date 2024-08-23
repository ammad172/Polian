using AutoMapper;
using Coupon.Services.Api.Data;
using Coupon.Services.Api.Model;
using Coupon.Services.Api.Model.CouponDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coupon.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        ResponseDTO _response = new ResponseDTO();

        public CouponController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/<CouponController>
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                var list = _db.Coupons.ToList();
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(list);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

            }

            return _response;
        }

        // GET api/<CouponController>/5
        [HttpGet("{id}")]
        public ResponseDTO Get(int id)
        {
            var obj = _db.Coupons.FirstOrDefault(data => data.CouponId == id);
            _response.Data = _mapper.Map<CouponDTO>(obj);
            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDTO Get(string code)
        {
            var obj = _db.Coupons.FirstOrDefault(data => data.CouponCode == code);
            _response.Data = _mapper.Map<CouponDTO>(obj);
            return _response;
        }

        // POST api/<CouponController>
        [HttpPost]
        [Authorize(Roles = "ADMIN,SUBADMIN")]
        public async Task<ResponseDTO> Post([FromBody] CouponModel value)
        {
            try
            {
                CouponModel data = _mapper.Map<CouponModel>(value);
                await _db.Coupons.AddAsync(data);
                await _db.SaveChangesAsync();

                var list = _db.Coupons.ToList();
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(list);
                _response.IsSuccess = true;
                _response.Message = "Record Added succesfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        // PUT api/<CouponController>/5
        [HttpPut("{id}")]
        public async Task<ResponseDTO> Put(int id, [FromBody] CouponModel value)
        {

            try
            {
                CouponModel data = _mapper.Map<CouponModel>(value);
                await _db.Coupons.Where(c => c.CouponId == id).
                     ExecuteUpdateAsync(setters => setters
                         .SetProperty(b => b.CouponCode, data.CouponCode)
                         .SetProperty(b => b.DiscoutAmount, data.DiscoutAmount)
                         .SetProperty(b => b.MinAmount, data.MinAmount)
                         .SetProperty(b => b.LastUpdated, DateTime.Now)
                          );


                var list = _db.Coupons.ToList();
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(list);
                _response.IsSuccess = true;
                _response.Message = "Record Updated succesfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                await _db.Coupons.Where(c => c.CouponId == id).ExecuteDeleteAsync();
                await _db.SaveChangesAsync();

                var list = _db.Coupons.ToList();
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(list);
                _response.IsSuccess = true;
                _response.Message = "Record Deleted succesfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }

            return _response;
        }
    }
}
