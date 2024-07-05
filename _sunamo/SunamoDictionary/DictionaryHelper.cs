namespace SunamoString._sunamo.SunamoDictionary;
internal class DictionaryHelper
{
    internal static void AddOrPlus<T>(Dictionary<T, int> sl, T key, int p)
    {
        if (sl.ContainsKey(key))
            sl[key] += p;
        else
            sl.Add(key, p);
    }
}
