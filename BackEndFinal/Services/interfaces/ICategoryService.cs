using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAllCategoryQuery();
        Task<List<Category>> GetAllCategoryAsync(int skip, int take, params Expression<Func<Category, object>>[] includes);
        Task<Category> GetCategoryByIdAsync(int? id, params Expression<Func<Category, object>>[] includes);
        Task AddCategoryAsync(Category Category);
        Task UpdateCategoryAsync(Category Category);
        Task DeleteCategoryAsync(Category Category);
    }
}
