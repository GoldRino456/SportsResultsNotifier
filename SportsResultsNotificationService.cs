using SportsResultsNotifier.Model;
using SportsResultsNotifier;

public sealed class SportsResultsNotificationService
{
    public async Task<bool> SendNotificationEmailAsync()
    {
        try
        {
            var results = await WebScraper.FetchSportsDataAsync();
            var body = $"NBA Results for {DateTime.Now.ToShortDateString()}\n\n";

            foreach (var result in results)
            {
                body += "----------\n" + result + "\n"
                    + "\n"
                    + result.TeamData[0].ToString() + "\n"
                    + result.TeamData[1].ToString() + "\n----------\n\n";
            }

            body += "To stop this automated email, close the application on your desktop.";

            var email = new EmailData() { Body = body, Subject = $"Your Daily NBA Breakdown is here! - {DateTime.Now.ToShortDateString()}" };
            EmailManager.SendEmail(email);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to send notification. See error message for details: \n" + ex.ToString());
            return false;
        }
    }
}



