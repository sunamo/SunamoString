namespace SunamoString._sunamo;

internal class CA
{
    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }
    /// <summary>
    ///     Usage: IEnumerableExtensions
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    internal static int Count(IEnumerable e)
    {
        if (e == null) return 0;
        var t = e.GetType();
        var tName = t.Name;
        // nevím jak to má .net core, zatím tu ThreadHelper nebudu přesouvat
        // if (ThreadHelper.NeedDispatcher(tName))
        // {
        //     int result = dCountSunExc(e);
        //     return result;
        // }
        if (e is IList) return (e as IList).Count;
        if (e is Array) return (e as Array).Length;
        var count = 0;
        foreach (var item in e) count++;
        return count;
    }
    /// <summary>
    ///     Direct edit input collection
    /// </summary>
    /// <param name="l"></param>
    internal static List<string> Trim(List<string> l)
    {
        for (var i = 0; i < l.Count; i++) l[i] = l[i].Trim();
        return l;
    }
    internal static string First(IEnumerable v2)
    {
        foreach (var item in v2) return item.ToString();
        return null;
    }



    internal static bool HasIndex(int p, IList nahledy)
    {
        if (p < 0)
        {
            throw new Exception("Chybn\u00FD parametr p");
        }
        if (nahledy.Count > p)
        {
            return true;
        }
        return false;
    }


}