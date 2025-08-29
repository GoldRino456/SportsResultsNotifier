using HtmlAgilityPack;

namespace SportsResultsNotifier;

public static class WebScraper
{
    private const string _teamDataSiteUrl = "https://www.baseball-reference.com/teams/TEX/2025.shtml";

    public async static Task<List<string>> FetchSportsDataAsync()
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(_teamDataSiteUrl);

        var sportsInfo = GetUpcomingGames(doc);
        sportsInfo.Add(GetCurrentRecord(doc));
        sportsInfo.Add(GetPlayoffOdds(doc));
        sportsInfo.AddRange(GetTop5Batters(doc));
        sportsInfo.AddRange(GetTop5Pitchers(doc));

        return sportsInfo;
    }

    private static List<string> GetUpcomingGames(HtmlDocument doc)
    {
        List<string> result = new List<string>();
        var gameSchedule = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div");

        foreach (var item in gameSchedule)
        {
            var newStr = item.SelectSingleNode("table/tbody/tr[1]").InnerText + ": "
            + item.SelectSingleNode("table/tbody/tr[2]/td[1]").InnerText.Trim() + " "
            + item.SelectSingleNode("table/tbody/tr[3]/td[1]").InnerText.Trim() + " "
            + item.SelectSingleNode("table/tbody/tr[3]/td[3]").InnerText.Trim();

            result.Add(newStr);
        }

        return result;
    }

    private static string GetPlayoffOdds(HtmlDocument doc)
    {
        return TextFormatting.CleanRawText(doc.DocumentNode.SelectSingleNode("//*[@id=\"meta\"]/div[2]/p[2]").InnerText).Trim();
    }

    private static string GetCurrentRecord(HtmlDocument doc)
    {
        var currentRecord = doc.DocumentNode.SelectSingleNode("//*[@id=\"meta\"]/div[2]/p[1]").ChildNodes;
        var recordStr = TextFormatting.CleanRawText(currentRecord[1].InnerText)
            + " " + TextFormatting.CleanRawText(currentRecord[2].InnerText)
            + TextFormatting.CleanRawText(currentRecord[3].InnerText).Replace("_", " ");
        
        return recordStr;
    }

    private static List<string> GetTop5Batters(HtmlDocument doc)
    {
        var topBatters = new List<string>();

        var tableHeaders = doc.DocumentNode.SelectSingleNode("//*[@id=\"players_standard_batting\"]/thead/tr").InnerText.Trim();
        topBatters.Add(tableHeaders);

        for (int i = 1; i < 6; i++)
        {
            var row = doc.DocumentNode.SelectSingleNode($"//*[@id=\"players_standard_batting\"]/tbody/tr[{i}]").InnerText.Trim();
            topBatters.Add(row);
        }

        return topBatters;
    }

    private static List<string> GetTop5Pitchers(HtmlDocument doc)
    {
        var topBatters = new List<string>();

        var tableHeaders = doc.DocumentNode.SelectSingleNode("//*[@id=\"players_standard_pitching\"]/thead/tr").InnerText.Trim();
        topBatters.Add(tableHeaders);

        for (int i = 1; i < 6; i++)
        {
            var row = doc.DocumentNode.SelectSingleNode($"//*[@id=\"players_standard_pitching\"]/tbody/tr[{i}]").InnerText.Trim();
            topBatters.Add(row);
        }

        return topBatters;
    }
}
