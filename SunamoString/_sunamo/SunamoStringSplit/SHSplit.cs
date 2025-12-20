// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._sunamo.SunamoStringSplit;

internal class SHSplit
{
    internal static void SplitByIndex(string input, int firstNormal, out string firstPart, out string secondPart)
    {
        firstPart = input.Substring(0, firstNormal);
        secondPart = input.Substring(firstNormal + 1);
    }
    internal static List<string> Split(string input, params string[] delimiters)
    {
        return input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string input, params string[] delimiters)
    {
        return input.Split(delimiters, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitChar(string input, params char[] delimiters)
    {
        return input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

}
