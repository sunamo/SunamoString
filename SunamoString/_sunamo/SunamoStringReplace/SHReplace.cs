namespace SunamoString._sunamo.SunamoStringReplace;

//namespace SunamoString;
internal class SHReplace
{
    //internal static Func<string, string, string, string> ReplaceAll;
    internal static string ReplaceAll(string input, string replacement, params string[] values)
    {
        //Stupid, replacement can be null

        //if (string.IsNullOrEmpty(replacement))
        //{
        //    return input;
        //}

        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item))
            {
                return input;
            }
        }

        foreach (var item in values)
        {
            input = input.Replace(item, replacement);
        }
        return input;
    }

    internal static string ReplaceManyFromString(string input, string mappingDefinition, string delimiter)
    {
        string methodName = "ReplaceManyFromString";
        var list = SHGetLines.GetLines(mappingDefinition);
        foreach (var item in list)
        {
            var parameter = SHSplit.Split(item, delimiter);
            parameter = parameter.ConvertAll(d => d.Trim());
            string from, to;
            from = to = null;
            if (parameter.Count > 0)
            {
                from = parameter[0];
            }
            else
            {
                throw new Exception(item + " hasn't from");
            }
            if (parameter.Count > 1)
            {
                to = parameter[1];
            }
            else
            {
                throw new Exception(item + " hasn't to");
            }
            if (WildcardHelper.IsWildcard(item))
            {
                Wildcard wildcardPattern = new Wildcard(from);
                ThrowEx.NotImplementedMethod();
                //var occurences = wildcardPattern.Matches(input);
                //foreach (Match match in occurences)
                //{
                //    var result = match.Result();
                //    var groups = match.Groups;
                //    var captues = match.Captures;
                //    var value = match.Value;
                //}
            }
            else
            {
                //Wildcard wildcard = new Wildcard();
                input = ReplaceAll(input, to, from);
            }
        }
        return input;
    }
}