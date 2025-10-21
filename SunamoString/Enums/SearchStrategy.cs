// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString.Enums;







public enum SearchStrategy
{
    /// <summary>
    /// Contains
    /// </summary>
    FixedSpace,
    /// <summary>
    /// split input by spaces and A1 must contains all parts
    /// </summary>
    AnySpaces,
    /// <summary>
    /// Is exactly the same
    /// </summary>
    ExactlyName
}