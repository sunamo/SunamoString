namespace SunamoString.Delegates;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class StringDelegates
{
    /// <summary>
    /// Checks if the text contains the specified value.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="value">The value to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string text, string value)
    {
        return text.Contains(value);
    }


}
