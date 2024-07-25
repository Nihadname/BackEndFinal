using BackEndFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class BlogCreateVM
    {
        [Required,MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(300)]
        public string Content { get; set; }
        [Required, MaxLength(100)]
        public string Writer { get; set; }
        [Required, MaxLength(200)]
        public string quote { get; set; }
        public int CategoryId { get; set; }
        public IFormFile[] Photos { get; set; }
    }
}
