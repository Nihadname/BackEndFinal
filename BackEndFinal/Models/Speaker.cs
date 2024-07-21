using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Speaker:BaseEntity
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string position { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
