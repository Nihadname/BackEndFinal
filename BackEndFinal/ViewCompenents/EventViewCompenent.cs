using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class EventViewComponent : ViewComponent
    {
        private readonly IEventService _eventService;

        public EventViewComponent(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take = 3)
        {
            var events =await _eventService.GetAllEventAsync(0, take, s => s.Category, s => s.Speakers, s => s.Images);
            return View(await Task.FromResult(events));
        }
    }
}
