using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repository;

        public EventService(IRepository<Event> repository)
        {
            _repository = repository;
        }

        public Task AddEventAsync(Event Event)
        {
            if (Event == null)
            {
            throw new ArgumentNullException(nameof(Event));
        }
          return _repository.AddAsync(Event);
        }

        public Task DeleteEventAsync(Event Event)
        {
            if (Event == null)
            {
                throw new ArgumentNullException(nameof(Event));
            }
            return  _repository.DeleteAsync(Event);
                }

        public Task<List<Event>> GetAllEventAsync(int skip, int take, params Expression<Func<Event, object>>[] includes)
        {
            return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Event> GetAllEventQuery()
        {
            return _repository.GetAllQuery();
        }

        public Task<Event> GetEventByIdAsync(int? id, params Expression<Func<Event, object>>[] includes)
        {
            return _repository.GetByIdAsync(id,includes);
        }

        public Task UpdateEventAsync(Event Event)
        {
            if (Event == null)
            {
                throw new ArgumentNullException(nameof(Event));
            }
            return      _repository.UpdateAsync(Event);
        }
    }
}
