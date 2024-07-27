using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext appDbContext;
        private IBlogService _blogService;
        public CourseController(ICourseService courseService, ICategoryService categoryService, IBlogService blogService, AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            this.courseService = courseService;
            _categoryService = categoryService;
            _blogService = blogService;
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string keyword = "")
        {
            var datas = await courseService.GetAlCourseAsync(0, 0, s => s.courseImages, s => s.Category);
            ViewBag.Count=datas.Count();
            ViewBag.Keyword = keyword;
            return View();
        }
        public async Task<IActionResult> Loadmore(int skip = 3)
        {
            var datas = await courseService.GetAlCourseAsync(skip, 3, s => s.courseImages, s => s.Category);
            return PartialView("_CoursePartialView", datas);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existedCourse = await courseService.GetAllCourseQuery()
                .Include(s => s.courseImages)
                .Include(s => s.courseTags).ThenInclude(s => s.Tag)
                .Include(s => s.Comments).ThenInclude(s => s.AppUser)
                .FirstOrDefaultAsync(s => s.Id == id);

            var blogs = await _blogService.GetAllBlogAsync(0, 3, s => s.Images);

            if (existedCourse is null) return NotFound();

            var courseDetailVM = new CourseDetailVM
            {
                Course = existedCourse,
                Blogs = blogs,
            };

            return View(courseDetailVM);
        }


        public async Task<IActionResult> CoursesInCategory(int? id, int page=1)
        {
            if (id is null) return BadRequest();
            var category =  _categoryService.GetAllCategoryQuery();
            var existedCategoryWithCourses = category.Include(s=>s.Courses).ThenInclude(s=>s.courseImages).FirstOrDefault(s=>s.Id==id);
            if (existedCategoryWithCourses is null) return NotFound();
            var CoursesQuery = courseService.GetAllCourseQuery()
                                        .Where(b => b.CategoryId == id)
                                        .Include(s => s.courseImages)
                                        .AsNoTracking();

            var paginatedCourses = PaginationVM<Course>.Create(CoursesQuery, page, 3);

            CourseInCategoryVM CourseInCategoryVM = new CourseInCategoryVM
            {
                Category = existedCategoryWithCourses,
                PaginationCourse = paginatedCourses
            };
            return View(CourseInCategoryVM);
        }
     


    }
}
