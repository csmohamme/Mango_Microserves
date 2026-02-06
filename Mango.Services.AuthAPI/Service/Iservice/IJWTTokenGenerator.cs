using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Service.Iservice
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
