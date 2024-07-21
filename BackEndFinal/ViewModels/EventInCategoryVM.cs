using BackEndFinal.Models;

namespace BackEndFinal.ViewModels
{
    public class EventInCategoryVM
    {
        public Category Category { get; set; }
        public Task<PaginationVM<Event>> PaginationEvents { get; set; }
    }
}
