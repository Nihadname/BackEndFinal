using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async IActionResult Index()
        {
           HomeViewModel model = new HomeViewModel();
            model.sliders =await _sliderService.GetAllSlidersAsync(0, 0);
            var SliderContent = await _sliderService.GetAllSlidersAsync(0, 0);
          
        }
    }
}
