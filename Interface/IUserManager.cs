using System.Security.Claims;

namespace ReadEase_C_.Interface
{
    public interface IUserManager
    {
        int GetUserId(ClaimsPrincipal user);
    }
}