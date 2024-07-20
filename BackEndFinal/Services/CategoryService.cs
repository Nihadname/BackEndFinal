using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public Task AddCategoryAsync(Category Category)
        {
            if (Category == null) throw new ArgumentNullException(nameof(Category));
            return _repository.AddAsync(Category);
        }

        public Task DeleteCategoryAsync(Category Category)
        {
            if(Category is null) throw new ArgumentNullException(nameof(Category));
            return _repository.DeleteAsync(Category);
        }

        public Task<List<Category>> GetAllCategoryAsync(int skip, int take, params Expression<Func<Category, object>>[] includes)
        {
           return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Category> GetAllCategoryQuery()
        {
           return _repository.GetAllQuery();
        }

        public Task<Category> GetCategoryByIdAsync(int? id, params Expression<Func<Category, object>>[] includes)
        {
            return _repository.GetByIdAsync(id,includes);
        }

        public Task UpdateCategoryAsync(Category Category)
        {
           if (_repository == null) throw new ArgumentNullException(nameof(Category));
           return _repository.UpdateAsync(Category);
        }
    }
}
