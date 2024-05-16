using System.Security.Claims;
using ReadEase_C_.Interface;

namespace ReadEase_C_.Helpers
{

    public class UserManager : IUserManager
    {

        public int GetUserId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("NameIdentifier claim not found."));
        }
    }
}
