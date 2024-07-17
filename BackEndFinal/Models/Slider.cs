using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Slider:BaseEntity
    {
        public string ImageUrl {  get; set; }
      
        public SliderContent SliderContent  { get; set; }
    }
}
