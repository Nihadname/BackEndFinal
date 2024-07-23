using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class SettingHeaderViewComponent:ViewComponent
    {
        private readonly ISettingService settingService;
        private readonly UserManager<AppUser> _userManager;

        public SettingHeaderViewComponent(ISettingService settingService, UserManager<AppUser> userManager)
        {
            this.settingService = settingService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.FullName = currentUser.FullName;
            }
            var settings = await settingService.GetSettingsAsDictionaryAsync();
            return View(await Task.FromResult(settings));
        }

    }
}
