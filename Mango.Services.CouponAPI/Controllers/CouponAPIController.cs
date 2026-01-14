using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all coupons
        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
                return Ok(coupons);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        // Get coupon by id
        [HttpGet]
        [Route("{Id:int}")]
        public object Get(int Id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(c => c.CouponId == Id);
                return coupon;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
