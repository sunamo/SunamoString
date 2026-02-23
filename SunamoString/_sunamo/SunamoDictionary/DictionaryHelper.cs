namespace SunamoString._sunamo.SunamoDictionary;

internal class DictionaryHelper
{
    internal static void AddOrPlus<T>(Dictionary<T, int> dictionary, T key, int increment) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }
}