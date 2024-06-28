namespace SunamoString;


internal class AB
{
    internal static Type type = typeof(AB);
    internal string A = null;
    internal object B = null;
    internal AB(string a, object b)
    {
        A = a;
        B = b;
    }
    internal static AB Get(Type a, object b)
    {
        return new AB(a.FullName, b);
    }
    /// <param name="a"></param>
    /// <param name="b"></param>
    internal static AB Get(string a, object b)
    {
        return new AB(a, b);
    }
    internal override string ToString()
    {
        return A + AllStrings.cs2 + B;
    }
}