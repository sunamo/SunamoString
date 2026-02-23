namespace SunamoString.Delegates;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class StringDelegates
{
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string input, string value)
    {
        return input.Contains(value);
    }


}
