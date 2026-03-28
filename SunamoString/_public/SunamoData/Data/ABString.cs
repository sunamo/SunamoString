namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents a key-value pair where the key is a string and the value is an object.
/// </summary>
public class ABString
{
    /// <summary>
    /// The type of this class, used for runtime type checking.
    /// </summary>
    public static Type Type = typeof(ABString);

    /// <summary>
    /// First component of the pair (A).
    /// </summary>
    public string A { get; set; } = null!;

    /// <summary>
    /// Second component of the pair (B).
    /// </summary>
    public object B { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="ABString"/> class.
    /// </summary>
    /// <param name="firstValue">The first component (key).</param>
    /// <param name="secondValue">The second component (value).</param>
    public ABString(string firstValue, object secondValue)
    {
        A = firstValue;
        B = secondValue;
    }

    /// <summary>
    /// Creates a new <see cref="ABString"/> from a type's full name and a value.
    /// </summary>
    /// <param name="type">The type whose full name becomes the first component.</param>
    /// <param name="secondValue">The second component (value).</param>
    public static ABString Get(Type type, object secondValue)
    {
        return new ABString(type.FullName!, secondValue);
    }

    /// <summary>
    /// Creates a new <see cref="ABString"/> from a string key and a value.
    /// </summary>
    /// <param name="firstValue">The first component (key).</param>
    /// <param name="secondValue">The second component (value).</param>
    public static ABString Get(string firstValue, object secondValue)
    {
        return new ABString(firstValue, secondValue);
    }

    /// <summary>
    /// Returns a string representation in the format "A:B".
    /// </summary>
    public override string ToString()
    {
        return A + ":" + B;
    }
}
