using BackEndFinal.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class Subscriber:BaseEntity
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
