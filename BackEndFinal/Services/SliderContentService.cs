using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class SliderContentService : ISliderContentService
    {
        private readonly IRepository<SliderContent> _SliderContentRepository;
        public Task AddSliderAsync(SliderContent Slider)
        {
         return   _SliderContentRepository.AddAsync(Slider);
        }

        public Task DeleteSliderAsync(SliderContent Slider)
        {
            return _SliderContentRepository.DeleteAsync(Slider);
        }

        public Task<List<SliderContent>> GetAllSlidersAsync(int skip, int take, params Expression<Func<SliderContent, object>>[] includes)
        {
            return _SliderContentRepository.GetAllAsync(skip, take, includes);
        }
         
       

        public Task<SliderContent> GetSliderByIdAsync(int? id)
        {
            return _SliderContentRepository.GetByIdAsync(id);
        }

        public Task UpdateSliderAsync(SliderContent Slider)
        {
            return _SliderContentRepository.UpdateAsync(Slider);
        }
    }
}
