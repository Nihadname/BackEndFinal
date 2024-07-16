using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class FooterContent:BaseEntity
    {
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
        public int FooterId { get; set; }
        public  Footer Footer { get; set; }
    }
}
