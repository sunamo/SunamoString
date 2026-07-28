namespace SunamoString._sunamo.SunamoNumbers;

internal class NH
{
    internal static string JoinAnotherTokensIfIsNumber(List<string> list, int startIndex)
    {
        StringBuilder stringBuilder = new();

        for (; startIndex < list.Count; startIndex++)
        {
            if (int.TryParse(list[startIndex], out _))
            {
                stringBuilder.Append(list[startIndex]);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}
