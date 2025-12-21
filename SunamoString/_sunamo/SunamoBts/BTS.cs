namespace SunamoString._sunamo.SunamoBts;

internal class BTS
{
    //        #region  from BTSShared64.cs
    internal static int LastInt = -1;
    internal static long LastLong = -1;
    internal static float LastFloat = -1;
    internal static double LastDouble = -1;

    ///// <summary>
    /////     Usage: Usage: Exceptions.ArrayElementContainsUnallowedStrings->SH.ContainsAny
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="c"></param>
    ///// <param name="isChar"></param>
    ///// <returns></returns>
    //internal static T CastToByT<T>(string c, bool isChar)
    //{
    //    return isChar ? (T)(dynamic)c.First() : (T)(dynamic)c;
    //}

    internal static string Replace(ref string input, bool replaceCommaForDot)
    {
        if (replaceCommaForDot)
        {
            input = input.Replace(",", ".");
        }

        return input;
    }

    internal static bool IsFloat(string input, bool replace = false)
    {
        if (input == null)
        {
            return false;
        }

        Replace(ref input, replace);
        return float.TryParse(input.Replace(",", "."), out LastFloat);
    }



    /// <summary>
    ///     Usage: Exceptions.IsInt
    /// </summary>
    /// <param name="id"></param>
    /// <param name="excIfIsFloat"></param>
    /// <param name="replaceCommaForDot"></param>
    /// <returns></returns>
    internal static bool IsInt(string input, bool excIfIsFloat = false, bool replaceCommaForDot = false)
    {
        if (input == null)
        {
            return false;
        }

        input = input.Replace(" ", "");
        Replace(ref input, replaceCommaForDot);


        bool isValid = int.TryParse(input, out LastInt);
        if (!isValid)
        {
            if (IsFloat(input))
            {
                if (excIfIsFloat)
                {
                    throw new Exception(input + " is float but is calling IsInt");
                }
            }
        }

        return isValid;
    }

    //        #endregion
}