namespace SunamoString;

/// <summary>
/// Represents the SH not translate able type.
/// </summary>
public static class SHNotTranslateAble
{
    // Due to app taking to2 which is \\" and first line does not have ending quote.
    /// <summary>
    /// Decode slash encoded string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The decode slash encoded string result.</returns>
    public static string DecodeSlashEncodedString(string text)
    {
        text = SHReplace.ReplaceAll(text, "\\", "\\\\");
        text = SHReplace.ReplaceAll(text, "\"", "\\\"");
        text = SHReplace.ReplaceAll(text, "\'", "\\\'");
        return text;
    }
}
