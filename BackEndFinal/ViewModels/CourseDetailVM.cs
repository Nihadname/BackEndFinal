using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class CourseDetailVM
    {
        public Course course { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Blog> blogs { get; set; }
    }
}
