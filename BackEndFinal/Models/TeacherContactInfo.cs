using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class TeacherContactInfo:BaseEntity
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        //[Phone]
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("^\\+?(\\d[\\d-. ]+)?(\\([\\d-. ]+\\))?[\\d-. ]+\\d$\r\n", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }
        public string FaceBookUrl { get; set; }
        public string pinterestUrl { get; set; }
        public string SkypeUrl  { get; set; }
        public string IntaUrl { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        
    }
}
