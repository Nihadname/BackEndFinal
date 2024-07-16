using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class SliderService : ISliderService
    {
        private readonly IRepository<Slider> _SliderRepository;

        public SliderService(IRepository<Slider> sliderRepository)
        {
            _SliderRepository = sliderRepository;
        }

        public Task AddSliderAsync(Slider Slider)
        {
        return   _SliderRepository.AddAsync(Slider);
        }

        public Task DeleteSliderAsync(Slider Slider)
        {
            return _SliderRepository.DeleteAsync(Slider);
        }

        public Task<List<Slider>> GetAllSlidersAsync(int skip = 0, int take = 0, params Expression<Func<Slider, object>>[] includes)
        {
            return _SliderRepository.GetAllAsync(skip, take, includes);
        }
        public Task<Slider> GetSliderByIdAsync(int? id)
        {
            return _SliderRepository.GetByIdAsync(id);
        }

        public Task UpdateSliderAsync(Slider Slider)
        {
            return _SliderRepository.UpdateAsync(Slider);
        }
    }
}
