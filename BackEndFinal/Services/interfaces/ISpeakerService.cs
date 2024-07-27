using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ISpeakerService
    {
        Task<List<Speaker>> GetAlSpeakerAsync(int skip, int take, params Expression<Func<Speaker, object>>[] includes);
        Task<Speaker> GetSpeakerByIdAsync(int? id, params Expression<Func<Speaker, object>>[] includes);
        IQueryable<Speaker> GetAllSpeakerQuery();

        Task AddSpeakerAsync(Speaker Speaker);
        Task UpdateSpeakerAsync(Speaker Speaker);
        Task DeleteSpeakerAsync(Speaker Speaker);
    }
}
