namespace SunamoString;

internal partial class BTS
{
    //        #region  from BTSShared64.cs
    internal static int lastInt = -1;
    internal static long lastLong = -1;
    internal static float lastFloat = -1;
    internal static double lastDouble = -1;

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

    internal static string Replace(ref string id, bool replaceCommaForDot)
    {
        if (replaceCommaForDot)
        {
            id = id.Replace(",", ".");
        }

        return id;
    }

    internal static bool IsFloat(string id, bool replace = false)
    {
        if (id == null)
        {
            return false;
        }

        Replace(ref id, replace);
        return float.TryParse(id.Replace(AllStrings.comma, AllStrings.dot), out lastFloat);
    }

    internal static bool IsDouble(string id, bool replace = false)
    {
        if (id == null)
        {
            return false;
        }

        Replace(ref id, replace);
        return double.TryParse(id.Replace(AllStrings.comma, AllStrings.dot), out lastDouble);
    }


    /// <summary>
    ///     Usage: Exceptions.IsInt
    /// </summary>
    /// <param name="id"></param>
    /// <param name="excIfIsFloat"></param>
    /// <param name="replaceCommaForDot"></param>
    /// <returns></returns>
    internal static bool IsInt(string id, bool excIfIsFloat = false, bool replaceCommaForDot = false)
    {
        if (id == null)
        {
            return false;
        }

        id = id.Replace(" ", "");
        Replace(ref id, replaceCommaForDot);


        bool vr = int.TryParse(id, out lastInt);
        if (!vr)
        {
            if (IsFloat(id))
            {
                if (excIfIsFloat)
                {
                    throw new Exception(id + " is float but is calling IsInt");
                }
            }
        }

        return vr;
    }

    internal static bool IsLong(string id, bool excIfIsDouble = false, bool replaceCommaForDot = false)
    {
        if (id == null)
        {
            return false;
        }

        id = id.Replace(" ", ""); //SHReplaceSH.ReplaceAll4(, "", " ");
        Replace(ref id, replaceCommaForDot);

        bool vr = long.TryParse(id, out lastLong);
        if (!vr)
        {
            if (IsDouble(id))
            {
                if (excIfIsDouble)
                {
                    throw new Exception(id + " is float but is calling IsInt");
                }
            }
        }

        return vr;
    }
    //        #endregion
}
