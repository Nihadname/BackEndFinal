using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class RegisterVM
    {
        [Required, StringLength(200)]
        public string FullName { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required,EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
        [Required,Phone, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


    }
}
