using BackEndFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(s => s.UserName.ToLower().Contains(searchText.ToLower()) ||
                                         s.FullName.ToLower().Contains(searchText.ToLower()));
            }

            var usersForActualForm = await users.ToListAsync();
            return View(usersForActualForm);
        }
    }
}
