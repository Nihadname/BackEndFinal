using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;

        public BlogService(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Task AddBlogAsync(Blog Blog)
        {
            if(Blog is null)
            {

            throw new ArgumentNullException(nameof(Blog)); 
            }
           return _blogRepository.AddAsync(Blog);   
        }

        public Task DeleteBlogAsync(Blog Blog)
        {
            if(Blog is null)  throw new ArgumentNullException(nameof(Blog));
            return _blogRepository.DeleteAsync(Blog);
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetAllBlogAsync(int skip, int take, params Expression<Func<Blog, object>>[] includes)
        {
return _blogRepository.GetAllAsync(skip, take, includes);
        }

        public Task<Blog> GetBlogByIdAsync(int? id)
        {
           return _blogRepository.GetByIdAsync(id);
        }

        public Task UpdateBlogAsync(Blog OfferedAdvantages)
        {
           if(OfferedAdvantages is null) throw new ArgumentNullException(nameof(Blog));
           return _blogRepository.UpdateAsync(OfferedAdvantages);
        }
    }
}
