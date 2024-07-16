using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Category:BaseEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Course > Courses { get; set; }
        public List<Event> events { get; set; }
        public List<Blog> blogs {  get; set; }
    }
}
