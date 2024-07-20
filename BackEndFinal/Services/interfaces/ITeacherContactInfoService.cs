using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ITeacherContactInfoService
    {
        IQueryable<TeacherContactInfo> GetAllTeacherContactInfoQuery();
        Task<List<TeacherContactInfo>> GetAllTeacherContactInfoAsync(int skip, int take, params Expression<Func<TeacherContactInfo, object>>[] includes);
        Task<TeacherContactInfo> GetTeacherContactInfoByIdAsync(int? id);
        Task AddTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo);
        Task UpdateTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo);
        Task DeleteTeacherContactInfoAsync(TeacherContactInfo TeacherContactInfo);
    }
}
