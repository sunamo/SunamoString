namespace SunamoString;




public static partial class SHNotTranslateAble
{
    /// <summary>
    /// Due to app take to2 which is \\\\" and first line dont have ending quote
    /// </summary>
    /// <param name="value"></param>
    public static string DecodeSlashEncodedString(string value)
    {
        // was added ; after 1,2 line and  after 2,3
        // keep as was writte
        value = SHReplace.ReplaceAll(value, "\\", "\\\\");
        value = SHReplace.ReplaceAll(value, "\"", "\\\"");
        value = SHReplace.ReplaceAll(value, "\'", "\\\'");
        return value;
    }
}
