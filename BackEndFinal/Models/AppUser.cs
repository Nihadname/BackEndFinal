using Microsoft.AspNetCore.Identity;

namespace BackEndFinal.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string? imageUrl { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }


    }
}
