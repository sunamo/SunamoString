using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoString._sunamo.SunamoNumbers;
internal class NH
{
    internal static string JoinAnotherTokensIfIsNumber(List<string> p, int i)
    {
        StringBuilder sb = new StringBuilder();

        for (; i < p.Count; i++)
        {
            if (int.TryParse(p[i], out var _))
            {
                sb.Append(p[i]);
            }
            else
            {
                break;
            }
        }

        return sb.ToString();
    }
}
