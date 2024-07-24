using BackEndFinal.Models;
using Microsoft.AspNetCore.Identity;

namespace BackEndFinal.ViewModels
{
    public class RoleDetailVM
    {
        public IdentityRole Role { get; set; }
        public IList<AppUser> Users { get; set; }
    }
}
