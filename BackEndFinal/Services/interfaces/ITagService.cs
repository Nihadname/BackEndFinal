using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ITagService
    {
        IQueryable<Tag> GetAllTagQuery();
        Task<List<Tag>> GetAllTagAsync(int skip, int take, params Expression<Func<Tag, object>>[] includes);
        Task<Tag> GetTagByIdAsync(int? id, params Expression<Func<Tag, object>>[] includes);
        Task AddTagAsync(Tag Comment);
        Task UpdateTagAsync(Tag Comment);
        Task DeleteTagAsync(Tag Comment);
    }
}
