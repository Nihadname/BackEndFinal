using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var allSliders = await sliderService.GetAllSlidersAsync(0, 0,s=>s.SliderContent);
            return View(allSliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if(!ModelState.IsValid) return View(request);
            var newProfileImage = request.photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed.");
                    return View(request);
                }
              

                
                // Save the new image file

            }
            Slider slider = new Slider();
            slider.ImageUrl = await newProfileImage.SaveFile("slider");
            slider.SliderContent = new SliderContent()
            {
                Title = request.SliderContent.Title,
                Description = request.SliderContent.Desc,
             
            };
            await sliderService.AddSliderAsync(slider);
            return RedirectToAction("Index");
        }
    }
}
