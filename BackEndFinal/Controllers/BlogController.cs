using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
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
    }
}
