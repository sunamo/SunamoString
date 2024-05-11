namespace SunamoString;



//namespace SunamoString._sunamo;
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

    internal static string ReplaceManyFromString(string c, string v, string comma)
    {
        throw new NotImplementedException();
    }
}
