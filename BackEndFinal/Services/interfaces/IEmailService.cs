namespace BackEndFinal.Services.interfaces
{
    public interface IEmailService
    {
        public void SendEmail(string from, string to, string subject, string body, string smtpHost, int smtpPort, bool enableSsl, string smtpUser, string smtpPass);
    }
}
