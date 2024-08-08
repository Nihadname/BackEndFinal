using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService categoryService;
        private readonly AppDbContext appDbContext;

        public BlogController(IBlogService blogService, ICategoryService categoryService, AppDbContext appDbContext)
        {
            _blogService = blogService;
            this.categoryService = categoryService;
            this.appDbContext = appDbContext;
        }

        public IActionResult Index(string searchText)
        {
            var blogs = _blogService.GetAllBlogQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                blogs = blogs.Where(s => s.Title.ToLower().Contains(searchText.ToLower()) ||
                                         s.Content.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm =  blogs.Include(s=>s.Category).Include(s=>s.Images).ToList();
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
                ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");

                ModelState.AddModelError("Photos", "Oimages can not bu null");
                return View(blogCreateVM);
            }
            Blog Newblog = new Blog();

            List<BlogImage> images = new List<BlogImage>();
            foreach (var newProfileImage in files)
            {
                
                    if (!newProfileImage.CheckContentType())
                    {
                    ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");

                    ModelState.AddModelError("Photos", "Only image files are allowed.");
                        return View(blogCreateVM);
                    }
                    if (!newProfileImage.CheckSize(10000))
                    {
                    ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");

                    ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                        return View(blogCreateVM);
                    }
                    BlogImage newImage = new BlogImage();
                newImage.imageUrl = await newProfileImage.SaveFile("blog");
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
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");

            Newblog.Images = images;
            Newblog.CategoryId = blogCreateVM.CategoryId;
            Newblog.Title = blogCreateVM.Title;
            Newblog.Content = blogCreateVM.Content;
            Newblog.Writer = blogCreateVM.Writer;
            Newblog.quote=blogCreateVM.quote;
            await _blogService.AddBlogAsync(Newblog);  
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedBlog=await _blogService.GetBlogByIdAsync(id,s=>s.Images,s=>s.Category);
            if(existedBlog == null) return NotFound();
            return View(existedBlog);
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            if (id == null) return BadRequest();
            var existedBlog = await _blogService.GetBlogByIdAsync(id, s => s.Images, s => s.Category);
            if (existedBlog == null) return NotFound();
            return View(new BlogUpdateVM
            {
                Title = existedBlog.Title,
                Content = existedBlog.Content,
                Writer = existedBlog.Writer,
                Quote = existedBlog.quote, 
                blogImages = existedBlog.Images,
                CategoryId = existedBlog.CategoryId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM blogUpdateVM)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            if (id == null) return BadRequest();

            var existedBlog = await _blogService.GetBlogByIdAsync(id, s => s.Images, s => s.Category);
            if (existedBlog == null) return NotFound();

            if (!ModelState.IsValid)
            {
                blogUpdateVM.blogImages = existedBlog.Images;
                return View(blogUpdateVM);
            }

            var files = blogUpdateVM.Photos;
            if (files != null)
            {
                if (files.Length > 4)
                {
                    blogUpdateVM.blogImages = existedBlog.Images;
                    ModelState.AddModelError("Photos", "Maximum 4 Photos!");
                    return View(blogUpdateVM);
                }

                List<BlogImage> newImages = new();
                foreach (var file in files)
                {
                    if (!file.CheckContentType())
                    {
                        ModelState.AddModelError("Photos", "Choose the right type!");
                        return View(blogUpdateVM);
                    }
                    foreach (var oldImage in existedBlog.Images)
                    {
                        oldImage.imageUrl.DeleteFile("blog");
                        appDbContext.blogImages.Remove(oldImage);

                    }

                    var blogImage = new BlogImage
                    {
                        imageUrl = await file.SaveFile("blog"),
                        BlogId = existedBlog.Id,
                        IsMain = newImages.Count == 0
                    };
                    newImages.Add(blogImage);
                }

               
                existedBlog.Images = newImages;
            }

            existedBlog.Title = blogUpdateVM.Title;
            existedBlog.Content = blogUpdateVM.Content;
            existedBlog.Writer = blogUpdateVM.Writer;
            existedBlog.quote = blogUpdateVM.Quote;
            existedBlog.CategoryId = blogUpdateVM.CategoryId;

            appDbContext.blogs.Update(existedBlog);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetMainPhoto(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.blogImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();

            var mainImage = await appDbContext.blogImages
                .FirstOrDefaultAsync(y => y.IsMain == true && y.BlogId == existedPhoto.BlogId);
            if (mainImage != null) mainImage.IsMain = false;

            existedPhoto.IsMain = true;
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = existedPhoto.BlogId });
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.blogImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            existedPhoto.imageUrl.DeleteFile("blog");
            appDbContext.blogImages.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Update", new { id = existedPhoto.BlogId });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            foreach (var image in existedPhoto.Images)
            {
                image.imageUrl.DeleteFile("blog");

            }
            appDbContext.blogs.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
