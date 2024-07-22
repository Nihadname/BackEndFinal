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
            
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var query=_teacherService.GetAllTeacherQuery();
            var existedTeacher = await query.AsNoTracking().Include(s => s.TeacherHobbies).ThenInclude(s => s.Hobby).Include(s => s.courseTeachers).ThenInclude(s => s.Course).Include(s => s.TeacherContactInfo)
                .FirstOrDefaultAsync(s=>s.Id == id);
            if(existedTeacher == null) return NotFound();
            return View(existedTeacher);

        }
    }
}
