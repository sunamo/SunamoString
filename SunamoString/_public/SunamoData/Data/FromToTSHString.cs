// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._public.SunamoData.Data;


public class FromToTSHString<T>
{

    public bool empty;
    protected long fromL;
    public FromToUseString ftUse = FromToUseString.DateTime;
    protected long toL;
    public FromToTSHString()
    {
        var type = typeof(type);
        if (type == typeof(int)) ftUse = FromToUseString.None;
    }




    private FromToTSHString(bool empty) : this()
    {
        this.empty = empty;
    }







    public FromToTSHString(type from, type to, FromToUseString ftUse = FromToUseString.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
    public type from
    {
        get => (type)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }
    public type to
    {
        get => (type)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }
    public long FromL => fromL;
    public long ToL => toL;
}