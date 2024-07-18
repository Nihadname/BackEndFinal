using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using System.Linq.Expressions;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseService;

        public CourseService(IRepository<Course> courseService)
        {
            _courseService = courseService;
        }

        public Task AddCourseAsync(Course Course)
        {
            if(Course == null) throw new ArgumentNullException(nameof(Course));
            return _courseService.AddAsync(Course);
        }

        public Task DeleteCourseAsync(Course Course)
        {
            if (Course == null) throw new ArgumentNullException(nameof(Course));
            return _courseService.DeleteAsync(Course);
        }

        public Task<List<Course>> GetAlCourseAsync(int skip, int take, params Expression<Func<Course, object>>[] includes)
        {
            return _courseService.GetAllAsync(skip, take, includes);
        }

        public Task<Course> GetCourseByIdAsync(int? id)
        {
           return _courseService.GetByIdAsync(id);
        }

        public Task UpdateCourseAsync(Course Course)
        {
            return _courseService.UpdateAsync(Course);
        }
    }
}
