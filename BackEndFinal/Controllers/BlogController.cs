using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async  Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllBlogAsync(0, 0, s => s.Images, s => s.Category);
            ViewBag.BlogCount = blogs.Count();
            return View(blogs);
        }
        public async Task<IActionResult> Loadmore(int skip = 3)
        {
            var datas = await _blogService.GetAllBlogAsync(skip, 3, s => s.Images);
            return PartialView("_BlogPartialView", datas);


        }
    }
}
