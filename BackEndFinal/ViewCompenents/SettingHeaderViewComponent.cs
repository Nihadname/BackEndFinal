using BackEndFinal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class SettingHeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public SettingHeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}
