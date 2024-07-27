using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class CourseRequest:BaseEntity
    {
        public string CourseName { get; set; }
        public string UserName { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? AppointmentTime { get; set; }
    }
}
