using BackEndFinal.Helpers;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser appUser = new AppUser();
            appUser.FullName = registerVM.FullName;
            appUser.UserName = registerVM.UserName;
            appUser.Email = registerVM.Email;
            appUser.PhoneNumber = registerVM.PhoneNumber;

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(appUser, nameof(RolesEnum.Member));

            return Content("you are registered");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var User = await _userManager.FindByEmailAsync(loginVM.EmailOrUserName);
            if (User == null)
            {
                User = await _userManager.FindByNameAsync(loginVM.EmailOrUserName);
                if (User == null)
                {
                    ModelState.AddModelError("", "userName or email is wrong");
                    return View(loginVM);
                }
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(User, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is blocked.");
                return View(loginVM);
            }
            if (User.IsBlocked)
            {
                ModelState.AddModelError("", "Your account is blocked.");
                return View(loginVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(loginVM);
            }


            // Use a service like Twilio to send the code
            //_smsService.SendSms(User.PhoneNumber, $"Your verification code is {code}");
            if (await _userManager.IsInRoleAsync(User, nameof(RolesEnum.Admin)))
            {
                return RedirectToAction("DashBoard", "AdminArea");
            }

            // Default to Home if no ReturnUrl and not an admin
            if (ReturnUrl == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(ReturnUrl);

        }
        public async Task<IActionResult> AddRole()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            }
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
            }
            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Member" });
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Student" });
            }
            return Content("roles are added");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}