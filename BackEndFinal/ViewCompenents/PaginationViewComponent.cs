using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEndFinal.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IPaginationVM paginationModel, string actionName, int categoryId)
        {
            ViewBag.ActionName = actionName;
            ViewBag.CategoryId = categoryId;
            return View(paginationModel);
        }
    }
}
