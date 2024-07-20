using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ISubscriberService
    {
        Task<List<Subscriber>> GetAlSubscriberAsync(int skip, int take, params Expression<Func<Subscriber, object>>[] includes);
        Task<Subscriber> GetSubscriberByIdAsync(int? id);
        IQueryable<Subscriber> GetAllSubscriberQuery();
        Task AddSubscriberAsync(Subscriber Course);
        Task UpdateSubscriberAsync(Subscriber Course);
        Task DeleteSubscriberAsync(Subscriber Course);
    }
}
