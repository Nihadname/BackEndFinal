using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private  readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async  Task<IActionResult> Index()
        {
            var allComments= await commentService.GetAllCommentQuery().Include(s=>s.AppUser).ToListAsync();
            return View(allComments);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedComment=await commentService.GetCommentByIdAsync(id);
            if(existedComment == null)  return NotFound();

            await commentService.DeleteCommentAsync(existedComment);
            return RedirectToAction("Index");
        }
    }
}
