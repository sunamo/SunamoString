namespace SunamoString;

/// <summary>
/// Text template helper for formatting name-value pairs.
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
    /// Formats all name-value pairs from the collection, separated by the specified delimiter.
    /// </summary>
    /// <param name="abcString">The collection of name-value pairs.</param>
    /// <param name="delimiter">The delimiter between formatted pairs.</param>
    public static string NameValue(ABCString abcString, string delimiter)
    {
        var stringBuilder = new StringBuilder();
        foreach (var abStringEntry in abcString) stringBuilder.Append(NameValue(abStringEntry.A, abStringEntry.B.ToString()!) + delimiter);
        return stringBuilder.ToString();
    }
}
