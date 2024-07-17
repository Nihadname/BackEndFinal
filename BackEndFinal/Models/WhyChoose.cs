using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class WhyChoose:BaseEntity
    {
        public string Name  { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
