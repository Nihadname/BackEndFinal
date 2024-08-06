using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

namespace BackEndFinal.Controllers
{
    [Route("Payment")]
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeOptions)
        {
            _stripeSettings = stripeOptions.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        [HttpPost("CreateCheckoutSession")]
        public IActionResult CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>()
            };

            foreach (var item in request.Items)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = item.Price,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Title,
                        },
                    },
                    Quantity = item.Quantity,
                });
            }

            options.Mode = "payment";
            options.SuccessUrl = "http://localhost:5016/Payment/success";
            options.CancelUrl = "http://localhost:5016/Payment/cancel";

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet("Cancel")]
        public IActionResult Cancel()
        {
            return View();
        }


    }

  
}
