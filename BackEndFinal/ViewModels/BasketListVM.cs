namespace BackEndFinal.ViewModels
{
    public class BasketListVM
    {
        public int CourseId { get; set; }
        public string Image {  get; set; }
        public string Title { get; set; } 
        public  int Price   { get; set; }
        public  int TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
