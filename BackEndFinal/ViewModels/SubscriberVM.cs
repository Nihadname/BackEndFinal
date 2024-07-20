using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class SubscriberVM
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
