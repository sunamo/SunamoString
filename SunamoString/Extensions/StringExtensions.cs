namespace SunamoString.Extensions;

/// <summary>
/// Represents the string extensions type.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Remove invisible chars.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The remove invisible chars result.</returns>
    public static string RemoveInvisibleChars(this string input)
    {
        int[] charsToRemove = [8205];
        return new string(input.ToCharArray()
            .Where(c => !charsToRemove.Contains(c))
            .ToArray());
    }

    /// <summary>
    /// Remove whitespace chars.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The remove whitespace chars result.</returns>
    public static string RemoveWhitespaceChars(this string input)
    {
        // https://g.co/gemini/share/b47b0b16b54f
        int[] charsToRemove = [9, 10, 11, 12, 13, 32, 160, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8239, 8287, 12288];
        return new string(input.ToCharArray()
            .Where(c => !charsToRemove.Contains(c))
            .ToArray());
    }

    /// <summary>
    /// From space 160 to 32.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The from space 160 to 32 result.</returns>
    public static string FromSpace160To32(this string text)
    {
        return Regex.Replace(text, @"\p{Z}", " ");
    }

    /// <summary>
    /// Split and keep.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="delims">The delims.</param>
    /// <returns>The split and keep result.</returns>
    public static IEnumerable<string> SplitAndKeep(this string input, char[] delims)
    {
        //delims allow only char[], not List<string>
        int start = 0, index;
        string? selectedSeparator = null;
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
