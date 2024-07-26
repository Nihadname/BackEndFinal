using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class EventUpdateVM
    {
        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime HeldTime { get; set; }
        public int CategoryId { get; set; }
        public List<EventImage>? Images { get; set; }
        public IFormFile[]? Photos { get; set; }
    }
}
