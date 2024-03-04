using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ReadEase_C_.Interface
{
    public interface IUserService
    {
        Task<string> CheckIfUserIsAdminAsync(int userId);
        void DeleteUser(int id);
        IEnumerable<FormModel> GetAllUsers();
        Task<IActionResult> UpdateUserAsync(string newUser, int id);
    }
}