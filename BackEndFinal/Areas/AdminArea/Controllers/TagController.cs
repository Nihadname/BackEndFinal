using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
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

        public async Task<IActionResult> Create(TagVIewModel tagVIewModel)
        {
            if(!ModelState.IsValid) return View(tagVIewModel);
            Tag tag = new Tag();
            tag.Name = tagVIewModel.Name;
            await _tagService.AddTagAsync(tag);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedTag = await _tagService.GetAllTagQuery().FirstOrDefaultAsync(s => s.Id == id);
            if (existedTag == null) return NotFound();
            return View(new TagVIewModel { Name=existedTag.Name});
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, TagVIewModel tagVIewModel)
        {
            if (!ModelState.IsValid) return View(tagVIewModel);

            if (id == null) return BadRequest();
            var existedTag = await _tagService.GetAllTagQuery().FirstOrDefaultAsync(s => s.Id == id);
            if (existedTag == null) return NotFound();
            existedTag.Name = tagVIewModel.Name;
           await _tagService.UpdateTagAsync(existedTag);
            return RedirectToAction(nameof(Index));

        }


    }
}
