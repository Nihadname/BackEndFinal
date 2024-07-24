using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface IHobbyService
    {
        Task<List<Hobby>> GetAllHobbyAsync(int skip, int take, params Expression<Func<Hobby, object>>[] includes);
        Task<Hobby> GetHobbyByIdAsync(int? id, params Expression<Func<Hobby, object>>[] includes);
        IQueryable<Hobby> GetAllHobbyQuery();

        Task AddHobbyAsync(Hobby OfferedAdvantages);
        Task UpdateHobbyAsync(Hobby OfferedAdvantages);
        Task DeleteHobbyAsync(Hobby OfferedAdvantages);
    }
}
