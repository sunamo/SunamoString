// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// variables names: ok
namespace SunamoString;

public static class SHNotTranslateAble
{
    /// <summary>
    ///     Due to app take to2 which is \\\\" and first line dont have ending quote
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