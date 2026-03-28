namespace SunamoString._sunamo.SunamoStringParts;

/// <summary>
/// String parts extraction helper methods.
/// </summary>
internal class SHParts
{
    /// <summary>
    /// Removes everything after the last occurrence of the delimiter in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="delimiter">The delimiter to search for.</param>
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
