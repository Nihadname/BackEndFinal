using BackEndFinal.Helpers;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;

namespace BackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
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

            AppUser appUser = new AppUser
            {
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, nameof(RolesEnum.Admin));
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = appUser.Email, token = token }, Request.Scheme, Request.Host.ToString());

            string body;
            using (StreamReader sr = new StreamReader("wwwroot/EmailPage/emailConfirm.html"))
            {
                body = sr.ReadToEnd();
            }
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{UserName}}", appUser.UserName);

            _emailService.SendEmail(
                from: "nihadmi@code.edu.az",
                to: appUser.Email,
                subject: "verify Email",
                body: body,
                smtpHost: "smtp.gmail.com",
                smtpPort: 587,
                enableSsl: true,
                smtpUser: "nihadmi@code.edu.az",
                smtpPass: "ilyo ibry uphi gnfe\r\n"
            );

            return Content("You are registered");
        }
        public async Task<IActionResult> VerifyEmail(string email,string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser is null) return NotFound();
            await _userManager.ConfirmEmailAsync(appUser, token);
            await _signInManager.SignInAsync(appUser,true);

            return RedirectToAction("Index","Home");
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
            if (!User.EmailConfirmed)
            {
                ModelState.AddModelError("", "You need to verify is account");
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

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                ModelState.AddModelError("Error1", "bele bir email movcud deyil");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string url = Url.Action(nameof(ResetPassword), "Account", new { email = appUser.Email, token = token }, Request.Scheme, Request.Host.ToString());

            string body;
            using (StreamReader sr = new StreamReader("wwwroot/EmailPage/ForgetPassword.html"))
            {
                body = sr.ReadToEnd();
            }
            body = body.Replace("{{link}}", url);
            body = body.Replace("{{UserName}}", appUser.UserName);

            _emailService.SendEmail(
                from: "nihadmi@code.edu.az",
                to: appUser.Email,
                subject: "forget Password",
                body: body,
                smtpHost: "smtp.gmail.com",
                smtpPort: 587,
                enableSsl: true,
                smtpUser: "nihadmi@code.edu.az",
                smtpPass: "ilyo ibry uphi gnfe\r\n"
            );

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser is null) return NotFound();
            bool result = await _userManager
                .VerifyUserTokenAsync(existUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (result is false) return Content("Token expired");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email,ResetPasswordVM resetPasswordVM, string token)
        {
            AppUser appUser= await _userManager.FindByEmailAsync(email);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _userManager.ResetPasswordAsync(appUser, token, resetPasswordVM.Password);
            await _userManager.UpdateSecurityStampAsync(appUser);
            return RedirectToAction("Login ","Account");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordVM);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(changePasswordVM);
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "Home");
        }
    }
}