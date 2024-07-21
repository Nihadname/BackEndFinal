using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class BlogInCategoryVM
    {
        public Category Category { get; set; }
        public Task<PaginationVM<Blog>> PaginationBlog {get; set; }
    }
}
