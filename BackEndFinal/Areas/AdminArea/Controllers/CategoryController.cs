using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index(string searchText)
        {
            var categories = categoryService.GetAllCategoryQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                categories = categories.Where(s => s.Name.ToLower().Contains(searchText.ToLower()) ||
                                         s.Description.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = categories.ToList();
            return View(usersForActualForm);

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if(id == null) return BadRequest();
            var existedCategory = await categoryService.GetCategoryByIdAsync(id, s => s.blogs, s => s.events, s => s.Courses);
            if(existedCategory == null) return NotFound();
            return View(existedCategory);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string CategoryName, string CategoryDesc)
        {
            if (string.IsNullOrEmpty(CategoryName) || string.IsNullOrEmpty(CategoryDesc))
            {
                ModelState.AddModelError(string.Empty, "Hobby Name and Description cannot be empty.");
                return View();
            }
            if (!ModelState.IsValid) return View();
            var existingHobby = await categoryService.GetAllCategoryQuery().AnyAsync(h => h.Name == CategoryName);
            if (existingHobby)
            {
                ModelState.AddModelError(string.Empty, "A category with the same name already exists.");
                return View();
            }

            Category category =new Category();
            category.Name = CategoryName;
            category.Description = CategoryDesc;
            try
            {
                await categoryService.AddCategoryAsync(category);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the hobby: " + ex.Message);
                return View();

            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedCategory=await categoryService.GetCategoryByIdAsync(id);
            if (existedCategory == null) return NotFound();
            try
            {
                await categoryService.DeleteCategoryAsync(existedCategory);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the Category: " + ex.Message);
                return View();
            }
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedCategory = await categoryService.GetCategoryByIdAsync(id);
            if(existedCategory == null) return NotFound();  
            return View(new CategoryUpdateVM() { Name=existedCategory.Name,Description=existedCategory.Description});
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM category)
        {
            if(id == null) return BadRequest();
            if(!ModelState.IsValid) return View(category);
            var existedCategory=await categoryService.GetCategoryByIdAsync(id);
            if( existedCategory == null) return NotFound();
            var existingCategory = await categoryService.GetAllCategoryQuery().AnyAsync(h => h.Name == category.Name);
            if (existingCategory)
            {
                ModelState.AddModelError("Name", "A category with the same name already exists.");
                return View(category);
            }
            existedCategory.Name = category.Name;
            existedCategory.Description = category.Description;
            try
            {
                await categoryService.UpdateCategoryAsync(existedCategory);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the Category: " + ex.Message);
                return View();

            }

        }
     }
}
