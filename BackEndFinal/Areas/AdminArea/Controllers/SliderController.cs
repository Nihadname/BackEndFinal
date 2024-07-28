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
            Slider slider = new Slider();

            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed.");
                    return View(request);
                }



                // Save the new image file
                slider.ImageUrl = await newProfileImage.SaveFile("slider");

            }
            slider.SliderContent = new SliderContent()
            {
                Title = request.SliderContent.Title,
                Description = request.SliderContent.Desc,
             
            };
            await sliderService.AddSliderAsync(slider);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedSlider=await sliderService.GetSliderByIdAsync(id);
            if(existedSlider == null) return NotFound();
            await sliderService.DeleteSliderAsync(existedSlider);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedSlider=await sliderService.GetSliderByIdAsync(id,s=>s.SliderContent);
            if (existedSlider == null) return NotFound();
            SliderUpdateVM sliderUpdateVM = new SliderUpdateVM()
            {
                SliderContent = new SliderContentVM()
                {
                    Desc = existedSlider.SliderContent.Description,
                    Title = existedSlider.SliderContent.Title,
                }

            };


            return View(sliderUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM sliderUpdateVM)
        {
            if (id == null) return BadRequest();

            var existedSlider = await sliderService.GetSliderByIdAsync(id, s => s.SliderContent);
            if (existedSlider == null) return NotFound();

            if (!ModelState.IsValid) return View(sliderUpdateVM);

            var newProfileImage = sliderUpdateVM.photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed.");
                    return View(sliderUpdateVM);
                }

                if (!string.IsNullOrEmpty(existedSlider.ImageUrl))
                {
                    existedSlider.ImageUrl.DeleteFile("slider");
                }

                existedSlider.ImageUrl = await newProfileImage.SaveFile("slider");
            }

            if (existedSlider.SliderContent == null)
            {
                existedSlider.SliderContent = new SliderContent();
            }

            existedSlider.SliderContent.Description = sliderUpdateVM.SliderContent.Desc;
            existedSlider.SliderContent.Title = sliderUpdateVM.SliderContent.Title;

            await sliderService.UpdateSliderAsync(existedSlider);

            return RedirectToAction("Index");
        }

    }
}
