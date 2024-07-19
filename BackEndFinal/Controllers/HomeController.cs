using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderContentService _sliderContentService;
        private readonly IOfferedAdvantageService _overlayService;
        private readonly IEventService _eventService;
        private readonly IBlogService _blogService;
        private readonly AppDbContext _appDbContext;
        public HomeController(ISliderService sliderService, ISliderContentService sliderContentService, IOfferedAdvantageService overlayService, IEventService eventService, AppDbContext appDbContext, IBlogService blogService)
        {
            _sliderService = sliderService;
            _sliderContentService = sliderContentService;
            _overlayService = overlayService;
            _eventService = eventService;
            _appDbContext = appDbContext;
            _blogService = blogService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
           HomeViewModel model = new HomeViewModel();
            model.sliders =await _sliderService.GetAllSlidersAsync(0, 0, s=>s.SliderContent);
            model.offeredAdvantages = await _overlayService.GetAllOfferedAdvantagesAsync(0, 3);
            ICollection<Event> events = await _eventService.GetAllEventAsync(0, 8, s => s.Speakers, skip => skip.Category);
            model.events = events.OrderByDescending(e => e.HeldTime).ToList();
           model.WhyChoose= await _appDbContext.whyChooses.AsNoTracking().FirstOrDefaultAsync();
            //model.testimonialAreas=await _appDbContext.testimonialAreas.AsNoTracking().ToListAsync();
            var query = _blogService.GetAllBlogQuery();
            var blogsQuery = query.Include(s => s.Images).AsNoTracking();
            var paginatedBlogs = PaginationVM<Blog>.Create(blogsQuery, page, 3);
            model.PaginatedBlogs = paginatedBlogs;

            return View(model);
        }
    }
}
