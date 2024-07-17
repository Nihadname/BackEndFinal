using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class TestimonialArea:BaseEntity
    {
        public string UserImageUrl { get; set; }
        public string Comment {  get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string field { get; set; }
    }
}
