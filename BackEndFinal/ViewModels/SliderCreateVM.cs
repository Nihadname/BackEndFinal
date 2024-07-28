using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class SliderCreateVM
    {
        public IFormFile photo { get; set; }
        public SliderContentVM SliderContent { get; set; }
    }
}
