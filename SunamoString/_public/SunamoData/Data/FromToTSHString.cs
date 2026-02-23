namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class FromToTSHString<T>
{

    /// <summary>
    /// Performs an operation.
    /// </summary>
    public bool Empty { get; set; }
    private long fromL;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public FromToUseString FtUse { get; set; } = FromToUseString.DateTime;
    private long toL;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public FromToTSHString()
    {
        var typeT = typeof(T);
        if (typeT == typeof(int)) FtUse = FromToUseString.None;
    }







    /// <summary>
    /// Performs an operation.
    /// </summary>
    public FromToTSHString(T from, T to, FromToUseString fromToUse = FromToUseString.DateTime) : this()
    {
        this.From = from;
        this.To = to;
        this.FtUse = fromToUse;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public T From
    {
        get => (T)(dynamic)fromL!;
        set => fromL = (long)(dynamic)value!;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public T To
    {
        get => (T)(dynamic)toL!;
        set => toL = (long)(dynamic)value!;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public long FromL => fromL;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public long ToL => toL;
}
