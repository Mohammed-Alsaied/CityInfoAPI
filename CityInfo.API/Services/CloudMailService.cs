namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        //before using configuration
        //private string _mailTo = "admin@mucompany.com";

        //private string _mailFrpm = "noreply@mucompany.com";

        //after  using configuration IConfiguration configuration
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrpm = string.Empty;

        //Inject IConfiguration configuration
        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAdress"];
            _mailTo = configuration["mailSettings:mailFromAdress"];

        }

        public void Send(string subject, string message)
        {
            // Send mail output yo console window
            Console.WriteLine($"Mail from {_mailFrpm} to {_mailTo}, " +
                $"with {nameof(CloudMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");

        }
    }
}
