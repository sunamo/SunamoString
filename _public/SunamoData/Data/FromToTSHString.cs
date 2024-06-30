namespace SunamoString;


public class FromToTSHString<T>
{

    public bool empty;
    protected long fromL;
    public FromToUseString ftUse = FromToUseString.DateTime;
    protected long toL;
    public FromToTSHString()
    {
        var t = typeof(T);
        if (t == Types.tInt) ftUse = FromToUseString.None;
    }




    private FromToTSHString(bool empty) : this()
    {
        this.empty = empty;
    }







    public FromToTSHString(T from, T to, FromToUseString ftUse = FromToUseString.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
    public T from
    {
        get => (T)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }
    public T to
    {
        get => (T)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }
    public long FromL => fromL;
    public long ToL => toL;
}