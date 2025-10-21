// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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
        var list = SHGetLines.GetLines(v);
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
                Wildcard wc = new Wildcard(from);
                ThrowEx.NotImplementedMethod();
                //var occurences = wc.Matches(input);
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