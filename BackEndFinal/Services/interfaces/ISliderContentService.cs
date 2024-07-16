using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ISliderContentService
    {
        Task<List<SliderContent>> GetAllSlidersAsync(int skip, int take, params Expression<Func<Slider, object>>[] includes);
        Task<SliderContent> GetSliderByIdAsync(int? id);
        Task AddSliderAsync(SliderContent Slider);
        Task UpdateSliderAsync(SliderContent Slider);
        Task DeleteSliderAsync(SliderContent Slider);
    }
}
