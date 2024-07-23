namespace BackEndFinal.ViewModels
{
    public class UserProfileVM
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? imageUrl { get; set; }
        public IFormFile photo {  get; set; }
    }
}
