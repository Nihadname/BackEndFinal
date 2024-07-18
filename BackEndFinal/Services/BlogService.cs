using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using Microsoft.EntityFrameworkCore;
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

        public  IQueryable<Blog> GetAllBlogQuery()
        {
            return   _blogRepository.GetAllQuery();
        }

        public Task<Blog> GetBlogByIdAsync(int? id)
        {
           return _blogRepository.GetByIdAsync(id);
        }

        public async Task<List<Blog>> SearchBlogsAsync(string searchTerm)
        {
        var blogs=await _blogRepository.GetAllAsync(0,0,s=>s.Images,s=>s.Category);
            return blogs.Where(b => b.Title.Contains(searchTerm) || b.Content.Contains(searchTerm)).Take(4).ToList();
        }

        public Task UpdateBlogAsync(Blog OfferedAdvantages)
        {
           if(OfferedAdvantages is null) throw new ArgumentNullException(nameof(Blog));
           return _blogRepository.UpdateAsync(OfferedAdvantages);
        }
    }
}
