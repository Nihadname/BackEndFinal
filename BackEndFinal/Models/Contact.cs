using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
