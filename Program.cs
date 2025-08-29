using HtmlAgilityPack;
using SportsResultsNotifier;

//var email = new EmailData() { Body = "Body!", Subject = "Subject Line" };
//EmailManager.SendEmail(email);

HtmlWeb web =  new HtmlWeb();
HtmlDocument doc = web.Load("https://www.baseball-reference.com/teams/TEX/2025.shtml");

var obj = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div"); ////*[@id="content"]/div[3]/div[1]/table //*[@id="content"]/div[3]/div[1]/table/tbody/tr[1]

foreach (var item in obj)
{
    Console.WriteLine(item.SelectSingleNode("table/tbody/tr[1]").InnerText);
    Console.WriteLine(item.SelectSingleNode("table/tbody/tr[2]").InnerText.Trim());
    Console.WriteLine(item.SelectSingleNode("table/tbody/tr[3]").InnerText.Trim());
    Console.Write("-----\n");
}

