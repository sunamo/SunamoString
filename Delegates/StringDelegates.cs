namespace SunamoString.Delegates;

public class StringDelegates
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string input, string value)
    {
        return input.Contains(value);
    }


}
