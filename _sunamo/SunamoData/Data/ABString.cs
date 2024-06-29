namespace SunamoString;


public class ABString
{
    internal static Type type = typeof(ABString);
    internal string A = null;
    internal object B = null;
    internal ABString(string a, object b)
    {
        A = a;
        B = b;
    }
    internal static ABString Get(Type a, object b)
    {
        return new ABString(a.FullName, b);
    }
    /// <param name="a"></param>
    /// <param name="b"></param>
    internal static ABString Get(string a, object b)
    {
        return new ABString(a, b);
    }
    public override string ToString()
    {
        return A + AllStrings.cs2 + B;
    }
}