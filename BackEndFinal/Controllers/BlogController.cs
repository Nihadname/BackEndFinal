using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace BackEndFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ISettingService settingService;


        public BlogController(IBlogService blogService, ICategoryService categoryService, ISettingService settingService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            this.settingService = settingService;
        }

        public async  Task<IActionResult> Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            return View();
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return BadRequest();
            var result = await _blogService.SearchCoursesAsync(searchTerm,0,4,s=>s.Images);
                  return PartialView("_SearchPartialView", result);

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
           var existedOne=await _blogService.GetBlogByIdAsync(id, s=>s.Images);
            if (existedOne == null) return NotFound();
            var categories = await _categoryService.GetAllCategoryAsync(0, 6, s => s.blogs);
            var blogs = await _blogService.GetAllBlogAsync(0, 3, s => s.Images);

            BlogDetailVm vm = new BlogDetailVm();
            vm.blog = existedOne;
            vm.categories = categories;
            vm.blogs = blogs;
            return View(vm);
        }
        public async Task<IActionResult> BlogInCategory(int? id)
        {
            if(id == null) return BadRequest(); 
            var categories= _categoryService.GetAllCategoryQuery();
            var categorieWithBlog=await categories.AsNoTracking().Include(s=>s.blogs).ThenInclude(s=>s.Images).FirstOrDefaultAsync(s=>s.Id==id);
            if(categorieWithBlog == null)   return NotFound();
          return  View(categorieWithBlog);
        }
    }
}
