using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class EventImage:BaseEntity
    {
        public string Name { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
