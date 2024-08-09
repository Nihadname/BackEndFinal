using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackEndFinal.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly StripeSettings _stripeSettings;

        public BasketController(AppDbContext appDbContext, UserManager<AppUser> userManager, IOptions<StripeSettings> stripeOptions)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _stripeSettings = stripeOptions.Value;

        }

        public async  Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            AppUser existedUser = await _userManager.GetUserAsync(User);
            var existedBasket =await _appDbContext.baskets
                .Include(s=>s.BaskerProducts)
                .ThenInclude(s=>s.Course)
                .ThenInclude(s=>s.courseImages)
                .FirstOrDefaultAsync(m=>m.AppUserId==existedUser.Id );
            List<BasketListVM> listVMs = new List<BasketListVM>();

            if(existedBasket is not null)
            {
                foreach(var item in existedBasket.BaskerProducts)
                {
                    listVMs.Add(new BasketListVM()
                    {
                        CourseId = item.Id,
                        Image = item.Course.courseImages.FirstOrDefault(s => s.IsMain is true)?.Name,
                        Title = item.Course.Title,
                        Price = item.Course.Price,
                        TotalPrice = item.Course.Price * item.Quantity,
                        Quantity=item.Quantity,
                      
                    });
                }
            }
            ViewBag.PublishableKey = _stripeSettings.PublishableKey;

            return View(listVMs);
        }
        [HttpPost]
        public async Task<IActionResult> Add(int? CourseId)
        {
            if (CourseId == null) return BadRequest();
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "User is not authenticated", redirectUrl = Url.Action("Login", "Account") });

            }
            AppUser existedUser =await _userManager.GetUserAsync(User);
            Course existedCourse = await _appDbContext.courses.FirstOrDefaultAsync(s => s.Id == CourseId);
            if (existedCourse == null) return NotFound();
            Basket existedBasket=await _appDbContext.baskets.Include(s=>s.BaskerProducts).FirstOrDefaultAsync(s=>s.AppUserId==existedUser.Id);
            BaskerCourse  BasketCourse=await _appDbContext.baskerCourses.FirstOrDefaultAsync(s=>s.CourseId==CourseId&&s.Basket.AppUserId==existedUser.Id);
            if (BasketCourse != null)
            {
                BasketCourse.Quantity++;
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Course added to basket" });
            }
            if (existedBasket != null)
            {
                existedBasket.BaskerProducts.Add(new BaskerCourse()
                {
                    Quantity = 1,
                   BasketId = existedBasket.Id,
                    CourseId = (int)CourseId,

                });
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Course added to basket" });
            }

            Basket Newbasket = new Basket()
            {
                AppUserId = existedUser.Id
            };
            await _appDbContext.baskets.AddAsync(Newbasket);
            await _appDbContext.SaveChangesAsync();

            BaskerCourse baskerCourse = new BaskerCourse();
            baskerCourse.Quantity = 1;
                baskerCourse.BasketId= Newbasket.Id;
            baskerCourse.CourseId = (int)CourseId;
            await _appDbContext.baskerCourses.AddAsync(baskerCourse);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true, message = "Course added to basket" });


        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            AppUser existedUser = await _userManager.GetUserAsync(User);
            if (existedUser == null) return Unauthorized();
            
            BaskerCourse BasketCourse = await _appDbContext.baskerCourses.Include(s => s.Basket).FirstOrDefaultAsync(s => s.Id == id && s.Basket.AppUserId == existedUser.Id);
            if (BasketCourse == null) return NotFound();
            _appDbContext.baskerCourses.Remove(BasketCourse);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true, message = "Course deleted from basket" });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int? id,int amount)
        {
            if (id == null) return BadRequest();
            AppUser existedUser = await _userManager.GetUserAsync(User);
            if (existedUser == null) return Unauthorized();
            Basket existedBasket = await _appDbContext.baskets
    .Include(s => s.BaskerProducts)
    .ThenInclude(bp => bp.Course)
    .FirstOrDefaultAsync(b => b.AppUserId == existedUser.Id);
            BaskerCourse BasketCourse = await _appDbContext.baskerCourses.Include(s => s.Basket).FirstOrDefaultAsync(s => s.Id == id && s.Basket.AppUserId == existedUser.Id);
            if (BasketCourse == null) return NotFound();
            BasketCourse.Quantity += amount;
            if (BasketCourse.Quantity < 1)
            {
                BasketCourse.Quantity = 1;
            }
            await  _appDbContext.SaveChangesAsync();
            var totalPrice = existedBasket.BaskerProducts.Sum(bp => bp.Course.Price * bp.Quantity);

            return Json(new { success = true, totalPrice = totalPrice });
        }
    }
}
