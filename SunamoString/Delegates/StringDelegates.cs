// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// variables names: ok
namespace SunamoString.Delegates;

public class StringDelegates
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string input, string value)
    {
        return input.Contains(value);
    }


}
