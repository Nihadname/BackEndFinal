using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface IOfferedAdvantageService
    {
        Task<List<OfferedAdvantages>> GetAllSlidersAsync(int skip, int take, params Expression<Func<OfferedAdvantages, object>>[] includes);
        Task<OfferedAdvantages> GetSliderByIdAsync(int? id);
        Task AddSliderAsync(OfferedAdvantages OfferedAdvantages);
        Task UpdateSliderAsync(OfferedAdvantages OfferedAdvantages);
        Task DeleteSliderAsync(OfferedAdvantages OfferedAdvantages);
    }
}
