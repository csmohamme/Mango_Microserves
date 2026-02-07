using Mango.Web.Models;

namespace Mango.Web.ViewModel
{
    public class AuthVM
    {
        public LoginRequestDto Login { get; set; } = new();
        public RegistrationRequestDto Register { get; set; } = new();

    }
}
