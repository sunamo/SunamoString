namespace SunamoString._sunamo;
internal class SH
{
    internal static List<string> GetLines(string v)
    {
        return v.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
