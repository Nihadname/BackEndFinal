using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ITeacherService
    {
        IQueryable<Teacher> GetAllTeacherQuery();
        Task<List<Teacher>> GetAllTeacherAsync(int skip, int take, params Expression<Func<Teacher, object>>[] includes);
        Task<Teacher> GetTeacherByIdAsync(int? id);
        Task AddTeacherAsync(Teacher Teacher);
        Task UpdateTeacherAsync(Teacher Teacher);
        Task DeleteTeacherAsync(Teacher Teacher);
    }
}
