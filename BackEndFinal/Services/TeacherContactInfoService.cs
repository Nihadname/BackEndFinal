using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class TeacherContactInfoService : ITeacherContactInfoService
    {
        private readonly IRepository<TeacherContactInfo> _repository;

        public TeacherContactInfoService(IRepository<TeacherContactInfo> repository)
        {
            _repository = repository;
        }

        public Task AddTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo)
        {
            if(TeacherContactInfo == null) throw new ArgumentNullException(nameof(TeacherContactInfo)); 
            return _repository.AddAsync(TeacherContactInfo);
        }

        public Task DeleteTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo)
        {
            if(TeacherContactInfo == null) throw new ArgumentNullException(nameof(TeacherContactInfo));
            return _repository.DeleteAsync(TeacherContactInfo);
        }

        public Task<List<TeacherContactInfo>> GetAllTeacherContactInfoAsync(int skip, int take, params Expression<Func<TeacherContactInfo, object>>[] includes)
        {
            return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<TeacherContactInfo> GetAllTeacherContactInfoQuery()
        {
            return _repository.GetAllQuery();
        }

        public Task<TeacherContactInfo> GetTeacherContactInfoByIdAsync(int? id)
        {
           return _repository.GetByIdAsync(id);
        }

        public Task UpdateTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo)
        {
            if(TeacherContactInfo is null) throw new ArgumentNullException(nameof(TeacherContactInfo));
            return _repository.UpdateAsync(TeacherContactInfo);
        }
    }
}
