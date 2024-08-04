namespace BackEndFinal.ViewModels
{
    public class PaymentModel
    {
        public string PublishableKey { get; set; }

        public void OnGet()
        {
            PublishableKey = "pk_test_51PekZOJzq6GPnvu3Z6EYwGUL0zicrGbCglndDk8AmjuR4P3KthnnziCGhk3dpuiLQrXZ0qWOPyIo6tgkNMlRPWtH00kdrtwcE5";
        }
    }
}
