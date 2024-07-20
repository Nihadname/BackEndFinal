using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class SubscriberViewComponent : ViewComponent
{
    private readonly ISubscriberService _subscriberService;

    public SubscriberViewComponent(ISubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // If you need to pass a specific model to the view, do it here
        var subscriberVM = new SubscriberVM();
        return View(subscriberVM);
    }
}
