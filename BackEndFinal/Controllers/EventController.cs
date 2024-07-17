using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async  Task<IActionResult> Index()
        {
         
            var events =await  _eventService.GetAllEventAsync(0, 0,  s => s.Images);
            ViewBag.EventCount = events.Count();
            return View(events);
        }
        public async Task<IActionResult> Loadmore(int skip=3)
        {
            var datas = await _eventService.GetAllEventAsync(skip, 3,  s => s.Images);
            return PartialView("_EventPartialView", datas);


        }
    }
}
