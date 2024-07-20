using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _repository;

        public TeacherService(IRepository<Teacher> repository)
        {
            _repository = repository;
        }

        public Task AddTeacherAsync(Teacher Teacher)
        {
            if (Teacher == null) throw new ArgumentNullException(nameof(Teacher));
            return _repository.AddAsync(Teacher);
        }

        public Task DeleteTeacherAsync(Teacher Teacher)
        {
            if(Teacher == null)  throw new ArgumentException(nameof(Teacher));
            return _repository.DeleteAsync(Teacher);  
        }

        public Task<List<Teacher>> GetAllTeacherAsync(int skip, int take, params Expression<Func<Teacher, object>>[] includes)
        {
            return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Teacher> GetAllTeacherQuery()
        {
           return _repository.GetAllQuery();
        }


        
        public Task<Teacher> GetTeacherByIdAsync(int? id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task UpdateTeacherAsync(Teacher Teacher)
        {
            if (_repository == null) throw new ArgumentNullException(nameof(Teacher));
            return _repository.UpdateAsync(Teacher);
        }
    }
}
