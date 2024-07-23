using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class VerifyPhoneVM
    {
        [Required(ErrorMessage = "Verification code is required")]
        [Display(Name = "Verification Code")]
        public string Code { get; set; }
    }
}
