namespace SunamoString._public.SunamoData.Data;


public class CharFormatDataString
{




    public bool? upper = false;



    public char[] mustBe = null;
    public static class Templates
    {
        static char notNumberChar = (char)9;
        public static CharFormatDataString dash = Get(null, new FromToString(1, 1), '-');
        public static CharFormatDataString notNumber = Get(null, new FromToString(1, 1), notNumberChar);



        public static CharFormatDataString twoLetterNumber;
        static Templates()
        {
            FromToString requiredLength = new FromToString(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToString(0, int.MaxValue));
        }
        public static CharFormatDataString Any;
    }
    public FromToString fromTo = null;
    public CharFormatDataString(bool? upper, char[] mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
    }
    public CharFormatDataString()
    {
    }
    public static CharFormatDataString GetOnlyNumbers(FromToString requiredLength)
    {
        LetterAndDigitCharService letterAndDigitChar = new();

        CharFormatDataString data = new CharFormatDataString();
        data.fromTo = requiredLength;
        data.mustBe = letterAndDigitChar.numericChars.ToArray();
        return data;
    }







    public static CharFormatDataString Get(bool? upper, FromToString fromTo, params char[] mustBe)
    {
        CharFormatDataString data = new CharFormatDataString(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}