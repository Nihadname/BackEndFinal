using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService categoryService;

        public BlogController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            this.categoryService = categoryService;
        }

        public IActionResult Index(string searchText)
        {
            var blogs = _blogService.GetAllBlogQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                blogs = blogs.Where(s => s.Title.ToLower().Contains(searchText.ToLower()) ||
                                         s.Content.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm =  blogs.Include(s=>s.Category).ToList();
            return View(usersForActualForm);

        }
        public async   Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0),"Id","Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create( BlogCreateVM blogCreateVM)
        {
            
             ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            if(!ModelState.IsValid) return View(blogCreateVM);
            var files = blogCreateVM.Photos;
            if (files.Length == 0)
            {
                ModelState.AddModelError("Photos", "Oimages can not bu null");
                return View(blogCreateVM);
            }
            Blog Newblog = new Blog();

            List<BlogImage> images = new List<BlogImage>();
            foreach (var newProfileImage in files)
            {
                
                    if (!newProfileImage.CheckContentType())
                    {
                        ModelState.AddModelError("Photos", "Only image files are allowed.");
                        return View(blogCreateVM);
                    }
                    if (!newProfileImage.CheckSize(10000))
                    {
                        ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                        return View(blogCreateVM);
                    }
                    BlogImage newImage = new BlogImage();
                newImage.imageUrl = await newProfileImage.SaveFile();
                newImage.BlogId = Newblog.Id;
                if (files[0] == newProfileImage)
                {
                    newImage.IsMain = true;
                }
                images.Add(newImage);   


                    // Save the new image file
                    //existingUser.imageUrl = await newProfileImage.SaveFile();
                    //var result = await _userManager.UpdateAsync(existingUser);
                    //if (!result.Succeeded)
                    //{
                    //    ModelState.AddModelError("", "Failed to update user profile.");
                    //    return View(blogCreateVM);
                    //    ;
                    //}

            }
            Newblog.Images = images;
            Newblog.CategoryId = blogCreateVM.CategoryId;
            Newblog.Title = blogCreateVM.Title;
            Newblog.Content = blogCreateVM.Content;
            Newblog.Writer = blogCreateVM.Writer;
            Newblog.quote=blogCreateVM.quote;
            await _blogService.AddBlogAsync(Newblog);  
            return RedirectToAction("Index");
        }
    }
}
