using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class Footer:BaseEntity
    {
        [Required]
        [MaxLength(95)]
        public string Title { get; set; }
        public List<FooterContent> footerContents { get; set; }
    }
}
