
namespace SunamoString;



/// <summary>
/// Text Templates
/// </summary>
public class TT
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(AllChars.colon) + ": " + value;
    }

    public static string NameValue(ABCString winrar, string delimiter)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in winrar)
        {
            builder.Append(NameValue(item.A, item.B.ToString()) + delimiter);
        }
        return builder.ToString();
    }
}
