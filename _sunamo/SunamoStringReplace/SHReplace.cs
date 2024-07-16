namespace SunamoString._sunamo.SunamoStringReplace;

//namespace SunamoString;
internal class SHReplace
{
    //internal static Func<string, string, string, string> ReplaceAll;
    internal static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        //Stupid, zaCo can be null

        //if (string.IsNullOrEmpty(zaCo))
        //{
        //    return vstup;
        //}

        foreach (var item in co)
        {
            if (string.IsNullOrEmpty(item))
            {
                return vstup;
            }
        }

        foreach (var item in co)
        {
            vstup = vstup.Replace(item, zaCo);
        }
        return vstup;
    }

    internal static string ReplaceManyFromString(string input, string v, string delimiter)
    {
        string methodName = "ReplaceManyFromString";
        var l = SHGetLines.GetLines(v);
        foreach (var item in l)
        {
            var p = SHSplit.SplitMore(item, delimiter);
            p = p.ConvertAll(d => d.Trim());
            string from, to;
            from = to = null;
            if (p.Count > 0)
            {
                from = p[0];
            }
            else
            {
                throw new Exception(item + " hasn't from");
            }
            if (p.Count > 1)
            {
                to = p[1];
            }
            else
            {
                throw new Exception(item + " hasn't to");
            }
            if (WildcardHelper.IsWildcard(item))
            {
                Wildcard wc = new Wildcard(from);
                ThrowEx.NotImplementedMethod();
                //var occurences = wc.Matches(input);
                //foreach (Match m in occurences)
                //{
                //    var result = m.Result();
                //    var groups = m.Groups;
                //    var captues = m.Captures;
                //    var value = m.Value;
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
