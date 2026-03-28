namespace SunamoString._public.SunamoEnums.Enums;

/// <summary>
/// Strategy for handling specific characters in letter statistics.
/// </summary>
public enum StatisticLetterCharsStrategy
{
    /// <summary>
    /// Add the specified characters as the first entries in the result.
    /// </summary>
    AddAsFirst,
    /// <summary>
    /// Completely ignore the specified characters in the statistics.
    /// </summary>
    IgnoreCompletely
}