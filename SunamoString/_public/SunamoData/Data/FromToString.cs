namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents a from-to range of long values with configurable usage type.
/// </summary>
public class FromToString : FromToTSHString<long>
{
    /// <summary>
    /// An empty <see cref="FromToString"/> instance.
    /// </summary>
    public static FromToString Empty = new(true);

    /// <summary>
    /// Initializes a new instance with default values.
    /// </summary>
    public FromToString()
    {
    }

    private FromToString(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    /// <summary>
    /// Initializes a new instance with specified range boundaries and usage type.
    /// </summary>
    /// <param name="from">The start of the range.</param>
    /// <param name="to">The end of the range.</param>
    /// <param name="fromToUse">How the range should be interpreted.</param>
    public FromToString(long from, long to, FromToUseString fromToUse = FromToUseString.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
}
