namespace Mango.Web.Utility
{
    public class SD
    {
        // Base URL for Coupon API
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
