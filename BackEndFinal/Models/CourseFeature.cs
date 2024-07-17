using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class CourseFeature:BaseEntity
    {
        public DateTime Starts { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assessments { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
