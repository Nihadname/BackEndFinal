using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;

        public CommentService(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        public Task AddCommentAsync(Comment Comment)
        {
            if(Comment == null) throw new ArgumentNullException(nameof(Comment));
            return  _repository.AddAsync(Comment);
        }

        public Task DeleteCommentAsync(Comment Comment)
        {
            if (Comment == null) throw new ArgumentNullException(nameof(Comment));
            return _repository.DeleteAsync(Comment);
        }

        public Task<List<Comment>> GetAllCommentAsync(int skip, int take, params Expression<Func<Comment, object>>[] includes)
        {
           return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Comment> GetAllCommentQuery()
        {
           return _repository.GetAllQuery();
        }

        public Task<Comment> GetCommentByIdAsync(int? id, params Expression<Func<Comment, object>>[] includes)
        {
            return _repository.GetByIdAsync(id, includes);
        }

        public Task UpdateCommentAsync(Comment Comment)
        {
            if (Comment == null) throw new ArgumentNullException(nameof(Comment));
            return _repository.UpdateAsync(Comment);
        }

    }
}
