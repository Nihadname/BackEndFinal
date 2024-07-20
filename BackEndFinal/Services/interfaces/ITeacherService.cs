using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ITeacherService
    {
        Task<List<Blog>> SearchBlogsAsync(string searchTerm);
        IQueryable<Blog> GetAllBlogQuery();
        Task<List<Blog>> GetAllBlogAsync(int skip, int take, params Expression<Func<Blog, object>>[] includes);
        Task<Blog> GetBlogByIdAsync(int? id);
        Task AddBlogAsync(Blog Blog);
        Task UpdateBlogAsync(Blog Blog);
        Task DeleteBlogAsync(Blog Blog);
    }
}
