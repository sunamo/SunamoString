namespace SunamoString._public.SunamoData.Data;

public class CharFormatDataString
{
    public bool? Upper { get; set; } = false;

    public char[]? MustBe { get; set; } = null;

    public static class Templates
    {
        static char nonNumericChar = (char)9;

        public static CharFormatDataString Dash { get; set; } = Get(null, new FromToString(1, 1), '-');

        public static CharFormatDataString NotNumber { get; set; } = Get(null, new FromToString(1, 1), nonNumericChar);

        public static CharFormatDataString TwoLetterNumber { get; set; }

        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }

        public static CharFormatDataString Any { get; set; } = null!;
    }

    public FromToString? FromTo { get; set; } = null;

    public CharFormatDataString(bool? isUpper, char[] mustBe)
    {
        this.Upper = isUpper;
        this.MustBe = mustBe;
    }

    public CharFormatDataString()
    {
    }

    public static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        LetterAndDigitCharService letterAndDigitCharService = new();

        var result = new CharFormatDataString();
        result.FromTo = requiredLength;
        result.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return result;
    }

    public static CharFormatDataString Get(bool? isUpper, FromToString fromTo, params char[] mustBe)
    {
        var result = new CharFormatDataString(isUpper, mustBe);
        result.FromTo = fromTo;
        return result;
    }
}
