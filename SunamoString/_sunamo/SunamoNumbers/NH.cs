// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._sunamo.SunamoNumbers;

internal class NH
{
    internal static string JoinAnotherTokensIfIsNumber(List<string> p, int i)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (; i < p.Count; i++)
        {
            if (int.TryParse(p[i], out var _))
            {
                stringBuilder.Append(p[i]);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}
