namespace SunamoString._public.SunamoData.Data;

public class FromToString : FromToTSHString<long>
{
    public static FromToString Empty = new(true);

    public FromToString()
    {
    }

    private FromToString(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    public FromToString(long from, long to, FromToUseString fromToUse = FromToUseString.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
}
