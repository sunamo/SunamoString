namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents the AB string type.
/// </summary>
public class ABString
{
    /// <summary>
    /// The type.
    /// </summary>
    public static Type Type = typeof(ABString);

    /// <summary>
    /// Gets or sets the a.
    /// </summary>
    public string A { get; set; } = null!;

    /// <summary>
    /// Gets or sets the b.
    /// </summary>
    public object B { get; set; } = null!;

    /// <summary>
    /// AB string.
    /// </summary>
    /// <param name="firstValue">The first value.</param>
    /// <param name="secondValue">The second value.</param>
    /// <returns>The AB string result.</returns>
    public ABString(string firstValue, object secondValue)
    {
        A = firstValue;
        B = secondValue;
    }

    /// <summary>
    /// Get.
    /// </summary>
    /// <param name="firstValue">The first value.</param>
    /// <param name="secondValue">The second value.</param>
    /// <returns>The get result.</returns>
    public static ABString Get(Type type, object secondValue)
    {
        return new ABString(type.FullName!, secondValue);
    }

    /// <summary>
    /// Get.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="secondValue">The second value.</param>
    /// <returns>The get result.</returns>
    public static ABString Get(string firstValue, object secondValue)
    {
        return new ABString(firstValue, secondValue);
    }

    /// <summary>
    /// To string.
    /// </summary>
    /// <param name="type.FullName!">The type full name.</param>
    /// <param name="secondValue">The second value.</param>
    /// <returns>The to string result.</returns>
    public override string ToString()
    {
        return $"{A}:{B}";
    }
}
