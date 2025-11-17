// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._sunamo.SunamoNumbers;

internal class NH
{
    internal static string JoinAnotherTokensIfIsNumber(List<string> tokens, int i)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (; i < tokens.Count; i++)
        {
            if (int.TryParse(tokens[i], out var _))
            {
                stringBuilder.Append(tokens[i]);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}
