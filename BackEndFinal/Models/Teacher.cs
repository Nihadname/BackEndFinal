using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Teacher:BaseEntity
    {
        public string Name { get; set; }
        public List<CourseTeacher> courseTeachers { get; set; }
    }
}
