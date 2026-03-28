namespace SunamoString._sunamo;

/// <summary>
/// Splits text into lines handling all newline conventions (CRLF, LFCR, CR, LF).
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits the text into lines handling all newline formats.
    /// </summary>
    /// <param name="text">The text to split into lines.</param>
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    private static void SplitBy(List<string> lines, string separator)
    {
        for (var i = lines.Count - 1; i >= 0; i--)
        {
            if (separator == "\r")
            {
                var carriageReturnNewline = lines[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturn = lines[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewline.Length > 1)
                    ThrowEx.Custom("cannot contain any \r\name, pass already split by this pattern");
                else if (newlineCarriageReturn.Length > 1) ThrowEx.Custom("cannot contain any \n\r, pass already split by this pattern");
            }

            var parts = lines[i].Split(new[] { separator }, StringSplitOptions.None);

            if (parts.Length > 1) InsertOnIndex(lines, parts.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> lines, List<string> splitLines, int index)
    {
        splitLines.Reverse();

        lines.RemoveAt(index);

        foreach (var line in splitLines) lines.Insert(index, line);
    }
}
