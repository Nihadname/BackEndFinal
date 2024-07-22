using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class TeacherHobby:BaseEntity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }
    }
}
