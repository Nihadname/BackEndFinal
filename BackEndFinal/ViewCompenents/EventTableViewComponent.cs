using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.ViewCompenents
{
    public class EventTableViewComponent:ViewComponent
    {
        private readonly IEventService _eventService;
       public EventTableViewComponent(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events =await _eventService.GetAllEventAsync(0, 8);
            return View(Task.FromResult(events));
        }
    }
}
