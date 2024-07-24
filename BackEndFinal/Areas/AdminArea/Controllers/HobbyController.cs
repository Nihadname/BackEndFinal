using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class HobbyController : Controller
    {
        private readonly IHobbyService _hobbyService;

        public HobbyController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var hobbies =  _hobbyService.GetAllHobbyQuery();
            if (!string.IsNullOrEmpty(searchText))
            {
                hobbies = hobbies.Where(s => s.Name.ToLower().Contains(searchText.ToLower()) ||
                                         s.Description.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = await hobbies.ToListAsync();
            return View(usersForActualForm);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existedHobby =await _hobbyService.GetAllHobbyQuery().Include(s=>s.TeacherHobbies)
                .ThenInclude(s=>s.Teacher)
                .FirstOrDefaultAsync(h => h.Id == id);
            if(existedHobby == null) return NotFound();
            return View(existedHobby);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string HobbyName, string HobbyDesc)
        {
            if (string.IsNullOrEmpty(HobbyName) || string.IsNullOrEmpty(HobbyDesc))
            {
                ModelState.AddModelError(string.Empty, "Hobby Name and Description cannot be empty.");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existingHobby = await _hobbyService.GetAllHobbyQuery().AnyAsync(h => h.Name == HobbyName);
            if (existingHobby)
            {
                ModelState.AddModelError(string.Empty, "A hobby with the same name already exists.");
                return View();
            }

            Hobby hobby = new Hobby
            {
                Name = HobbyName,
                Description = HobbyDesc
            };

            try
            {
                await _hobbyService.AddHobbyAsync(hobby);
                TempData["SuccessMessage"] = "Hobby created successfully!";
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
            if( id == null ) return BadRequest();
            var existedHobby=await _hobbyService.GetHobbyByIdAsync(id);
            if( existedHobby == null ) return NotFound();
            try
            {
                await _hobbyService.DeleteHobbyAsync(existedHobby);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the hobby: " + ex.Message);
                return View();
            }
        }
         

    }
}
