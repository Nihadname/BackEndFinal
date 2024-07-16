using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApplication11.Repositories.interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(int skip = 0, int take = 0, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int? id, params Expression<Func<T, object>>[] includes);
  
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

