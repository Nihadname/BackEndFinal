using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class TeacherController : Controller
    {
        private readonly  ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers =  _teacherService.GetAllTeacherQuery();
            var teachersWithCourses=await teachers.Include(s=>s.courseTeachers).ThenInclude(s=>s.Course).ToListAsync();

            return View(teachersWithCourses);
        }
    }
}
