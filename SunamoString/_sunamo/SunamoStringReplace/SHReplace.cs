namespace SunamoString._sunamo.SunamoStringReplace;

/// <summary>
/// String replacement helper methods.
/// </summary>
internal class SHReplace
{
    /// <summary>
    /// Replaces all occurrences of the specified values in the text with the replacement string.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The replacement string.</param>
    /// <param name="values">The values to replace.</param>
    internal static string ReplaceAll(string text, string replacement, params string[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value))
            {
                return text;
            }
        }

        foreach (var value in values)
        {
            text = text.Replace(value, replacement);
        }
        return text;
    }

    /// <summary>
    /// Replaces multiple substrings in the text based on a mapping definition where each line contains a what/replacement pair separated by the delimiter.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="mappingDefinition">The multi-line mapping definition (each line: what + delimiter + replacement).</param>
    /// <param name="delimiter">The delimiter separating what and replacement in each line.</param>
    internal static string ReplaceManyFromString(string text, string mappingDefinition, string delimiter)
    {
        var list = SHGetLines.GetLines(mappingDefinition);
        foreach (var line in list)
        {
            var parts = SHSplit.Split(line, delimiter);
            parts = parts.ConvertAll(part => part.Trim());
            string? what, replacement;
            what = replacement = null;
            if (parts.Count > 0)
            {
                what = parts[0];
            }
            else
            {
                throw new Exception(line + " hasn't from");
            }
            if (parts.Count > 1)
            {
                replacement = parts[1];
            }
            else
            {
                throw new Exception(line + " hasn't to");
            }
            if (WildcardHelper.IsWildcard(line))
            {
                Wildcard wildcardPattern = new Wildcard(what);
                ThrowEx.NotImplementedMethod();
            }
            else
            {
                text = ReplaceAll(text, replacement, what);
            }
        }
        return text;
    }
}
