namespace SunamoString._sunamo;


internal class CASH
{
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
