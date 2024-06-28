namespace SunamoString;


/// <summary>
/// Udává jak musí být vstupní text zformátovaný
/// </summary>
internal class CharFormatData
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
        internal static CharFormatData dash = Get(null, new FromTo(1, 1), AllChars.dash);
        internal static CharFormatData notNumber = Get(null, new FromTo(1, 1), AllChars.notNumber);
        /// <summary>
        /// When doesn't contains fixed, is from 0 to number
        /// </summary>
        internal static CharFormatData twoLetterNumber;
        static Templates()
        {
            FromTo requiredLength = new FromTo(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromTo(0, int.MaxValue));
        }
        internal static CharFormatData Any;
    }
    internal FromTo fromTo = null;
    internal CharFormatData(bool? upper, char[] mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
    }
    internal CharFormatData()
    {
    }
    internal static CharFormatData GetOnlyNumbers(FromTo requiredLength)
    {
        CharFormatData data = new CharFormatData();
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
    internal static CharFormatData Get(bool? upper, FromTo fromTo, params char[] mustBe)
    {
        CharFormatData data = new CharFormatData(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}