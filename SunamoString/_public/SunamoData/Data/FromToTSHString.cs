namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Generic base class representing a from-to range with configurable usage type.
/// </summary>
/// <typeparam name="T">The type of the range boundaries.</typeparam>
public class FromToTSHString<T>
{
    /// <summary>
    /// Indicates whether this instance represents an empty range.
    /// </summary>
    public bool IsEmpty { get; set; }

    private long fromLong;

    /// <summary>
    /// Specifies how the from-to range should be interpreted.
    /// </summary>
    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;

    private long toLong;

    /// <summary>
    /// Initializes a new instance with default values. Sets FtUse to None if T is int.
    /// </summary>
    public FromToTSHString()
    {
        var typeOfT = typeof(T);
        if (typeOfT == typeof(int)) FtUse = FromToUseString.None;
    }

    /// <summary>
    /// Initializes a new instance with specified range boundaries and usage type.
    /// </summary>
    /// <param name="from">The start of the range.</param>
    /// <param name="to">The end of the range.</param>
    /// <param name="fromToUse">How the range should be interpreted.</param>
    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }

    /// <summary>
    /// Gets or sets the start of the range.
    /// </summary>
    public T From
    {
        get => (T)(dynamic)fromLong!;
        set => fromLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// Gets or sets the end of the range.
    /// </summary>
    public T To
    {
        get => (T)(dynamic)toLong!;
        set => toLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// Gets the start of the range as a long value.
    /// </summary>
    public long FromLong => fromLong;

    /// <summary>
    /// Gets the end of the range as a long value.
    /// </summary>
    public long ToLong => toLong;
}
