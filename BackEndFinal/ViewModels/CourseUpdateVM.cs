using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class CourseUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CourseImage>? courseImages { get; set; }

        public int CategoryId { get; set; }
        public IFormFile[] Photos { get; set; }
        public DateTime Starts { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assessments { get; set; }
        public string? AboutCourse { get; set; }
        public string? HowToApply { get; set; }
        public string? CERTIFICATION { get; set; }
        public int Price { get; set; }
        public List<int> TeacherIds { get; set; }
        public List<int> TagIds { get; set; }
    }
}
