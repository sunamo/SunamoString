namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents the from to string type.
/// </summary>
public class FromToString : FromToTSHString<long>
{
    /// <summary>
    /// The empty.
    /// </summary>
    public static FromToString Empty = new(true);

    /// <summary>
    /// From to string.
    /// </summary>
    /// <param name="true">The true.</param>
    /// <returns>The from to string result.</returns>
    public FromToString()
    {
    }

    private FromToString(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    /// <summary>
    /// From to string.
    /// </summary>
    /// <param name="from">The from.</param>
    /// <param name="to">The to.</param>
    /// <param name="fromToUse">The from to use.</param>
    /// <returns>The from to string result.</returns>
    public FromToString(long from, long to, FromToUseString fromToUse = FromToUseString.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
}
