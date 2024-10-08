﻿using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ISubscriberService subscriberService;
        private readonly ITeacherService teacherService;
        private readonly ICategoryService categoryService;
        private readonly IEmailService emailService;
        private readonly AppDbContext appDbContext;

        public CourseController(ICourseService courseService, ICategoryService categoryService, AppDbContext appDbContext, ITeacherService teacherService, IEmailService emailService, ISubscriberService subscriberService)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.appDbContext = appDbContext;
            this.teacherService = teacherService;
            this.emailService = emailService;
            this.subscriberService = subscriberService;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var courses = courseService.GetAllCourseQuery();
            if (!string.IsNullOrEmpty(searchText))
            {
                courses = courses.Where(s => s.Title.ToLower().Contains(searchText.ToLower()) ||
                                             s.Description.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = await courses.Include(s => s.Category).ToListAsync();
            return View(usersForActualForm);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            var existedTeacher = await courseService.GetAllCourseQuery().Include(s => s.courseImages).Include(s => s.courseTeachers)
                .ThenInclude(s => s.Teacher).Include(s => s.courseTags).ThenInclude(s => s.Tag)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (existedTeacher == null) return BadRequest();
            return View(existedTeacher);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            ViewBag.Teachers = new SelectList(await teacherService.GetAllTeacherAsync(0, 0), "Id", "Name");
            ViewBag.Tags = new SelectList(await appDbContext.tags.AsNoTracking().ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM courseCreateVM)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            ViewBag.Teachers = new SelectList(await teacherService.GetAllTeacherAsync(0, 0), "Id", "Name");
            ViewBag.Tags = new SelectList(await appDbContext.tags.AsNoTracking().ToListAsync(), "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View(courseCreateVM);
            }

            var files = courseCreateVM.Photos;
            if (files.Length == 0)
            {
                ModelState.AddModelError("Photos", "Images cannot be null");
                return View(courseCreateVM);
            }

            Course newCourse = new Course();
            List<CourseImage> images = new List<CourseImage>();

            foreach (var newProfileImage in files)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photos", "Only image files are allowed.");
                    return View(courseCreateVM);
                }
                if (!newProfileImage.CheckSize(10000))
                {
                    ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                    return View(courseCreateVM);
                }

                CourseImage newImage = new CourseImage
                {
                    Name = await newProfileImage.SaveFile("course"),
                    CourseId = newCourse.Id
                };

                if (files[0] == newProfileImage)
                {
                    newImage.IsMain = true;
                }

                images.Add(newImage);
            }

            newCourse.courseImages = images;
            newCourse.CategoryId = courseCreateVM.CategoryId;
            newCourse.Title = courseCreateVM.Title;
            newCourse.Description = courseCreateVM.Description;
            newCourse.Starts = courseCreateVM.Starts;
            newCourse.AboutCourse = courseCreateVM.AboutCourse;
            newCourse.HowToApply = courseCreateVM.HowToApply;
            newCourse.Assessments = courseCreateVM.Assessments;
            newCourse.SkillLevel = courseCreateVM.SkillLevel;
            newCourse.CERTIFICATION = courseCreateVM.CERTIFICATION;
            newCourse.ClassDuration = courseCreateVM.ClassDuration;
            newCourse.Language = courseCreateVM.Language;
            newCourse.Price = courseCreateVM.Price;
            newCourse.Students = courseCreateVM.Students;
            newCourse.Duration = courseCreateVM.Duration;
            newCourse.courseTeachers = courseCreateVM.TeacherIds.Select(tid => new CourseTeacher
            {
                TeacherId = tid
            }).ToList();
            newCourse.courseTags = courseCreateVM.TagIds.Select(tid => new CourseTag
            {
                TagId = tid
            }).ToList();

            await courseService.AddCourseAsync(newCourse);

            var allSubs = await subscriberService.GetAlSubscriberAsync(0, 0);
            var emailRecipients = allSubs.Select(sub => sub.EmailAddress).ToList();

            await emailService.SendEmailAsyncToManyPeople(
               from: "nihadmi@code.edu.az",
               recipients: emailRecipients,
               subject: "Verify Email",
               body: $"http://localhost:5016/Course/Detail/{newCourse.Id}",
               smtpHost: "smtp.gmail.com",
               smtpPort: 587,
               enableSsl: true,
               smtpUser: "nihadmi@code.edu.az",
               smtpPass: "ilyo ibry uphi gnfe"
            );

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SetMainPhoto(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.courseImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();

            var mainImage = await appDbContext.courseImages
                .FirstOrDefaultAsync(y => y.IsMain == true && y.CourseId == existedPhoto.CourseId);
            if (mainImage != null) mainImage.IsMain = false;

            existedPhoto.IsMain = true;
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = existedPhoto.CourseId });
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.courseImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            existedPhoto.Name.DeleteFile("course");
            appDbContext.courseImages.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Update", new { id = existedPhoto.CourseId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await courseService.GetCourseByIdAsync(id, s => s.courseImages);
            if (existedPhoto == null) return NotFound();
            foreach (var image in existedPhoto.courseImages)
            {
                image.Name.DeleteFile("course");
            }
            appDbContext.courses.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            ViewBag.Teachers = new SelectList(await teacherService.GetAllTeacherAsync(0, 0), "Id", "Name");
            ViewBag.Tags = new SelectList(await appDbContext.tags.AsNoTracking().ToListAsync(), "Id", "Name");
            if (id == null) return BadRequest();
            var existedCourse = await courseService.GetCourseByIdAsync(id, s => s.courseImages, s => s.Category);
            if (existedCourse == null) return NotFound();
            return View(new CourseUpdateVM
            {
                CategoryId = existedCourse.CategoryId,
                Title = existedCourse.Title,
                Description = existedCourse.Description,
                Starts = existedCourse.Starts,
                AboutCourse = existedCourse.AboutCourse,
                HowToApply = existedCourse.HowToApply,
                Assessments = existedCourse.Assessments,
                SkillLevel = existedCourse.SkillLevel,
                CERTIFICATION = existedCourse.CERTIFICATION,
                ClassDuration = existedCourse.ClassDuration,
                Language = existedCourse.Language,
                courseImages = existedCourse.courseImages,
                Price = existedCourse.Price,
                Students = existedCourse.Students,
                Duration = existedCourse.Duration,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, CourseUpdateVM vm)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            ViewBag.Teachers = new SelectList(await teacherService.GetAllTeacherAsync(0, 0), "Id", "Name");
            ViewBag.Tags = new SelectList(await appDbContext.tags.AsNoTracking().ToListAsync(), "Id", "Name");
            if (id == null) return BadRequest();
            var existedCourse = await courseService.GetCourseByIdAsync(id, s => s.courseImages);
            if (existedCourse == null) return NotFound();
            if (!ModelState.IsValid)
            {
                vm.courseImages = existedCourse.courseImages;
                return View(vm);
            }

            List<CourseImage> list = new();
            var files = vm.Photos;
            if (files != null)
            {
                if (files.Length > 4)
                {
                    vm.courseImages = existedCourse.courseImages;
                    ModelState.AddModelError("Photos", "Minimum 4 Photos!");
                    return View(vm);
                }

                foreach (var file in files)
                {
                    if (!file.CheckContentType())
                    {
                        ModelState.AddModelError("Photos", "Choose right type!");
                        return View(vm);
                    }

                    var courseImage = new CourseImage
                    {
                        Name = await file.SaveFile("course"),
                        CourseId = existedCourse.Id,
                        IsMain = false
                    };

                    list.Add(courseImage);
                }

                existedCourse.courseImages = list;
            }

            existedCourse.CategoryId = vm.CategoryId;
            existedCourse.Title = vm.Title;
            existedCourse.Description = vm.Description;
            existedCourse.Starts = vm.Starts;
            existedCourse.AboutCourse = vm.AboutCourse;
            existedCourse.HowToApply = vm.HowToApply;
            existedCourse.Assessments = vm.Assessments;
            existedCourse.SkillLevel = vm.SkillLevel;
            existedCourse.CERTIFICATION = vm.CERTIFICATION;
            existedCourse.ClassDuration = vm.ClassDuration;
            existedCourse.Language = vm.Language;
            existedCourse.Price = vm.Price;
            existedCourse.Students = vm.Students;
            existedCourse.Duration = vm.Duration;

            appDbContext.coursesTeachers.RemoveRange(appDbContext.coursesTeachers.Where(ct => ct.CourseId == existedCourse.Id));
            appDbContext.courseTags.RemoveRange(appDbContext.courseTags.Where(ct => ct.CourseId == existedCourse.Id));

            existedCourse.courseTeachers = vm.TeacherIds.Select(tid => new CourseTeacher
            {
                CourseId = existedCourse.Id,
                TeacherId = tid
            }).ToList();

            existedCourse.courseTags = vm.TagIds.Select(tid => new CourseTag
            {
                CourseId = existedCourse.Id,
                TagId = tid
            }).ToList();

            await courseService.UpdateCourseAsync(existedCourse);
            return RedirectToAction("Index");
        }
    }
}
