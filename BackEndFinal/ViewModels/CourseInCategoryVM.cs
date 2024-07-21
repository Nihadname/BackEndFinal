using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class CourseInCategoryVM
    {
        public Category Category { get; set; }
        public Task<PaginationVM<Course>> PaginationCourse { get; set; }

    }
}
