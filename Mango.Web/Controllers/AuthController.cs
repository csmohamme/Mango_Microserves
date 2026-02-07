using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using Mango.Web.ViewModel;
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
            var model = new AuthVM
            {
                Login = new LoginRequestDto(),
                Register = new RegistrationRequestDto()
            };

            var roleList = new List<SelectListItem>() {
                new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer },
                };
            ViewBag.RoleList = roleList;

            ViewBag.RoleAdmin = SD.RoleAdmin;
            ViewBag.RoleCustomer = SD.RoleCustomer;
            return View(model);
        }

        // ============================================== Login ============================================= 
        [HttpPost]
        public async Task<IActionResult> Login(AuthVM model)
        {
            return View();
        }

        // ============================================== Register ============================================= 

        [HttpPost]
        public async Task<IActionResult> Register(AuthVM model)
        {
            ModelState.Remove("Login.UserName");
            ModelState.Remove("Login.Password");
            if (ModelState.IsValid)
            {
                // Register the user
                ResponseDto result = await _authService.RegisterAsync(model.Register);
                ResponseDto assignRole;

                // if it is success
                if (result != null && result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(model.Register.RoleName))
                    {
                        model.Register.RoleName = SD.RoleCustomer;
                    }
                    // if the role is exist
                    assignRole = await _authService.AssignRoleAsync(model.Register);
                    if (assignRole != null && assignRole.IsSuccess)
                    {
                        TempData["success"] = "Registration Successful";
                        TempData["ActiveTab"] = "login";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Show API error for AssignRole
                        ViewBag.ErrorMessage = assignRole?.Message ?? "Failed to assign role";
                    }
                }
                else
                {
                    // Show API error for Register
                    ViewBag.ErrorMessage = result?.Message ?? "Registration failed";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please correct the validation errors and try again.";
            }
            var roleList = new List<SelectListItem>() {
                new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer },
                };
            ViewBag.RoleList = roleList;
            ViewBag.RoleAdmin = SD.RoleAdmin;
            ViewBag.RoleCustomer = SD.RoleCustomer;
            ViewBag.ActiveTab = "register";

            return View("Index", model);
        }

        // ============================================== Logout ============================================= 

        [HttpPost]
        public async Task<IActionResult> Logout(RegistrationRequestDto registerRequestDto)
        {
            return View();
        }
    }
}
