using BackEndFinal.Data;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles ="Admin")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;

        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            var dashboardViewModel = new DashboardViewModel
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalRoles = await _context.Roles.CountAsync(),
                TotalCourses = await _context.courses.CountAsync(),
                RecentUsers = await _context.Users.Take(5).ToListAsync(),
                RecentCourses = await _context.courses.OrderByDescending(c => c.CreatedTime).Take(5).ToListAsync()
            };

            return View(dashboardViewModel);
        }
    }
}
