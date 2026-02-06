using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ============================================== Index ============================================= 
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roleList = new List<SelectListItem>() {
                new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer },
                };
            ViewBag.RoleList = roleList;

            ViewBag.RoleAdmin = SD.RoleAdmin;
            ViewBag.RoleCustomer = SD.RoleCustomer;
            return View();
        }

        // ============================================== Login ============================================= 
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            return View();
        }

        // ============================================== Register ============================================= 

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registerRequestDto)
        {
            return View();
        }

        // ============================================== Logout ============================================= 

        [HttpPost]
        public async Task<IActionResult> Logout(RegistrationRequestDto registerRequestDto)
        {
            return View();
        }
    }
}
