// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// variables names: ok
namespace SunamoString;

/// <summary>
///     Text Templates
/// </summary>
public class TT
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(':') + ": " + value;
    }

    public static string NameValue(ABCString items, string delimiter)
    {
        var builder = new StringBuilder();
        foreach (var abString in items) builder.Append(NameValue(abString.A, abString.B.ToString()) + delimiter);
        return builder.ToString();
    }
}