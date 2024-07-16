using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Blog:BaseEntity
    {
        public string Title  { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
