using BackEndFinal.Data;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class SettingHeaderViewComponent:ViewComponent
    {
        private readonly ISettingService settingService;

        public SettingHeaderViewComponent(ISettingService settingService)
        {
            this.settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await settingService.GetSettingsAsDictionaryAsync();
            return View(await Task.FromResult(settings));
        }

    }
}
