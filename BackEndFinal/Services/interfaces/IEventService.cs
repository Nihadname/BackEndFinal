using BackEndFinal.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace BackEndFinal.Services.interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventAsync(int skip, int take, params Expression<Func<Event, object>>[] includes);
        Task<Event> GetEventByIdAsync(int? id);
        Task AddEventAsync(Event OfferedAdvantages);
        Task UpdateEventAsync(Event OfferedAdvantages);
        Task DeleteEventAsync(Event OfferedAdvantages);
    }
}
