using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Teacher:BaseEntity
    {
        public Teacher()
        {
            TeacherHobbies = new List<TeacherHobby>();
            courseTeachers = new List<CourseTeacher>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string degree { get; set; }
        public int experience { get; set; }
        public string faculty { get; set; }

        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public int Language {  get; set; }
        public int TeamLeader {  get; set; }
        public int Development {  get; set; }
        public int Design {  get; set; }
        public int Innovation { get; set; }
        public int Communication { get; set; }

        public TeacherContactInfo TeacherContactInfo { get; set; }
        public List<CourseTeacher> courseTeachers { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; } 

    }
}
