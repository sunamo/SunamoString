namespace SunamoString.Enums;

/// <summary>
/// Represents the search strategy type.
/// </summary>
public enum SearchStrategy
{
    // Contains
    /// <summary>
    /// The fixed space value.
    /// </summary>
    FixedSpace,
    // split input by spaces and A1 must contains all parts
    /// <summary>
    /// The any spaces value.
    /// </summary>
    AnySpaces,
    // Is exactly the same
    /// <summary>
    /// The exactly name value.
    /// </summary>
    ExactlyName
}
