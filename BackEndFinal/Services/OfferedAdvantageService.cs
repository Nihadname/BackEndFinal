using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class OfferedAdvantageService : IOfferedAdvantageService
    {
        private readonly IRepository<OfferedAdvantages> _offeredAdvantagesRepository;

        public OfferedAdvantageService(IRepository<OfferedAdvantages> offeredAdvantagesRepository)
        {
            _offeredAdvantagesRepository = offeredAdvantagesRepository;
        }

        public Task AddOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages)
        {
       return     _offeredAdvantagesRepository.AddAsync(OfferedAdvantages);
        }

        public Task DeleteOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages)
        {
            return _offeredAdvantagesRepository.DeleteAsync(OfferedAdvantages);
        }

        public Task<List<OfferedAdvantages>> GetAllOfferedAdvantagesAsync(int skip, int take, params Expression<Func<OfferedAdvantages, object>>[] includes)
        {
           return _offeredAdvantagesRepository.GetAllAsync(skip, take, includes);
        }

        public Task<OfferedAdvantages> GetOfferedAdvantagesByIdAsync(int? id)
        {
            return _offeredAdvantagesRepository.GetByIdAsync(id);
        }

        public Task UpdateOfferedAdvantagesAsync(OfferedAdvantages OfferedAdvantages)
        {
            return _offeredAdvantagesRepository.UpdateAsync(OfferedAdvantages);
        }
    }
}
