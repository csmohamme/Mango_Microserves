using Mango.Web.Models;

namespace Mango.Web.ViewModel
{
    public class ModelVM
    {
    }
    public class CouponsViewModel
    {
        public List<CouponDto> Coupons { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
