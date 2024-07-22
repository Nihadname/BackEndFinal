using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.ViewCompenents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly ITeacherService _teacherService;

        public TeacherViewComponent(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        } 
        public async Task<IViewComponentResult> InvokeAsync(int take=4)
        {
            var teachers = _teacherService.GetAllTeacherQuery();
            var teachersWithCourses = await teachers.Include(s => s.courseTeachers).ThenInclude(s => s.Course).Take(take).ToListAsync();


            return View(Task.FromResult(teachersWithCourses));
        }
    }
}
