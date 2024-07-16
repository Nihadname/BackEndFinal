using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class CourseTeacher:BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int TeacherId    { get; set; }
        public Teacher Teacher  { get; set; }
    }
}
