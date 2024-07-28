using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllSlidersAsync(int skip, int take, params Expression<Func<Slider, object>>[] includes);
        Task<Slider> GetSliderByIdAsync(int? id, params Expression<Func<Slider, object>>[] includes);
        Task AddSliderAsync(Slider Slider);
        Task UpdateSliderAsync(Slider Slider);
        Task DeleteSliderAsync(Slider Slider);
    }
}
