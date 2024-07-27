using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public List<CourseTag> courseTags { get; set; }
    }
}
