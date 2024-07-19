using BackEndFinal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.ViewCompenents
{
    public class TestimonialAreaViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public TestimonialAreaViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonialAreas = await _appDbContext.testimonialAreas.AsNoTracking().ToListAsync();
            return View(await Task.FromResult(testimonialAreas));
        }
    }
}
