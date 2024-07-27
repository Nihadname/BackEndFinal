using BackEndFinal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RequestedBuyingsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RequestedBuyingsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var allOfThem = _appDbContext.CourseRequests.AsNoTracking().ToList();
            return View(allOfThem);
        }

        [HttpGet]
        public IActionResult SetAppointmentTime(int? Id)
        {
            if (Id == null) return BadRequest("Request ID cannot be null.");
            var courseRequest = _appDbContext.CourseRequests.FirstOrDefault(x => x.Id == Id);
            if (courseRequest == null)
            {
                return NotFound("Course request not found.");
            }

            return View(courseRequest);
        }

        [HttpPost]
        public async Task<IActionResult> SetAppointmentTime(int? Id, DateTime appointmentTime)
        {
            if(Id is null) return BadRequest();
            var courseRequest = await _appDbContext.CourseRequests.FirstOrDefaultAsync(s=>s.Id==Id);
            if (courseRequest == null)
            {
                return NotFound("Course request not found.");
            }

            courseRequest.AppointmentTime = appointmentTime;
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            var courseRequest = await _appDbContext.CourseRequests.FindAsync(requestId);
            if (courseRequest == null)
            {
                return NotFound();
            }

            courseRequest.IsApproved = true;
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
