namespace SunamoString._sunamo.SunamoBts;

/// <summary>
/// Basic type service providing parsing and validation for numeric types.
/// </summary>
internal class BTS
{
    internal static int LastInt = -1;
    internal static long LastLong = -1;
    internal static float LastFloat = -1;
    internal static double LastDouble = -1;

    /// <summary>
    /// Optionally replaces comma with dot in the text for numeric parsing.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="isReplacingCommaForDot">Whether to replace comma with dot.</param>
    internal static string Replace(ref string text, bool isReplacingCommaForDot)
    {
        if (isReplacingCommaForDot)
        {
            text = text.Replace(",", ".");
        }

        return text;
    }

    /// <summary>
    /// Checks whether the text represents a valid float value.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="isReplacing">Whether to replace comma with dot before parsing.</param>
    internal static bool IsFloat(string text, bool isReplacing = false)
    {
        if (text == null)
        {
            return false;
        }

        Replace(ref text, isReplacing);
        return float.TryParse(text.Replace(",", "."), out LastFloat);
    }

    /// <summary>
    /// Checks whether the text represents a valid integer value.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="isThrowingIfFloat">Whether to throw an exception if the value is a float but not an integer.</param>
    /// <param name="isReplacingCommaForDot">Whether to replace comma with dot before parsing.</param>
    internal static bool IsInt(string text, bool isThrowingIfFloat = false, bool isReplacingCommaForDot = false)
    {
        if (text == null)
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
