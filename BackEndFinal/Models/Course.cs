using BackEndFinal.Models.Common;
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
        public CourseFeature courseFeature { get; set; }
        //[NotMapped]
        //public string ShortDesc => Description.Length >= 50 ? Description.Substring(0, 50) : Description;


    }
}
