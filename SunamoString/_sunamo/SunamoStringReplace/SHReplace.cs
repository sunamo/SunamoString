namespace SunamoString._sunamo.SunamoStringReplace;

internal class SHReplace
{
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
