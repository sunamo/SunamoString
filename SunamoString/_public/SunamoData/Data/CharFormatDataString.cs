namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Defines formatting rules for character validation including case, required characters, and length constraints.
/// </summary>
public class CharFormatDataString
{
    /// <summary>
    /// Indicates if characters should be uppercase. Null means no constraint.
    /// </summary>
    public bool? Upper { get; set; } = false;

    /// <summary>
    /// Array of characters that must be present.
    /// </summary>
    public char[]? MustBe { get; set; } = null;

    /// <summary>
    /// Provides predefined character format templates.
    /// </summary>
    public static class Templates
    {
        static char nonNumericChar = (char)9;

        /// <summary>
        /// Template that matches a single dash character.
        /// </summary>
        public static CharFormatDataString Dash { get; set; } = Get(null, new FromToString(1, 1), '-');

        /// <summary>
        /// Template that matches a single non-numeric character.
        /// </summary>
        public static CharFormatDataString NotNumber { get; set; } = Get(null, new FromToString(1, 1), nonNumericChar);

        /// <summary>
        /// Template that matches a one or two digit number.
        /// </summary>
        public static CharFormatDataString TwoLetterNumber { get; set; }

        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }

        /// <summary>
        /// Template that matches any characters with no constraints.
        /// </summary>
        public static CharFormatDataString Any { get; set; } = null!;
    }

    /// <summary>
    /// Range specification for character count (min/max length).
    /// </summary>
    public FromToString? FromTo { get; set; } = null;

    /// <summary>
    /// Initializes a new instance with the specified case constraint and required characters.
    /// </summary>
    /// <param name="isUpper">Whether characters should be uppercase. Null means no constraint.</param>
    /// <param name="mustBe">Array of characters that must be present.</param>
    public CharFormatDataString(bool? isUpper, char[] mustBe)
    {
        this.Upper = isUpper;
        this.MustBe = mustBe;
    }

    /// <summary>
    /// Initializes a new empty instance with default values.
    /// </summary>
    public CharFormatDataString()
    {
    }

    /// <summary>
    /// Creates a <see cref="CharFormatDataString"/> that only allows numeric characters within the specified length range.
    /// </summary>
    /// <param name="requiredLength">The required length range.</param>
    public static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        LetterAndDigitCharService letterAndDigitCharService = new();

        CharFormatDataString result = new CharFormatDataString();
        result.FromTo = requiredLength;
        result.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return result;
    }

    /// <summary>
    /// Creates a <see cref="CharFormatDataString"/> with the specified constraints.
    /// </summary>
    /// <param name="isUpper">Whether characters should be uppercase. Null means no constraint.</param>
    /// <param name="fromTo">The required length range.</param>
    /// <param name="mustBe">Characters that must be present.</param>
    public static CharFormatDataString Get(bool? isUpper, FromToString fromTo, params char[] mustBe)
    {
        CharFormatDataString result = new CharFormatDataString(isUpper, mustBe);
        result.FromTo = fromTo;
        return result;
    }
}
