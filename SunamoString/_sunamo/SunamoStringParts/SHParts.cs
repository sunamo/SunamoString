namespace SunamoString._sunamo.SunamoStringParts;

internal class SHParts
{
    internal static string RemoveAfterLast(string text, object delimiter)
    {
        int index = text.LastIndexOf(delimiter.ToString()!);
        if (index != -1)
        {
            string beforeDelimiter = text.Substring(0, index);
            return beforeDelimiter;
        }
        return text;
    }
}
