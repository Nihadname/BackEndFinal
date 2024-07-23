using Microsoft.AspNetCore.Identity;

namespace BackEndFinal.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string? imageUrl { get; set; }

    }
}
