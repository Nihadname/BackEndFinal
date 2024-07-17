using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class OfferedAdvantages:BaseEntity
    {
 
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
