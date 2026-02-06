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

        // ============================================== Login ============================================= 
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // ============================================== Register ============================================= 

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
