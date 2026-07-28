namespace SunamoString;

public class TT
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(':') + ": " + value;
    }

    public static string NameValue(ABCString abcString, string delimiter)
    {
        var stringBuilder = new StringBuilder();
        foreach (var abStringEntry in abcString) stringBuilder.Append(NameValue(abStringEntry.A, abStringEntry.B.ToString()!) + delimiter);
        return stringBuilder.ToString();
    }
}
