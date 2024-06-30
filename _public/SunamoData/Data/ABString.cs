namespace SunamoString;


public class ABString
{
    public static Type type = typeof(ABString);
    public string A = null;
    public object B = null;
    public ABString(string a, object b)
    {
        A = a;
        B = b;
    }
    public static ABString Get(Type a, object b)
    {
        return new ABString(a.FullName, b);
    }
    
    
    public static ABString Get(string a, object b)
    {
        return new ABString(a, b);
    }
    public override string ToString()
    {
        return A + AllStrings.cs2 + B;
    }
}