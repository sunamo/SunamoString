
namespace SunamoString;
using Diacritics.Extensions;


public partial class SH : SHSH
{
    #region 
    /*
     * Můžou být pouze zde a nikoliv ve SunamoStringData
     * když byl Brackets globální, často jsem měl "claims it is defined"
     * takže jsem musel Brackets přesunout zde a s ním i kód níže
     */
    protected static Dictionary<Brackets, char> bracketsLeft = null;
    protected static Dictionary<Brackets, char> bracketsRight = null;
    protected static void Init()
    {
        if (bracketsLeft == null)
        {
            bracketsLeft = new Dictionary<Brackets, char>();
            bracketsLeft.Add(Brackets.Curly, '{');
            bracketsLeft.Add(Brackets.Square, '[');
            bracketsLeft.Add(Brackets.Normal, '(');
            bracketsLeftList = bracketsLeft.Values.ToList();
            bracketsRight = new Dictionary<Brackets, char>();
            bracketsRight.Add(Brackets.Curly, '}');
            bracketsRight.Add(Brackets.Square, ']');
            bracketsRight.Add(Brackets.Normal, ')');
            bracketsRightList = bracketsRight.Values.ToList();
        }
    }
    #endregion
    protected static List<char> bracketsLeftList = null;
    protected static List<char> bracketsRightList = null;
    #region TrimStartingAndTrailingChars
    #endregion
    private static StringBuilder sb = new StringBuilder();
    public static string DetectNewline(string s)
    {
        if (s.Contains(Consts.rn))
        {
            return Consts.rn;
        }
        return "\n";
    }
    public static string RemoveLastWord(string t)
    {
        return SHParts.RemoveAfterLast(t.Trim(), " ");
    }
    public static List<int> GetIndexesOfLinesStartingWith(List<string> list, Func<string, bool> predicate)
    {
        List<int> allIndices = list.Select((s, i) => new { Str = s, Index = i })
    .Where(x => predicate(x.Str))
    .Select(x => x.Index).ToList();
        return allIndices;
    }
    public static string RemoveLinesWhichContains(string p, string c)
    {
        var l = SHGetLines.GetLines(p);
        l = l.Where(d => !d.Contains(c)).ToList(); //CA.RemoveWhichContains(l, c, false);
        var result = string.Join(Environment.NewLine, l);
        return result;
    }
    public static string AddIfNotContains(string input, string s, string sLower = null)
    {
        if (sLower != null)
        {
            s = sLower;
            input = input.ToLower();
        }
        if (!input.Contains(s))
        {
            return input + AllStrings.space + s;
        }
        return input;
    }
    public static string SwitchSwap(string co, string v)
    {
        var p = SHSplit.Split(co, v);
        if (p.Count == 2)
        {
            return p[1] + AllStrings.comma + p[0];
        }
        return null;
    }
    public static string InsertBeforeEndingBracket(string postfixSpaceCommaNewline, string v)
    {
        var dx = postfixSpaceCommaNewline.LastIndexOf(AllChars.rb);
        if (dx != -1)
        {
            return postfixSpaceCommaNewline.Insert(dx, v);
        }
        return postfixSpaceCommaNewline;
    }
    public static Dictionary<char, int> StatisticLetterChars(string between, StatisticLetterCharsStrategy s, params char[] charsToStrategy)
    {
        List<char> ignoreCompletely = null;
        if (s == StatisticLetterCharsStrategy.IgnoreCompletely)
        {
            ignoreCompletely = new List<char>(charsToStrategy);
        }
        Dictionary<char, int> list = new Dictionary<char, int>();
        if (s == StatisticLetterCharsStrategy.AddAsFirst)
        {
            foreach (var item in charsToStrategy)
            {
                list.Add(item, 0);
            }
        }
        foreach (var item in between)
        {
            if (s == StatisticLetterCharsStrategy.IgnoreCompletely)
            {
                if (ignoreCompletely.Contains(item))
                {
                    continue;
                }
            }
            DictionaryHelper.AddOrPlus(list, item, 1);
        }
        return list;
    }
    public static List<char> AllBrackets(string c)
    {
        List<char> ch = new List<char>();
        bool end = false;
        for (int i = 0; i < c.Length; i++)
        {
            var s = SH.GetBracketFromBegin(c[i], ref end, false);
            if (s != Brackets.None)
            {
                ch.Add(c[i]);
            }
        }
        return ch;
    }
    public static Tuple<SquareMap, SquareMapLines> IndexesOfBrackets(string c)
    {
        SquareMap m = new SquareMap();
        SquareMapLines me = new SquareMapLines(m);
        bool end = false;
        int line = 0;
        bool r = false;
        for (int i = 0; i < c.Length; i++)
        {
            var ch = c[i];
            if (r)
            {
                r = false;
                line++;
                if (ch == '\n')
                {
                    continue;
                }
            }
            if (ch == '\n')
            {
                line++;
                continue;
            }
            if (ch == '\r')
            {
                r = true;
                continue;
            }
            var s = SH.GetBracketFromBegin(ch, ref end, false);
            if (s != Brackets.None)
            {
                m.Add(s, end, i);
                me.Add(s, end, i, line);
            }
        }
        return new Tuple<SquareMap, SquareMapLines>(m, me);
    }
    public static string ReplaceBrackets(string item, Brackets from, Brackets to)
    {
        item = item.Replace(bracketsLeft[from], bracketsLeft[to]);
        item = item.Replace(bracketsRight[from], bracketsRight[to]);
        return item;
    }
    public static List<int> ContainsAnyFromElement(StringBuilder s, IList<string> list)
    {
        List<int> result = new List<int>();
        int i = 0;
        foreach (var item in list)
        {
            if (s.ToString().Contains(item))
            {
                result.Add(i);
            }
            i++;
        }
        return result;
    }
    public static int FindClosingBracketIndexChar(StringBuilder text, bool removeBetween, string openedBracket = "{")
    {
        int index = text.ToString().IndexOf(openedBracket);
        return FindClosingBracketIndex(text, removeBetween, text[index]);
    }
    public static int FindClosingBracketIndex(StringBuilder text, bool removeBetween, int dxOfStart)
    {
        char openedBracket = text[dxOfStart];
        char closedBracket = SH.ClosingBracketFor(openedBracket);
        int start = dxOfStart;
        int bracketCount = 1;
        //var textArray = text.ToString().ToCharArray();
        char ai = 'a';
        for (int i = dxOfStart + 1; i < text.Length; i++)
        {
            ai = text[i];
            if (ai == openedBracket)
            {
                bracketCount++;
            }
            else if (ai == closedBracket)
            {
                bracketCount--;
            }
            if (bracketCount == 0)
            {
                dxOfStart = i;
                break;
            }
        }
        if (removeBetween)
        {
            RemoveBetweenIndexes(text, start, dxOfStart);
        }
        return dxOfStart;
    }
    private static void RemoveBetweenIndexes(StringBuilder text, int start, int end)
    {
        ThrowEx.StartIsHigherThanEnd(start, end);
        start++;
        for (; start < end; start++)
        {
            text[start] = AllChars.space;
        }
    }
    public static bool CheckWhetherNoBrackedIsBeforeOther2(string braces)
    {
        return BalancedBrackets.areBracketsBalanced(AllBrackets(braces));
    }
    public static bool CheckWhetherNoBrackedIsBeforeOther1(string braces)
    {
        const string openBraces = "([{";
        const string closeBraces = ")]}";
        Stack<char> stack = new Stack<char>();
        foreach (char c in braces)
        {
            if (openBraces.Contains(c))
            {
                stack.Push(c);
            }
            else if (stack.Count == 0 || openBraces.IndexOf(stack.Pop()) != closeBraces.IndexOf(c))
            {
                return false;
            }
        }
        return stack.Count == 0;
    }
    public static string ConvertWhitespaceToVisible(string t)
    {
        t = t.Replace(AllChars.tab, UnicodeWhiteToVisible.tab);
        t = t.Replace(AllChars.nl, UnicodeWhiteToVisible.newLine);
        t = t.Replace(AllChars.cr, UnicodeWhiteToVisible.carriageReturn);
        t = t.Replace(AllChars.space, UnicodeWhiteToVisible.space);
        return t;
    }
    public static string ConcatSpace(IList r)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in r)
        {
            sb.Append(item + AllStrings.space);
        }
        return sb.ToString();
    }
    public static bool IsNullOrWhiteSpaceRange(params string[] l)
    {
        foreach (string item in l)
        {
            if (IsNullOrWhiteSpace(item))
            {
                return true;
            }
        }
        return false;
    }
    public static bool IsSingleLine(string sbNullS)
    {
        return !sbNullS.Trim().Contains(Environment.NewLine);
    }
    public static string GetWhitespaceFromBeginning(StringBuilder sb, string line)
    {
        sb.Clear();
        foreach (var item in line)
        {
            if (char.IsWhiteSpace(item))
            {
                sb.Append(item);
            }
            else
            {
                break;
            }
        }
        return sb.ToString();
    }
    //public static bool IsCharOn(string item, int v, UnicodeChars number)
    //{
    //    if (item.Length > v)
    //    {
    //        return CharHelper.IsUnicodeChar(number, item[v]);
    //    }
    //    return false;
    //}
    /// <summary>
    /// A2 is use to calculate length of center
    /// </summary>
    /// <param name="text"></param>
    /// <param name="centerString"></param>
    /// <param name="centerIndex"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static string CharsBeforeAndAfter(string text, string centerString, int centerIndex, int before, int after)
    {
        var b = centerIndex - before;
        var a = centerIndex + centerString.Length + after;
        StringBuilder sb = new StringBuilder();
        if (HasIndex(b, text, false))
        {
            sb.Append(text.Substring(b, before));
            sb.Append(AllStrings.space);
        }
        sb.Append(centerString);
        if (HasIndex(a, text, false))
        {
            sb.Append(text.Substring(a, after));
            sb.Append(AllStrings.space);
        }
        return sb.ToString();
    }
    public static bool ContainsNewLine(string between)
    {
        return between.Contains(AllChars.nl) || between.Contains(AllChars.cr);
    }
    public static bool ChangeEncodingProcessWrongCharacters(ref string c)
    {
        return ChangeEncodingProcessWrongCharacters(ref c, Encoding.GetEncoding("latin1"));
    }
    /// <summary>
    /// když je v souboru rozsypaný čaj, přečíst přes TF.ReadAllText, převést přes SH.ChangeEncodingProcessWrongCharacters. Pokud u žádného není text smysluplný, je to beznadějně poškozené.
    /// V opačném případě 10 kódování by mělo být v pořádku.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="oldEncoding"></param>
    public static bool ChangeEncodingProcessWrongCharacters(ref string c, Encoding oldEncoding)
    {
        if (IsValidISO(c))
        {
            var b = oldEncoding.GetBytes(c);
            c = Encoding.UTF8.GetString(b);
            return true;
        }
        else
        {
            // ý musí být před í, ě před č
            c = SHReplace.ReplaceManyFromString(c, @"Ã©,ý
Ã½,ý
Ă˝,é
Å¥,š
Ĺ,ř
Ã¡,á
Åˆ,ň
Å¡,š
Ä›,ě
Å¯,ů
Å¾,ž
Ãº,ú
Å™,ř
Ã,í
Ä,č
", AllStrings.comma);
            return true;
        }
    }
    public static List<string> AddSpaceAfterFirstLetterForEveryAndSort(List<string> input)
    {
        CASE.Trim(input);
        for (int i = 0; i < input.Count; i++)
        {
            input[i] = input[i].Insert(1, AllStrings.space);
        }
        input.Sort();
        return input;
    }
    public static string GetLastWord(string p, bool returnEmptyWhenDontHaveLenght = true)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(dex).Trim();
        }
        else
        {
            if (returnEmptyWhenDontHaveLenght)
            {
                return string.Empty;
            }
        }
        return p;
    }
    public static string AddSpaceAndDontDuplicate(bool after, string text, string colon)
    {
        List<int> dxsColons = null;
        StringBuilder sb = new StringBuilder();
        sb.Append(text);
        if (after)
        {
            dxsColons = SH.ReturnOccurencesOfString(text, colon);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Insert(dxsColons[i] + 1, AllStrings.space);
            }
            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), colon + AllStrings.doubleSpace);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Remove(dxsColons[i] + 1, 1);
            }
        }
        else
        {
            dxsColons = SH.ReturnOccurencesOfString(text, colon);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Insert(dxsColons[i], AllStrings.space);
            }
            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), AllStrings.doubleSpace + colon);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Remove(dxsColons[i], 1);
            }
        }
        return sb.ToString();
    }
    public static string CountOfItems(List<KeyValuePair<string, int>> counted)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in counted)
        {
            sb.AppendLine(item.Value + "x " + item.Key);
        }
        return sb.ToString();
    }
    public static string MultiWhitespaceLineToSingle(List<string> lines)
    {
        var str = string.Join(Environment.NewLine, lines);
        //SunamoCollectionsShared.CASH.DoubleOrMoreMultiLinesToSingle(ref str);
        return str;
        //CA.Trim(lines);
        //var str = string.Join(Environment.NewLine, lines);
        //var nl3 = string.Join(Times(3, Environment.NewLine);
        //var nl2 = string.Join(Times(2, Environment.NewLine);
        //while (str.Contains(nl3))
        //{
        //    str = str.SHReplace.Replace(nl3, nl2);
        //}
        //return str;
        // Keep as is
        //return Regex.SHReplace.Replace(str, @"(\r\n)+", "\r\n\r\n", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        // Ódstranuje nám to
        //return Regex.SHReplace.Replace(str, @"(\r\n){2,}", Environment.NewLine);
        //throw new Exception("NOT WORKING, IN FIRST DEBUG WITH UNIT TESTS AND THEN USE");
        //List<int> toRemove = new List<int>();
        //List<bool> isWhitespace = new List<bool>(l.Count);
        ////l.Add(false)
        //for (int i = 0; i < l.Count; i++)
        //{
        //    isWhitespace.Add(l[i].Trim() == string.Empty);
        //}
        //isWhitespace.Reverse();
        //for (int i = isWhitespace.Count - 1; i >= 0; i--)
        //{
        //    if (isWhitespace[i] && isWhitespace[i + 1])
        //    {
        //        l.RemoveAt(i+1);
        //    }
        //}
    }
    public static void IndentAsPreviousLine(List<string> lines)
    {
        string indentPrevious = string.Empty;
        string line = null;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < lines.Count - 1; i++)
        {
            line = lines[i];
            if (line.Length > 0)
            {
                if (!char.IsWhiteSpace(line[0]))
                {
                    lines[i] = indentPrevious + lines[i];
                }
                else
                {
                    sb.Clear();
                    foreach (var item in line)
                    {
                        if (char.IsWhiteSpace(item))
                        {
                            sb.Append(item);
                        }
                        else
                        {
                            break;
                        }
                    }
                    indentPrevious = sb.ToString();
                }
            }
        }
    }
    public static bool ContainsLine(string item, bool checkInCaseOnlyOneString, params string[] contains)
    {
        return ContainsLine2(item, checkInCaseOnlyOneString, contains);
    }
    /// <summary>
    /// Whether A1 contains any from a3. a2 only logical chcek
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    public static bool ContainsLine2(string item, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        bool hasLine = false;
        if (contains.Count() == 1)
        {
            if (checkInCaseOnlyOneString)
            {
                hasLine = item.Contains(contains.First());
            }
        }
        else
        {
            foreach (var c in contains)
            {
                if (item.Contains(c))
                {
                    hasLine = true;
                    break;
                }
            }
        }
        return hasLine;
    }
    public static string WordAfter(string input, string word)
    {
        input = SH.WrapWithChar(input, AllChars.space);
        int dex = input.IndexOf(word);
        int dex2 = input.IndexOf(AllChars.space, dex + 1);
        StringBuilder sb = new StringBuilder();
        if (dex2 != -1)
        {
            dex2++;
            for (int i = dex2; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch != AllChars.space)
                {
                    sb.Append(ch);
                }
                else
                {
                    break;
                }
            }
        }
        return sb.ToString();
    }
    public static string Leading(string v, Func<char, bool> isWhiteSpace)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in v)
        {
            if (isWhiteSpace.Invoke(item))
            {
                sb.Append(item);
            }
            else
            {
                break;
            }
        }
        return sb.ToString();
    }
    public static bool IsOnIndex(string input, int dx, Func<char, bool> isWhiteSpace)
    {
        if (input.Length > dx)
        {
            return isWhiteSpace.Invoke(input[dx]);
        }
        return false;
    }
    public static int CountLines(string text)
    {
        return Regex.Matches(text, Environment.NewLine).Count;
    }
    public static bool HasLetter(string s)
    {
        foreach (var item in s)
        {
            if (char.IsLetter(item))
            {
                return true;
            }
        }
        return false;
    }
    public static List<string> GetTextsBetween(string p, string after, string before, bool cannotBeLetterBeforeFounded = false)
    {
        var firstCharBeforeIsLetter = false;
        return GetTextsBetween(p, after, before, cannotBeLetterBeforeFounded, out cannotBeLetterBeforeFounded);
    }
    public static List<string> GetTextsBetween(string p, string after, string before, bool cannotBeLetterBeforeFounded, out bool firstCharBeforeIsLetter)
    {
        firstCharBeforeIsLetter = false;
#if DEBUG
        if (p.Contains("headerName: t(\"Name\"),"))
        {
        }
#endif
        List<string> vr = new List<string>();
        List<int> indexesAfter = SH.ReturnOccurencesOfString(p, after);
        List<int> indexesBefore = SH.ReturnOccurencesOfString(p, before);
        int min = Math.Min(indexesAfter.Count, indexesBefore.Count);
        int indexAfterToAccessCollection = 0;
        int indexBeforeToAccessCollection = 0;
        for (; indexAfterToAccessCollection < min; indexAfterToAccessCollection++, indexBeforeToAccessCollection++)
        {
            int indexAfter = indexesAfter[indexAfterToAccessCollection];
            int indexBefore = indexesBefore[indexBeforeToAccessCollection];
            int indexAfterFinal, indexBeforeFinal;
            if (indexAfter > indexBefore)
            {
                if (indexesAfter.Count == 1 || indexesBefore.Count == 1)
                {
                    indexAfterFinal = indexesAfter[0] + after.Length;
                    indexBeforeFinal = indexesBefore.FirstOrDefault(d => d > indexAfterFinal) - 1;
                    if (indexBeforeFinal == 0) ;
                    {
                        throw new Exception("There is no number higher than " + indexAfterFinal);
                    }
                }
                else
                {
                    // zde to nedává smysl když indexesBefore bude mít méně prvků než indexesAfter protože to vždy končí zde
                    indexBeforeToAccessCollection--;
                    continue;
                }
            }
            else
            {
                indexAfterFinal = indexAfter + after.Length;
                indexBeforeFinal = indexBefore - 1;
            }
            // When I return between ( ), there must be +1
            var substringed = p.Substring(indexAfterFinal, indexBeforeFinal - indexAfterFinal + 1).Trim();
#if DEBUG
            if (p.Contains(".matches(new RegExp(endsWit"))
            {
            }
#endif
            // t("Url must end with .json"
            if (cannotBeLetterBeforeFounded)
            {
                if (indexAfterFinal != 0)
                {
                    var ch2 = p[indexBeforeFinal - 1];
                    var ch = p[indexAfterFinal - after.Length - 1];
                    if (!char.IsLetter(ch))
                    {
                        vr.Add(substringed);
                    }
                    else
                    {
                        firstCharBeforeIsLetter = true;
                    }
                }
                else
                {
                    vr.Add(substringed);
                }
            }
            else
            {
                vr.Add(substringed);
            }
        }
        return vr;
    }
    public static string RemoveLastLetters(string v1, int v2)
    {
        if (v1.Length > v2)
        {
            return v1.Substring(0, v1.Length - v2);
        }
        return v1;
    }
    public static bool IsAllUnique(List<string> c)
    {
        ThrowEx.NotImplementedMethod();
        return false;
    }
    public static string xMismatchCountInInputArraysOfSHAllHaveRightFormat = "MismatchCountInInputArraysOfSHAllHaveRightFormat";
    /// <summary>
    /// Pokud je A1 true, bere se z A2,3 menší počet prvků
    /// Simply call HasTextRightFormat for every in A2
    /// </summary>
    /// <param name="canBeDifferentCount"></param>
    /// <param name="typeDynamics"></param>
    /// <param name="tfd"></param>
    public static bool AllHaveRightFormat(bool canBeDifferentCount, List<string> typeDynamics, List<TextFormatData> tfd)
    {
        if (!canBeDifferentCount)
        {
            if (typeDynamics.Count != tfd.Count)
            {
                throw new Exception(xMismatchCountInInputArraysOfSHAllHaveRightFormat);
            }
        }
        int lowerCount = Math.Min(typeDynamics.Count, tfd.Count);
        for (int i = 0; i < lowerCount; i++)
        {
            if (!HasTextRightFormat(typeDynamics[i], tfd[i]))
            {
                return false;
            }
        }
        return true;
    }
    public static bool HasCharRightFormat(char ch, CharFormatData cfd)
    {
        if (cfd.upper.HasValue)
        {
            if (cfd.upper.Value)
            {
                if (char.IsLower(ch))
                {
                    return false;
                }
            }
            else
            {
                if (char.IsUpper(ch))
                {
                    return false;
                }
            }
        }
        if (cfd.mustBe.Length != 0)
        {
            foreach (char item in cfd.mustBe)
            {
                if (item == ch)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
    public static bool GetTextInLastSquareBracketsAndOther(string p, out string title, out string remix)
    {
        title = remix = null;
        p = p.Trim();
        if (p[p.Length - 1] != AllChars.lsqb)
        {
            return false;
        }
        else
        {
            p = p.Substring(0, p.Length - 1);
        }
        int firstHranata = p.LastIndexOf(AllChars.rsqb);
        if (firstHranata == -1)
        {
            return false;
        }
        else if (firstHranata != -1)
        {
            SHSplit.SplitByIndex(p, firstHranata, out title, out remix);
        }
        return true;
    }
    public static string RemoveBracketsWithTextCaseInsensitive(string vr, string zaCo, params string[] co)
    {
        vr = SHReplace.ReplaceAll(vr, AllStrings.lb, "( ");
        vr = SHReplace.ReplaceAll(vr, AllStrings.rsqb, " ]");
        vr = SHReplace.ReplaceAll(vr, AllStrings.rb, " )");
        vr = SHReplace.ReplaceAll(vr, AllStrings.lsqb, "[ ");
        for (int i = 0; i < co.Length; i++)
        {
            vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        }
        return vr;
    }
    public static string RemoveBracketsWithoutText(string vr)
    {
        return SHReplace.ReplaceAll(vr, "", "()", "[]");
    }
    public static string WithoutSpecialChars(string v, params char[] over)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in v)
        {
            if (!AllChars.specialChars.Contains(item) && !over.Any(d => d == item))// CAGSH.IsEqualToAnyElement(item, over))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
    public static string RemoveBracketsFromStart(string vr)
    {
        while (true)
        {
            bool neco = false;
            if (vr.StartsWith(AllStrings.lb))
            {
                int ss = vr.IndexOf(AllStrings.rb);
                if (ss != -1 && ss != vr.Length - 1)
                {
                    neco = true;
                    vr = vr.Substring(ss + 1);
                }
            }
            else if (vr.StartsWith(AllStrings.lsqb))
            {
                int ss = vr.IndexOf(AllStrings.rsqb);
                if (ss != -1 && ss != vr.Length - 1)
                {
                    neco = true;
                    vr = vr.Substring(ss + 1);
                }
            }
            if (!neco)
            {
                break;
            }
        }
        return vr;
    }
    public static string RemoveLastCharIfIs(string slozka, char znak)
    {
        int a = slozka.Length - 1;
        if (slozka[a] == znak)
        {
            return slozka.Substring(0, a);
        }
        return slozka;
    }
    public static List<string> GetLinesList(string p)
    {
        return SHSplit.Split(p, Environment.NewLine).ToList();
    }
    public static string GetStringNL(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in list)
        {
            sb.AppendLine(item);
        }
        return sb.ToString();
    }
    /// <summary>
    /// If A1 contains A2, return A2 and all following. Otherwise A1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="returnFromString"></param>
    public static string GetLastPartByString(string input, string returnFromString)
    {
        int dex = input.LastIndexOf(returnFromString);
        if (dex == -1)
        {
            return input;
        }
        int start = dex + returnFromString.Length;
        if (start < input.Length)
        {
            return input.Substring(start);
        }
        return input;
    }
    public static string AddEmptyLines(string content, int addRowsDuringScrolling)
    {
        var lines = SHGetLines.GetLines(content);
        for (int i = 0; i < addRowsDuringScrolling; i++)
        {
            lines.Add(string.Empty);
        }
        return string.Join(Environment.NewLine, lines);
    }
    public static string ToCase(string v, bool? velkym)
    {
        if (velkym.HasValue)
        {
            if (velkym.Value)
            {
                return v.ToUpper();
            }
            else
            {
                return v.ToLower();
            }
        }
        return v;
    }
    public static bool EndsWithNumber(string nameSolution)
    {
        for (int i = 0; i < 10; i++)
        {
            if (nameSolution.EndsWith(i.ToString()))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Výchozí byla metoda NullToStringOrEmpty
    /// OrNull pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="v"></param>
    public static string NullToStringOrNull(object v)
    {
        if (v == null)
        {
            return null;
        }
        return v.ToString();
    }
    public static bool LastCharEquals(string input, char delimiter)
    {
        if (!string.IsNullOrEmpty(input))
        {
            return false;
        }
        char ch = input[input.Length - 1];
        if (ch == delimiter)
        {
            return true;
        }
        return false;
    }
    public static string GetWithoutLastWord(string p)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }
        return p;
    }
    public static string DeleteCharsOutOfAscii(string s)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char item in s)
        {
            int i = item;
            if (i < 128)
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// Not working for czech, same as https://stackoverflow.com/a/249126
    /// </summary>
    /// <param name="text"></param>
    public static string RemoveDiacritics(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in normalizedString)
        {
            switch (CharUnicodeInfo.GetUnicodeCategory(c))
            {
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    stringBuilder.Append(c);
                    break;
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                    stringBuilder.Append('_');
                    break;
            }
        }
        string result = stringBuilder.ToString();
        return string.Join("_", result.Split(new char[] { '_' }
        , StringSplitOptions.RemoveEmptyEntries)); // remove duplicate underscores
    }
    public static string StripFunctationsAndSymbols(string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char item in p)
        {
            if (!char.IsPunctuation(item) && !char.IsSymbol(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// Vrátí mi v každém prvku index na které se nachází první znak a index na kterém se nachází poslední
    /// </summary>
    /// <param name="vcem"></param>
    /// <param name="co"></param>
    public static List<FromTo> ReturnOccurencesOfStringFromTo(string vcem, string co)
    {
        int l = co.Length;
        List<FromTo> Results = new List<FromTo>();
        for (int Index = 0; Index < vcem.Length - co.Length + 1; Index++)
        {
            if (vcem.Substring(Index, co.Length) == co)
            {
                FromTo ft = new FromTo();
                ft.from = Index;
                ft.to = Index + l - 1;
                Results.Add(ft);
            }
        }
        return Results;
    }
    public static string GetWithoutFirstWord(string item2)
    {
        item2 = item2.Trim();
        //return item2.Substring(
        int dex = item2.IndexOf(AllChars.space);
        if (dex != -1)
        {
            return item2.Substring(dex + 1);
        }
        return item2;
    }
    public static int EndsWithIndex(string source, params string[] p2)
    {
        for (int i = 0; i < p2.Length; i++)
        {
            if (source.EndsWith(p2[i]))
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// Return A1 if wont find A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="searchFor"></param>
    public static string GetToFirst(string input, string searchFor)
    {
        int indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
    }
    #region MyRegion
    private static Type type = typeof(SH);
    public static int CountOf(string pi, char v)
    {
        int i = 0;
        foreach (var item in pi)
        {
            if (item == v)
            {
                i++;
            }
        }
        return i;
    }
    //    public static
    //#if ASYNC
    //    async Task<bool>
    //#else
    //    bool
    //#endif
    //    ContainsInShared(string item, string mustContains, string v)
    //    {
    //        var cs = AllExtensions.cs;
    //        item = item.Replace(cs, v + cs);
    //        if (File.Exists(item))
    //        {
    //            var c =
    //#if ASYNC
    //            await
    //#endif
    //            File.ReadAllTextAsync(item);
    //            if (c.Contains(mustContains))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    ///// <summary>
    ///// Trim all A2 from beginning A1
    ///// </summary>
    ///// <param name="v"></param>
    ///// <param name="s"></param>
    //public static string TrimStart(string v, string s)
    //{
    //    return se.SHTrim.TrimStart(v, s);
    //}
    public static bool HasIndex(int p, string nahledy, bool throwExcWhenInvalidIndex = true)
    {
        if (p < 0)
        {
            if (throwExcWhenInvalidIndex)
            {
                throw new Exception("Chybn\u00FD parametr ");
            }
            else
            {
                return false;
            }
        }
        if (nahledy.Length > p)
        {
            return true;
        }
        return false;
    }
    public static bool IsNegation(string contains)
    {
        if (contains[0] == AllChars.excl)
        {
            return true;
        }
        return false;
    }
    public static (bool, string) IsNegationTuple(string contains)
    {
        if (contains[0] == '!')
        {
            contains = contains.Substring(1);
            return (true, contains);
        }
        return (false, contains);
    }
    /// <summary>
    /// Version wo ref - dont auto remove first!
    /// </summary>
    /// <param name="item"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static bool IsContained(string item, string contains)
    {
        var (negation, contains2) = IsNegationTuple(contains);
        contains = contains2;
        if (negation && item.Contains(contains))
        {
            return false;
        }
        else if (!negation && !item.Contains(contains))
        {
            return false;
        }
        return true;
    }
    public static bool EqualsOneOfThis(string p1, params string[] p2)
    {
        foreach (string item in p2)
        {
            if (p1 == item)
            {
                return true;
            }
        }
        return false;
    }
    ///// <summary>
    ///// Trim all A2 from end A1
    ///// Originally named TrimWithEnd
    ///// Pokud A1 končí na A2, ořežu A2
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="ext"></param>
    //public static string TrimEnd(string name, string ext)
    //{
    //    return se.SHTrim.TrimEnd(name, ext);
    //}
    /// <summary>
    /// Another method is RemoveDiacritics
    /// G text bez dia A1.
    /// </summary>
    /// <param name="sDiakritik"></param>
    public static string TextWithoutDiacritic(string sDiakritik)
    {
        return sDiakritik.RemoveDiacritics();
        // but also with this don't throw exception but no working Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));
        //if (!initDiactitic)
        //{
        //    System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        //    Encoding.RegisterProvider(provider);
        //    initDiactitic = true;
        //}
        //originally was "ISO-8859-8" but not working in .net standard. 1252 is eqvivalent
        //return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));
        // FormC - followed by SHReplace.Replacement of sequences
        // As default using FormC
        //return sDiakritik.Normalize(NormalizationForm.FormC);
        //return RemoveDiacritics(sDiakritik);
    }
    /// <summary>
    /// Vrátí prázdný řetězec pokud nebude nalezena mezera a A1
    ///
    /// </summary>
    /// <param name="p"></param>
    public static string GetFirstWord(string p, bool returnEmptyWhenDontHaveLenght = true)
    {
        p = p.Trim();
        int dex = p.IndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }
        if (returnEmptyWhenDontHaveLenght)
        {
            return string.Empty;
        }
        return p;
    }
    //public static List<string> SplitChar(string parametry, params char[] deli)
    //{
    //    return se.SHSplit.SplitChar(parametry, deli);
    //}
    //public static List<string> Split(string parametry, params string[] deli)
    //{
    //    return se.SHSplit.Split(parametry, deli);
    //}
    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    //private static string Join(IList parts, object delimiter)
    //{
    //    if (delimiter is string)
    //    {
    //        return Join(delimiter, parts);
    //    }
    //    // TODO: Delete after all app working, has flipped A1 and A2
    //    return Join(delimiter, parts);
    //}
    ///// <summary>
    ///// If null, return Consts.nulled
    ///// </summary>
    ///// <param name="n"></param>
    ///// <returns></returns>
    //public static string NullToStringOrDefault(object n)
    //{
    //    return se.SH.NullToStringOrDefault(n);
    //}
    ///// <summary>
    ///// If null, return Consts.nulled
    ///// </summary>
    ///// <param name="n"></param>
    ///// <param name="v"></param>
    ///// <returns></returns>
    //public static string NullToStringOrDefault(object n, string v)
    //{
    //    return se.SH.NullToStringOrDefault(n, v);
    //}
    ///// <summary>
    ///// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    ///// Format2 - use string.Format with error checking
    ///// Format3 - SHReplace.Replace {x} with my code. Can be used with wildcard
    ///// Format4 - use string.Format without error checking
    /////
    ///// Try to use in minimum!! Better use Format3 which dont raise "Input string was in wrong format"
    /////
    ///// Simply return from string.Format. SHFormat.Format is more intelligent
    ///// If input has curly bracket but isnt in right format, return A1. Otherwise apply string.Format.
    ///// string.Format return string.Format always
    ///// Wont working if contains {0} and another non-format SHReplace.Replacement. For this case of use is there Format3
    ///// </summary>
    ///// <param name="template"></param>
    ///// <param name="args"></param>
    //public static string Format2(string status, params string[] args)
    //{
    //    return se.string.Format(status, args);
    //}
    #endregion
    #region For easy copy
    public static bool ContainsAnyBool(string item, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        return ContainsAny(item, checkInCaseOnlyOneString, contains).Count > 0;
    }
    //public static List<string> ContainsAny(string item, bool checkInCaseOnlyOneString, IList<string> contains)
    //{
    //    return se.SH.ContainsAny(item, checkInCaseOnlyOneString, contains);
    //}
    ///// <summary>
    ///// Return which a3 is contained in A1. if a2 and A3 contains only 1 element, check for contains these first element
    ///// If A3 contains more than 1 element, A2 is not used
    ///// If contains more elements, wasnts check
    ///// Return elements from A3 which is contained
    ///// If don't contains, return zero element collection
    ///// </summary>
    ///// <param name="item"></param>
    ///// <param name="hasFirstEmptyLength"></param>
    ///// <param name="contains"></param>
    //public static List<T> ContainsAny<T>(bool checkInCaseOnlyOneString, T item, IList<T> contains)
    //{
    //    return se.SH.ContainsAny<T>(checkInCaseOnlyOneString, item, contains);
    //}
    #endregion
    #region For easy copy from SHShared64.cs
    ///// <summary>
    ///// Will be delete after final refactoring
    ///// Automaticky ořeže poslední A1
    ///// </summary>
    ///// <param name="delimiter"></param>
    ///// <param name="parts"></param>
    //public static string JoinString(object delimiter, IList parts)
    //{
    //    return se.SHJoin.JoinString(delimiter, parts);
    //}
    //public static string JoinNL(IList parts, bool removeLastNl = false)
    //{
    //    return se.string.Join(Environment.NewLine, parts, removeLastNl);
    //}
    ///// <summary>
    ///// Automaticky ořeže poslední znad A1
    ///// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    ///// </summary>
    ///// <param name="delimiter"></param>
    ///// <param name="parts"></param>
    //public static string Join(object delimiter, params string[] parts)
    //{
    //    return se.string.Join(delimiter, parts);
    //}
    ///// <summary>
    ///// Start at 0
    ///// </summary>
    ///// <param name="input"></param>
    ///// <param name="lenght"></param>
    ///// <returns></returns>
    //public static string SubstringIfAvailable(string input, int lenght)
    //{
    //    return se.SHSubstring.SubstringIfAvailable(input, lenght);
    //}
    ///// <summary>
    ///// Remove with A2
    ///// </summary>
    ///// <param name="t"></param>
    ///// <param name="ch"></param>
    //public static string RemoveAfterFirst(string t, char ch)
    //{
    //    return se.SHParts.RemoveAfterFirst(t, ch);
    //}
    //public static string FirstLine(string item)
    //{
    //    return se.SH.FirstLine(item);
    //}
    #endregion
    #region MyRegion
    /// <summary>
    /// keep joinAnotherWordsIfIsAlsoNumber = false
    /// </summary>
    /// <param name="nameTrim"></param>
    /// <param name="probablyIndex"></param>
    /// <param name="joinAnotherWordsIfIsAlsoNumber"></param>
    /// <returns></returns>
    public static int FirstWordWhichIsNumber(string nameTrim, int probablyIndex, bool joinAnotherWordsIfIsAlsoNumber = false)
    {
        var p = SHSplit.Split(nameTrim, AllStrings.space);
        if (p.Count > probablyIndex)
        {
            if (BTS.IsInt(p[probablyIndex]))
            {
                if (joinAnotherWordsIfIsAlsoNumber)
                {
                    string s = BTS.lastInt + NH.JoinAnotherTokensIfIsNumber(p, probablyIndex + 1);
                    return int.Parse(s);
                }
                return BTS.lastInt;
            }
            else
            {
                return FirstWordWhichIsNumberAllIndexes(p, joinAnotherWordsIfIsAlsoNumber);
            }
        }
        else
        {
            return FirstWordWhichIsNumberAllIndexes(p, joinAnotherWordsIfIsAlsoNumber);
        }
        return int.MinValue;
    }
    public static int FirstWordWhichIsNumberAllIndexes(List<string> p, bool joinAnotherWordsIfIsAlsoNumber = true)
    {
        int i = 0;
        foreach (var item in p)
        {
            if (BTS.IsInt(item))
            {
                i++;
                if (joinAnotherWordsIfIsAlsoNumber)
                {
                    string s = BTS.lastInt + NH.JoinAnotherTokensIfIsNumber(p, i);
                    return int.Parse(s);
                }
                return BTS.lastInt;
            }
        }
        return int.MinValue;
    }
    public static bool CompareStringIgnoreWhitespaces2(string s1, string s2)
    {
        return String.Compare(s1, s2, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
    }
    public static bool CompareStringIgnoreWhitespaces(string s1, string s2)
    {
        string normalized1 = Regex.Replace(s1, @"\s", "");
        string normalized2 = Regex.Replace(s2, @"\s", "");
        bool stringEquals = String.Equals(
            normalized1,
            normalized2,
            StringComparison.OrdinalIgnoreCase);
        return stringEquals;
    }
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static string WrapWith(string value, char v, bool _trimWrapping = false)
    //{
    //    // TODO: Make with StringBuilder, because of SH.WordAfter and so
    //    return WrapWith(value, v.ToString(), _trimWrapping);
    //}
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static string WrapWith(string value, string h, bool _trimWrapping = false)
    //{
    //    return h + (_trimWrapping ? Trim(value, h) : value) + h;
    //}
    //public static string SHReplace.ReplaceAll4(string t, string to, string from)
    //{
    //    while (t.Contains(from))
    //    {
    //        t = t.Replace(from, to);
    //    }
    //    return t;
    //}
    public static bool ContainsOnly(string floorS, List<char> numericChars)
    {
        if (floorS.Length == 0)
        {
            return false;
        }
        foreach (char item in floorS)
        {
            if (!numericChars.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
    #region MyRegion
    ///// <summary>
    /////     This can be only one
    ///// </summary>
    ///// <param name="delimiter"></param>
    ///// <param name="parts"></param>
    //public static string JoinIList<T>(object delimiter, IList<T> parts)
    //{
    //    // TODO: Delete after all app working
    //    return JoinString(delimiter, new List<string>2(parts));
    //} //
    #endregion
    //        #region  from SHShared64.cs
    //
    /// <summary>
    ///     Usage: Exc.MethodOfOccuredFromStackTrace
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string FirstLine(string item)
    {
        List<string> lines = SHGetLines.GetLines(item);
        return lines.Count == 0 ? string.Empty : lines[0];
    }
    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="nazevPP"></param>
    /// <param name="only"></param>
    public static string FirstCharUpper(string nazevPP, bool only = false)
    {
        if (nazevPP != null)
        {
            string sb = nazevPP.Substring(1);
            if (only)
            {
                sb = sb.ToLower();
            }
            return nazevPP[0].ToString().ToUpper() + sb;
        }
        return null;
    }
    public static string InBrackets(string podlazi)
    {
        return GetTextBetweenTwoCharsInts(podlazi, podlazi.IndexOf('('), podlazi.IndexOf(')'));
    }
    //public static string JoinNL(params string[] parts)
    //{
    //    return SHJoin.JoinString(Environment.NewLine, parts);
    //}
    /// <summary>
    ///     Usage: Exceptions.ArrayElementContainsUnallowedStrings
    ///     Return which a3 is contained in A1. if a2 and A3 contains only 1 element, check for contains these first element
    ///     If A3 contains more than 1 element, A2 is not used
    ///     If contains more elements, wasnts check
    ///     Return elements from A3 which is contained
    ///     If don't contains, return zero element collection
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    public static List<string> ContainsAny(/*T itemT,*/  /*IList<T> containsT,*/
        string item, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        List<string> founded = new List<string>();
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
        {
            item.Contains(contains.First());
        }
        else
        {
            foreach (string c in contains)
            {
                if (item.Contains(c))
                {
                    founded.Add(c);
                }
            }
        }
        return founded;
    }
    public static List<char> ContainsAnyChar(/*T itemT,*/  /*IList<T> containsT,*/
        string item, bool checkInCaseOnlyOneString, IList<char> contains)
    {
        List<char> founded = new List<char>();
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
        {
            item.Contains(contains.First());
        }
        else
        {
            foreach (var c in contains)
            {
                if (item.Contains(c))
                {
                    founded.Add(c);
                }
            }
        }
        return founded;
    }
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="checkInCaseOnlyOneString"></param>
    /// <param name="item"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static List<T> ContainsAny<T>(/*T itemT,*/ /*IList<T> containsT,*/
       bool checkInCaseOnlyOneString, T item, IList<T> contains)
    {
        throw new Exception("Tahle metoda je celá špatně, používat vždy jinou. Je tu stejná metoda ContainsAny jen negenerická");
        //Type type = typeof(T);
        //bool isChar = type == Types.tChar;
        //List<T> founded = new List<T>();
        ////
        //if (contains.Count() == 1 && checkInCaseOnlyOneString)
        //{
        //    ThrowEx.NotImplementedMethod();
        //    //item.Contains(contains.First());
        //}
        //else
        //{
        //    foreach (var c in contains)
        //    {
        //        //ThrowEx.NotImplementedMethod();
        //        //if (item.Contains(c))
        //        //{
        //        //    founded.Add(BTS.CastToByT<T>(c, isChar));
        //        //}
        //    }
        //}
        //return founded;
    }
    public static string GetWordOnIndex(string line, int v)
    {
        var p = SHSplit.Split(line, SHData.ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(true).ToArray());
        if (p.Count < v)
        {
            throw new Exception("\"" + line + "\"" + " don't have " + v + " words");
        }
        return p[v];
    }
    public static bool ContainsUpper(string d)
    {
        return d.Any(char.IsUpper);
    }
    public static bool ContainsLower(string d)
    {
        return d.Any(char.IsLower);
    }
    //
    //        public static bool ContainsOnly(string floorS, List<char> numericChars)
    //        {
    //            if (floorS.Length == 0)
    //            {
    //                return false;
    //            }
    //
    //            foreach (var item in floorS)
    //            {
    //                if (!numericChars.Contains(item))
    //                {
    //                    return false;
    //                }
    //            }
    //
    //            return true;
    //        }
    //
    //        public static string FirstCharLower(string nazevPP)
    //        {
    //            if (nazevPP.Length < 2)
    //            {
    //                return nazevPP;
    //            }
    //
    //            string sb = nazevPP.Substring(1);
    //            return nazevPP[0].ToString().ToLower() + sb;
    //        }
    //        #endregion
    //
    //        /// <summary>
    //        /// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    //        /// Format2 - use string.Format with error checking
    //        /// Format3 - SHReplace.Replace {x} with my code. Can be used with wildcard
    //        /// Format4 - use string.Format without error checking
    //        ///
    //        /// Manually SHReplace.Replace every {i}
    //        /// </summary>
    //        /// <param name="template"></param>
    //        /// <param name="args"></param>
    //        public static string Format3(string template, params string[] args)
    //        {
    //            // this was original implementation but dont know why isnt used string.format
    //            for (int i = 0; i < args.Length; i++)
    //            {
    //                template = SHReplace.ReplaceAll2(template, args[i].ToString(), AllStrings.lcub + i + AllStrings.rcub);
    //            }
    //            return template;
    //        }
    //
    //            public static string WrapWithQm(string commitMessage)
    //        {
    //            return SH.WrapWith(commitMessage, AllChars.qm);
    //        }
    //
    //        [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //        public static string WrapWith(string value, char v, bool _trimWrapping = false)
    //        {
    //            // TODO: Make with StringBuilder, because of SH.WordAfter and so
    //            return WrapWith(value, v.ToString());
    //        }
    //
    //        [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //        public static string WrapWith(string value, string h, bool _trimWrapping = false)
    //        {
    //            return h + SHTrim.Trim(value, h) + h;
    //        }
    //
    //        public static string SHReplace.ReplaceOnce(string text, string xmlns, string empty)
    //        {
    //            ThrowEx.NotImplementedMethod();
    //        }
    //
    // Nev�m co tu d�l� tohle. V�echny metody maj� po�ad� delimiter/parts, tahle opa�n�
    //    /// <summary>
    //    /// Will be delete after final refactoring
    //    /// Automaticky o�e�e posledn� znad A1
    //    /// Pokud m� inty v A2, pou�ij metodu JoinMakeUpTo2NumbersToZero
    //    /// </summary>
    //    /// <param name="delimiter"></param>
    //    /// <param name="parts"></param>
    //    public static string Join(IList parts, object delimiter)
    //{
    //        if (CA.Count(parts) == 0)
    //        {
    //            return string.Empty;
    //        }
    //        var d = delimiter.ToString();
    //        StringBuilder sb = new StringBuilder();
    //        foreach (var item in parts)
    //        {
    //        sb.Append(item.ToString() + d);
    //    }
    //    var vr = sb.ToString();
    //    return vr.Substring(0, vr.Length - d.Length);
    //}
    //
    //        public static string Trim(string s, string args)
    //        {
    //            s = TrimStart(s, args);
    //            s = TrimEnd(s, args);
    //
    //            return s;
    //        }
    //
    //        public static Type type = typeof(SH);
    //
    //        #region
    //        public static string JoinComma(params string[] args)
    //        {
    //            return Join(AllStrings.comma, (IList)args);
    //        }
    //
    //        public static void FirstCharUpper(ref string nazevPP)
    //        {
    //            nazevPP = FirstCharUpper(nazevPP);
    //        }
    //
    //
    //        /// <summary>
    //        /// Stejn� jako metoda SHReplace.ReplaceAll, ale bere si do A3 pouze jedin� parametr, nikoliv jejich pole
    //        /// </summary>
    //        /// <param name="vstup"></param>
    //        /// <param name="zaCo"></param>
    //        /// <param name="co"></param>
    //        public static string SHReplace.ReplaceAll2(string vstup, string zaCo, string co)
    //        {
    //            return vstup.SHReplace.Replace(co, zaCo);
    //        }
    //
    //        public static string GetTextBetweenTwoChars(string p, int begin, int end)
    //        {
    //            if (end > begin)
    //            {
    //                // a(1) - 1,3
    //                return p.Substring(begin + 1, end - begin - 1);
    //                // originally
    //                //return p.Substring(begin+1, end - begin - 1);
    //            }
    //            return p;
    //        }
    //
    //
    //        /// <summary>
    //        /// Work like everybody excepts, from a {b} c return b
    //        /// </summary>
    //        /// <param name="p"></param>
    //        /// <param name="begin"></param>
    //        /// <param name="end"></param>
    //        public static string GetTextBetweenTwoChars(string p, char beginS, char endS, bool throwExceptionIfNotContains = true)
    //        {
    //            var begin = p.IndexOf(beginS);
    //            var end = p.IndexOf(endS, begin + 1);
    //            if (begin == NumConsts.mOne || end == NumConsts.mOne)
    //            {
    //                if (throwExceptionIfNotContains)
    //                {
    //                    ThrowEx.NotContains( p, beginS.ToString(), endS.ToString());
    //                }
    //            }
    //            else
    //            {
    //                return GetTextBetweenTwoChars(p, begin, end);
    //            }
    //            return p;
    //        }
    //        #endregion
    #endregion
    #region MyRegion
    #region MyRegion
    /// <summary>
    /// notAllowedInRanges can be Func<int, bool> (delegát který vrací zda daný index může být použít pro end) or FromToList
    ///
    /// Dříve se mi na to používalo FromToList
    ///
    /// Nikde jsem nenašel způsob užítí s Func<int, bool> notAllowedInRanges = null takže vytvořím novou metodu co bude brát FromToList
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <param name="notAllowedInRanges"></param>
    /// <param name="endLastIndexOf"></param>
    /// <returns></returns>
    public static string GetTextBetween(string p, char after, char before, bool throwExceptionIfNotContains = true /*cant have implicit value*/, object notAllowedInRanges = null /*cant have implicit value*/, bool endLastIndexOf = false)
    {
        return GetTextBetweenTwoChars(p, after, before, throwExceptionIfNotContains, notAllowedInRanges, endLastIndexOf);
    }
    public static string GetTextBetweenTwoChars(string p, char beginS, char endS, bool throwExceptionIfNotContains = true, object notAllowedInRanges = null, bool endLastIndexOf = false)
    {
        int num = p.IndexOf(beginS);
        int num2 = -1;
        if (endLastIndexOf)
        {
            num2 = p.LastIndexOf(endS);
        }
        else
        {
            num2 = p.IndexOf(endS, num + 1);
            if (notAllowedInRanges != null)
            {
                while (num2 != -1 && NotAllowedInRanges(notAllowedInRanges, num2))
                {
                    num2 = p.IndexOf(endS, num2 + 1);
                }
            }
        }
        if (num == -1 || num2 == -1)
        {
            if (throwExceptionIfNotContains)
            {
                ThrowEx.NotContains(p, beginS.ToString(), endS.ToString());
            }
            else if (num2 == -1)
            {
                return null;
            }
            return p;
        }
        return GetTextBetweenTwoCharsInts(p, num, num2);
    }
    #endregion
    public static List<string> ValuesBetweenQuotes(string str, bool insertAgainToQm, bool apos = false)
    {
        string q = "\"";
        if (apos)
        {
            q = "'";
        }
        return ValuesBetweenQuotesOrApos(str, insertAgainToQm, q);
    }
    public static List<string> ValuesBetweenQuotesAndApos(string str, bool insertAgainToQm, bool onlyWhichIsNotInT = false)
    {
        List<string> ls = ValuesBetweenQuotesOrApos(str, insertAgainToQm, "\"", onlyWhichIsNotInT);
        ls.AddRange(ValuesBetweenQuotesOrApos(str, insertAgainToQm, "'", onlyWhichIsNotInT));
        return ls;
    }
    static List<string> ValuesBetweenQuotesOrApos(string str, bool insertAgainToQm, string q, bool onlyWhichIsNotInT = false)
    {
        var ch = q[0];
        var reg = new Regex(q + ".*?" + q);
        var matches = reg.Matches(str);
        List<string> result = new List<string>(matches.Count);
        foreach (var item in matches)
        {
            var itemS = item.ToString();
#if DEBUG
            if (itemS.Contains("Module registration is not provided!"))
            {
            }
#endif
            if (onlyWhichIsNotInT)
            {
                if (str.Contains("t(" + itemS + ")"))
                {
                    continue;
                }
            }
            if (insertAgainToQm)
            {
                result.Add(itemS);
            }
            else
            {
                result.Add(itemS.TrimEnd(ch).TrimStart(ch));
            }
        }
        //SunamoCollectionsShared.CASH.RemoveStringsEmpty2(result);
        return result;
    }
    public static bool ContainsAtLeastOne(string p, List<string> aggregate)
    {
        foreach (var item in aggregate)
        {
            if (p.Contains(item))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Dont automatically change case
    /// </summary>
    /// <param name="value"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    public static string FirstCharOfEveryWordPart(string value, string deli)
    {
        var p = SHSplit.Split(value, deli);
        StringBuilder sb = new StringBuilder();
        foreach (var item in p)
        {
            sb.Append(item[0].ToString());
        }
        return sb.ToString();
    }
    /// <summary>
    /// When there is no number, append 1
    /// Otherwise incr.
    /// </summary>
    /// <param name="acronym"></param>
    public static void IncrementLastNumber(ref string acronym)
    {
        var ch = acronym[acronym.Length - 1];
        if (char.IsNumber(ch))
        {
            var i = int.Parse(ch.ToString());
            i++;
            acronym = acronym.Substring(0, acronym.Length - 1) + i;
            return;
        }
        acronym = acronym + "1";
    }
    /// <summary>
    /// Nothing can be null
    /// </summary>
    /// <param name="content"></param>
    /// <param name="lines"></param>
    /// <param name="dx2"></param>
    /// <returns></returns>
    public static string GetLineFromCharIndex(string content, List<string> lines, int dx2)
    {
        var dx = GetLineIndexFromCharIndex(content, dx2);
        return lines[dx];
    }
    /// <summary>
    /// Return index, therefore x-1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pos"></param>
    public static int GetLineIndexFromCharIndex(string input, int pos)
    {
        var lineNumber = input.Take(pos).Count(c => c == '\n') + 1;
        return lineNumber - 1;
    }
    public static int AnotherOtherThanLetterOrDigit(string content, int v)
    {
        int i = v;
        for (; i < content.Length; i++)
        {
            if (!char.IsLetterOrDigit(content[i]))
            {
                //i--;
                return i;
            }
        }
        //i--;
        return i--;
    }
    public static string LastChars(string v1, int v2)
    {
        return v1.Substring(v1.Length - v2);
        //mystring.Substring(Math.Max(0, mystring.Length - 4));
    }
    public static string TabToNewLine(string v)
    {
        //Environment.NewLine
        v = v.Replace("\t", "\r");
        var l = SHGetLines.GetLines(v);
        CASE.Trim(l);
        l = l.Where(d => d.Trim() != string.Empty).ToList();
        return string.Join(Environment.NewLine, l);
    }
    public static bool IsAllLower(string ext)
    {
        return IsAllLower(ext, char.IsLower);
    }
    private static bool IsAllLower(string ext, Func<char, bool> isLower)
    {
        for (int i = 0; i < ext.Length; i++)
        {
            if (!isLower(ext[i]))
            {
                return false;
            }
        }
        return true;
    }
    public static bool IsAllUpper(string ext)
    {
        return IsAllLower(ext, char.IsUpper);
    }
    public static bool ContainsBracket(string t, bool mustBeLeftAndRight = false)
    {
        List<char> left, right;
        left = right = null;
        return ContainsBracket(t, ref left, ref right, mustBeLeftAndRight);
    }
    protected static bool s_cs = false;
    static SH()
    {
        s_cs = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();
    }
    public static List<int> IndexesOfChars(string input, char ch)
    {
        return IndexesOfCharsList(input, new List<char>(ch));
    }
    /// <summary>
    /// IndexesOfChars - char
    /// ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="input"></param>
    /// <param name="whiteSpacesChars"></param>
    /// <returns></returns>
    public static List<int> IndexesOfCharsList(string input, List<char> whiteSpacesChars)
    {
        var dx = new List<int>();
        foreach (var item in whiteSpacesChars)
        {
            dx.AddRange(SH.ReturnOccurencesOfString(input, item.ToString()));
        }
        dx.Sort();
        return dx;
    }
    public static bool ContainsBracket(string t, ref List<char> left, ref List<char> right, bool mustBeLeftAndRight = false)
    {
        left = SH.ContainsAnyChar(t, false, AllLists.leftBrackets);
        right = SH.ContainsAnyChar(t, false, AllLists.leftBrackets);
        if (mustBeLeftAndRight)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                return true;
            }
        }
        else
        {
            if (left.Count > 0 || right.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    public static char ClosingBracketFor(char v)
    {
        foreach (var item in bracketsLeft)
        {
            if (item.Value == v)
            {
                return bracketsRight[item.Key];
            }
        }
        ThrowEx.IsNotAllowed(v + " as bracket");
        return char.MaxValue;
    }
    /// <summary>
    /// Get text after cz#cd => #cd
    /// </summary>
    /// <param name="item"></param>
    /// <param name="after"></param>
    public static string TextAfter(string item, string after)
    {
        var dex = item.IndexOf(after);
        if (dex != -1)
        {
            return item.Substring(dex + after.Length);
        }
        return string.Empty;
    }
    public static string PadRight(string empty, string newLine, int v)
    {
        StringBuilder sb = new StringBuilder(empty);
        for (int i = 0; i < v; i++)
        {
            sb.Append(newLine);
        }
        return sb.ToString();
    }
    public static void RemoveLastCharSb(StringBuilder sb)
    {
        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
        }
    }
    public static string RemoveUselessWhitespaces(string innerText)
    {
        var p = SHSplit.SplitChar(innerText);
        return string.Join(AllStrings.space, p);
    }
    /// <summary>
    /// Is used in btnShortTextOfLyrics
    /// Short text but always keep whole paragraps
    /// Can be use also for non paragraph strings abcd->ab
    /// </summary>
    /// <param name="c"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string ShortToLengthByParagraph(string c, int maxLength)
    {
        //var delimiter = SH.PadRight(string.Empty, Environment.NewLine, 2);
        var p = SHSplit.SplitChar(c, AllChars.whiteSpacesChars.ToArray());
        while (c.Length + p.Count > maxLength)
        {
            if (p.Count > 1)
            {
                p.RemoveAt(p.Count - 1);
                c = string.Join(AllStrings.space, p);
            }
            else
            {
                //c = SHSubstring.SubstringIfAvailable(c, maxLength); 
                c = c.Substring(0, maxLength);
                break;
            }
        }
        if (maxLength < c.Length)
        {
        }
        return c;
    }
    public static T ToNumber<T>(Func<string, T> parse, string v)
    {
        return parse.Invoke(v);
    }
    public static string RepairQuotes(string c)
    {
        c = c.Replace(AllStrings.lq, AllStrings.qm);
        c = c.Replace(AllStrings.rq, AllStrings.qm);
        c = c.Replace(AllStrings.la, AllStrings.apostrophe);
        c = c.Replace(AllStrings.ra, AllStrings.apostrophe);
        return c;
    }
    public static bool IsNumbered(string v)
    {
        int i = 0;
        foreach (var item in v)
        {
            if (char.IsNumber(item))
            {
                i++;
                continue;
            }
            else if (item == AllChars.dot)
            {
                if (i > 0)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public static string InsertEndingBracket(string v, char startingBracket)
    {
        var cb = ClosingBracketFor(startingBracket);
        var occB = SH.ReturnOccurencesOfString(v, startingBracket.ToString());
        var occE = SH.ReturnOccurencesOfString(v, cb.ToString());
        return InsertEndingBracket(v, occB, occE, startingBracket);
    }
    private static string InsertEndingBracket(string songName, List<int> countStart, List<int> countEnd, char startingBracket)
    {
        return InsertEndingBracketWorker(songName, countStart.Count, countEnd.Count, new List<char>(), startingBracket);
    }
    public static string InsertEndingBracket(string songName, List<char> countStart, List<char> countEnd)
    {
        return InsertEndingBracketWorker(songName, countStart.Count, countEnd.Count, countStart, char.MaxValue);
    }
    public static string InsertEndingBracketWorker(string songName, int countStartCount, int countEndCount, List<char> countStart, char startingBracket)
    {
        var min = Math.Min(countStartCount, countEndCount);
        var max = Math.Max(countStartCount, countEndCount);
        if (countStartCount < countEndCount)
        {
            return songName;
        }
        if (startingBracket != char.MaxValue)
        {
            var to = max - min;
            countStart.Clear();
            for (int i = 0; i < to; i++)
            {
                countStart.Add(startingBracket);
            }
        }
        songName = InsertEndingBrackets(songName, countStart, min, max);
        return songName;
    }
    private static string InsertEndingBrackets(string songName, List<char> countStart, int min, int max)
    {
        var to = max - 1;
        var ml = songName.Contains(Environment.NewLine);
        for (int i = min; i < to; i++)
        {
            if (ml)
            {
                songName += Environment.NewLine;
            }
            songName += bracketsRight[SH.GetBracketFromBegin(countStart[i])];
        }
        return songName;
    }
    public static string PairsBracketsToCompleteBlock(string input)
    {
#if DEBUG
        if (input.Contains("name, price,"))
        {
        }
#endif
        List<char> add = new List<char>();
        foreach (var item in input)
        {
            if (bracketsLeftList.Contains(item))
            {
                add.Add(item);
            }
            if (bracketsRightList.Contains(item))
            {
                Brackets b = GetBracketFromBegin(item);
                var dx = add.IndexOf(bracketsLeft[b]);
                if (dx != -1)
                {
                    add.RemoveAt(dx);
                }
            }
        }
        StringBuilder sb = new StringBuilder(input);
        if (add.Count > 0)
        {
            sb.AppendLine();
            for (int i = add.Count - 1; i >= 0; i--)
            {
                Brackets b = GetBracketFromBegin(add[i]);
                sb.Append(bracketsRight[b]);
            }
            sb.Append(AllChars.sc);
        }
        var result = sb.ToString();
        if (input == result)
        {
            result = result.TrimEnd(AllChars.comma);
        }
        return result.ToUnixLineEnding();
    }
    private static Brackets GetBracketFromBegin(char v)
    {
        bool end = false;
        return GetBracketFromBegin(v, ref end, false);
    }
    private static Brackets GetBracketFromBegin(char v, ref bool end, bool throwExIsNotBracket)
    {
        switch (v)
        {
            case '(':
                return Brackets.Normal;
            case '{':
                return Brackets.Curly;
            case '[':
                return Brackets.Square;
            case ')':
                return Brackets.Normal;
            case '}':
                return Brackets.Curly;
            case ']':
                return Brackets.Square;
            default:
                if (throwExIsNotBracket)
                {
                    ThrowEx.NotImplementedCase(v);
                }
                break;
        }
        return Brackets.None;
    }
    public static List<char> IncludeBrackets(string s, bool starting)
    {
        List<char> containsBracket = new List<char>();
        if (starting)
        {
            foreach (var item in s)
            {
                if (SH.bracketsLeftList.Contains(item))
                {
                    containsBracket.Add(item);
                }
            }
        }
        else
        {
            foreach (var item in s)
            {
                if (SH.bracketsRightList.Contains(item))
                {
                    containsBracket.Add(item);
                }
            }
        }
        return containsBracket;
    }
    public static bool IsValidISO(string input)
    {
        // ISO-8859-1 je to samé jako latin1 https://en.wikipedia.org/wiki/ISO/IEC_8859-1
        byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
        String result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
        return String.Equals(input, result);
    }
    private static bool IsInFirstXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (int i = 0; i < pl; i++)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private static string ShortForLettersCount(string p, int p_2, out bool pridatTriTecky)
    {
        pridatTriTecky = false;
        // Vše tu funguje výborně
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;
        if (jeDelsiA1)
        {
            if (SH.IsInFirstXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;
                int napocitano = 0;
                for (int i = 0; i < to; i++)
                {
                    napocitano++;
                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }
                        dexMezery = i;
                    }
                }
                d = d.Substring(0, dexMezery + 1);
                if (d.Trim() != "")
                {
                    pridatTriTecky = true;
                    //d = d ;
                }
                return d;
                //}
            }
            else
            {
                pridatTriTecky = true;
                return p.Substring(0, p_2);
            }
        }
        return p;
    }
    public static bool ContainsOnlyCase(string between, bool upper, bool ignoreOtherThanLetters = false)
    {
        bool isLetter = false;
        foreach (var item in between)
        {
            isLetter = char.IsLetter(item);
            if (isLetter || (!isLetter && ignoreOtherThanLetters))
            {
                if (upper)
                {
                    if (!char.IsUpper(item))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!char.IsLower(item))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    public static string ShortForLettersCount(string p, int p_2)
    {
        bool pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
    }
    /// <summary>
    /// Insert prefix starting with +
    /// </summary>
    /// <param name="v"></param>
    public static string TelephonePrefixToBrackets(string v)
    {
        if (string.IsNullOrWhiteSpace(v))
        {
            return string.Empty;
        }
        v = NormalizeString(v);
        var p = SHSplit.Split(v, AllStrings.space);
        p[0] = AllStrings.lb + p[0] + AllStrings.rb;
        return string.Join(AllStrings.space, p);
    }
    public static bool ContainsVariable(string innerHtml)
    {
        return ContainsVariable(AllChars.lcub, AllChars.rcub, innerHtml);
    }
    public static bool ContainsVariable(char p, char k, string innerHtml)
    {
        if (string.IsNullOrEmpty(innerHtml))
        {
            return false;
        }
        StringBuilder sbNepridano = new StringBuilder();
        StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;
        foreach (var item in innerHtml)
        {
            if (item == p)
            {
                inVariable = true;
                continue;
            }
            else if (item == k)
            {
                if (inVariable)
                {
                    inVariable = false;
                }
                int nt = 0;
                if (int.TryParse(sbNepridano.ToString(), out nt))
                {
                    return true;
                }
                else
                {
                    sbPridano.Append(p + sbNepridano.ToString() + k);
                }
                sbNepridano.Clear();
            }
            else if (inVariable)
            {
                sbNepridano.Append(item);
            }
            else
            {
                sbPridano.Append(item);
            }
        }
        return false;
    }
    public static List<int> GetVariablesInString(string innerHtml)
    {
        return GetVariablesInString(AllChars.lcub, AllChars.rcub, innerHtml);
    }
    /// <param name="ret"></param>
    /// <param name="pocetDo"></param>
    public static List<int> GetVariablesInString(char p, char k, string innerHtml)
    {
        /// Vrátí mi formáty, které jsou v A1 od 0 do A2-1
        /// A1={0} {2} {3} A2=3 G=0,2
        List<int> vr = new List<int>();
        StringBuilder sbNepridano = new StringBuilder();
        //StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;
        foreach (var item in innerHtml)
        {
            if (item == p)
            {
                inVariable = true;
                continue;
            }
            else if (item == k)
            {
                if (inVariable)
                {
                    inVariable = false;
                }
                int nt = 0;
                if (int.TryParse(sbNepridano.ToString(), out nt))
                {
                    vr.Add(nt);
                }
                sbNepridano.Clear();
            }
            else if (inVariable)
            {
                sbNepridano.Append(item);
            }
        }
        return vr;
    }
    /// <summary>
    /// Really return list, for string join value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="p2"></param>
    public static List<string> RemoveDuplicates(string input, string delimiter)
    {
        var split = SHSplit.Split(input, delimiter);
        return split.Distinct().ToList();
        //return CAG.RemoveDuplicitiesList(new List<string>(split));
    }
    /// <summary>
    /// G zda je jedinz znak v A1 s dia.
    /// </summary>
    public static bool ContainsDiacritic(string slovo)
    {
        return slovo != SH.TextWithoutDiacritic(slovo);
    }
    /// <summary>
    /// Snaž se tuto metodu využívat co nejméně, je zbytečná.
    /// </summary>
    /// <param name="s"></param>
    public static string Copy(string s)
    {
        return s;
    }
    /// <summary>
    /// Pokud je poslední znak v A1 A2, odstraním ho
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="p"></param>
    public static string ConvertPluralToSingleEn(string nazevTabulky)
    {
        if (nazevTabulky[nazevTabulky.Length - 1] == 's')
        {
            if (nazevTabulky[nazevTabulky.Length - 2] == 'e')
            {
                if (nazevTabulky[nazevTabulky.Length - 3] == 'i')
                {
                    return nazevTabulky.Substring(0, nazevTabulky.Length - 3) + "y";
                }
            }
            return nazevTabulky.Substring(0, nazevTabulky.Length - 1);
        }
        return nazevTabulky;
    }
    // takhle to bylo předtím ale teď to tu mám 2x se stejnými parametry
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static string WrapWith(string value, char v, bool _trimWrapping = false)
    //{
    //    // TODO: Make with StringBuilder, because of SH.WordAfter and so
    //    return WrapWith(value, v.ToString(), _trimWrapping);
    //}
    /// <summary>
    /// Vše tu funguje výborně
    /// Metoda pokud chci vybrat ze textu A1 posledních p_2 znaků které jsou v celku(oddělené mezerami) a vložit před ně ...
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public static string ShortForLettersCountThreeDots(string p, int p_2)
    {
        bool pridatTriTecky = false;
        string vr = ShortForLettersCount(p, p_2, out pridatTriTecky);
        if (pridatTriTecky)
        {
            vr += " ... ";
        }
        vr = vr.Replace(AllStrings.bs, string.Empty);
        return vr;
    }
    public static bool ContainsOtherChatThanLetterAndDigit(string p)
    {
        foreach (char item in p)
        {
            if (!char.IsLetterOrDigit(item))
            {
                return true;
            }
        }
        return false;
    }
    public static string GetOddIndexesOfWord(string hash)
    {
        int polovina = hash.Length / 2;
        polovina = (polovina / 2);
        polovina += polovina / 2;
        StringBuilder sb = new StringBuilder(polovina);
        int pricist = 2;
        for (int i = 0; i < hash.Length; i += pricist)
        {
            sb.Append(hash[i]);
        }
        return sb.ToString();
    }
    #region GetPartsByLocation
    #endregion
    ///// <summary>
    ///// This can be only one
    ///// </summary>
    ///// <param name="delimiter"></param>
    ///// <param name="parts"></param>
    //public static string JoinIList(object delimiter, IList parts)
    //{
    //    return se.SHJoin.JoinIList(delimiter, parts);
    //}
    /// <summary>
    /// Údajně detekuje i japonštinu a podpobné jazyky
    /// </summary>
    /// <param name="text"></param>
    public static bool IsChinese(string text)
    {
        var hiragana = GetCharsInRange(text, 0x3040, 0x309F);
        if (hiragana)
        {
            return true;
        }
        var katakana = GetCharsInRange(text, 0x30A0, 0x30FF);
        if (katakana)
        {
            return true;
        }
        var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF);
        if (kanji)
        {
            return true;
        }
        if (text.Any(c => c >= 0x20000 && c <= 0xFA2D))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Nevraci znaky na indexech ale zda nektere znaky maji rozsah char definovany v A2,3
    /// </summary>
    /// <param name="text"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static bool GetCharsInRange(string text, int min, int max)
    {
        return text.Where(e => e >= min && e <= max).Count() != 0;
    }
    ///// <param name="nazevPP"></param>
    ///// <param name="only"></param>
    //public static string FirstCharUpper(string nazevPP, bool only = false)
    //{
    //    return se.SH.FirstCharUpper(nazevPP, only);
    //}
    public static List<string> RemoveDuplicatesNone(string p1, string delimiter)
    {
        var split = SHSplit.SplitNone(p1, delimiter);
        split = split.Distinct().ToList();
        return split;
    }
    /// <summary>
    /// Most of Love me like you do have in title - from Fifty shades of grey
    /// </summary>
    /// <param name="title"></param>
    /// <param name="squareBrackets"></param>
    /// <param name="parentheses"></param>
    /// <param name="braces"></param>
    /// <param name="afterSds"></param>
    public static string RemoveBracketsAndHisContent(string title, bool squareBrackets, bool parentheses, bool braces, bool afterSdsFrom)
    {
        if (squareBrackets)
        {
            title = RemoveBetweenAndEdgeChars(title, AllStrings.rsqb, AllStrings.lsqb);
        }
        if (parentheses)
        {
            title = RemoveBetweenAndEdgeChars(title, AllStrings.lb, AllStrings.rb);
        }
        if (braces)
        {
            title = RemoveBetweenAndEdgeChars(title, AllStrings.lcub, AllStrings.rcub);
        }
        if (afterSdsFrom)
        {
            var dex = title.IndexOf(" - from");
            if (dex == -1)
            {
                dex = title.IndexOf(SunamoNotTranslateAble.From);
            }
            if (dex != -1)
            {
                title = title.Substring(0, dex + 1);
            }
        }
        title = title.Replace(AllStrings.doubleSpace, string.Empty).Trim(); //SHReplace.ReplaceAll(title, "", AllStrings.doubleSpace).Trim();
        return title;
    }
    /// <summary>
    /// A2,3 can be string or char
    /// </summary>
    /// <param name="s"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static string RemoveBetweenAndEdgeChars(string s, string begin, string end)
    {
        Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
        return regex.Replace(s, string.Empty);
    }
    /// <summary>
    /// Je dobré před voláním této metody převést bílé znaky v A1 na mezery
    /// </summary>
    /// <param name="celyObsah"></param>
    /// <param name="stred"></param>
    /// <param name="naKazdeStrane"></param>
    public static string XCharsBeforeAndAfterWholeWords(string celyObsah, int stred, int naKazdeStrane)
    {
        StringBuilder prava = new StringBuilder();
        StringBuilder slovo = new StringBuilder();
        // Teď to samé udělám i pro levou stranu
        StringBuilder leva = new StringBuilder();
        for (int i = stred - 1; i >= 0; i--)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    leva.Insert(0, ts + AllStrings.space);
                    if (leva.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                slovo.Insert(0, ch);
            }
        }
        string l = slovo.ToString() + AllStrings.space + leva.ToString().TrimEnd(AllChars.space);
        l = l.TrimEnd(AllChars.space);
        naKazdeStrane += naKazdeStrane - l.Length;
        slovo.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (int i = stred; i < celyObsah.Length; i++)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    prava.Append(AllStrings.space + ts);
                    if (prava.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                slovo.Append(ch);
            }
        }
        string p = prava.ToString().TrimStart(AllChars.space) + AllStrings.space + slovo.ToString();
        p = p.TrimStart(AllChars.space);
        string vr = "";
        if (celyObsah.Contains(l + AllStrings.space) && celyObsah.Contains(AllStrings.space + p))
        {
            vr = l + AllStrings.space + p;
        }
        else
        {
            vr = l + p;
        }
        return vr;
    }
    /// <summary>
    /// Vše tu funguje výborně
    /// G text z A1, ktery bude obsahovat max A2 písmen - ne slov, protože někdo tam může vložit příliš dlouhé slova a nevypadalo by to pak hezky.
    ///
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public static string ShortForLettersCountThreeDotsReverse(string p, int p_2)
    {
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;
        if (jeDelsiA1)
        {
            if (SH.IsInLastXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;
                int napocitano = 0;
                for (int i = to - 1; i >= 0; i--)
                {
                    napocitano++;
                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }
                        dexMezery = i;
                    }
                }
                d = d.Substring(dexMezery + 1);
                if (d.Trim() != "")
                {
                    d = " ... " + d;
                }
                return d;
                //}
            }
            else
            {
                return " ... " + p.Substring(p.Length - p_2);
            }
        }
        return p;
    }
    public static List<FromToWord> ReturnOccurencesOfStringFromToWord(string celyObsah, params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0)
        {
            return new List<FromToWord>();
        }
        celyObsah = celyObsah.ToLower();
        List<FromToWord> vr = new List<FromToWord>();
        int l = celyObsah.Length;
        for (int i = 0; i < l; i++)
        {
            foreach (string item in hledaneSlova)
            {
                bool vsechnoStejne = true;
                int pridat = 0;
                while (pridat < item.Length)
                {
                    int dex = i + pridat;
                    if (l > dex)
                    {
                        if (celyObsah[dex] != item[pridat])
                        {
                            vsechnoStejne = false;
                            break;
                        }
                    }
                    else
                    {
                        vsechnoStejne = false;
                        break;
                    }
                    pridat++;
                }
                if (vsechnoStejne)
                {
                    FromToWord ftw = new FromToWord();
                    ftw.from = i;
                    ftw.to = i + pridat - 1;
                    ftw.word = item;
                    vr.Add(ftw);
                    i += pridat;
                    break;
                }
            }
        }
        return vr;
    }
    private static bool IsInLastXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (int i = p.Length - 1; i >= pl; i--)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;
    }
    //
    /// <summary>
    /// Oddělovač může být pouze jediný znak, protože se to pak předává do metody s parametrem int!
    /// If A1 dont have index A2, all chars
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="deli"></param>
    public static string GetFirstPartByLocation(string p1, char deli)
    {
        int dx = p1.IndexOf(deli);
        return GetFirstPartByLocation(p1, dx);
    }
    public static string GetFirstPartByLocation(string p1, int dx)
    {
        string p, z;
        p = p1;
        if (dx < p1.Length)
        {
            GetPartsByLocation(out p, out z, p1, dx);
        }
        return p;
    }
    /// <summary>
    /// return whether A1 ends with anything with A2
    /// </summary>
    /// <param name="source"></param>
    /// <param name="p2"></param>
    public static bool EndsWithArray(string source, params string[] p2)
    {
        foreach (var item in p2)
        {
            if (source.EndsWith(item))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Auto trim
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <returns></returns>
    public static string GetTextBetweenSimple(string p, string after, string before, bool throwExceptionIfNotContains = true)
    {
        int dxOfFounded = int.MinValue;
        var t = GetTextBetween(p, after, before, out dxOfFounded, 0, throwExceptionIfNotContains);
        return t;
    }
    /// <summary>
    /// Auto trim
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="dxOfFounded"></param>
    /// <param name="startSearchingAt"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <returns></returns>
    public static string GetTextBetween(string p, string after, string before, out int dxOfFounded, int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string vr = null;
        dxOfFounded = p.IndexOf(after, startSearchingAt);
        int p3 = p.IndexOf(before, dxOfFounded + after.Length);
        bool b2 = dxOfFounded != -1;
        bool b3 = p3 != -1;
        if (b2 && b3)
        {
            dxOfFounded += after.Length;
            p3 -= 1;
            // When I return between ( ), there must be +1
            var length = p3 - dxOfFounded + 1;
            if (length < 1)
            {
                // Takhle to tu bylo předtím ale logicky je to nesmysl.
                //return p;
            }
            vr = p.Substring(dxOfFounded, length).Trim();
        }
        else
        {
            if (throwExceptionIfNotContains)
            {
                ThrowEx.NotContains(p, after, before);
            }
            else
            {
                // 24-1-21 return null instead of p
                return null;
                //vr = p;
            }
        }
        return vr.Trim();
    }
    public static bool EndsWith(string input, string endsWith)
    {
        return input.EndsWith(endsWith);
    }
    public static bool RemovePrefix(ref string s, string v)
    {
        if (s.StartsWith(v))
        {
            s = s.Substring(v.Length);
            return true;
        }
        return false;
    }
    public static string GetToFirstChar(string input, int indexOfChar)
    {
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
    }
    /// <summary>
    /// Tato metoda byla výchozí, jen se jmenovala NullToString
    /// OrEmpty pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="v"></param>
    public static string NullToStringOrEmpty(object v)
    {
        if (v == null)
        {
            return "";
        }
        var s = v.ToString();
        return s;
    }
    public static bool ContainsFromEnd(string p1, char p2, out int ContainsFromEndResult)
    {
        for (int i = p1.Length - 1; i >= 0; i--)
        {
            if (p1[i] == p2)
            {
                ContainsFromEndResult = i;
                return true;
            }
        }
        ContainsFromEndResult = -1;
        return false;
    }
    public static string FirstWhichIsNotEmpty(params string[] s)
    {
        foreach (var item in s)
        {
            if (item != "")
            {
                return item;
            }
        }
        return "";
    }
    /// <summary>
    /// Whether A1 is under A2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mask"></param>
    public static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, AllChars.q, AllChars.asterisk);
    }
    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat)
        {
            return true;
        }
        string escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        string escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, AllStrings.dot);
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        Regex reg = new Regex(pat);
        return reg.IsMatch(str);
    }
    /// <summary>
    /// Return joined with space
    /// </summary>
    /// <param name="v"></param>
    public static string FirstCharOfEveryWordUpperDash(string v)
    {
        return FirstCharOfEveryWordUpper(v, AllChars.dash);
    }
    /// <summary>
    /// Return joined with space
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dash"></param>
    private static string FirstCharOfEveryWordUpper(string v, char dash)
    {
        var p = SHSplit.SplitChar(v, dash);
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = SH.FirstCharUpper(p[i]);
        }
        //p = CAChangeContent.ChangeContent0(null, p, FirstCharUpper);
        return string.Join(AllStrings.space, p);
    }
    public static bool IsNullOrWhiteSpace(string s)
    {
        if (s != null)
        {
            s = s.Trim();
            return s == "";
        }
        return true;
    }
    public static bool HasTextRightFormat(string r, TextFormatData tfd)
    {
        if (tfd.trimBefore)
        {
            r = r.Trim();
        }
        long tfdOverallLength = 0;
        foreach (var item in tfd)
        {
            tfdOverallLength += (item.fromTo.to - item.fromTo.from) + 1;
        }
        int partsCount = tfd.Count;
        int actualCharFormatData = 0;
        CharFormatData actualFormatData = tfd[actualCharFormatData];
        CharFormatData followingFormatData = tfd[actualCharFormatData + 1];
        //int charCount = r.Length;
        //if (tfd.requiredLength != -1)
        //{
        //    if (r.Length != tfd.requiredLength)
        //    {
        //        return false;
        //    }
        //    charCount = Math.Min(r.Length, tfd.requiredLength);
        //}
        int actualChar = 0;
        int processed = 0;
        long from = actualFormatData.fromTo.FromL;
        long remains = actualFormatData.fromTo.ToL;
        int tfdCountM1 = tfd.Count - 1;
        while (true)
        {
            bool canBeAnyChar = actualFormatData.mustBe == null || actualFormatData.mustBe.Length == 0; //SunamoCollectionsShared.CASH.IsEmptyOrNull();
            bool isRightChar = false;
            if (canBeAnyChar)
            {
                isRightChar = true;
                remains--;
            }
            else
            {
                if (r.Length <= actualChar)
                {
                    return false;
                }
                isRightChar = actualFormatData.mustBe.Any(d => d == r[actualChar]); //CAGSH.IsEqualToAnyElement<char>(, );
                if (isRightChar && !canBeAnyChar)
                {
                    actualChar++;
                    processed++;
                    remains--;
                }
            }
            if (!isRightChar)
            {
                if (r.Length <= actualChar)
                {
                    return false;
                }
                isRightChar = followingFormatData.mustBe.Any(d => d == r[actualChar]);  //CAGSH.IsEqualToAnyElement<char>(, );
                if (!isRightChar)
                {
                    return false;
                }
                if (remains != 0 && processed < from)
                {
                    return false;
                }
                if (isRightChar && !canBeAnyChar)
                {
                    actualCharFormatData++;
                    processed++;
                    actualChar++;
                    if (!CASH.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar)
                    {
                        return false;
                    }
                    actualFormatData = tfd[actualCharFormatData];
                    if (CASH.HasIndex(actualCharFormatData + 1, tfd))
                    {
                        followingFormatData = tfd[actualCharFormatData + 1];
                    }
                    else
                    {
                        followingFormatData = CharFormatData.Templates.Any;
                    }
                    processed = 0;
                    remains = actualFormatData.fromTo.to;
                    remains--;
                }
            }
            if (actualChar == tfdOverallLength)
            {
                if (actualChar == r.Length)
                {
                    //break;
                    return true;
                }
            }
            if (remains == 0)
            {
                ++actualCharFormatData;
                if (!CASH.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar)
                {
                    return false;
                }
                actualFormatData = tfd[actualCharFormatData];
                if (CASH.HasIndex(actualCharFormatData + 1, tfd))
                {
                    followingFormatData = tfd[actualCharFormatData + 1];
                }
                else
                {
                    followingFormatData = CharFormatData.Templates.Any;
                }
                processed = 0;
                remains = actualFormatData.fromTo.to;
            }
        }
    }
    /// <param name="text"></param>
    /// <param name="append"></param>
    public static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append))
        {
            return text;
        }
        return text + append;
    }
    #endregion
    #region MyRegion
    #endregion
}
