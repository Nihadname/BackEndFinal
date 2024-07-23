using BackEndFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Areas.AdminArea.ViewComponents
{
    public class AdminPanelHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminPanelHeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser= await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.ImageUrl = currentUser.imageUrl;
                ViewBag.FullName=currentUser.FullName;
            }
            return View();
        }
    }
}
