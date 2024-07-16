using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class HomeViewModel
    {
        public SliderContent SliderContent { get; set; }
        public ICollection<Slider> sliders { get; set; }
    }
}
