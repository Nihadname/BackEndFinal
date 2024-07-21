using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ISettingService
    {
        IQueryable<Setting> GetAllSettingQuery();
        Task<List<Setting>> GetAllSettingAsync(int skip, int take, params Expression<Func<Setting, object>>[] includes);
        Task<Setting> GetSettingByIdAsync(int? id, params Expression<Func<Setting, object>>[] includes);
        Task AddSettingAsync(Setting Setting);
        Task UpdateSettingAsync(Setting Setting);
        Task DeleteSettingAsync(Setting Setting);
        Task<Dictionary<string, string>> GetSettingsAsDictionaryAsync();

    }
}
