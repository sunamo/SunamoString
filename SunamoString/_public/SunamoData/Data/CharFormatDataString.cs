namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents the char format data string type.
/// </summary>
public class CharFormatDataString
{
    /// <summary>
    /// Gets or sets the upper.
    /// </summary>
    public bool? Upper { get; set; } = false;

    /// <summary>
    /// Gets or sets the must be.
    /// </summary>
    public char[]? MustBe { get; set; } = null;

    /// <summary>
    /// Represents the templates type.
    /// </summary>
    public static class Templates
    {
        static char nonNumericChar = (char)9;

        /// <summary>
        /// Gets or sets the dash.
        /// </summary>
        public static CharFormatDataString Dash { get; set; } = Get(null, new FromToString(1, 1), '-');

        /// <summary>
        /// Gets or sets the not number.
        /// </summary>
        public static CharFormatDataString NotNumber { get; set; } = Get(null, new FromToString(1, 1), nonNumericChar);

        /// <summary>
        /// Gets or sets the two letter number.
        /// </summary>
        public static CharFormatDataString TwoLetterNumber { get; set; }

        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }

        /// <summary>
        /// Gets or sets the any.
        /// </summary>
        public static CharFormatDataString Any { get; set; } = null!;
    }

    /// <summary>
    /// Gets or sets the from to.
    /// </summary>
    public FromToString? FromTo { get; set; } = null;

    /// <summary>
    /// Char format data string.
    /// </summary>
    /// <returns>The char format data string result.</returns>
    public CharFormatDataString(bool? isUpper, char[] mustBe)
    {
        this.Upper = isUpper;
        this.MustBe = mustBe;
    }

    /// <summary>
    /// Char format data string.
    /// </summary>
    /// <param name="requiredLength">The required length.</param>
    /// <returns>The char format data string result.</returns>
    public CharFormatDataString()
    {
    }

    /// <summary>
    /// Get only numbers.
    /// </summary>
    /// <param name="isUpper">The is upper.</param>
    /// <param name="mustBe">The must be.</param>
    /// <returns>The get only numbers result.</returns>
    public static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        LetterAndDigitCharService letterAndDigitCharService = new();

        var result = new CharFormatDataString();
        result.FromTo = requiredLength;
        result.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return result;
    }

    /// <summary>
    /// Initializes a new instance of the get class.
    /// </summary>
    /// <param name="isUpper">The is upper.</param>
    /// <param name="mustBe">The must be.</param>
    public static CharFormatDataString Get(bool? isUpper, FromToString fromTo, params char[] mustBe)
    {
        var result = new CharFormatDataString(isUpper, mustBe);
        result.FromTo = fromTo;
        return result;
    }
}
