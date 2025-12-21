namespace SunamoString._public.SunamoData.Data;

public class FromToTSHString<T>
{

    public bool Empty { get; set; }
    private long fromL;
    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;
    private long toL;
    public FromToTSHString()
    {
        var typeT = typeof(T);
        if (typeT == typeof(int)) FtUse = FromToUseString.None;
    }







    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
    public T From
    {
        get => (T)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }
    public T To
    {
        get => (T)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }
    public long FromL => fromL;
    public long ToL => toL;
}