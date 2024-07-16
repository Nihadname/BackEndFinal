using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Repositories.interfaces;
using static NuGet.Packaging.PackagingConstants;

namespace BackEndFinal.ViewCompenents
{
    public class SettingFooterViewComponent : ViewComponent  
    {
        private readonly IRepository<Footer> _footerRepository;
        private readonly IRepository<Setting> _settingRepository;

        public SettingFooterViewComponent(IRepository<Footer> footerRepository, IRepository<Setting> settingRepository)
        {
            _footerRepository = footerRepository;
            _settingRepository = settingRepository;
        }
     
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel footerViewModel = new FooterViewModel();
            var settings = await _settingRepository.GetAllAsync();
            footerViewModel.Setting = settings.ToDictionary(setting => setting.Key, setting => setting.Value);
            footerViewModel.footers = await _footerRepository.GetAllAsync(0,0,include => include.footerContents);
           
            return View(await Task.FromResult(footerViewModel));
        }
    }
}
