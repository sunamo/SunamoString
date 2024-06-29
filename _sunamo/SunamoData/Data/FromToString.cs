namespace SunamoString;


/// <summary>
///     Must have always entered both from and to
///     None of event could have unlimited time!
/// </summary>
internal class FromToString : FromToTSHString<long>
{
    internal static FromToString Empty = new(true);
    internal FromToString()
    {
    }
    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToString(bool empty)
    {
        this.empty = empty;
    }
    /// <summary>
    ///     A3 true = DateTime
    ///     A3 False = None
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="ftUse"></param>
    internal FromToString(long from, long to, FromToUse ftUse = FromToUse.DateTime)
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
}