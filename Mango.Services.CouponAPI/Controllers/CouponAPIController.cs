using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
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
                _responseDto.Result = coupons.Adapt<IEnumerable<CouponDto>>();
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
                _responseDto.Result = coupon.Adapt<CouponDto>();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        // Get coupon by code
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToUpper() == code.ToUpper());
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                }

                _responseDto.Result = coupon.Adapt<CouponDto>();

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        // Create coupon
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Coupon coupon = dto.Adapt<Coupon>();
                    _dbContext.Add(coupon);
                    _dbContext.SaveChanges();

                    _responseDto.Result = coupon.Adapt<CouponDto>();
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        // Update coupon
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto dto)
        {
            try
            {
                Coupon coupon = dto.Adapt<Coupon>();

                _dbContext.Update(coupon);
                _dbContext.SaveChanges();
                _responseDto.Result = coupon.Adapt<CouponDto>();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public ResponseDto Delete(int Id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(c => c.CouponId == Id);
                _dbContext.Remove(coupon);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }
    }
}
