namespace SunamoString._public.SunamoData.Data;

public class FromToString : FromToTSHString<long>
{
    public static FromToString Empty = new(true);
    public FromToString()
    {
    }
    
    
    
    
    private FromToString(bool empty)
    {
        base.Empty = empty;
    }
    
    
    
    
    
    
    
    public FromToString(long from, long to, FromToUseString fromToUse = FromToUseString.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
}