using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class BlogDetailViewComponent:ViewComponent
    {
        private readonly IBlogService blogService;

        public BlogDetailViewComponent(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs=await blogService.GetAllBlogAsync(0,3,s=>s.Images);
            return View(blogs);
        }
    }

}
