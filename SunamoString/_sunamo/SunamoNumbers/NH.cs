namespace SunamoString._sunamo.SunamoNumbers;

internal class NH
{
    internal static string JoinAnotherTokensIfIsNumber(List<string> tokens, int startIndex)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (; startIndex < tokens.Count; startIndex++)
        {
            if (int.TryParse(tokens[startIndex], out var _))
            {
                stringBuilder.Append(tokens[startIndex]);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}