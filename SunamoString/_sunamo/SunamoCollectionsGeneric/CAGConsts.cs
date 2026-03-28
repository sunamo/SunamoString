namespace SunamoString._sunamo.SunamoCollectionsGeneric;

/// <summary>
/// Collection-to-generic conversion constants. Must be here because SunamoValues cannot inherit from SunamoCollectionGeneric (cycle dependency).
/// </summary>
internal class CAGConsts
{
    /// <summary>
    /// Converts a params array to a list.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="array">The elements to convert.</param>
    internal static List<T> ToList<T>(params T[] array)
    {
        return array.ToList();
    }
}
