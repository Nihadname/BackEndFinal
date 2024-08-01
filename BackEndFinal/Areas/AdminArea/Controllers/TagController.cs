using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]

    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var allTags = await _tagService.GetAllTagAsync(0, 0);
            return View(allTags);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedTag = await _tagService.GetAllTagQuery().Include(s => s.courseTags).ThenInclude(s => s.Course).FirstOrDefaultAsync(s => s.Id == id);
            if (existedTag == null) return NotFound();
            return View(existedTag);
        }
        public  IActionResult Create(){
            return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(string TagName)
        {
            if (string.IsNullOrEmpty(TagName))  return BadRequest();
            Tag tag = new();
            tag.Name = TagName;
            await _tagService.AddTagAsync(tag);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedTag = await _tagService.GetAllTagQuery().FirstOrDefaultAsync(s => s.Id == id);
            if (existedTag == null) return NotFound();
            return View(existedTag);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,string NewTag)
        {
            if (!ModelState.IsValid) return BadRequest("it can not be recieving empty value");

            if (id == null) return BadRequest();
            var existedTag = await _tagService.GetAllTagQuery().FirstOrDefaultAsync(s => s.Id == id);
            if (existedTag == null) return NotFound();
            existedTag.Name = NewTag;
           await _tagService.UpdateTagAsync(existedTag);
            return RedirectToAction(nameof(Index));

        }


    }
}
