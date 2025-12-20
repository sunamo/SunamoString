// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._sunamo.SunamoCollectionsGeneric;

internal class CAGConsts
{

    /// <summary>
    /// Tady to musí být, SunamoValues nemůže dědit od SunamoCollectionGeneric - vzniklo by Cycle detected
    /// Těch pár řádků mě snad nezabije.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    internal static List<T> ToList<T>(params T[] items)
    {
        return items.ToList();
    }
}
