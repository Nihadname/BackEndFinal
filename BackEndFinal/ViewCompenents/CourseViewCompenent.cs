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
        public async Task<IViewComponentResult> InvokeAsync(string keyword = "", int skip = 0, int take = 3)
        {
            var courses = string.IsNullOrEmpty(keyword)
                ? await _courseService.GetAlCourseAsync(skip, take, s => s.Category, s => s.courseImages)
                : await _courseService.SearchCoursesAsync(keyword, skip, take, s => s.Category, s => s.courseImages);

            return View(courses);
        }
    }
}
