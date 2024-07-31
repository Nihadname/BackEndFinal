using BackEndFinal.Models;
using System.Linq.Expressions;

namespace BackEndFinal.Services.interfaces
{
    public interface ICommentService
    {
        IQueryable<Comment> GetAllCommentQuery();
        Task<List<Comment>> GetAllCommentAsync(int skip, int take, params Expression<Func<Comment, object>>[] includes);
        Task<Comment> GetCommentByIdAsync(int? id, params Expression<Func<Comment, object>>[] includes);
        Task AddCommentAsync(Comment Comment);
        Task UpdateCommentAsync(Comment Comment);
        Task DeleteCommentAsync(Comment Comment);
    }
}
