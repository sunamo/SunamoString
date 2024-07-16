namespace SunamoString._sunamo;


internal class CA
{
    internal static List<string> Trim(List<string> l)
    {
        for (var i = 0; i < l.Count; i++) l[i] = l[i].Trim();
        return l;
    }

    internal static bool HasIndex(int dex, Array col)
    {
        return col.Length > dex;
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
