using BackEndFinal.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _context.Set<T>().AsNoTracking().ToListAsync();
        //}

        public async Task<List<T>> GetAllAsync(int skip = 0, int take = 0, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryForAddingDataInto = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryForAddingDataInto = queryForAddingDataInto.Include(include);
                }
            }
            if (skip > 0)
            {
                queryForAddingDataInto = queryForAddingDataInto.Skip(skip); 
            }
            if (take > 0)
            {
                queryForAddingDataInto = queryForAddingDataInto.Take(take); 
            }
            return await queryForAddingDataInto.AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int? id, params Expression<Func<T, object>>[] includes)
        {
            if (id is null) return null;
            IQueryable<T> queryForAddingDataInto = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryForAddingDataInto = queryForAddingDataInto.Include(include);
                }
            }
            return await queryForAddingDataInto.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
