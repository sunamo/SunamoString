// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString.Extensions;
public static class StringExtensions
{
    public static string RemoveInvisibleChars(this string input)
    {
        int[] charsToRemove = [8205];
        return new string(input.ToCharArray()
            .Where(c => !charsToRemove.Contains(c))
            .ToArray());
    }

    public static string RemoveWhitespaceChars(this string input)
    {
        // https://g.co/gemini/share/b47b0b16b54f
        int[] charsToRemove = [9, 10, 11, 12, 13, 32, 160, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8239, 8287, 12288];
        return new string(input.ToCharArray()
            .Where(c => !charsToRemove.Contains(c))
            .ToArray());
    }

    public static string FromSpace160To32(this string text)
    {
        return Regex.Replace(text, @"\p{Z}", " ");
    }

    public static IEnumerable<string> SplitAndKeep(this string input, char[] delims)
    {
        //delims allow only char[], not List<string>
        int start = 0, index;
        string selectedSeparator = null;
        while ((index = input.IndexOfAny(delims, start)) != -1)
        {
            if (selectedSeparator == null)
                continue;
            if (index - start > 0)
                yield return input.Substring(start, index - start);
            yield return input.Substring(index, selectedSeparator.Length);
            start = index + selectedSeparator.Length;
        }
        if (start < input.Length)
        {
            yield return input.Substring(start);
        }
    }
}
