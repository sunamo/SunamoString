namespace SunamoString._public.SunamoData.Data;

public class FromToTSHString<T>
{
    public bool IsEmpty { get; set; }

    private long fromLong;

    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;

    private long toLong;

    public FromToTSHString()
    {
        var typeOfT = typeof(T);
        if (typeOfT == typeof(int)) FtUse = FromToUseString.None;
    }

    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        From = from;
        To = to;
        FtUse = fromToUse;
    }

    public T From
    {
        get => (T)(dynamic)fromLong!;
        set => fromLong = (long)(dynamic)value!;
    }

    public T To
    {
        get => (T)(dynamic)toLong!;
        set => toLong = (long)(dynamic)value!;
    }

    public long FromLong => fromLong;

    public long ToLong => toLong;
}
