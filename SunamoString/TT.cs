namespace SunamoString;

/// <summary>
/// Represents the TT type.
/// </summary>
public class TT
{
    /// <summary>
    /// Name value.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The name value result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(':') + ": " + value;
    }

    /// <summary>
    /// Name value.
    /// </summary>
    /// <param name="abcString">The abc string.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The name value result.</returns>
    public static string NameValue(ABCString abcString, string delimiter)
    {
        var stringBuilder = new StringBuilder();
        foreach (var abStringEntry in abcString) stringBuilder.Append(NameValue(abStringEntry.A, abStringEntry.B.ToString()!) + delimiter);
        return stringBuilder.ToString();
    }
}
