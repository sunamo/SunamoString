namespace SunamoString._sunamo.SunamoStringSplit;

/// <summary>
/// String splitting helper methods.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits the text at the specified index into two parts.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="index">The index to split at.</param>
    /// <param name="firstPart">Output: the part before the index.</param>
    /// <param name="secondPart">Output: the part after the index.</param>
    internal static void SplitByIndex(string text, int index, out string firstPart, out string secondPart)
    {
        firstPart = text.Substring(0, index);
        secondPart = text.Substring(index + 1);
    }

    /// <summary>
    /// Splits the text by the specified string delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The string delimiters.</param>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// Splits the text by the specified string delimiters, keeping empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The string delimiters.</param>
    internal static List<string> SplitNone(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    /// <summary>
    /// Splits the text by the specified char delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The char delimiters.</param>
    internal static List<string> SplitChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
