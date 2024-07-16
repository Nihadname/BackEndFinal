using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderContentService _sliderContentService;

        public HomeController(ISliderService sliderService, ISliderContentService sliderContentService)
        {
            _sliderService = sliderService;
            _sliderContentService = sliderContentService;
        }
        public async Task<IActionResult> Index()
        {
           HomeViewModel model = new HomeViewModel();
            model.sliders =await _sliderService.GetAllSlidersAsync(0, 0, s=>s.SliderContent);
            return View(model);
        }
    }
}
