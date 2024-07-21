using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string ItemType,string actionType, string controllerType)
        {
            var categories =await categoryService.GetAllCategoryAsync(0, 6,s=>s.Courses,s=>s.blogs,s=>s.events);
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.Categories=categories;
            viewModel.ActionType = actionType;
            viewModel.ControllerType = controllerType;
            viewModel.itemType = ItemType;
            
            return View(viewModel);
        }
    }
}
