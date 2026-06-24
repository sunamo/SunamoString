namespace SunamoString.Delegates;

public class StringDelegates
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string text, string value)
    {
        return text.Contains(value);
    }


}
