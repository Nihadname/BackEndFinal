using BackEndFinal.Models;
using Microsoft.AspNetCore.Identity;

namespace BackEndFinal.ViewModels
{
    public class LoginResultVM
    {
        public SignInResult SignInResult { get; set; }
        public AppUser User { get; set; }
    }
}
