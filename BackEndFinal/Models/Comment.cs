using BackEndFinal.Models.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BackEndFinal.Models
{
    public class Comment:BaseEntity
    {
        [Required]

        public string Content { get; set; }
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
