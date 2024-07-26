using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
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

        public async Task<IActionResult> Index(int page=1)
        {

            var pageSize = 4;
            var teachers = _teacherService.GetAllTeacherQuery();
            var teachersWithCourses = teachers.Include(s => s.courseTeachers).ThenInclude(s => s.Course);
            var paginatedTeachers = await PaginationVM<Teacher>.Create(teachersWithCourses, page, pageSize);
            return View(paginatedTeachers);
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            var query=_teacherService.GetAllTeacherQuery();
            var existedTeacher = await query.AsNoTracking().Include(s => s.courseTeachers).ThenInclude(s => s.Course).Include(s => s.TeacherContactInfo)
                .FirstOrDefaultAsync(s=>s.Id == id);
            if(existedTeacher == null) return NotFound();
            return View(existedTeacher);

        }
    }
}
