namespace SunamoString._sunamo.SunamoBts;

internal class BTS
{
    internal static int LastInt = -1;
    internal static long LastLong = -1;
    internal static float LastFloat = -1;
    internal static double LastDouble = -1;

    internal static string Replace(ref string text, bool isReplacingCommaForDot)
    {
        if (isReplacingCommaForDot)
        {
            text = text.Replace(",", ".");
        }

        return text;
    }

    internal static bool IsFloat(string text, bool isReplacing = false)
    {
        if (text is null)
        {
            return false;
        }

        Replace(ref text, isReplacing);
        return float.TryParse(text.Replace(",", "."), out LastFloat);
    }

    internal static bool IsInt(string text, bool isThrowingIfFloat = false, bool isReplacingCommaForDot = false)
    {
        if (text is null)
        {
            return false;
        }

        text = text.Replace(" ", "");
        Replace(ref text, isReplacingCommaForDot);

        bool isValid = int.TryParse(text, out LastInt);
        if (!isValid)
        {
            if (IsFloat(text))
            {
                if (isThrowingIfFloat)
                {
                    throw new Exception(text + " is float but is calling IsInt");
                }
            }
        }

        return isValid;
    }
}
