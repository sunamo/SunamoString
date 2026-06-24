namespace SunamoString;

public static class SHNotTranslateAble
{
    // Due to app taking to2 which is \\" and first line does not have ending quote.
    public static string DecodeSlashEncodedString(string text)
    {
        text = SHReplace.ReplaceAll(text, "\\", "\\\\");
        text = SHReplace.ReplaceAll(text, "\"", "\\\"");
        text = SHReplace.ReplaceAll(text, "\'", "\\\'");
        return text;
    }
}
