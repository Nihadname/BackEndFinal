using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _repository;

        public TagService(IRepository<Tag> repository)
        {
            _repository = repository;
        }

        public Task AddTagAsync(Tag Comment)
        {
           if(Comment == null) throw new ArgumentNullException( nameof(Comment) );
           return _repository.AddAsync(Comment);
        }

        public Task DeleteTagAsync(Tag Comment)
        {
            if(Comment == null) throw new ArgumentNullException(nameof(Comment));
            return _repository.DeleteAsync(Comment);
            throw new NotImplementedException();
        }

        public Task<List<Tag>> GetAllTagAsync(int skip, int take, params Expression<Func<Tag, object>>[] includes)
        {
            return _repository.GetAllAsync(skip, take, includes);
        }

        public IQueryable<Tag> GetAllTagQuery()
        {
            return _repository.GetAllQuery();
        }

        public Task<Tag> GetTagByIdAsync(int? id, params Expression<Func<Tag, object>>[] includes)
        {
            return _repository.GetByIdAsync(id, includes);
        }

        public Task UpdateTagAsync(Tag Comment)
        {
            if (Comment is null)
            {
                throw new ArgumentNullException(nameof(Comment));
                
            }
          return _repository.UpdateAsync(Comment);  
        }
    }
}
