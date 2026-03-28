namespace SunamoString._sunamo.SunamoRegex;

/// <summary>
/// Helper methods for wildcard pattern detection.
/// </summary>
internal class WildcardHelper
{
    /// <summary>
    /// Checks whether the text contains wildcard characters (* or ?).
    /// </summary>
    /// <param name="text">The text to check for wildcards.</param>
    internal static bool IsWildcard(string text)
    {
        return text.ToCharArray().Any(character => character == '?') || text.ToCharArray().Any(character => character == '*');
    }
}
