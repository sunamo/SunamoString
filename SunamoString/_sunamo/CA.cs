// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._sunamo;

internal class CA
{
        /// <summary>
    ///     Direct edit input collection
    /// </summary>
    /// <param name="list"></param>
    internal static List<string> Trim(List<string> list)
    {
        for (var i = 0; i < list.Count; i++) list[i] = list[i].Trim();
        return list;
    }



    internal static bool HasIndex(int index, IList collection)
    {
        if (index < 0)
        {
            throw new Exception("Invalid parameter index");
        }
        if (collection.Count > index)
        {
            return true;
        }
        return false;
    }


}