using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface IOfferedAdvantageService
    {
        Task<List<OfferedAdvantages>> GetAllOfferedAdvantagesAsync(int skip, int take, params Expression<Func<OfferedAdvantages, object>>[] includes);
        Task<OfferedAdvantages> GetOfferedAdvantagesByIdAsync(int? id);
        Task AddOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages);
        Task UpdateOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages);
        Task DeleteOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages);
    }
}
