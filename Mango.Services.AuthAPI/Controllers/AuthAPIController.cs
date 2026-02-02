using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        // ================================= Register API =================================
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok("Register API is working!");
        }

        // ================================= Login API =================================
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Login API is working!");
        }
    }
}
