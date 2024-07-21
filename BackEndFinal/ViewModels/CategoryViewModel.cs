using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public string ActionType { get; set; }
        public string ControllerType { get; set; }
       public  string itemType { get; set; }
    }
}
