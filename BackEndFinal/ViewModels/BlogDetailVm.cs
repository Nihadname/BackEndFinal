using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class BlogDetailVm
    {
        public Blog blog { get; set; }
        public IEnumerable<Category> categories { get; set; }   
        public IEnumerable<Blog> blogs { get; set; }
        public Dictionary<string, string> settings { get; set; }
    }
}
