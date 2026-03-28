namespace SunamoString._sunamo.SunamoValues.Constants;

/// <summary>
/// Numeric constants used across the library.
/// </summary>
internal class NumConsts
{
    internal const int MOne = -1;
    internal const int DefaultPortIfCannotBeParsed = 587;
    /// <summary>
    /// Min age is 18 due to GDPR - below 18 requires parent agreement.
    /// </summary>
    internal const int MinAge = 18;
    internal static short NDtMinVal = 10101;
    internal static short NDtMaxVal = 32271;
    /// <summary>
    /// One thousand.
    /// </summary>
    internal static int Thousand { get; set; } = 1000;
    internal const long KB = 1024;
    internal const double ZeroDouble = 0;
    internal const float ZeroFloat = 0;
    internal const int One = 1;
    internal const int ZeroInt = 0;
}
