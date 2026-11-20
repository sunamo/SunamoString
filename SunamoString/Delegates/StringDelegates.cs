namespace SunamoString.Delegates;

/// <summary>
/// Represents the string delegates type.
/// </summary>
public class StringDelegates
{
    /// <summary>
    /// Contains.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="value">The value.</param>
    /// <returns>The contains result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string text, string value)
    {
        return text.Contains(value);
    }


}
