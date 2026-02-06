using Mango.Web.Models;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;

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
