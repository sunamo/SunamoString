namespace SunamoString;

public class SHSplit
{
    public static void SplitByIndex(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal + 1);
    }
    public static List<string> Split(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<string> SplitNoneChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }
    public static List<string> SplitNone(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }

    public static List<string> SplitChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<string> SplitCharNone(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }
}
