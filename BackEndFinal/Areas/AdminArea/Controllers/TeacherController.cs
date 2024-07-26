using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var teachers = teacherService.GetAllTeacherQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                teachers = teachers.Where(s => s.Name.ToLower().Contains(searchText.ToLower()) ||
                                         s.Description.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = teachers.ToList();
            return View(usersForActualForm);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existedTeacher = await teacherService.GetTeacherByIdAsync(id, s => s.TeacherContactInfo);
            if (existedTeacher == null) return NotFound();
            return View(existedTeacher);
        }
        public async Task<IActionResult> Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newProfileImage = viewModel.Photo;
                if (newProfileImage != null)
                {
                    if (!newProfileImage.CheckContentType())
                    {
                        ModelState.AddModelError("Photo", "Only image files are allowed.");
                        return View(viewModel);
                    }
                    if (!newProfileImage.CheckSize(500))
                    {
                        ModelState.AddModelError("Photo", "The image size is too large. Maximum allowed size is 500KB.");
                        return View(viewModel);
                    }

                    // Save the new image file

                }

                var teacher = new Teacher
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    degree = viewModel.Degree,
                    experience = viewModel.Experience,
                    faculty = viewModel.Faculty,
                    Position = viewModel.Position,
                    ImageUrl = await newProfileImage.SaveFile("teacher"),
                    Hobbies = viewModel.Hobbies,
                    Language = viewModel.Language,
                    TeamLeader = viewModel.TeamLeader,
                    Development = viewModel.Development,
                    Design = viewModel.Design,
                    Innovation = viewModel.Innovation,
                    Communication = viewModel.Communication,
                    TeacherContactInfo = new TeacherContactInfo
                    {
                        EmailAddress = viewModel.ContactInfo.EmailAddress,
                        PhoneNumber = viewModel.ContactInfo.PhoneNumber,
                        FaceBookUrl = viewModel.ContactInfo.FacebookUrl,
                        pinterestUrl = viewModel.ContactInfo.PinterestUrl,
                        SkypeUrl = viewModel.ContactInfo.SkypeUrl,
                        IntaUrl = viewModel.ContactInfo.InstagramUrl
                    }
                };

                await teacherService.AddTeacherAsync(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedTeacher = await teacherService.GetTeacherByIdAsync(id);
            if (existedTeacher == null) return NotFound();
            existedTeacher.ImageUrl.DeleteFile("teacher");
            await teacherService.DeleteTeacherAsync(existedTeacher);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var teacher = await teacherService.GetTeacherByIdAsync(id, s => s.TeacherContactInfo);
            if (teacher == null)
            {
                return NotFound();
            }

            var viewModel = new TeacherUpdateViewModel
            {
                Name = teacher.Name,
                Description = teacher.Description,
                Degree = teacher.degree,
                Experience = teacher.experience,
                Faculty = teacher.faculty,
                Position = teacher.Position,
                Language = teacher.Language,
                Hobbies = teacher.Hobbies,
                TeamLeader = teacher.TeamLeader,
                Development = teacher.Development,
                Design = teacher.Design,
                Innovation = teacher.Innovation,
                Communication = teacher.Communication,
                ContactInfo = new TeacherContactInfoViewModel
                {
                    EmailAddress = teacher.TeacherContactInfo.EmailAddress,
                    PhoneNumber = teacher.TeacherContactInfo.PhoneNumber,
                    FacebookUrl = teacher.TeacherContactInfo.FaceBookUrl,
                    PinterestUrl = teacher.TeacherContactInfo.pinterestUrl,
                    SkypeUrl = teacher.TeacherContactInfo.SkypeUrl,
                    InstagramUrl = teacher.TeacherContactInfo.IntaUrl
                }
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, TeacherUpdateViewModel viewModel)
        {
            if (id == null) return BadRequest();
            var teacher = await teacherService.GetTeacherByIdAsync(id, s => s.TeacherContactInfo);
            if (teacher == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View(viewModel);

            var newProfileImage = viewModel.Photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed.");
                    return View(viewModel);
                }
                if (!newProfileImage.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "The image size is too large. Maximum allowed size is 500KB.");
                    return View(viewModel);
                }
                teacher.ImageUrl?.DeleteFile("teacher");
                teacher.ImageUrl = await newProfileImage.SaveFile("teacher");
            }

            teacher.Name = viewModel.Name;
            teacher.Description = viewModel.Description;
            teacher.degree = viewModel.Degree;
            teacher.experience = viewModel.Experience;
            teacher.faculty = viewModel.Faculty;
            teacher.Position = viewModel.Position;
            teacher.Hobbies = viewModel.Hobbies;
            teacher.Language = viewModel.Language;
            teacher.TeamLeader = viewModel.TeamLeader;
            teacher.Development = viewModel.Development;
            teacher.Design = viewModel.Design;
            teacher.Innovation = viewModel.Innovation;
            teacher.Communication = viewModel.Communication;

            if (teacher.TeacherContactInfo == null)
            {
                teacher.TeacherContactInfo = new TeacherContactInfo
                {
                    EmailAddress = viewModel.ContactInfo.EmailAddress,
                    PhoneNumber = viewModel.ContactInfo.PhoneNumber,
                    FaceBookUrl = viewModel.ContactInfo.FacebookUrl,
                    pinterestUrl = viewModel.ContactInfo.PinterestUrl,
                    SkypeUrl = viewModel.ContactInfo.SkypeUrl,
                    IntaUrl = viewModel.ContactInfo.InstagramUrl
                };
            }
            else
            {
                teacher.TeacherContactInfo.EmailAddress = viewModel.ContactInfo.EmailAddress;
                teacher.TeacherContactInfo.PhoneNumber = viewModel.ContactInfo.PhoneNumber;
                teacher.TeacherContactInfo.FaceBookUrl = viewModel.ContactInfo.FacebookUrl;
                teacher.TeacherContactInfo.pinterestUrl = viewModel.ContactInfo.PinterestUrl;
                teacher.TeacherContactInfo.SkypeUrl = viewModel.ContactInfo.SkypeUrl;
                teacher.TeacherContactInfo.IntaUrl = viewModel.ContactInfo.InstagramUrl;
            }

            await teacherService.UpdateTeacherAsync(teacher);
            return RedirectToAction(nameof(Index));
        }

    }
}
