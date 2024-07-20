using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Teacher:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public TeacherContactInfo TeacherContactInfo { get; set; }
        public List<CourseTeacher> courseTeachers { get; set; }
    }
}
