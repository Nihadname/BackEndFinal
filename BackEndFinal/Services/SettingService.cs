using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingService;

        public SettingService(IRepository<Setting> settingService)
        {
            _settingService = settingService;
        }

        public Task AddSettingAsync(Setting Setting)
        {
            if(Setting == null) throw new ArgumentNullException(nameof(Setting));
            return _settingService.AddAsync(Setting);
        }

        public Task DeleteSettingAsync(Setting Setting)
        {
            if(Setting == null) throw new ArgumentNullException(nameof(Setting));

            return _settingService.DeleteAsync(Setting);
        }

        public Task<List<Setting>> GetAllSettingAsync(int skip, int take, params Expression<Func<Setting, object>>[] includes)
        {
            return _settingService.GetAllAsync(skip, take, includes);   
        }

        public IQueryable<Setting> GetAllSettingQuery()
        {
            return _settingService.GetAllQuery();
        }

        public Task<Setting> GetSettingByIdAsync(int? id, params Expression<Func<Setting, object>>[] includes)
        {
          return _settingService.GetByIdAsync (id, includes);
        }

        public Task UpdateSettingAsync(Setting Setting)
        {
           if( Setting == null) throw new ArgumentNullException(    nameof(Setting));
           return _settingService.UpdateAsync(Setting);
        }
        public async Task<Dictionary<string, string>> GetSettingsAsDictionaryAsync()
        {
            var settings = await _settingService.GetAllAsync(0, int.MaxValue);
            return settings.ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
