using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalRoles { get; set; }
        public int TotalCourses { get; set; }
        public List<AppUser> RecentUsers { get; set; }
        public List<Course> RecentCourses { get; set; }
        
    }
}
