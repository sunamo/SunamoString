namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents the from to TSH string t type.
/// </summary>
public class FromToTSHString<T>
{
    /// <summary>
    /// Gets or sets the is empty.
    /// </summary>
    public bool IsEmpty { get; set; }

    private long fromLong;

    /// <summary>
    /// Gets or sets the ft use.
    /// </summary>
    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;

    private long toLong;

    /// <summary>
    /// From to TSH string.
    /// </summary>
    /// <returns>The from to TSH string result.</returns>
    public FromToTSHString()
    {
        var typeOfT = typeof(T);
        if (typeOfT == typeof(int)) FtUse = FromToUseString.None;
    }

    /// <summary>
    /// Initializes a new instance of the from to TSH string class.
    /// </summary>
    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        From = from;
        To = to;
        FtUse = fromToUse;
    }

    /// <summary>
    /// The from.
    /// </summary>
    public T From
    {
        get => (T)(dynamic)fromLong!;
        set => fromLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// The to.
    /// </summary>
    public T To
    {
        get => (T)(dynamic)toLong!;
        set => toLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// Gets the from long.
    /// </summary>
    public long FromLong => fromLong;

    /// <summary>
    /// Gets the to long.
    /// </summary>
    public long ToLong => toLong;
}
