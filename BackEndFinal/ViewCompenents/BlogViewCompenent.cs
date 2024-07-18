using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.ViewCompenents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly AppDbContext appDbContext;

        public BlogViewComponent(IBlogService blogService, AppDbContext appDbContext)
        {
            _blogService = blogService;
            this.appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int page=1)
        {
            var query =_blogService.GetAllBlogQuery();
            var blogsQuery = query.Include(s => s.Images).AsNoTracking();

            var paginatedBlogs = PaginationVM<Blog>.Create(blogsQuery, page, 3);
            return View(paginatedBlogs);
        }
    }
}
