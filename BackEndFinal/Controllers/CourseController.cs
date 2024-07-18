using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses =await courseService.GetAlCourseAsync(0, 0, s => s.courseImages, s => s.Category);
            ViewBag.CourseCount = courses.Count();
            return View(courses);
        }
        public async Task<IActionResult> Loadmore(int skip = 3)
        {
            var datas = await courseService.GetAlCourseAsync(skip, 3, s => s.courseImages, s => s.Category);
            return PartialView("_CoursePartialView", datas);


        }
    }
}
