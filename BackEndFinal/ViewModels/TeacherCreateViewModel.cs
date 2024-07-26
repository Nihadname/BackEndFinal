using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class TeacherCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public string Position { get; set; }

        public IFormFile Photo { get; set; }

        [Required]
        public int Language { get; set; }
        public string Hobbies { get; set; }
        public int TeamLeader { get; set; }
        public int Development { get; set; }
        public int Design { get; set; }
        public int Innovation { get; set; }
        public int Communication { get; set; }

        public TeacherContactInfoViewModel ContactInfo { get; set; }
    }
}
