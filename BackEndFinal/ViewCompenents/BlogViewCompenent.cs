using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.ViewCompenents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take = 3)
        {
            var blogs = await _blogService.GetAllBlogAsync(0, take, s => s.Category, s => s.Category, s => s.Images);
            return View(await Task.FromResult(blogs));
        }
    }
}
