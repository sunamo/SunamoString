namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class CharFormatDataString
{
    /// <summary>
    /// EN: Indicates if characters should be uppercase.
    /// CZ: Indikuje, zda by znaky měly být velkými písmeny.
    /// </summary>
    public bool? Upper { get; set; } = false;

    /// <summary>
    /// EN: Array of characters that must be present.
    /// CZ: Pole znaků, které musí být přítomny.
    /// </summary>
    public char[]? MustBe { get; set; } = null;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public static class Templates
    {
        static char notNumberChar = (char)9;
        /// <summary>
        /// Performs an operation.
        /// </summary>
        public static CharFormatDataString Dash { get; set; } = Get(null, new FromToString(1, 1), '-');
        /// <summary>
        /// Performs an operation.
        /// </summary>
        public static CharFormatDataString NotNumber { get; set; } = Get(null, new FromToString(1, 1), notNumberChar);



        /// <summary>
        /// Performs an operation.
        /// </summary>
        public static CharFormatDataString TwoLetterNumber { get; set; }
        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }
        /// <summary>
        /// Performs an operation.
        /// </summary>
        public static CharFormatDataString Any;
    }

    /// <summary>
    /// EN: Range specification for character formatting.
    /// CZ: Specifikace rozsahu pro formátování znaků.
    /// </summary>
    public FromToString? FromTo { get; set; } = null;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public CharFormatDataString(bool? upper, char[] mustBe)
    {
        this.Upper = upper;
        this.MustBe = mustBe;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public CharFormatDataString()
    {
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        LetterAndDigitCharService letterAndDigitChar = new();

        CharFormatDataString data = new CharFormatDataString();
        data.FromTo = requiredLength;
        data.MustBe = letterAndDigitChar.NumericChars.ToArray();
        return data;
    }







    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static CharFormatDataString Get(bool? upper, FromToString fromTo, params char[] mustBe)
    {
        CharFormatDataString data = new CharFormatDataString(upper, mustBe);
        data.FromTo = fromTo;
        return data;
    }
}
