using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class HobbyService : IHobbyService
    {
        private readonly IRepository<Hobby> _repository;

        public HobbyService(IRepository<Hobby> repository)
        {
            _repository = repository;
        }

        public Task AddHobbyAsync(Hobby OfferedAdvantages)
        {
         if(OfferedAdvantages == null)  throw new ArgumentException(nameof(OfferedAdvantages));
         return _repository.AddAsync(OfferedAdvantages);

        }

        public Task DeleteHobbyAsync(Hobby OfferedAdvantages)
        {
            if (OfferedAdvantages == null) throw new ArgumentException(nameof(OfferedAdvantages));
            return _repository.DeleteAsync(OfferedAdvantages);
        }

        public IQueryable<Hobby> GetAllHobbyQuery()
        {
            return _repository.GetAllQuery();
        }

        public Task<List<Hobby>> GetAllHobbyAsync(int skip, int take, params Expression<Func<Hobby, object>>[] includes)
        {
            return _repository.GetAllAsync(skip, take, includes);
        }

        public Task<Hobby> GetHobbyByIdAsync(int? id, params Expression<Func<Hobby, object>>[] includes)
        {
            return _repository.GetByIdAsync(id, includes);
        }

        public Task UpdateHobbyAsync(Hobby OfferedAdvantages)
        {
            if (OfferedAdvantages == null) throw new ArgumentException(nameof(OfferedAdvantages));
            return _repository.UpdateAsync(OfferedAdvantages);
        }
    }
}
