using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Models;
using WebApplication1.Models;

namespace ReadEase_C_.Interface
{
    public interface IUserService
    {
        Task<string> CheckIfUserIsAdminAsync(int userId);
        void DeleteUser(int id);
        public IEnumerable<protectedUserModel> GetAllUsers();
        Task<IActionResult> UpdateUserAsync(string newUser,string newPass, int id);
        public string getUsername(int id);

    }
}