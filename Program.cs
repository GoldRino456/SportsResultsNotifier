using HtmlAgilityPack;
using SportsResultsNotifier;
using System.Text.RegularExpressions;

//var email = new EmailData() { Body = "Body!", Subject = "Subject Line" };
//EmailManager.SendEmail(email);

var results = await WebScraper.FetchSportsDataAsync();

foreach (var result in results)
{
    Console.WriteLine(result + "\n");
    Console.WriteLine(result.TeamData[0].ToString() + "\n");
    Console.WriteLine(result.TeamData[1].ToString() + "\n");
}