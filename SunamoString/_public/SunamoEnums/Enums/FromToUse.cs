namespace SunamoString._public.SunamoEnums.Enums;

// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Specifies the format used for FromTo range values.
/// </summary>
public enum FromToUseString
{
    /// <summary>
    /// DateTime format.
    /// </summary>
    DateTime,
    /// <summary>
    /// Unix timestamp format.
    /// </summary>
    Unix,
    /// <summary>
    /// Unix timestamp with time only.
    /// </summary>
    UnixJustTime,
    /// <summary>
    /// No specific format.
    /// </summary>
    None
}
