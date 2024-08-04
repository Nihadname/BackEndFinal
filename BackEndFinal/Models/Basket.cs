using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Basket:BaseEntity
    {
        public  string AppUserId { get; set; }
        public  AppUser AppUser { get; set; }
        public ICollection<BaskerCourse> BaskerProducts { get; set; }
    }
}
