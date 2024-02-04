

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoString._sunamo;

internal class SHSplit
{
    internal static void SplitByIndex(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal + 1);
    }
    internal static List<string> Split(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNoneChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }
    internal static List<string> SplitNone(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitCharNone(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }
}
