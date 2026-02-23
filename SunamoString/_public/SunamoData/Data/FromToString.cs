namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class FromToString : FromToTSHString<long>
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public new static FromToString Empty = new(true);
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public FromToString()
    {
    }
    
    
    
    
    private FromToString(bool empty)
    {
        base.Empty = empty;
    }
    
    
    
    
    
    
    
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public FromToString(long from, long to, FromToUseString fromToUse = FromToUseString.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
}
