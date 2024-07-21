using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
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
        private readonly ISettingService _settingRepository;

        public SettingFooterViewComponent(IRepository<Footer> footerRepository, ISettingService settingRepository)
        {
            _footerRepository = footerRepository;
            _settingRepository = settingRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel footerViewModel = new FooterViewModel();
            //var settings = await _settingRepository.GetAllAsync();
            footerViewModel.Setting =await _settingRepository.GetSettingsAsDictionaryAsync();
            footerViewModel.footers = await _footerRepository.GetAllAsync(0,0,include => include.footerContents);
           
            return View(await Task.FromResult(footerViewModel));
        }
    }
}
