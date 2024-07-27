using BackEndFinal.Models.Common;
using Humanizer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndFinal.Models
{
    public class Course : BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CourseImage> courseImages { get; set; }
        public List<CourseTeacher> courseTeachers { get; set; }
        public List<CourseTag> courseTags { get; set; }
        public DateTime Starts { get; set; }
        public string Duration { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assessments { get; set; }
        public string? AboutCourse { get; set; }
        public string? HowToApply { get; set; }
        public string? CERTIFICATION { get; set; }
        public int Price { get; set; }


        //[NotMapped]
        //public string ShortDesc => Description.Length >= 50 ? Description.Substring(0, 50) : Description;


    }
}
