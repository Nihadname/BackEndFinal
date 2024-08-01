using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (!string.IsNullOrEmpty(identityRole.Name))
            {
                if (!await _roleManager.RoleExistsAsync(identityRole.Name))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = identityRole.Name });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "this already exists");

                    return View(identityRole);
                }

            }
             ModelState.AddModelError("Name", "this can not be empty");
            return View(identityRole);

        }
        public async Task<IActionResult> Detail(string id)
        {
            if(id is null) return BadRequest();
            var currentRole=await _roleManager.FindByIdAsync(id);
            if (currentRole == null) return NotFound();
            var usersInRole = await _userManager.GetUsersInRoleAsync(currentRole.Name);

            RoleDetailVM roleDetailVM =new RoleDetailVM();
            roleDetailVM.Role=currentRole;
            roleDetailVM.Users = usersInRole;
            return View(roleDetailVM);


        }
        
        public async Task<IActionResult> Delete(string id)
        {
            if(id is null) return BadRequest();
            var role=await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateUserRole(string id)
        {
            if (id is null) return BadRequest();
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null) return NotFound();
            var AllRoles= await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var roleUpdateVm = new RoleUpdateVM(userRoles, currentUser.UserName, AllRoles);
            return View(roleUpdateVm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string id,List<string> NewRoles)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, NewRoles);
            return RedirectToAction("Index", "User");

        }
        public async Task<IActionResult> Update(string id)
        {
            if (id is null) return BadRequest();
            var existedRole = await _roleManager.FindByIdAsync(id);
            if (existedRole == null) return NotFound();
            return View(existedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, IdentityRole identityRole)
        {
            if (string.IsNullOrEmpty(identityRole.Name)) return View(identityRole);
            if (id is null) return BadRequest();
            var existedRole = await _roleManager.FindByIdAsync(id);
            if (existedRole == null) return NotFound();

            existedRole.Name = identityRole.Name;
            IdentityResult result = await _roleManager.UpdateAsync(existedRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Name", error.Description);
            }
            return View(existedRole); 
        }

    }
}
