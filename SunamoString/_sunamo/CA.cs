namespace SunamoString._sunamo;

internal class CA
{
    internal static List<string> Trim(List<string> list)
    {
        for (var i = 0; i < list.Count; i++) list[i] = list[i].Trim();
        return list;
    }

    internal static bool HasIndex(int index, IList list)
    {
        if (index < 0)
        {
            throw new Exception("Invalid parameter index");
        }
        if (list.Count > index)
        {
            return true;
        }
        return false;
    }
}
