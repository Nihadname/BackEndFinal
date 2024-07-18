using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public CourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take = 3)
        {
            var courses = await _courseService.GetAlCourseAsync(0, take, s => s.Category, s=>s.courseImages);
            return View(await Task.FromResult(courses));
        }
    }
}
