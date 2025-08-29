using System.Text.RegularExpressions;

namespace SportsResultsNotifier;

//Regex pattern for removing excess whitespace found here: https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
//And converted to a source-generated regular expression following this: https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators
public static partial class TextFormatting
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex RemoveExtraWhiteSpace();

    public static string CleanRawText(string rawText)
    {
        return RemoveExtraWhiteSpace().Replace(rawText, " ");
    }
}
