namespace BackEndFinal.ViewModels
{
    public class SpeakerUpdateVM
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string Position { get; set; }
        public int EventId { get; set; }
    }
}
