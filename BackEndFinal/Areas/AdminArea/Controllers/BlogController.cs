using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index(string searchText)
        {
            var blogs = _blogService.GetAllBlogQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                blogs = blogs.Where(s => s.Title.ToLower().Contains(searchText.ToLower()) ||
                                         s.Content.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm =  blogs.ToList();
            return View(usersForActualForm);

        }
        public  IActionResult Create()
        {
            return View();
        }
    }
}
