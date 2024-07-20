using BackEndFinal.Data;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AboutController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            AboutViewModel aboutViewModel = new AboutViewModel();
            aboutViewModel.About= await _appDbContext.abouts.AsNoTracking().FirstOrDefaultAsync();
            var settings =await _appDbContext.settings.ToDictionaryAsync(Key => Key.Key, val => val.Value);
            aboutViewModel.settings = settings;

            return View(aboutViewModel);
        }
    }
}
