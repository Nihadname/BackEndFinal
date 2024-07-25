using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class Event:BaseEntity
    {
        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime HeldTime { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<EventImage> Images { get; set; }
        public Event()
        {
            Images = new List<EventImage>();
        }
    }
}
