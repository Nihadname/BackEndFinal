using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Event:BaseEntity
    {
        public string Title { get; set; }
        public DateTime HeldTime    { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
