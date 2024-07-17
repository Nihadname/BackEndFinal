using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderContentService _sliderContentService;
        private readonly IOfferedAdvantageService _overlayService;
        private readonly IEventService _eventService;

        public HomeController(ISliderService sliderService, ISliderContentService sliderContentService, IOfferedAdvantageService overlayService, IEventService eventService)
        {
            _sliderService = sliderService;
            _sliderContentService = sliderContentService;
            _overlayService = overlayService;
            _eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
           HomeViewModel model = new HomeViewModel();
            model.sliders =await _sliderService.GetAllSlidersAsync(0, 0, s=>s.SliderContent);
            model.offeredAdvantages = await _overlayService.GetAllOfferedAdvantagesAsync(0,3);
            model.events = await _eventService.GetAllEventAsync(0, 8,s=>s.Speakers,skip=>skip.Category);
            //model.events=await 
            return View(model);
        }
    }
}
