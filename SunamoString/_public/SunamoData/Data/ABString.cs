namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class ABString
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public static Type Type = typeof(ABString);

    /// <summary>
    /// EN: First component of the pair (A).
    /// CZ: První komponenta páru (A).
    /// </summary>
    public string A { get; set; } = null!;

    /// <summary>
    /// EN: Second component of the pair (B).
    /// CZ: Druhá komponenta páru (B).
    /// </summary>
    public object B { get; set; } = null!;

    /// <summary>
    /// Performs an operation.
    /// </summary>
    public ABString(string firstValue, object secondValue)
    {
        A = firstValue;
        B = secondValue;
    }

    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static ABString Get(Type firstValue, object secondValue)
    {
        return new ABString(firstValue.FullName!, secondValue);
    }

    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static ABString Get(string firstValue, object secondValue)
    {
        return new ABString(firstValue, secondValue);
    }

    /// <summary>
    /// Performs an operation.
    /// </summary>
    public override string ToString()
    {
        return A + ":" + B;
    }
}
