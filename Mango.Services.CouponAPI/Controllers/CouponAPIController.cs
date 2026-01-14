using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ResponseDto _responseDto;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
        }

        // Get all coupons
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
                _responseDto.Result = coupons;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        // Get coupon by id
        [HttpGet]
        [Route("{Id:int}")]
        public ResponseDto Get(int Id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(c => c.CouponId == Id);
                _responseDto.Result= coupon;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
