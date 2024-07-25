using BackEndFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.ViewModels
{
    public class BlogUpdateVM
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(300)]
        public string Content { get; set; }
        [Required, MaxLength(100)]
        public string Writer { get; set; }
        [Required, MaxLength(200)]
        public string Quote { get; set; }
        public int CategoryId { get; set; }
        public List<BlogImage>? blogImages { get; set; }
        public IFormFile[]? Photos { get; set; }
    }
}
