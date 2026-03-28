namespace SunamoString;

/// <summary>
/// Provides string encoding methods for non-translatable escape sequences.
/// </summary>
public static class SHNotTranslateAble
{
    /// <summary>
    /// Encodes backslashes, double quotes, and single quotes with escape sequences.
    /// Due to app taking to2 which is \\" and first line does not have ending quote.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    public static string DecodeSlashEncodedString(string text)
    {
        text = SHReplace.ReplaceAll(text, "\\", "\\\\");
        text = SHReplace.ReplaceAll(text, "\"", "\\\"");
        text = SHReplace.ReplaceAll(text, "\'", "\\\'");
        return text;
    }
}
