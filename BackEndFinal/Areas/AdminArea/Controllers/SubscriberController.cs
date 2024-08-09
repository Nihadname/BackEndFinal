using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            this.subscriberService = subscriberService;
        }

        public async Task<IActionResult> Index()
        {
            var allSubs = await subscriberService.GetAlSubscriberAsync(0, 0);
            return View(allSubs);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)  return BadRequest();
            var existedSub=await subscriberService.GetSubscriberByIdAsync(id);
            if(existedSub is null) return NotFound();
            await subscriberService.DeleteSubscriberAsync(existedSub);
            return RedirectToAction("Index");
        }
    }
}
