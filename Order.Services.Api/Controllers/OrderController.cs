using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Services.Api.Data;
using Order.Services.Api.Models;
using Order.Services.Api.Models.Dto;
using Order.Services.Api.Services.IService;

namespace Order.Services.Api.Controllers
{
    [Route("api/Order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        public readonly ResponseDTO _responseDTO = new ResponseDTO();
        public readonly IMapper _mapper;
        public readonly AppDBContext _db;
        public readonly IProductSerivce _productSerivce;
        public OrderController(
            IMapper mapper,
            AppDBContext db,
            IProductSerivce productSerivce
            )
        {
            _mapper = mapper;
            _db = db;
            _productSerivce = productSerivce;
        }

        [HttpGet("GetOrdersById/{orderId}")]
        public async Task<ResponseDTO?> GetOrdersById(int? orderId)
        {
            try
            {
                OrderHeader orderHeader = _db.orderHeaders
                    .AsSplitQuery()
                    .Include(U => U.orderDetails).First(v => v.OrderHeaderId == orderId);

                _responseDTO.Data = _mapper.Map<OrderHeaderDto>(orderHeader);
                _responseDTO.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = ex.Message;
            }

            return _responseDTO;
        }

        [HttpGet("GetOrdersByUserId/{UserId}")]
        public async Task<ResponseDTO?> GetOrdersByUserId(string? UserId)
        {
            try
            {
                IEnumerable<OrderHeader> orderHeader;
                if (User.IsInRole("Admin"))
                {
                    orderHeader = _db.orderHeaders
                        .AsSplitQuery()
                        .Include(U => U.orderDetails).OrderByDescending(v => v.OrderHeaderId);

                    _responseDTO.Data = _mapper.Map<OrderHeaderDto>(orderHeader);
                    _responseDTO.IsSuccess = true;
                }
                else
                {
                    orderHeader = _db.orderHeaders
                        .AsSplitQuery()
                        .Include(U => U.orderDetails).OrderByDescending(v => v.UserId == UserId);

                    _responseDTO.Data = _mapper.Map<IEnumerable<OrderHeaderDto>>(orderHeader);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = ex.Message;
            }

            return _responseDTO;
        }


        [HttpPost("CreateOrder")]
        public async Task<ResponseDTO?> CreateOrder([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(shoppingCartDto.ShoppingCartHeader);
                orderHeaderDto.OrderDateTime = DateTime.Now;
                orderHeaderDto.Status = Utility.Utility.Status_Pending;
                orderHeaderDto.orderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(shoppingCartDto.shoppingCartDetail);

                OrderHeader orderHeader = _db.orderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;

                await _db.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderHeader.OrderHeaderId;
                _responseDTO.Data = orderHeaderDto;
                _responseDTO.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }


        [HttpPost("UpdateOrderStatus/{OrderId}")]
        public async Task<ResponseDTO?> UpdateOrderStatus(int OrderId, [FromBody] string Status)
        {
            try
            {
                OrderHeader orderHeader = _db.orderHeaders
                    .AsSplitQuery()
                    .Include(u => u.orderDetails).First(y => y.OrderHeaderId == OrderId);
                orderHeader.Status = Status;

                _db.orderHeaders.Update(orderHeader);

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return _responseDTO;
        }

    }
}
