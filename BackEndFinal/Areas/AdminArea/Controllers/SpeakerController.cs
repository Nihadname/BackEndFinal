using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SpeakerController : Controller
    {
        private readonly ISpeakerService speakerService;

        public SpeakerController(ISpeakerService speakerService)
        {
            this.speakerService = speakerService;
        }

        public IActionResult Index(string searchText)
        {
            var events = speakerService.GetAllSpeakerQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                events = events.Where(s => s.Name.ToLower().Contains(searchText.ToLower()) ||
                                         s.position.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = events.ToList();
            return View(usersForActualForm);

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedSpeaker=await speakerService.GetSpeakerByIdAsync(id,s=>s.Event);
            if(existedSpeaker == null) return NotFound();
            return View(existedSpeaker);
        }
    }
}
