using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Hobby:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }

    }
}
