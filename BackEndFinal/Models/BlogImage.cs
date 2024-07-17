using BackEndFinal.Models.Common;
using System.ComponentModel;

namespace BackEndFinal.Models
{
    public class BlogImage:BaseEntity
    {
        public string imageUrl  { get; set; }
        public int BlogId { get; set; } 
        public Blog Blog    { get; set; }
    }
}
