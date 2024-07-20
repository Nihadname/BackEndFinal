using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IRepository<Subscriber> _subscriberService;

        public SubscriberService(IRepository<Subscriber> subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public Task AddSubscriberAsync(Subscriber Course)
        {
            if (Course == null) throw new ArgumentNullException(nameof(Course));
            return _subscriberService.AddAsync(Course);
        }

        public Task DeleteSubscriberAsync(Subscriber Course)
        {
            if(_subscriberService == null) throw new ArgumentNullException(nameof(Course));
            return _subscriberService.DeleteAsync(Course);
        }

        public IQueryable<Subscriber> GetAllSubscriberQuery()
        {
            return _subscriberService.GetAllQuery();
        }

        public Task<List<Subscriber>> GetAlSubscriberAsync(int skip, int take, params Expression<Func<Subscriber, object>>[] includes)
        {
           return _subscriberService.GetAllAsync(skip, take, includes);
        }

        public Task<Subscriber> GetSubscriberByIdAsync(int? id)
        {
           return _subscriberService.GetByIdAsync(id);
        }

        public Task UpdateSubscriberAsync(Subscriber Course)
        {
            if (_subscriberService == null) throw new ArgumentNullException(nameof(Course));
            return _subscriberService.UpdateAsync(Course);
        }
    }
}
