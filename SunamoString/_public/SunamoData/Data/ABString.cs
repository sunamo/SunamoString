namespace SunamoString._public.SunamoData.Data;

public class ABString
{
    public static Type Type = typeof(ABString);

    public string A { get; set; } = null!;

    public object B { get; set; } = null!;

    public ABString(string firstValue, object secondValue)
    {
        A = firstValue;
        B = secondValue;
    }

    public static ABString Get(Type type, object secondValue)
    {
        return new ABString(type.FullName!, secondValue);
    }

    public static ABString Get(string firstValue, object secondValue)
    {
        return new ABString(firstValue, secondValue);
    }

    public override string ToString()
    {
        return $"{A}:{B}";
    }
}
