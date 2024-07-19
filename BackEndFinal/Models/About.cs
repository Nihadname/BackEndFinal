using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class About: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
