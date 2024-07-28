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
        public IActionResult Index()
        {
            var dashboardViewModel = new DashboardViewModel
            {
                TotalUsers = _context.Users.Count(),
                TotalRoles = _context.Roles.Count(),
                TotalCourses = _context.courses.Count(),
                RecentUsers = _context.Users.Take(5).ToList(),
                RecentCourses = _context.courses.OrderByDescending(c => c.CreatedTime).Take(5).ToList()
            };

            return View(dashboardViewModel);
        }
    }
}
