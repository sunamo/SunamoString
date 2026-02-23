namespace SunamoString._public.SunamoEnums.Enums;

/// <summary>
/// Specifies the comparison method for contains operations.
/// </summary>
public enum ContainsCompareMethodString
{
    /// <summary>
    /// Compare against the whole input string.
    /// </summary>
    WholeInput,
    /// <summary>
    /// Split the input into words before comparing.
    /// </summary>
    SplitToWords,
    /// <summary>
    /// Support negation patterns in the comparison.
    /// </summary>
    Negations
}
