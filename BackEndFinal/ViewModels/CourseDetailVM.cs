using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class CourseDetailVM
    {
        public Course Course { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public CommentFormVM CommentForm { get; set; }

    }


}

