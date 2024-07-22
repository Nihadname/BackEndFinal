using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Register()
        //{

        //}
    }
}
