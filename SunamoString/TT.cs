namespace SunamoString;

/// <summary>
///     Text Templates
/// </summary>
public class TT
{
    /// <summary>
    /// Formats a name-value pair as "name: value".
    /// </summary>
    /// <param name="name">The name part.</param>
    /// <param name="value">The value part.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(':') + ": " + value;
    }

    /// <summary>
    /// Name Value operation on the string.
    /// </summary>
    public static string NameValue(ABCString items, string delimiter)
    {
        var builder = new StringBuilder();
        foreach (var abString in items) builder.Append(NameValue(abString.A, abString.B.ToString()!) + delimiter);
        return builder.ToString();
    }
}
