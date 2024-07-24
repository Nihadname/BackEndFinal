using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required, StringLength(200)]
        public string FullName { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
          [Required,Phone, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
