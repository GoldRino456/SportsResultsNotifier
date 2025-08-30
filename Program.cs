using SportsResultsNotifier;

var results = await WebScraper.FetchSportsDataAsync();
var body = String.Empty;

foreach (var result in results)
{
    body += result + "\n";
    body += result.TeamData[0].ToString() + "\n";
    body += result.TeamData[1].ToString() + "\n";
}

var email = new EmailData() { Body = $"<b>{body}</b>", Subject = $"Your Daily NBA Breakdown is here! - {DateTime.Now.Date}" };
EmailManager.SendEmail(email);