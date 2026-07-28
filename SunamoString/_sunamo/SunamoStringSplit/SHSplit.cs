namespace SunamoString._sunamo.SunamoStringSplit;

internal class SHSplit
{
    internal static void SplitByIndex(string text, int index, out string firstPart, out string secondPart)
    {
        firstPart = text.Substring(0, index);
        secondPart = text.Substring(index + 1);
    }

    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
