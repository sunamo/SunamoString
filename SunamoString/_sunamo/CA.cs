namespace SunamoString._sunamo;

/// <summary>
/// Collection helper methods for internal use.
/// </summary>
internal class CA
{
    /// <summary>
    /// Trims whitespace from all strings in the list. Modifies the list in place.
    /// </summary>
    /// <param name="list">The list of strings to trim.</param>
    internal static List<string> Trim(List<string> list)
    {
        for (var i = 0; i < list.Count; i++) list[i] = list[i].Trim();
        return list;
    }

    /// <summary>
    /// Checks whether the specified index is valid for the given collection.
    /// </summary>
    /// <param name="index">The index to check.</param>
    /// <param name="list">The collection to check against.</param>
    internal static bool HasIndex(int index, IList list)
    {
        if (index < 0)
        {
            throw new Exception("Invalid parameter index");
        }
        if (list.Count > index)
        {
            return true;
        }
        return false;
    }
}
