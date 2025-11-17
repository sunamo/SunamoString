// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._public.SunamoData.Data;


public class FromToTSHString<T>
{

    public bool Empty { get; set; }
    protected long fromL;
    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;
    protected long toL;
    public FromToTSHString()
    {
        var typeT = typeof(T);
        if (typeT == typeof(int)) FtUse = FromToUseString.None;
    }




    private FromToTSHString(bool empty) : this()
    {
        this.Empty = empty;
    }







    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.FtUse = fromToUse;
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