using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ShoppingCart.Api.Data;
using ShoppingCart.Services.Api.Models;
using ShoppingCart.Services.Api.Models.Dto;
using ShoppingCart.Services.Api.Services.IService;
using Microsoft.AspNetCore.Authorization;


namespace ShoppingCart.Services.Api.Controllers
{
    [Route("api/ShoppingCart")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDBContext? _db;
        private readonly ResponseDTO? _responseDTO = new ResponseDTO();
        private readonly IMapper? _mapper1;
        private readonly IProductSerivce? _productSerivce;
        private readonly ICouponService? _couponserivce;

        public ShoppingCartController(AppDBContext appDBContext, IMapper mapper1,
            IProductSerivce? productSerivce, ICouponService? couponserivce)
        {
            _db = appDBContext;
            _mapper1 = mapper1;
            _productSerivce = productSerivce;
            _couponserivce = couponserivce;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<ResponseDTO> RemoveCoupon([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var shoppingCartHeaderDto = await _db.shoppingCartHeaders.
                    FirstAsync(d => d.UserId == shoppingCartDto.ShoppingCartHeader.UserId);

                shoppingCartHeaderDto.CouponCode = "";

                _db.shoppingCartHeaders.Update(shoppingCartHeaderDto);
                await _db.SaveChangesAsync();
                _responseDTO.IsSuccess = true;


            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }

            return _responseDTO;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDTO> ApplyCoupon([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var shoppingCartHeaderDto = await _db.shoppingCartHeaders.
                    FirstAsync(d => d.UserId == shoppingCartDto.ShoppingCartHeader.UserId);

                shoppingCartHeaderDto.CouponCode = shoppingCartDto.ShoppingCartHeader.CouponCode;

                _db.shoppingCartHeaders.Update(shoppingCartHeaderDto);
                await _db.SaveChangesAsync();

                _responseDTO.IsSuccess = true;


            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }

            return _responseDTO;
        }


        [HttpGet("GetCartData/{userId}")]
        public async Task<ResponseDTO> GetCartData(string userId)
        {
            string accessToken = HttpContext.Request.Headers["Authorization"];

            try
            {
                ShoppingCartDto shoppingCartDto = new()
                {

                    ShoppingCartHeader = _mapper1.Map<ShoppingCartHeaderDto>(
                        _db.shoppingCartHeaders.First(da => da.UserId == userId.ToString())),
                };

                shoppingCartDto.shoppingCartDetail = _mapper1.Map<IEnumerable<ShoppingCartDetailDto>>(
                        _db.shoppingCartDetails.Where(ds => ds.ShoppingCartId == shoppingCartDto.ShoppingCartHeader.ShoppingCartId));

                IEnumerable<ProductsDto> prdList = await _productSerivce.GetProducts(accessToken);
                var value = 0.0;
                foreach (var item in shoppingCartDto.shoppingCartDetail)
                {
                    item.product = prdList.FirstOrDefault(d => d.ProductId == item.ProductId);
                    value += Convert.ToDouble((Convert.ToDecimal(item.Count) * item.product.Price));
                }
                shoppingCartDto.ShoppingCartHeader.CartTotal = value;

                if (!string.IsNullOrEmpty(shoppingCartDto.ShoppingCartHeader.CouponCode))
                {
                    var code = await _couponserivce.GetCoupon(accessToken, shoppingCartDto.ShoppingCartHeader.CouponCode);


                    if (code != null && shoppingCartDto.ShoppingCartHeader.CartTotal > code.DiscoutAmount)
                    {
                        shoppingCartDto.ShoppingCartHeader.CartTotal -= code.DiscoutAmount;

                        shoppingCartDto.ShoppingCartHeader.Discount = code.DiscoutAmount;
                    }

                }


                _responseDTO.IsSuccess = true;
                _responseDTO.Data = shoppingCartDto;

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }

            return _responseDTO;
        }

        [HttpPost]
        public async Task<ResponseDTO> CartUpsert([FromBody] ShoppingCartDto? shoppingCartDto)
        {
            try
            {
                var cart = await _db.shoppingCartHeaders.FirstOrDefaultAsync(data => data.UserId == shoppingCartDto.ShoppingCartHeader.UserId);
                if (cart == null)
                {

                    ShoppingCartHeader? shoppingCartHeader = _mapper1.Map<ShoppingCartHeader>(shoppingCartDto.ShoppingCartHeader);

                    await _db.shoppingCartHeaders.AddAsync(shoppingCartHeader);
                    await _db.SaveChangesAsync();
                    shoppingCartDto.shoppingCartDetail.FirstOrDefault().ShoppingCartId = shoppingCartHeader.ShoppingCartId;

                    await _db.shoppingCartDetails.AddAsync(_mapper1.Map<ShoppingCartDetail>(shoppingCartDto.shoppingCartDetail.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var detailcart = await _db.shoppingCartDetails.AsNoTracking().
                        FirstOrDefaultAsync(data =>
                        data.ProductId == shoppingCartDto.shoppingCartDetail.FirstOrDefault().ProductId
                        && data.ShoppingCartId == cart.ShoppingCartId
                        );

                    if (detailcart == null)
                    {
                        shoppingCartDto.shoppingCartDetail.FirstOrDefault().ShoppingCartId = cart.ShoppingCartId;

                        await _db.shoppingCartDetails.AddAsync(_mapper1.Map<ShoppingCartDetail>(shoppingCartDto.shoppingCartDetail.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        shoppingCartDto.shoppingCartDetail.First().Count += detailcart.Count;
                        shoppingCartDto.shoppingCartDetail.First().ShoppingCartId += detailcart.ShoppingCartId;
                        shoppingCartDto.shoppingCartDetail.First().CardDetailId += detailcart.CardDetailId;

                        _db.shoppingCartDetails.Update(_mapper1.Map<ShoppingCartDetail>(shoppingCartDto.shoppingCartDetail.First()));
                        await _db.SaveChangesAsync();

                    }

                }

                _responseDTO.Data = shoppingCartDto;
            }
            catch (Exception ex)
            {

                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }

            return _responseDTO;

        }

        [HttpDelete("RemoveCart/{carDetailId}")]
        public async Task<ResponseDTO?> RemoveCart(int carDetailId)
        {
            try
            {
                var cart = await _db.shoppingCartDetails.FirstOrDefaultAsync(data => data.CardDetailId == carDetailId);

                int cardDetailcnt = await _db.shoppingCartDetails.
                    Where(data => data.ShoppingCartId == cart.ShoppingCartId).CountAsync();
                _db.shoppingCartDetails.Remove(cart);

                if (cardDetailcnt == 0)
                {

                    var cardheader = await _db.shoppingCartHeaders.
                   FirstOrDefaultAsync(data => data.ShoppingCartId == cart.ShoppingCartId);

                    _db.shoppingCartHeaders.Remove(cardheader);

                }

                await _db.SaveChangesAsync();

                _responseDTO.IsSuccess = true;

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }

            return _responseDTO;
        }

    }
}
