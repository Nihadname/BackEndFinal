using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class TeacherContactInfoViewModel
    {
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("^\\+?(\\d[\\d-. ]+)?(\\([\\d-. ]+\\))?[\\d-. ]+\\d$", ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        public string FacebookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string SkypeUrl { get; set; }
        public string InstagramUrl { get; set; }
    }
}
