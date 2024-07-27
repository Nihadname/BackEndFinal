using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var allSliders = await sliderService.GetAllSlidersAsync(0, 0);
            return View(allSliders);
        }
    }
}
