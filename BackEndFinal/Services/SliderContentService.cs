using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;

namespace BackEndFinal.Services
{
    public class SliderContentService : ISliderContentService
    {
        public Task AddSliderAsync(SliderContent Slider)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSliderAsync(SliderContent Slider)
        {
            throw new NotImplementedException();
        }

        public Task<List<SliderContent>> GetAllSlidersAsync(int skip, int take, params Expression<Func<Slider, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<SliderContent> GetSliderByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSliderAsync(SliderContent Slider)
        {
            throw new NotImplementedException();
        }
    }
}
