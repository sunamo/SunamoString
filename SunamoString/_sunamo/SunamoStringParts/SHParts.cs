namespace SunamoString._sunamo.SunamoStringParts;

internal class SHParts
{
    internal static string RemoveAfterLast(string input, object delimiter)
    {
        int dex = input.LastIndexOf(delimiter.ToString()!);
        if (dex != -1)
        {
            string beforeDelimiter = input.Substring(0, dex); //SHSubstring.Substring(, 0, dex, new SubstringArgs());
            return beforeDelimiter;
        }
        return input;
    }
}