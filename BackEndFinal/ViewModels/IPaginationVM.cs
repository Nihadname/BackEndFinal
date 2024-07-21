namespace BackEndFinal.ViewModels
{
    public interface IPaginationVM
    {
        int CurrentPage { get; }
        int TotalPage { get; }
        bool HasNext { get; }
        bool HasPrev { get; }
    }
}
