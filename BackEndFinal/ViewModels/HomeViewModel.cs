using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Slider> sliders { get; set; }
        public ICollection<OfferedAdvantages> offeredAdvantages { get; set; }
        public List<Event> events { get; set; } 
        public WhyChoose WhyChoose { get; set; }
    }
}
