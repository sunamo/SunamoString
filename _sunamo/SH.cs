namespace SunamoString._sunamo;
internal class SH
{
    internal static List<string> GetLines(string v)
    {
        return vv.Split(new string[] { v.Contains("\r\n") ? "\r\n" : "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
