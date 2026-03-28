namespace SunamoString._sunamo.SunamoDictionary;

/// <summary>
/// Helper methods for dictionary operations.
/// </summary>
internal class DictionaryHelper
{
    /// <summary>
    /// Adds the increment to the value for the specified key, or inserts a new entry if the key does not exist.
    /// </summary>
    /// <typeparam name="T">The key type.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="increment">The value to add.</param>
    internal static void AddOrPlus<T>(Dictionary<T, int> dictionary, T key, int increment) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }
}
