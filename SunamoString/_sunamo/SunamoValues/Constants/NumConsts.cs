// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._sunamo.SunamoValues.Constants;


internal class NumConsts
{
    #region For easy copy
    internal const int MOne = -1;
    #endregion
    internal const int DefaultPortIfCannotBeParsed = 587;
    /// <summary>
    /// Min age is 18 due to GDPR - below 18 is needed parent agreement of child
    /// </summary>
    internal const int MinAge = 18;
    internal static short NDtMinVal = 10101;
    internal static short NDtMaxVal = 32271;
    /// <summary>
    /// one thousand
    /// </summary>
    internal static int Thousand { get; set; } = 1000;
    internal const long KB = 1024;
    internal const double ZeroDouble = 0;
    internal const float ZeroFloat = 0;
    /// <summary>
    /// at int should be no postfix
    /// </summary>
    internal const int One = 1;
    internal const int ZeroInt = 0;
}
