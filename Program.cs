using HtmlAgilityPack;
using SportsResultsNotifier;
using System.Text.RegularExpressions;

//var email = new EmailData() { Body = "Body!", Subject = "Subject Line" };
//EmailManager.SendEmail(email);

var results = await WebScraper.FetchSportsDataAsync();

foreach (var result in results)
{
    Console.WriteLine(result);
}