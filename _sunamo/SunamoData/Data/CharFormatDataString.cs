namespace SunamoString;


/// <summary>
/// Udává jak musí být vstupní text zformátovaný
/// </summary>
public class CharFormatDataString
{
    /// <summary>
    /// Null = no matter
    /// Nejvhodnější je zde výčet Windows.UI.Text.LetterCase
    /// </summary>
    internal bool? upper = false;
    /// <summary>
    /// Nemusí mít žádný prvek, pak může být znak libovolný
    /// </summary>
    internal char[] mustBe = null;
    internal static class Templates
    {
        internal static CharFormatDataString dash = Get(null, new FromToString(1, 1), AllChars.dash);
        internal static CharFormatDataString notNumber = Get(null, new FromToString(1, 1), AllChars.notNumber);
        /// <summary>
        /// When doesn't contains fixed, is from 0 to number
        /// </summary>
        internal static CharFormatDataString twoLetterNumber;
        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }
        internal static CharFormatDataString Any;
    }
    internal FromToString fromTo = null;
    internal CharFormatDataString(bool? upper, char[] mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
    }
    internal CharFormatDataString()
    {
    }
    internal static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        CharFormatDataString data = new CharFormatDataString();
        data.fromTo = requiredLength;
        data.mustBe = AllChars.numericChars.ToArray();
        return data;
    }
    /// <summary>
    /// A1 Null = no matter
    ///
    /// </summary>
    /// <param name="upper"></param>
    /// <param name="fromTo"></param>
    /// <param name="mustBe"></param>
    internal static CharFormatDataString Get(bool? upper, FromToString fromTo, params char[] mustBe)
    {
        CharFormatDataString data = new CharFormatDataString(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}