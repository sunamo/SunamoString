// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._sunamo;

internal class SHGetLines
{
    internal static List<string> GetLines(string input)
    {
        var parts = input.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
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

            var name = lines[i].Split(new[] { separator }, StringSplitOptions.None);

            if (name.Length > 1) InsertOnIndex(lines, name.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> lines, List<string> splitLines, int i)
    {
        splitLines.Reverse();

        lines.RemoveAt(i);

        foreach (var item in splitLines) lines.Insert(i, item);
    }

}
