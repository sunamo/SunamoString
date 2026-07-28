namespace SunamoString._sunamo.SunamoCollectionsGeneric;

// Collection-to-generic conversion constants. Must be here because SunamoValues cannot inherit from SunamoCollectionGeneric (cycle dependency).
internal class CAGConsts
{
    internal static List<T> ToList<T>(params T[] array) => array.ToList();
}
