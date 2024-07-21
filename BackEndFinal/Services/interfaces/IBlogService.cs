using BackEndFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> SearchCoursesAsync(string keyword, int skip, int take, params Expression<Func<Blog, object>>[] includes);
            IQueryable<Blog> GetAllBlogQuery();
        Task<List<Blog>> GetAllBlogAsync(int skip, int take, params Expression<Func<Blog, object>>[] includes);
        Task<Blog> GetBlogByIdAsync(int? id, params Expression<Func<Blog, object>>[] includes);
        Task AddBlogAsync(Blog Blog);
        Task UpdateBlogAsync(Blog Blog);
        Task DeleteBlogAsync(Blog Blog);
    }
}
