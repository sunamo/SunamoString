namespace SunamoString;






public class FromToString : FromToTSHString<long>
{
    public static FromToString Empty = new(true);
    public FromToString()
    {
    }
    
    
    
    
    private FromToString(bool empty)
    {
        this.empty = empty;
    }
    
    
    
    
    
    
    
    public FromToString(long from, long to, FromToUseString ftUse = FromToUseString.DateTime)
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
}