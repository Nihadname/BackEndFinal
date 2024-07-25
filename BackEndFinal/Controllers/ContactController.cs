using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly IRepository<Contact> repository;
        private readonly IEmailService emailService;
        public ContactController(IRepository<Contact> repository, IEmailService emailService)
        {
            this.repository = repository;
            this.emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactVM contactVM)
        {
            if(!ModelState.IsValid) return View("Index",contactVM);
            var allContact = await repository.GetAllAsync(0, 0);
            var checikingUniquenessOfEmaoil = allContact.Any(h => h.Email == contactVM.Email);
            if (checikingUniquenessOfEmaoil)
            {
                ModelState.AddModelError(string.Empty, "A Email with the same name already Send.");
                return View("Index", contactVM);
            }
            Contact contact = new Contact();
            contact.Email = contactVM.Email;
            contact.Name = contactVM.Name;
            contact.Subject = contactVM.Subject;
            contact.Message = contactVM.Message;
            try
            {
               await repository.AddAsync(contact);

                // Send email
                emailService.SendEmail(
                                from: "nihadmi@code.edu.az",
                                to: contact.Email,
                                subject: "forget Password",
                 body :$"Name: {contact.Name}<br>Email: {contact.Email}<br>Subject: {contact.Subject}<br>Message: {contact.Message}",
            smtpHost: "smtp.gmail.com",
                                smtpPort: 587,
                                enableSsl: true,
                                smtpUser: "nihadmi@code.edu.az",
                                smtpPass: "ilyo ibry uphi gnfe\r\n"
                            );
                return View("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the hobby: " + ex.Message);
                return View("Index", contactVM);

            }
        }
    }
}
