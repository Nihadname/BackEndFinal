using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class BaskerCourse:BaseEntity
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int CourseId  { get; set; }  
        public Course Course {  get; set; } 
    }
}
