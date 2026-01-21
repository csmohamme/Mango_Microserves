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

        // ==============================================  Get All Coupons ============================================== 
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            List<CouponDto> coupons = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if (response != null && response.IsSuccess)
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

        // ============================================== Create Coupon ============================================== 
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess)
                {
                    response.Message = "Coupon created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        // ============================================== Delete Coupon ============================================== 
        public async Task<IActionResult> Delete(int couponId)
        {
                ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
                if(response != null && response.IsSuccess)
                {
                    CouponDto? coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                    return View(coupon);
                }
            else
            {
                TempData["ErrorMessage"] = "Coupon NOt Found";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CouponDto coupon)
        {
            try{

            ResponseDto? response = await _couponService.DeleteCouponAsync(coupon.CouponId);
            if(response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Coupon Deleted Successfully";
            }
            }catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
