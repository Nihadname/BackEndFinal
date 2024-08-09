using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class WishlistItem:BaseEntity
    {
        public string UserId { get; set; }
        public  AppUser User { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
