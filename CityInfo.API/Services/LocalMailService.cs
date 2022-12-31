namespace CityInfo.API.Services
{
    //Create own Services
    public class LocalMailService : IMailService
    {
        //we don't want to hard code that, we want it to be in a
        //configuration file, and it also makes sense to make this
        //different depending on the environment written. 
        //If you're still working on code and testing it,
        //we want this address to be different than when we're in staging or production. 

        //before using configuration
        //private string _mailTo = "admin@mucompany.com";
        //private string _mailFrpm = "noreply@mucompany.com";


        //After  using configurationIConfiguration configuration
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrpm = string.Empty;

        //Inject IConfiguration configuration
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings: mailToAddress"];
            _mailTo = configuration["mailSettings: mailFromAddress"];

        }

        public void Send(string subject, string message)
        {
            // Send mail output yo console window
            Console.WriteLine($"Mail from {_mailFrpm} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");

        }
    }
}
