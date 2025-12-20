// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._public.SunamoData.Data;


public class ABString
{
    public static Type Type = typeof(ABString);

    /// <summary>
    /// EN: First component of the pair (A).
    /// CZ: První komponenta páru (A).
    /// </summary>
    public string A { get; set; } = null;

    /// <summary>
    /// EN: Second component of the pair (B).
    /// CZ: Druhá komponenta páru (B).
    /// </summary>
    public object B { get; set; } = null;

    public ABString(string firstValue, object secondValue)
    {
        A = firstValue;
        B = secondValue;
    }

    public static ABString Get(Type firstValue, object secondValue)
    {
        return new ABString(firstValue.FullName, secondValue);
    }

    public static ABString Get(string firstValue, object secondValue)
    {
        return new ABString(firstValue, secondValue);
    }

    public override string ToString()
    {
        return A + ":" + B;
    }
}
