using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            List<CouponDto> coupons = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if(response != null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            // Count total items for pagination
            var totalCount = coupons.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            page = Math.Max(1, Math.Min(page, totalPages));

            var paginatedCoupons = coupons.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Prepare the ViewModel
            var viewModel = new CouponsViewModel
            {
                Coupons = paginatedCoupons,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return View(viewModel);
        }
    }
}
