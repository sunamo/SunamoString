namespace SunamoString;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class SH
{
    /// <summary>
    /// List of left bracket characters.
    /// </summary>
    protected static List<char> BracketsLeftList { get; set; } = null!;
    /// <summary>
    /// List of right bracket characters.
    /// </summary>
    protected static List<char> BracketsRightList { get; set; } = null!;
    private static StringBuilder StringBuilder { get; set; } = new();
    /// <summary>
    /// Exception message constant.
    /// </summary>
    public static string XMismatchCountInInputArraysOfSHAllHaveRightFormat =
        "MismatchCountInInputArraysOfSHAllHaveRightFormat";

    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsCl(string input, StringOrStringList searchTerm, SearchStrategy searchStrategy = SearchStrategy.FixedSpace, bool caseSensitive = false, bool isEnoughPartialContainsOfSplitted = true)
    {
        string? term = null;
        if (!caseSensitive)
        {
            input = input.ToLower();
            term = searchTerm.GetString().ToLower();
        }
        // musel bych dotáhnout min 2 metody argument další enumy
        if (searchStrategy == SearchStrategy.ExactlyName)
        {
            return input == term;
        }
        if (searchStrategy == SearchStrategy.AnySpaces)
        {
            var inputParts = input.Split(input.Where(ch => !char.IsLetterOrDigit(ch)).ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var termParts = searchTerm.GetList();
            if (inputParts.Length == 1)
            {
                foreach (var termToCheck in termParts)
                {
                    if (!input.Contains(termToCheck))
                    {
                        return false;
                    }
                }
            }
            if (isEnoughPartialContainsOfSplitted)
            {
                foreach (var termToSearch in termParts)
                {
                    if (!input.Contains(termToSearch))
                    {
                        return false;
                    }
                }
                return true;
            }
            bool containsAll = true;
            foreach (var part in termParts)
            {
                if (!inputParts.Contains(part))
                {
                    containsAll = false;
                    break;
                }
            }
            return containsAll;
        }
        return input.Contains(term!);
    }
    /// <summary>
    /// Extracts whitespace characters from the specified position.
    /// </summary>
    public static string WhiteSpaceFromStart(string input)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in input)
            if (char.IsWhiteSpace(character))
                stringBuilder.Append(character);
            else
                break;
        return stringBuilder.ToString();
    }
    /// <summary>
    ///     FixedSpace - Contains
    ///     AnySpaces - split input by spaces and A1 must contains all parts
    ///     ExactlyName - Is exactly the same
    /// </summary>
    /// <param name="input"></param>
    /// <param name="term"></param>
    /// <param name="enoughIsContainsAttribute"></param>
    /// <param name="caseSensitive"></param>
    public static bool ContainsBoolBool(string input, string term, bool enoughIsContainsAttribute, bool caseSensitive)
    {
        return Contains(input, term, enoughIsContainsAttribute ? SearchStrategy.AnySpaces : SearchStrategy.ExactlyName,
            caseSensitive);
    }
    /// <summary>
    ///     AnySpaces - split A2 by spaces and A1 must contains all parts
    ///     ExactlyName - ==
    ///     FixedSpace - simple contains
    /// </summary>
    /// <param name="input"></param>
    /// <param name="term"></param>
    /// <param name="searchStrategy"></param>
    /// <param name="caseSensitive"></param>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy, bool caseSensitive)
    {
        if (term != "")
        {
            if (searchStrategy == SearchStrategy.ExactlyName)
            {
                if (caseSensitive)
                    return input == term;
                return input.ToLower() == term.ToLower();
            }
            if (searchStrategy == SearchStrategy.FixedSpace)
            {
                if (caseSensitive)
                    return input.Contains(term);
                return input.ToLower().Contains(term.ToLower());
            }
            if (caseSensitive)
            {
                var allWords = term.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList(); // SHSplit.Split(term, );
                return ContainsAll(input, allWords);
            }
            else
            {
                var allWords = term.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList(); // SHSplit.Split(term, "");
                for (var i = 0; i < allWords.Count; i++) allWords[i] = allWords[i].ToLower();
                return ContainsAll(input.ToLower(), allWords);
            }
        }
        return false;
    }
    /// <summary>
    ///     Auto remove potentially first !
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="contains"></param>
    public static bool IsContained(string input, ref string contains)
    {
        var (negation, trimmedContains) = IsNegationTuple(contains);
        contains = trimmedContains;
        if (negation && input.Contains(contains))
            return false;
        if (!negation && !input.Contains(contains)) return false;
        return true;
    }
    /// <summary>
    ///     Return whether A1 contains all from A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="allWords"></param>
    public static bool ContainsAll(string input, IList<string> allWords,
        ContainsCompareMethodString ccm = ContainsCompareMethodString.WholeInput)
    {
        if (ccm == ContainsCompareMethodString.SplitToWords)
        {
            foreach (var word in allWords)
                if (!input.Contains(word))
                    return false;
        }
        else if (ccm == ContainsCompareMethodString.Negations)
        {
            foreach (var word in allWords)
            {
                var searchTerm = word;
                if (!IsContained(input, ref searchTerm)) return false;
            }
        }
        else if (ccm == ContainsCompareMethodString.WholeInput)
        {
            foreach (var word in allWords)
                if (!input.Contains(word))
                    return false;
        }
        return true;
    }
    /// <summary>
    ///     AnySpaces - split A2 by spaces and A1 must contains all parts
    ///     ExactlyName - ==
    ///     FixedSpace - simple contains
    ///     A1 = search for exact occur. otherwise split both to words
    ///     Control for string.Empty, because otherwise all results are true
    /// </summary>
    /// <param name="input"></param>
    /// <param name="what"></param>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        return Contains(input, term, searchStrategy, true);
    }
    /// <summary>
    /// Adds a prefix to the string if not already present.
    /// </summary>
    public static string PrefixIfNotStartedWith(string item, string prefix, bool skipWhitespaces = false)
    {
        var whitespaces = string.Empty;
        if (skipWhitespaces)
        {
            whitespaces = WhiteSpaceFromStart(item);
            item = item.Substring(whitespaces.Length);
        }
        if (!item.StartsWith(prefix)) return whitespaces + prefix + item;
        return whitespaces + item;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveLastChar(string input)
    {
        return input.Substring(0, input.Length - 1);
    }
    /// <summary>
    ///     Add postfix if text not ends with
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="postfix"></param>
    /// <returns></returns>
    public static string PostfixIfNotEmpty(string input, string postfix)
    {
        if (input.Length != 0)
            if (!input.EndsWith(postfix))
                return input + postfix;
        return input;
    }
    /// <summary>
    /// Adds specified content to the string.
    /// </summary>
    public static string AddBeforeUpperChars(string input, char add, bool preserveAcronyms)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        var newText = new StringBuilder(input.Length * 2);
        newText.Append(input[0]);
        for (var i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
                if ((input[i - 1] != add && !char.IsUpper(input[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(input[i - 1]) &&
                     i < input.Length - 1 && !char.IsUpper(input[i + 1])))
                    newText.Append(add);
            newText.Append(input[i]);
        }
        return newText.ToString();
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveEndingPairCharsWhenDontHaveStarting(string input, string leftBracket, string rightBracket)
    {
        var removeOnIndexes = new List<int>();
        var stringBuilder = new StringBuilder(input);
        var leftBracketOccurrences = ReturnOccurencesOfString(input, leftBracket);
        var rightBracketOccurrences = ReturnOccurencesOfString(input, rightBracket);
        List<int>? unmatchedLeftBrackets = null;
        List<int>? unmatchedRightBrackets = null;
        var list = GetPairsStartAndEnd(leftBracketOccurrences, rightBracketOccurrences, ref unmatchedLeftBrackets, ref unmatchedRightBrackets);
        unmatchedLeftBrackets!.AddRange(unmatchedRightBrackets!);
        unmatchedLeftBrackets.Sort();
        for (var i = unmatchedLeftBrackets.Count - 1; i >= 0; i--) stringBuilder.Remove(unmatchedLeftBrackets[i], 1);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public static List<Tuple<int, int>> GetPairsStartAndEnd(List<int> leftBracketOccurrences, List<int> rightBracketOccurrences, ref List<int>? unmatchedLeftBrackets,
        ref List<int>? unmatchedRightBrackets)
    {
        var list = new List<Tuple<int, int>>();
        unmatchedLeftBrackets = leftBracketOccurrences.ToList();
        unmatchedRightBrackets = rightBracketOccurrences.ToList();
        for (var i = rightBracketOccurrences.Count - 1; i >= 0; i--)
        {
            var lastRight = rightBracketOccurrences[i];
            if (leftBracketOccurrences.Count == 0) break;
            var lastLeft = leftBracketOccurrences.Last();
            if (lastRight < lastLeft)
            {
                i++;
                // Na konci přebývá lastLeft
                // unmatchedLeftBrackets.Add(lastLeft);
                // I will remove it on end
                leftBracketOccurrences.RemoveAt(leftBracketOccurrences.Count - 1);
            }
            else
            {
                // když je lastLeft menší, znamená to že last right má svůj levý protějšek
                list.Add(new Tuple<int, int>(lastLeft, lastRight));
            }
        }
        leftBracketOccurrences = unmatchedLeftBrackets;

        var addToAnotherCollection = new List<int>();
        var additionalPairs = new List<Tuple<int, int>>();
        var alreadyProcessedItem1 = new List<int>();
        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (alreadyProcessedItem1.Contains(list[i].Item1))
            {
                addToAnotherCollection.Add(list[i].Item1);
                additionalPairs.Add(list[i]);
                list.RemoveAt(i);
                //continue;
            }
            alreadyProcessedItem1.Add(list[i].Item1);
        }

        addToAnotherCollection = addToAnotherCollection.Distinct().ToList();
        foreach (var index in addToAnotherCollection)
        {
            var input = alreadyProcessedItem1.Where(data => data == index).Count();
            //!alreadyProcessedItem1.Contains(item)
            if (input > 2)
            {
                var sele = additionalPairs.Where(data => data.Item1 == index).ToList();
                //for (int i = sele.Count() - 1; i >= 1; i--)
                //{
                //    additionalPairs.Remove(sele[i]);
                //}
                var leftBracketIndex = leftBracketOccurrences.IndexOf(sele[0].Item1);
                if (leftBracketIndex != -1)
                {
                    var pairIndex = list.IndexOf(sele[0]);
                    list.Add(new Tuple<int, int>(leftBracketOccurrences[leftBracketIndex - 1], sele[0].Item2));
                }
            }
        }
        //list.AddRange(additionalPairs);
        leftBracketOccurrences.Sort();
        var result = list; //list.OrderByDescending(data => data.Item1).ToList();
        //
        var alreadyProcessed = new List<int>();
        var foundIndex = -1;
        for (var yValue = 0; yValue < result.Count; yValue++)
        {
            var pairEntry = result[yValue];
            var startIndex = pairEntry.Item1;
            if (alreadyProcessed.Contains(startIndex))
            {
                foundIndex = leftBracketOccurrences.IndexOf(startIndex);
                if (foundIndex != -1)
                {
                    startIndex = leftBracketOccurrences[foundIndex - 1];
                    result[startIndex] = new Tuple<int, int>(startIndex, result[yValue - 1].Item2);
                }
            }
            alreadyProcessed.Add(startIndex);
        }
        unmatchedLeftBrackets = leftBracketOccurrences;
        unmatchedLeftBrackets = unmatchedLeftBrackets.Distinct().ToList();
        unmatchedRightBrackets = unmatchedRightBrackets.Distinct().ToList();
        foreach (var pair in result)
        {
            unmatchedLeftBrackets.Remove(pair.Item1);
            unmatchedRightBrackets.Remove(pair.Item2);
        }
        result.Reverse();
        return result;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveAndInsertReplace(string input, int startIndex, string oldValue, string newValue)
    {
        input = input.Remove(startIndex, oldValue.Length);
        input = input.Insert(startIndex, newValue);
        return input;
    }
    //public static string JoinMakeUpTo2NumbersToZero(string data, int[] d2)
    //{
    //    return data;
    //}
    /// <summary>
    /// Replaces content in the string.
    /// </summary>
    public static string ReplaceOnce(string input, string oldValue, string replacement)
    {
        if (oldValue == "") return input;
        var position = input.IndexOf(oldValue);
        if (position == -1) return input;
        return input.Substring(0, position) + replacement + input.Substring(position + oldValue.Length);
    }
    /// <summary>
    /// Replaces content in the string.
    /// </summary>
    public static string ReplaceOnceIfStartedWith(string input, string searchPrefix, string replacement)
    {
        bool replaced;
        return ReplaceOnceIfStartedWith(input, searchPrefix, replacement, out replaced);
    }
    /// <summary>
    /// Replaces content in the string.
    /// </summary>
    public static string ReplaceOnceIfStartedWith(string input, string searchPrefix, string replacement, out bool replaced)
    {
        replaced = false;
        if (input.StartsWith(searchPrefix))
        {
            replaced = true;
            return ReplaceOnce(input, searchPrefix, replacement);
        }
        return input;
    }
    /// <summary>
    /// Normalizes the string by standardizing its format.
    /// </summary>
    public static string NormalizeString(string input)
    {
        if (input.Contains((char)160))
        {
            var stringBuilder = new StringBuilder();
            foreach (var character in input)
                if (character == (char)160)
                    stringBuilder.Append(' ');
                else
                    stringBuilder.Append(character);
            return stringBuilder.ToString();
        }
        return input;
    }
    /// <summary>
    ///     IndexesOfChars - char
    ///     ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="vcem"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static List<int> ReturnOccurencesOfString(string searchText, string searchTerm)
    {
        var Results = new List<int>();
        for (var Index = 0; Index < searchText.Length - searchTerm.Length + 1; Index++)
        {
            var subs = searchText.Substring(Index, searchTerm.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            var firstCharOfSubstring = subs[0];
            var firstCharOfSearchTerm = searchTerm[0];
            if (subs == searchTerm)
                Results.Add(Index);
        }
        return Results;
    }
    /// <summary>
    /// Tab Or Space Next To operation on the input.
    /// </summary>
    public static List<int> TabOrSpaceNextTo(string input)
    {
        var tabs = ReturnOccurencesOfString(input, "\t");
        // nevím k čemu to tu je ale když jsem měl řetězec builder nopCommerce\tSimplCommerce\tSmartStoreNET\tgrandnode\tKartris tak mi to vrátilo navíc o 2 \t kde nikdy nebyly
        //for (int i = 0; i < tabs.Count-1; i++)
        //{
        //    var dx = tabs[i] + 1;
        //    if (input[i] == ' ')
        //    {
        //        tabs.Add(dx);
        //    }
        //}
        //for (int i = 1; i < tabs.Count; i++)
        //{
        //    var dx = tabs[i] - 1;
        //    if (input[i] == ' ')
        //    {
        //        tabs.Add(dx);
        //    }
        //}
        return tabs;
    }
    /// <summary>
    /// Wraps the string with the specified characters.
    /// </summary>
    public static string WrapWithBs(string input)
    {
        return WrapWithChar(input, '\\');
    }
    /// <summary>
    /// Wraps the string with the specified characters.
    /// </summary>
    public static string WrapWithSpace(string input)
    {
        return WrapWithChar(input, ' ');
    }
    /// <summary>
    /// Wraps the string with the specified characters.
    /// </summary>
    public static string WrapWithQm(string input)
    {
        return WrapWithQm(input, true);
    }
    /// <summary>
    /// Wraps the string with the specified characters.
    /// </summary>
    public static string WrapWithIf(string value, string wrapper, Func<string, string, bool> predicate)
    {
        if (predicate.Invoke(value, wrapper)) return WrapWith(value, wrapper);
        return value;
    }
    /// <summary>
    /// Wraps the string with the specified characters.
    /// </summary>
    public static string WrapWithQm(string input, bool alsoIfIsWhitespaceOrEmpty = true)
    {
        return WrapWithChar(input, '"', alsoIfIsWhitespaceOrEmpty);
    }
    /// <summary>
    /// Counts occurrences of the specified pattern in the string.
    /// </summary>
    public static int OccurencesOfStringIn(string source, string searchTerm)
    {
        return source.Split(new[] { searchTerm }, StringSplitOptions.None).Length - 1;
    }
    /// <summary>
    ///     Into A1,2 never put null
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <param name="strategy"></param>
    /// <param name="position"></param>
    public static void GetPartsByLocation(out string before, out string after, string input, int position)
    {
        if (position == -1)
        {
            before = input;
            after = "";
        }
        else
        {
            before = input.Substring(0, position);
            if (input.Length > position + 1)
                after = input.Substring(position + 1);
            else
                after = string.Empty;
        }
    }
    /// <summary>
    /// Get Parts By Location No Out Int on the input.
    /// </summary>
    public static (string, string) GetPartsByLocationNoOutInt(string input, int position)
    {
        string before, after;
        GetPartsByLocation(out before, out after, input, position);
        return (before, after);
    }
    /// <summary>
    /// Get Parts By Location No Out on the input.
    /// </summary>
    public static (string, string) GetPartsByLocationNoOut(string input, char delimiter)
    {
        GetPartsByLocation(out var pred, out var after, input, delimiter);
        return (pred, after);
    }
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <param name="input"></param>
    /// <param name="delimiter"></param>
    public static void GetPartsByLocation(out string before, out string after, string input, char delimiter)
    {
        var delimiterIndex = input.IndexOf(delimiter);
        GetPartsByLocation(out before, out after, input, delimiterIndex);
    }
    /// <summary>
    ///     Func<int, bool> / FromToList
    /// </summary>
    /// <param name="rangeChecker"></param>
    /// <param name="indexToTest"></param>
    /// <returns></returns>
    public static bool NotAllowedInRanges(object rangeChecker, int indexToTest)
    {
        if (rangeChecker is Func<int, bool>)
        {
            var temp = (Func<int, bool>)rangeChecker;
            return temp(indexToTest);
        }
        // nemůže tu být protože SunamoData musí dědit od SunamoStringShared - hodně metod *.
        //if (rangeChecker is FromToList)
        //{
        //    var result = (FromToList)rangeChecker;
        //    return result.IsInRange(indexToTest);
        //}
        ThrowEx.NotImplementedCase("NotAllowedInRanges: " + rangeChecker);
        return false;
    }
    /// <summary>
    ///     notAllowedInRanges can be Func
    ///     <int, bool>
    ///         (delegát který vrací zda daný index může být použít pro end) or FromToList
    ///         Used in: Metaproject.PackageIndex.Functions.ParseCsprojFile
    ///         Work like everybody excepts, from argument {b} count return builder
    ///         A5 is type FromToList but into SE could be only absolutely minimal code base
    /// </summary>
    /// <param name="p"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static string GetTextBetweenTwoChars(string parameter, char beginS, char endS,
        bool throwExceptionIfNotContains = true, object? notAllowedInRanges = null, bool endLastIndexOf = false)
    {
        var begin = parameter.IndexOf(beginS);
        var end = -1;
        if (endLastIndexOf)
        {
            end = parameter.LastIndexOf(endS);
        }
        else
        {
            end = parameter.IndexOf(endS, begin + 1);
            if (notAllowedInRanges != null)
                while (end != NumConsts.MOne && NotAllowedInRanges(notAllowedInRanges, end))
                    end = parameter.IndexOf(endS, end + 1);
        }
        if (begin == NumConsts.MOne || end == NumConsts.MOne)
        {
            if (throwExceptionIfNotContains)
            {
                ThrowEx.NotContains(parameter, beginS.ToString(), endS.ToString());
            }
            else
            {
                if (end == NumConsts.MOne) return null!;
            }
        }
        else
        {
            return GetTextBetweenTwoCharsInts(parameter, begin, end);
        }
        return parameter;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetTextBetweenTwoCharsInts(string parameter, int begin, int end)
    {
        if (end > begin)
            // argument(1) - 1,3
            return parameter.Substring(begin + 1, end - begin - 1);
        // originally
        //return parameter.Substring(begin+1, end - begin - 1);
        return parameter;
    }
    /// <summary>
    /// Processes or retrieves content from the beginning of the string.
    /// </summary>
    public static void FirstCharUpper(ref string input)
    {
        input = FirstCharUpper(input);
    }
    /// <summary>
    /// Processes or retrieves content from the beginning of the string.
    /// </summary>
    public static string FirstCharUpper(string input)
    {
        if (input.Length == 1) return input.ToUpper();
        var stringBuilder = input.Substring(1);
        return input[0].ToString().ToUpper() + stringBuilder;
    }
    /// <summary>
    ///     Musi mit sudy pocet prvku
    ///     Pokud sudý [0], [2], ... bude mít aspoň jeden nebílý znak, pak se přidá lichý [1], [3] i sudý ve dvojicích. jinak
    ///     nic
    /// </summary>
    /// <param name="className"></param>
    /// <param name="v1"></param>
    /// <param name="methodName"></param>
    /// <param name="v2"></param>
    public static string ConcatIfBeforeHasValue(params string[] className)
    {
        var result = new StringBuilder();
        for (var i = 0; i < className.Length; i++)
        {
            var even = className[i];
            if (!string.IsNullOrWhiteSpace(even))
                //string odd =
                result.Append(even + className[++i]);
        }
        return result.ToString();
    }
    /// <summary>
    /// Converts character encoding from one format to another.
    /// </summary>
    public static string FromSpace160To32(string input)
    {
        input = Regex.Replace(input, @"\p{Z}", " ");
        return input;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsNumber(string str, params char[] nextAllowedChars)
    {
        foreach (var character in str)
            if (!char.IsNumber(character))
                if (!nextAllowedChars.Contains(character))
                    return false;
        return true;
    }
    /// <summary>
    /// Formats the value to have the specified number of characters.
    /// </summary>
    public static string MakeUpToXChars(int parameter, int targetLength)
    {
        var stringBuilder = new StringBuilder();
        var data = parameter.ToString();
        var doplnit = (parameter.ToString().Length - targetLength) * -1;
        for (var i = 0; i < doplnit; i++) stringBuilder.Append(0);
        stringBuilder.Append(data);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static char GetFirstChar(string arg)
    {
        return arg[0];
    }
    /// <summary>
    /// Converts the string to the specified format.
    /// </summary>
    public static string ToPascalCase(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        // Rozdělení řetězce na slova
        var words = str.Split(' ');
        // Kapitalizace prvního písmene každého slova
        for (var i = 0; i < words.Length; i++)
            if (words[i].Length > 0)
                words[i] = words[i][0].ToString().ToUpper() + words[i].Substring(1);
        // Spojení slov do Pascal konvence
        return string.Join("", words);
    }
    /// <summary>
    /// Checks if the string starts with a whitespace character.
    /// </summary>
    public static bool StartWithWhitespace(string input)
    {
        // toto nefungovalo
        //return new List<char>(['\n', '\r', '\t', ' ']).Any(data => text.StartsWith(data));
        return input.TrimStart() != input;
    }
    /// <summary>
    /// Detects the newline format used in the string.
    /// </summary>
    public static string DetectNewline(string input)
    {
        if (input.Contains("\r\n")) return "\r\n";
        return "\n";
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveLastWord(string temp)
    {
        return SHParts.RemoveAfterLast(temp.Trim(), " ");
    }
    /// <summary>
    /// Get Indexes Of Lines Starting With operation on the input.
    /// </summary>
    public static List<int> GetIndexesOfLinesStartingWith(List<string> list, Func<string, bool> predicate)
    {
        var allIndices = list.Select((strategy, i) => new { Str = strategy, Index = i })
            .Where(x => predicate(x.Str))
            .Select(x => x.Index).ToList();
        return allIndices;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveLinesWhichContains(string parameter, string textToRemove)
    {
        var list = SHGetLines.GetLines(parameter);
        list = list.Where(data => !data.Contains(textToRemove)).ToList(); //CA.RemoveWhichContains(list, textToRemove, false);
        var result = string.Join(Environment.NewLine, list);
        return result;
    }
    /// <summary>
    /// Adds specified content to the string.
    /// </summary>
    public static string AddIfNotContains(string input, string textToAdd, string? lowerCaseVersion = null)
    {
        if (lowerCaseVersion != null)
        {
            textToAdd = lowerCaseVersion;
            input = input.ToLower();
        }
        if (!input.Contains(textToAdd)) return input + " " + textToAdd;
        return input;
    }
    /// <summary>
    /// Swaps parts of the string around the delimiter.
    /// </summary>
    public static string SwitchSwap(string input, string delimiter)
    {
        var parameter = SHSplit.Split(input, delimiter);
        if (parameter.Count == 2) return parameter[1] + "," + parameter[0];
        return null!;
    }
    /// <summary>
    /// Inserts content into the string at the specified position.
    /// </summary>
    public static string InsertBeforeEndingBracket(string postfixSpaceCommaNewline, string textToInsert)
    {
        var bracketIndex = postfixSpaceCommaNewline.LastIndexOf(')');
        if (bracketIndex != -1) return postfixSpaceCommaNewline.Insert(bracketIndex, textToInsert);
        return postfixSpaceCommaNewline;
    }
    /// <summary>
    /// Statistic Letter Chars operation on the input.
    /// </summary>
    public static Dictionary<char, int> StatisticLetterChars(string between, StatisticLetterCharsStrategy strategy,
        params char[] charsToStrategy)
    {
        List<char>? ignoreCompletely = null;
        if (strategy == StatisticLetterCharsStrategy.IgnoreCompletely) ignoreCompletely = new List<char>(charsToStrategy);
        var list = new Dictionary<char, int>();
        if (strategy == StatisticLetterCharsStrategy.AddAsFirst)
            foreach (var character in charsToStrategy)
                list.Add(character, 0);
        foreach (var character in between)
        {
            if (strategy == StatisticLetterCharsStrategy.IgnoreCompletely)
                if (ignoreCompletely!.Contains(character))
                    continue;
            DictionaryHelper.AddOrPlus(list, character, 1);
        }
        return list;
    }
    /// <summary>
    /// All Brackets operation on the input.
    /// </summary>
    public static List<char> AllBrackets(string strategy)
    {
        var bracketChars = new List<char>();
        var end = false;
        for (var i = 0; i < strategy.Length; i++)
        {
            var bracket = GetBracketFromBegin(strategy[i], ref end, false);
            if (bracket != Brackets.None) bracketChars.Add(strategy[i]);
        }
        return bracketChars;
    }
    /// <summary>
    /// Indexes Of Brackets operation on the input.
    /// </summary>
    public static Tuple<SquareMap, SquareMapLines> IndexesOfBrackets(string strategy)
    {
        var message = new SquareMap();
        var lineBasedMap = new SquareMapLines(message);
        var end = false;
        var line = 0;
        var result = false;
        for (var i = 0; i < strategy.Length; i++)
        {
            var currentChar = strategy[i];
            if (result)
            {
                result = false;
                line++;
                if (currentChar == '\n') continue;
            }
            if (currentChar == '\n')
            {
                line++;
                continue;
            }
            if (currentChar == '\r')
            {
                result = true;
                continue;
            }
            var bracket = GetBracketFromBegin(currentChar, ref end, false);
            if (bracket != Brackets.None)
            {
                message.Add(bracket, end, i);
                lineBasedMap.Add(bracket, end, i, line);
            }
        }
        return new Tuple<SquareMap, SquareMapLines>(message, lineBasedMap);
    }
    /// <summary>
    /// Replaces content in the string.
    /// </summary>
    public static string ReplaceBrackets(string strategy, Brackets from, Brackets to)
    {
        strategy = strategy.Replace(BracketsLeft[from], BracketsLeft[to]);
        strategy = strategy.Replace(BracketsRight[from], BracketsRight[to]);
        return strategy;
    }
    /// <summary>
    /// Contains Any From Element operation on the input.
    /// </summary>
    public static List<int> ContainsAnyFromElement(StringBuilder strategy, IList<string> list)
    {
        var result = new List<int>();
        var matchIndex = 0;
        foreach (var searchValue in list)
        {
            if (strategy.ToString().Contains(searchValue)) result.Add(matchIndex);
            matchIndex++;
        }
        return result;
    }
    /// <summary>
    /// Finds the specified element in the string.
    /// </summary>
    public static int FindClosingBracketIndexChar(StringBuilder strategy, bool removeBetween, string openedBracket = "{")
    {
        var index = strategy.ToString().IndexOf(openedBracket);
        return FindClosingBracketIndex(strategy, removeBetween, strategy[index]);
    }
    /// <summary>
    /// Finds the specified element in the string.
    /// </summary>
    public static int FindClosingBracketIndex(StringBuilder strategy, bool removeBetween, int dxOfStart)
    {
        var openedBracket = strategy[dxOfStart];
        var closedBracket = ClosingBracketFor(openedBracket);
        var start = dxOfStart;
        var bracketCount = 1;
        //var textArray = text.ToString().ToCharArray();
        var currentChar = 'a';
        for (var i = dxOfStart + 1; i < strategy.Length; i++)
        {
            currentChar = strategy[i];
            if (currentChar == openedBracket)
                bracketCount++;
            else if (currentChar == closedBracket) bracketCount--;
            if (bracketCount == 0)
            {
                dxOfStart = i;
                break;
            }
        }
        if (removeBetween) RemoveBetweenIndexes(strategy, start, dxOfStart);
        return dxOfStart;
    }
    private static void RemoveBetweenIndexes(StringBuilder strategy, int start, int end)
    {
        ThrowEx.StartIsHigherThanEnd(start, end);
        start++;
        for (; start < end; start++) strategy[start] = ' ';
    }
    /// <summary>
    /// Validates the specified condition in the string.
    /// </summary>
    public static bool CheckWhetherNoBrackedIsBeforeOther2(string braces)
    {
        return BalancedBrackets.AreBracketsBalanced(AllBrackets(braces));
    }
    /// <summary>
    /// Validates the specified condition in the string.
    /// </summary>
    public static bool CheckWhetherNoBrackedIsBeforeOther1(string braces)
    {
        const string openBraces = "([{";
        const string closeBraces = ")]}";
        var stack = new Stack<char>();
        foreach (var count in braces)
            if (openBraces.Contains(count))
                stack.Push(count);
            else if (stack.Count == 0 || openBraces.IndexOf(stack.Pop()) != closeBraces.IndexOf(count)) return false;
        return stack.Count == 0;
    }
    /// <summary>
    /// Converts the string content to the specified format.
    /// </summary>
    public static string ConvertWhitespaceToVisible(string temp)
    {
        temp = temp.Replace('\t', UnicodeWhiteToVisible.Tab);
        temp = temp.Replace('\n', UnicodeWhiteToVisible.NewLine);
        temp = temp.Replace('\r', UnicodeWhiteToVisible.CarriageReturn);
        temp = temp.Replace(' ', UnicodeWhiteToVisible.Space);
        return temp;
    }
    /// <summary>
    /// Concatenates strings with the specified separator.
    /// </summary>
    public static string ConcatSpace(IList result)
    {
        var stringBuilder = new StringBuilder();
        foreach (string element in result) stringBuilder.Append(element + " ");
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsNullOrWhiteSpaceRange(params string[] list)
    {
        foreach (var text in list)
            if (IsNullOrWhiteSpace(text))
                return true;
        return false;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsSingleLine(string strategy)
    {
        return !strategy.Trim().Contains(Environment.NewLine);
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetWhitespaceFromBeginning(StringBuilder stringBuilder, string line)
    {
        stringBuilder.Clear();
        foreach (var character in line)
            if (char.IsWhiteSpace(character))
                stringBuilder.Append(character);
            else
                break;
        return stringBuilder.ToString();
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
    ///     A2 is use to calculate length of center
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="centerString"></param>
    /// <param name="centerIndex"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static string CharsBeforeAndAfter(string strategy, string centerString, int centerIndex, int before, int after)
    {
        var builder = centerIndex - before;
        var argument = centerIndex + centerString.Length + after;
        var stringBuilder = new StringBuilder();
        if (HasIndex(builder, strategy, false))
        {
            stringBuilder.Append(strategy.Substring(builder, before));
            stringBuilder.Append(" ");
        }
        stringBuilder.Append(centerString);
        if (HasIndex(argument, strategy, false))
        {
            stringBuilder.Append(strategy.Substring(argument, after));
            stringBuilder.Append(" ");
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsNewLine(string between)
    {
        return between.Contains('\n') || between.Contains('\r');
    }
    /// <summary>
    /// Changes encoding or format of the string.
    /// </summary>
    public static bool ChangeEncodingProcessWrongCharacters(ref string input)
    {
        return ChangeEncodingProcessWrongCharacters(ref input, Encoding.GetEncoding("latin1"));
    }
    /// <summary>
    ///     když je v souboru rozsypaný čaj, přečíst přes File.ReadAllText, převést přes SH.ChangeEncodingProcessWrongCharacters.
    ///     Pokud u žádného není text smysluplný, je to beznadějně poškozené.
    ///     V opačném případě 10 kódování by mělo být v pořádku.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="oldEncoding"></param>
    public static bool ChangeEncodingProcessWrongCharacters(ref string input, Encoding oldEncoding)
    {
        if (IsValidISO(input))
        {
            var builder = oldEncoding.GetBytes(input);
            input = Encoding.UTF8.GetString(builder);
            return true;
        }
        // ý musí být před í, ě před č
        input = SHReplace.ReplaceManyFromString(input, @"Ã©,ý
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
", ",");
        return true;
    }
    /// <summary>
    /// Add Space After First Letter For Every And Sort operation on the input.
    /// </summary>
    public static List<string> AddSpaceAfterFirstLetterForEveryAndSort(List<string> input)
    {
        CA.Trim(input);
        for (var i = 0; i < input.Count; i++) input[i] = input[i].Insert(1, " ");
        input.Sort();
        return input;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetLastWord(string parameter, bool returnEmptyWhenDontHaveLenght = true)
    {
        parameter = parameter.Trim();
        var dex = parameter.LastIndexOf(' ');
        if (dex != -1) return parameter.Substring(dex).Trim();
        if (returnEmptyWhenDontHaveLenght) return string.Empty;
        return parameter;
    }
    /// <summary>
    /// Adds specified content to the string.
    /// </summary>
    public static string AddSpaceAndDontDuplicate(bool after, string strategy, string colon)
    {
        List<int>? dxsColons = null;
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(strategy);
        if (after)
        {
            dxsColons = ReturnOccurencesOfString(strategy, colon);
            for (var i = dxsColons.Count - 1; i >= 0; i--) stringBuilder.Insert(dxsColons[i] + 1, " ");
            dxsColons = ReturnOccurencesOfString(stringBuilder.ToString(), colon + "  ");
            for (var i = dxsColons.Count - 1; i >= 0; i--) stringBuilder.Remove(dxsColons[i] + 1, 1);
        }
        else
        {
            dxsColons = ReturnOccurencesOfString(strategy, colon);
            for (var i = dxsColons.Count - 1; i >= 0; i--) stringBuilder.Insert(dxsColons[i], " ");
            dxsColons = ReturnOccurencesOfString(stringBuilder.ToString(), "  " + colon);
            for (var i = dxsColons.Count - 1; i >= 0; i--) stringBuilder.Remove(dxsColons[i], 1);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Counts the specified elements in the string.
    /// </summary>
    public static string CountOfItems(List<KeyValuePair<string, int>> counted)
    {
        var stringBuilder = new StringBuilder();
        foreach (var kvp in counted) stringBuilder.AppendLine(kvp.Value + "x " + kvp.Key);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Reduces multiple whitespace lines to a single one.
    /// </summary>
    public static string MultiWhitespaceLineToSingle(List<string> lines)
    {
        var str = string.Join(Environment.NewLine, lines);
        //SunamoCollectionsShared.CA.DoubleOrMoreMultiLinesToSingle(ref str);
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
        //List<bool> isWhitespace = new List<bool>(list.Count);
        ////list.Add(false)
        //for (int i = 0; i < list.Count; i++)
        //{
        //    isWhitespace.Add(list[i].Trim() == string.Empty);
        //}
        //isWhitespace.Reverse();
        //for (int i = isWhitespace.Count - 1; i >= 0; i--)
        //{
        //    if (isWhitespace[i] && isWhitespace[i + 1])
        //    {
        //        list.RemoveAt(i+1);
        //    }
        //}
    }
    /// <summary>
    /// Adjusts indentation of lines based on the previous line.
    /// </summary>
    public static void IndentAsPreviousLine(List<string> lines)
    {
        var indentPrevious = string.Empty;
        string? line = null;
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < lines.Count - 1; i++)
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
                    stringBuilder.Clear();
                    foreach (var character in line)
                        if (char.IsWhiteSpace(character))
                            stringBuilder.Append(character);
                        else
                            break;
                    indentPrevious = stringBuilder.ToString();
                }
            }
        }
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsLine(string strategy, bool checkInCaseOnlyOneString, params string[] contains)
    {
        return ContainsLine2(strategy, checkInCaseOnlyOneString, contains);
    }
    /// <summary>
    ///     Whether A1 contains any from a3. a2 only logical chcek
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    public static bool ContainsLine2(string strategy, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        var hasLine = false;
        if (contains.Count() == 1)
        {
            if (checkInCaseOnlyOneString) hasLine = strategy.Contains(contains.First());
        }
        else
        {
            foreach (var count in contains)
                if (strategy.Contains(count)) {
                    hasLine = true;
                    break;
                }
        }
        return hasLine;
    }
    /// <summary>
    /// Gets the word at the specified position in the string.
    /// </summary>
    public static string WordAfter(string input, string word)
    {
        input = WrapWithChar(input, ' ');
        var dex = input.IndexOf(word);
        var dex2 = input.IndexOf(' ', dex + 1);
        var stringBuilder = new StringBuilder();
        if (dex2 != -1)
        {
            dex2++;
            for (var i = dex2; i < input.Length; i++)
            {
                var currentChar = input[i];
                if (currentChar != ' ')
                    stringBuilder.Append(currentChar);
                else
                    break;
            }
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Gets leading characters matching the predicate.
    /// </summary>
    public static string Leading(string input, Func<char, bool> isWhiteSpace)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in input)
            if (isWhiteSpace.Invoke(character))
                stringBuilder.Append(character);
            else
                break;
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsOnIndex(string input, int index, Func<char, bool> isWhiteSpace)
    {
        if (input.Length > index) return isWhiteSpace.Invoke(input[index]);
        return false;
    }
    /// <summary>
    /// Counts the specified elements in the string.
    /// </summary>
    public static int CountLines(string strategy)
    {
        return Regex.Matches(strategy, Environment.NewLine).Count;
    }
    /// <summary>
    /// Checks whether the string has the specified characteristic.
    /// </summary>
    public static bool HasLetter(string strategy)
    {
        foreach (var item in strategy)
            if (char.IsLetter(item))
                return true;
        return false;
    }
    /// <summary>
    /// Get Texts Between operation on the input.
    /// </summary>
    public static List<string> GetTextsBetween(string parameter, string after, string before,
        bool cannotBeLetterBeforeFounded = false)
    {
        return GetTextsBetween(parameter, after, before, cannotBeLetterBeforeFounded, out cannotBeLetterBeforeFounded);
    }
    /// <summary>
    /// Get Texts Between operation on the input.
    /// </summary>
    public static List<string> GetTextsBetween(string parameter, string after, string before, bool cannotBeLetterBeforeFounded,
        out bool firstCharBeforeIsLetter)
    {
        firstCharBeforeIsLetter = false;
#if DEBUG
        if (parameter.Contains("headerName: temp(\"Name\"),"))
        {
        }
#endif
        var results = new List<string>();
        var indexesAfter = ReturnOccurencesOfString(parameter, after);
        var indexesBefore = ReturnOccurencesOfString(parameter, before);
        var min = Math.Min(indexesAfter.Count, indexesBefore.Count);
        var indexAfterToAccessCollection = 0;
        var indexBeforeToAccessCollection = 0;
        for (; indexAfterToAccessCollection < min; indexAfterToAccessCollection++, indexBeforeToAccessCollection++)
        {
            var indexAfter = indexesAfter[indexAfterToAccessCollection];
            var indexBefore = indexesBefore[indexBeforeToAccessCollection];
            int indexAfterFinal, indexBeforeFinal;
            if (indexAfter > indexBefore)
            {
                if (indexesAfter.Count == 1 || indexesBefore.Count == 1)
                {
                    indexAfterFinal = indexesAfter[0] + after.Length;
                    indexBeforeFinal = indexesBefore.FirstOrDefault(data => data > indexAfterFinal) - 1;
                    if (indexBeforeFinal == 0)
                    {
                        throw new Exception("There is no number higher than " + indexAfterFinal);
                    }
                }
                // zde to nedává smysl když indexesBefore bude mít méně prvků než indexesAfter protože to vždy končí zde
                indexBeforeToAccessCollection--;
                continue;
            }
            indexAfterFinal = indexAfter + after.Length;
            indexBeforeFinal = indexBefore - 1;
            // When I return between ( ), there must be +1
            var substringed = parameter.Substring(indexAfterFinal, indexBeforeFinal - indexAfterFinal + 1).Trim();
#if DEBUG
            if (parameter.Contains(".matches(new RegExp(endsWit"))
            {
            }
#endif
            // temp("Url must end with .json"
            if (cannotBeLetterBeforeFounded)
            {
                if (indexAfterFinal != 0)
                {
                    var charBeforeEnd = parameter[indexBeforeFinal - 1];
                    var charBeforeStart = parameter[indexAfterFinal - after.Length - 1];
                    if (!char.IsLetter(charBeforeStart))
                        results.Add(substringed);
                    else
                        firstCharBeforeIsLetter = true;
                }
                else
                {
                    results.Add(substringed);
                }
            }
            else
            {
                results.Add(substringed);
            }
        }
        return results;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveLastLetters(string input, int characterCount)
    {
        if (input.Length > characterCount) return input.Substring(0, input.Length - characterCount);
        return input;
    }
    //public static bool IsAllUnique(List<string> count)
    //{
    //    ThrowEx.NotImplementedMethod();
    //    return false;
    //}
    ///// <summary>
    /////     Pokud je A1 true, bere se z A2,3 menší počet prvků
    /////     Simply call HasTextRightFormat for every in A2
    ///// </summary>
    ///// <param name="canBeDifferentCount"></param>
    ///// <param name="typeDynamics"></param>
    ///// <param name="tfd"></param>
    //public static bool AllHaveRightFormat(bool canBeDifferentCount, List<string> typeDynamics,
    //    List<TextFormatDataString> tfd)
    //{
    //    if (!canBeDifferentCount)
    //        if (typeDynamics.Count != tfd.Count)
    //            throw new Exception(XMismatchCountInInputArraysOfSHAllHaveRightFormat);
    //    var lowerCount = Math.Min(typeDynamics.Count, tfd.Count);
    //    for (var i = 0; i < lowerCount; i++)
    //        if (!HasTextRightFormat(typeDynamics[i], tfd[i]))
    //            return false;
    //    return true;
    //}
    /// <summary>
    /// Checks whether the string has the specified characteristic.
    /// </summary>
    public static bool HasCharRightFormat(char character, CharFormatDataString cfd)
    {
        if (cfd.Upper.HasValue)
        {
            if (cfd.Upper.Value)
            {
                if (char.IsLower(character)) return false;
            }
            else
            {
                if (char.IsUpper(character)) return false;
            }
        }
        if (cfd.MustBe != null && cfd.MustBe.Length != 0)
        {
            foreach (var requiredChar in cfd.MustBe)
                if (requiredChar == character)
                    return true;
            return false;
        }
        return true;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static bool GetTextInLastSquareBracketsAndOther(string parameter, out string mainText, out string bracketedText)
    {
        mainText = bracketedText = null!;
        parameter = parameter.Trim();
        if (parameter[parameter.Length - 1] != ']')
            return false;
        parameter = parameter.Substring(0, parameter.Length - 1);
        var firstHranata = parameter.LastIndexOf(']');
        if (firstHranata == -1)
            return false;
        if (firstHranata != -1) SHSplit.SplitByIndex(parameter, firstHranata, out mainText, out bracketedText);
        return true;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveBracketsWithTextCaseInsensitive(string input, string replacement, params string[] patterns)
    {
        input = SHReplace.ReplaceAll(input, "(", "( ");
        input = SHReplace.ReplaceAll(input, "]", " ]");
        input = SHReplace.ReplaceAll(input, ")", " )");
        input = SHReplace.ReplaceAll(input, "[", "[ ");
        for (var i = 0; i < patterns.Length; i++) input = Regex.Replace(input, patterns[i], replacement, RegexOptions.IgnoreCase);
        return input;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveBracketsWithoutText(string input)
    {
        return SHReplace.ReplaceAll(input, "", "()", "[]");
    }
    /// <summary>
    /// Without Special Chars operation on the string.
    /// </summary>
    public static string WithoutSpecialChars(string input, params char[] over)
    {
        SpecialCharsService specialCharsService = new();
        var stringBuilder = new StringBuilder();
        foreach (var character in input)
            if (!specialCharsService.SpecialChars.Contains(character) &&
                !over.Any(data => data == character)) // CAG.IsEqualToAnyElement(character, over))
                stringBuilder.Append(character);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveBracketsFromStart(string input)
    {
        while (true)
        {
            var foundBracket = false;
            if (input.StartsWith("("))
            {
                var ss = input.IndexOf(")");
                if (ss != -1 && ss != input.Length - 1)
                {
                    foundBracket = true;
                    input = input.Substring(ss + 1);
                }
            }
            else if (input.StartsWith("["))
            {
                var ss = input.IndexOf("]");
                if (ss != -1 && ss != input.Length - 1)
                {
                    foundBracket = true;
                    input = input.Substring(ss + 1);
                }
            }
            if (!foundBracket) break;
        }
        return input;
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveLastCharIfIs(string input, char znak)
    {
        var argument = input.Length - 1;
        if (input[argument] == znak) return input.Substring(0, argument);
        return input;
    }

    /// <summary>
    ///     If A1 contains A2, return A2 and all following. Otherwise A1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="returnFromString"></param>
    public static string GetLastPartByString(string input, string returnFromString)
    {
        var dex = input.LastIndexOf(returnFromString);
        if (dex == -1) return input;
        var start = dex + returnFromString.Length;
        if (start < input.Length) return input.Substring(start);
        return input;
    }
    /// <summary>
    /// Adds specified content to the string.
    /// </summary>
    public static string AddEmptyLines(string content, int addRowsDuringScrolling)
    {
        var lines = SHGetLines.GetLines(content);
        for (var i = 0; i < addRowsDuringScrolling; i++) lines.Add(string.Empty);
        return string.Join(Environment.NewLine, lines);
    }
    /// <summary>
    /// Converts the string to the specified format.
    /// </summary>
    public static string ToCase(string input, bool? velkym)
    {
        if (velkym.HasValue)
        {
            if (velkym.Value)
                return input.ToUpper();
            return input.ToLower();
        }
        return input;
    }
    /// <summary>
    /// Checks if the string ends with the specified suffix.
    /// </summary>
    public static bool EndsWithNumber(string input)
    {
        for (var i = 0; i < 10; i++)
            if (input.EndsWith(i.ToString()))
                return true;
        return false;
    }
    /// <summary>
    ///     Výchozí byla metoda NullToStringOrEmpty
    ///     OrNull pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="value"></param>
    public static string? NullToStringOrNull(object? value)
    {
        if (value == null) return null;
        return value.ToString();
    }
    /// <summary>
    /// Processes or retrieves content from the end of the string.
    /// </summary>
    public static bool LastCharEquals(string input, char delimiter)
    {
        if (!string.IsNullOrEmpty(input)) return false;
        var lastChar = input[input.Length - 1];
        if (lastChar == delimiter) return true;
        return false;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetWithoutLastWord(string parameter)
    {
        parameter = parameter.Trim();
        var dex = parameter.LastIndexOf(' ');
        if (dex != -1) return parameter.Substring(0, dex);
        return parameter;
    }
    /// <summary>
    /// Deletes characters outside the valid range.
    /// </summary>
    public static string DeleteCharsOutOfAscii(string strategy)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in strategy)
        {
            int i = character;
            if (i < 128) stringBuilder.Append(character);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    ///     Not working for czech, same as https://stackoverflow.com/a/249126
    /// </summary>
    /// <param name="strategy"></param>
    public static string RemoveDiacritics(string strategy)
    {
        var normalizedString = strategy.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var count in normalizedString)
            switch (CharUnicodeInfo.GetUnicodeCategory(count))
            {
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    stringBuilder.Append(count);
                    break;
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                    stringBuilder.Append('_');
                    break;
            }
        var result = stringBuilder.ToString();
        return string.Join("_", result.Split(new[] { '_' }
            , StringSplitOptions.RemoveEmptyEntries)); // remove duplicate underscores
    }
    /// <summary>
    /// Strips punctuation and symbols from the string.
    /// </summary>
    public static string StripFunctationsAndSymbols(string parameter)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in parameter)
            if (!char.IsPunctuation(character) && !char.IsSymbol(character))
                stringBuilder.Append(character);
        return stringBuilder.ToString();
    }
    /// <summary>
    ///     Vrátí mi v každém prvku index na které se nachází první znak argument index na kterém se nachází poslední
    /// </summary>
    /// <param name="vcem"></param>
    /// <param name="co"></param>
    public static List<FromToString> ReturnOccurencesOfStringFromTo(string searchText, string searchTerm)
    {
        var searchLength = searchTerm.Length;
        var Results = new List<FromToString>();
        for (var Index = 0; Index < searchText.Length - searchTerm.Length + 1; Index++)
            if (searchText.Substring(Index, searchTerm.Length) == searchTerm)
            {
                var range = new FromToString();
                range.From = Index;
                range.To = Index + searchLength - 1;
                Results.Add(range);
            }
        return Results;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetWithoutFirstWord(string item2)
    {
        item2 = item2.Trim();
        //return item2.Substring(
        var dex = item2.IndexOf(' ');
        if (dex != -1) return item2.Substring(dex + 1);
        return item2;
    }
    /// <summary>
    /// Checks if the string ends with the specified suffix.
    /// </summary>
    public static int EndsWithIndex(string source, params string[] endingsToCheck)
    {
        for (var i = 0; i < endingsToCheck.Length; i++)
            if (source.EndsWith(endingsToCheck[i]))
                return i;
        return -1;
    }
    /// <summary>
    ///     Return A1 if wont find A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="searchFor"></param>
    public static string GetToFirst(string input, string searchFor)
    {
        var indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1) return input.Substring(0, indexOfChar + 1);
        return input;
    }

    /// <summary>
    /// Processes or retrieves content from the beginning of the string.
    /// </summary>
    public static string FirstCharLower(string input)
    {
        if (input.Length < 2) return input;
        var stringBuilder = input.Substring(1);
        return input[0].ToString().ToLower() + stringBuilder;
    }
    /// <summary>
    ///     Convert \r\n to NewLine etc.
    /// </summary>
    /// <param name="delimiter"></param>
    public static string ConvertTypedWhitespaceToString(string delimiter)
    {
        const string newline = @"
";
        switch (delimiter)
        {
            // must use \r\n, not Environment.NewLine (is not constant)
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return newline;
            case "\\t":
                return "\t";
        }
        return delimiter;
    }

    //public static string NullToStringOrDefault(object n, string v)
    //{
    //    throw new Exception(
    //        "Tahle metoda vypadala jinak ale jak idiot jsem ji změnil. Tím jak jsem poté přesouval metody tam zpět už je těžké se k tomu dostat.");
    //    return null;
    //    //return n == null ? " " + "(null)" : "" + v.ToString();
    //}

    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return "(null)"
    ///     jsem
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static string NullToStringOrDefault(object nullableObject)
    {
        //return NullToStringOrDefault(n, null);
        return nullableObject == null ? " " + "(null)" : " " + nullableObject;
    }
    #region MyRegion
    /// <summary>
    /// Wraps the string with the specified wrapper on both sides.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }
    /// <summary>
    /// Wraps the string with the specified character on both sides.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWithChar(string value, char wrapperChar, bool _trimWrapping = false,
        bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty) return string.Empty;
        // TODO: Make with StringBuilder, because of WordAfter and so
        return WrapWith(_trimWrapping ? value.Trim() : value, wrapperChar.ToString());
    }
    #endregion
    #region
    /*
     * Můžou být pouze zde argument nikoliv ve SunamoStringData
     * když byl Brackets globální, často jsem měl "claims it is defined"
     * takže jsem musel Brackets přesunout zde argument text ním i kód níže
     */
    /// <summary>
    /// Mapping of bracket types to their left characters.
    /// </summary>
    protected static Dictionary<Brackets, char> BracketsLeft = null!;
    /// <summary>
    /// Mapping of bracket types to their right characters.
    /// </summary>
    protected static Dictionary<Brackets, char> BracketsRight = null!;
    /// <summary>
    /// Initializes bracket mappings and related data structures.
    /// </summary>
    protected static void Init()
    {
        if (BracketsLeft == null)
        {
            BracketsLeft = new Dictionary<Brackets, char>();
            BracketsLeft.Add(Brackets.Curly, '{');
            BracketsLeft.Add(Brackets.Square, '[');
            BracketsLeft.Add(Brackets.Normal, '(');
            BracketsLeftList = BracketsLeft.Values.ToList();
            BracketsRight = new Dictionary<Brackets, char>();
            BracketsRight.Add(Brackets.Curly, '}');
            BracketsRight.Add(Brackets.Square, ']');
            BracketsRight.Add(Brackets.Normal, ')');
            BracketsRightList = BracketsRight.Values.ToList();
        }
    }
    #endregion
    #region MyRegion
    private static Type type = typeof(SH);
    /// <summary>
    /// Counts the specified elements in the string.
    /// </summary>
    public static int CountOf(string input, char character)
    {
        var i = 0;
        foreach (var currentChar in input)
            if (currentChar == character)
                i++;
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
    //            var count =
    //#if ASYNC
    //            await
    //#endif
    //            File.ReadAllTextAsync(item);
    //            if (count.Contains(mustContains))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    ///// <summary>
    ///// Trim all A2 from beginning A1
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="s"></param>
    //public static string TrimStart(string v, string text)
    //{
    //    return se.SHTrim.TrimStart(v, text);
    //}
    /// <summary>
    /// Checks whether the string has the specified characteristic.
    /// </summary>
    public static bool HasIndex(int parameter, string strategy, bool throwExcWhenInvalidIndex = true)
    {
        if (parameter < 0)
        {
            if (throwExcWhenInvalidIndex)
                throw new Exception("Chybn\u00FD parametr ");
            return false;
        }
        if (strategy.Length > parameter) return true;
        return false;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsNegation(string contains)
    {
        if (contains[0] == '!') return true;
        return false;
    }
    /// <summary>
    /// Is Negation Tuple on the input.
    /// </summary>
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
    ///     Version wo ref - dont auto remove first!
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static bool IsContained(string strategy, string contains)
    {
        var (negation, contains2) = IsNegationTuple(contains);
        contains = contains2;
        if (negation && strategy.Contains(contains))
            return false;
        if (!negation && !strategy.Contains(contains)) return false;
        return true;
    }
    /// <summary>
    /// Checks equality of the string against specified values.
    /// </summary>
    public static bool EqualsOneOfThis(string p1, params string[] p2)
    {
        foreach (var element in p2)
            if (p1 == element)
                return true;
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
    ///     Another method is RemoveDiacritics
    ///     G text bez dia A1.
    /// </summary>
    /// <param name="input"></param>
    public static string TextWithoutDiacritic(string input)
    {
        return input.RemoveDiacritics();
        // but also with this don't throw exception but no working Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(input));
        //if (!initDiactitic)
        //{
        //    System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        //    Encoding.RegisterProvider(provider);
        //    initDiactitic = true;
        //}
        //originally was "ISO-8859-8" but not working in .net standard. 1252 is eqvivalent
        //return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(textWithDiacritics));
        // FormC - followed by SHReplace.Replacement of sequences
        // As default using FormC
        //return textWithDiacritics.Normalize(NormalizationForm.FormC);
        //return RemoveDiacritics(textWithDiacritics);
    }
    /// <summary>
    ///     Vrátí prázdný řetězec pokud nebude nalezena mezera argument A1
    /// </summary>
    /// <param name="p"></param>
    public static string GetFirstWord(string parameter, bool returnEmptyWhenDontHaveLenght = true)
    {
        parameter = parameter.Trim();
        var dex = parameter.IndexOf(' ');
        if (dex != -1) return parameter.Substring(0, dex);
        if (returnEmptyWhenDontHaveLenght) return string.Empty;
        return parameter;
    }
    //public static List<string> SplitChar(string parametry, params char[] deli)
    //{
    //    return se.SHSplit.SplitChar(parametry, deli);
    //}
    //public static List<string> Split(string parametry, params string[] deli)
    //{
    //    return se.SHSplit.Split(parametry, deli);
    //}
    // Will be deleted after final refactoring
    // Automaticky ořeže poslední znad A1
    // Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
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
    ///// If null, return "(null)"
    ///// </summary>
    ///// <param name="n"></param>
    ///// <returns></returns>
    //public static string NullToStringOrDefault(object n)
    //{
    //    return se.SH.NullToStringOrDefault(n);
    //}
    ///// <summary>
    ///// If null, return "(null)"
    ///// </summary>
    ///// <param name="n"></param>
    ///// <param name="value"></param>
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
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsAnyBool(string strategy, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        return ContainsAny(strategy, checkInCaseOnlyOneString, contains).Count > 0;
    }
    //public static List<string> ContainsAny(string item, bool checkInCaseOnlyOneString, IList<string> contains)
    //{
    //    return se.SH.ContainsAny(text, checkInCaseOnlyOneString, contains);
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
    //public static List<T> ContainsAny<T>(bool checkInCaseOnlyOneString, temp item, IList<T> contains)
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
    //public static string RemoveAfterFirst(string temp, char ch)
    //{
    //    return se.SHParts.RemoveAfterFirst(temp, ch);
    //}
    //public static string FirstLine(string text)
    //{
    //    return se.SH.FirstLine(item);
    //}
    #endregion
    #region MyRegion
    /// <summary>
    ///     keep joinAnotherWordsIfIsAlsoNumber = false
    /// </summary>
    /// <param name="input"></param>
    /// <param name="probablyIndex"></param>
    /// <param name="joinAnotherWordsIfIsAlsoNumber"></param>
    /// <returns></returns>
    public static int FirstWordWhichIsNumber(string input, int probablyIndex,
        bool joinAnotherWordsIfIsAlsoNumber = false)
    {
        var parameter = SHSplit.Split(input, " ");
        if (parameter.Count > probablyIndex)
        {
            if (BTS.IsInt(parameter[probablyIndex]))
            {
                if (joinAnotherWordsIfIsAlsoNumber)
                {
                    var strategy = BTS.LastInt + NH.JoinAnotherTokensIfIsNumber(parameter, probablyIndex + 1);
                    return int.Parse(strategy);
                }
                return BTS.LastInt;
            }
            return FirstWordWhichIsNumberAllIndexes(parameter, joinAnotherWordsIfIsAlsoNumber);
        }
        return FirstWordWhichIsNumberAllIndexes(parameter, joinAnotherWordsIfIsAlsoNumber);
    }
    /// <summary>
    /// Processes or retrieves content from the beginning of the string.
    /// </summary>
    public static int FirstWordWhichIsNumberAllIndexes(List<string> parameter, bool joinAnotherWordsIfIsAlsoNumber = true)
    {
        var i = 0;
        foreach (var item in parameter)
            if (BTS.IsInt(item))
            {
                i++;
                if (joinAnotherWordsIfIsAlsoNumber)
                {
                    var strategy = BTS.LastInt + NH.JoinAnotherTokensIfIsNumber(parameter, i);
                    return int.Parse(strategy);
                }
                return BTS.LastInt;
            }
        return int.MinValue;
    }
    /// <summary>
    /// Compares strings ignoring whitespace differences.
    /// </summary>
    public static bool CompareStringIgnoreWhitespaces2(string s1, string s2)
    {
        return string.Compare(s1, s2, CultureInfo.CurrentCulture,
            CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
    }
    /// <summary>
    /// Compares strings ignoring whitespace differences.
    /// </summary>
    public static bool CompareStringIgnoreWhitespaces(string s1, string s2)
    {
        var normalized1 = Regex.Replace(s1, @"\s", "");
        var normalized2 = Regex.Replace(s2, @"\s", "");
        var stringEquals = string.Equals(
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
    //public static string SHReplace.ReplaceAll4(string temp, string to, string from)
    //{
    //    while (temp.Contains(from))
    //    {
    //        temp = temp.Replace(from, to);
    //    }
    //    return temp;
    //}
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsOnly(string input, List<char> numericChars)
    {
        if (input.Length == 0) return false;
        foreach (var item in input)
            if (!numericChars.Contains(item))
                return false;
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
    /// <param name="strategy"></param>
    /// <returns></returns>
    public static string FirstLine(string strategy)
    {
        var lines = SHGetLines.GetLines(strategy);
        return lines.Count == 0 ? string.Empty : lines[0];
    }
    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="only"></param>
    public static string FirstCharUpper(string strategy, bool only = false)
    {
        if (strategy != null)
        {
            var stringBuilder = strategy.Substring(1);
            if (only) stringBuilder = stringBuilder.ToLower();
            return strategy[0].ToString().ToUpper() + stringBuilder;
        }
        return null!;
    }
    /// <summary>
    /// In Brackets operation on the string.
    /// </summary>
    public static string InBrackets(string input)
    {
        return GetTextBetweenTwoCharsInts(input, input.IndexOf('('), input.IndexOf(')'));
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
    /// <param name="strategy"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    public static List<string> ContainsAny( /*T itemT,*/ /*IList<T> containsT,*/ string strategy, bool checkInCaseOnlyOneString, IList<string> contains)
    {
        var founded = new List<string>();
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
            strategy.Contains(contains.First());
        else
            foreach (var count in contains)
                if (strategy.Contains(count))
                    founded.Add(count);
        return founded;
    }
    /// <summary>
    /// Contains Any Char operation on the input.
    /// </summary>
    public static List<char> ContainsAnyChar( /*T itemT,*/ /*IList<T> containsT,*/ string strategy, bool checkInCaseOnlyOneString, IList<char> contains)
    {
        var founded = new List<char>();
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
            strategy.Contains(contains.First());
        else
            foreach (var count in contains)
                if (strategy.Contains(count))
                    founded.Add(count);
        return founded;
    }
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="checkInCaseOnlyOneString"></param>
    /// <param name="text"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static List<T> ContainsAny<T>( /*T itemT,*/ /*IList<T> containsT,*/
        bool checkInCaseOnlyOneString, T item, IList<T> contains)
    {
        throw new Exception(
            "Tahle metoda je celá špatně, používat vždy jinou. Je tu stejná metoda ContainsAny jen negenerická");
        //Type type = typeof(T);
        //bool isChar = type == Types.CharType;
        //List<T> founded = new List<T>();
        ////
        //if (contains.Count() == 1 && checkInCaseOnlyOneString)
        //{
        //    ThrowEx.NotImplementedMethod();
        //    //item.Contains(contains.First());
        //}
        //else
        //{
        //    foreach (var count in contains)
        //    {
        //        //ThrowEx.NotImplementedMethod();
        //        //if (item.Contains(count))
        //        //{
        //        //    founded.Add(BTS.CastToByT<T>(count, isChar));
        //        //}
        //    }
        //}
        //return founded;
    }
    /// <summary>
    /// Gets the word at the specified character index in the line.
    /// </summary>
    [Obsolete("Tahle metoda využívala SHData.ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces. To bylo úplně složité. Už to nevracat argument případně to napsat znovu")]
    public static string? GetWordOnIndex(string line, int index)
    {
        return null;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsUpper(string data)
    {
        return data.Any(char.IsUpper);
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsLower(string data)
    {
        return data.Any(char.IsLower);
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
    //            string stringBuilder = nazevPP.Substring(1);
    //            return nazevPP[0].ToString().ToLower() + stringBuilder;
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
    //                template = SHReplace.ReplaceAll2(template, args[i].ToString(), "{" + i + "}");
    //            }
    //            return template;
    //        }
    //
    //            public static string WrapWithQm(string commitMessage)
    //        {
    //            return SH.WrapWith(commitMessage, '"');
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
    // Nev�m co tu data�l� tohle. V�echny metody maj� po�ad� delimiter/parts, tahle opa�n�
    //    /// <summary>
    //    /// Will be delete after final refactoring
    //    /// Automaticky o�e�e posledn� znad A1
    //    /// Pokud message� inty v A2, pou�ij metodu JoinMakeUpTo2NumbersToZero
    //    /// </summary>
    //    /// <param name="delimiter"></param>
    //    /// <param name="parts"></param>
    //    public static string Join(IList parts, object delimiter)
    //{
    //        if (CA.Count(parts) == 0)
    //        {
    //            return string.Empty;
    //        }
    //        var data = delimiter.ToString();
    //        StringBuilder stringBuilder = new StringBuilder();
    //        foreach (var item in parts)
    //        {
    //        stringBuilder.Append(item.ToString() + data);
    //    }
    //    var vr = stringBuilder.ToString();
    //    return vr.Substring(0, vr.Length - data.Length);
    //}
    //
    //        public static string Trim(string text, string args)
    //        {
    //            text = TrimStart(text, args);
    //            text = TrimEnd(text, args);
    //
    //            return text;
    //        }
    //
    //        public static Type type = typeof(SH);
    //
    //        #region
    //        public static string JoinComma(params string[] args)
    //        {
    //            return Join(",", (IList)args);
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
    //        public static string GetTextBetweenTwoChars(string parameter, int begin, int end)
    //        {
    //            if (end > begin)
    //            {
    //                // argument(1) - 1,3
    //                return parameter.Substring(begin + 1, end - begin - 1);
    //                // originally
    //                //return parameter.Substring(begin+1, end - begin - 1);
    //            }
    //            return parameter;
    //        }
    //
    //
    //        /// <summary>
    //        /// Work like everybody excepts, from argument {b} count return builder
    //        /// </summary>
    //        /// <param name="p"></param>
    //        /// <param name="begin"></param>
    //        /// <param name="end"></param>
    //        public static string GetTextBetweenTwoChars(string parameter, char beginS, char endS, bool throwExceptionIfNotContains = true)
    //        {
    //            var begin = parameter.IndexOf(beginS);
    //            var end = parameter.IndexOf(endS, begin + 1);
    //            if (begin == NumConsts.MOne || end == NumConsts.MOne)
    //            {
    //                if (throwExceptionIfNotContains)
    //                {
    //                    ThrowEx.NotContains( parameter, beginS.ToString(), endS.ToString());
    //                }
    //            }
    //            else
    //            {
    //                return GetTextBetweenTwoChars(parameter, begin, end);
    //            }
    //            return parameter;
    //        }
    //        #endregion
    #endregion
    #region MyRegion
    #region MyRegion
    /// <summary>
    ///     notAllowedInRanges can be Func
    ///     <int, bool>
    ///         (delegát který vrací zda daný index může být použít pro end) or FromToList
    ///         Dříve se mi na to používalo FromToList
    ///         Nikde jsem nenašel způsob užítí text Func
    ///         <int, bool> notAllowedInRanges = null takže vytvořím novou metodu co bude brát FromToList
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <param name="notAllowedInRanges"></param>
    /// <param name="endLastIndexOf"></param>
    /// <returns></returns>
    public static string GetTextBetween(string parameter, char after, char before,
        bool throwExceptionIfNotContains = true /*cant have implicit value*/,
        object? notAllowedInRanges = null /*cant have implicit value*/, bool endLastIndexOf = false)
    {
        return GetTextBetweenTwoChars(parameter, after, before, throwExceptionIfNotContains, notAllowedInRanges,
            endLastIndexOf);
    }
    #endregion
    /// <summary>
    /// Values Between Quotes operation on the input.
    /// </summary>
    public static List<string> ValuesBetweenQuotes(string str, bool insertAgainToQm, bool apos = false)
    {
        var quoteString = "\"";
        if (apos) quoteString = "'";
        return ValuesBetweenQuotesOrApos(str, insertAgainToQm, quoteString);
    }
    /// <summary>
    /// Values Between Quotes And Apos operation on the input.
    /// </summary>
    public static List<string> ValuesBetweenQuotesAndApos(string str, bool insertAgainToQm,
        bool onlyWhichIsNotInT = false)
    {
        var sourceList = ValuesBetweenQuotesOrApos(str, insertAgainToQm, "\"", onlyWhichIsNotInT);
        sourceList.AddRange(ValuesBetweenQuotesOrApos(str, insertAgainToQm, "'", onlyWhichIsNotInT));
        return sourceList;
    }
    private static List<string> ValuesBetweenQuotesOrApos(string str, bool insertAgainToQm, string quoteString,
        bool onlyWhichIsNotInT = false)
    {
        var quoteChar = quoteString[0];
        var quotedTextRegex = new Regex(quoteString + ".*?" + quoteString);
        var matches = quotedTextRegex.Matches(str);
        var result = new List<string>(matches.Count);
        foreach (var item in matches)
        {
            var itemS = item.ToString()!;
#if DEBUG
            if (itemS.Contains("Module registration is not provided!"))
            {
            }
#endif
            if (onlyWhichIsNotInT)
                if (str.Contains("t(" + itemS + ")"))
                    continue;
            if (insertAgainToQm)
                result.Add(itemS);
            else
                result.Add(itemS.TrimEnd(quoteChar).TrimStart(quoteChar));
        }
        //SunamoCollectionsShared.CA.RemoveStringsEmpty2(result);
        return result;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsAtLeastOne(string parameter, List<string> aggregate)
    {
        foreach (var item in aggregate)
            if (parameter.Contains(item))
                return true;
        return false;
    }
    /// <summary>
    ///     Dont automatically change case
    /// </summary>
    /// <param name="value"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    public static string FirstCharOfEveryWordPart(string value, string deli)
    {
        var parameter = SHSplit.Split(value, deli);
        var stringBuilder = new StringBuilder();
        foreach (var item in parameter) stringBuilder.Append(item[0].ToString());
        return stringBuilder.ToString();
    }
    /// <summary>
    ///     When there is no number, append 1
    ///     Otherwise incr.
    /// </summary>
    /// <param name="input"></param>
    public static void IncrementLastNumber(ref string input)
    {
        var lastChar = input[input.Length - 1];
        if (char.IsNumber(lastChar))
        {
            var i = int.Parse(lastChar.ToString());
            i++;
            input = input.Substring(0, input.Length - 1) + i;
            return;
        }
        input = input + "1";
    }
    /// <summary>
    ///     Nothing can be null
    /// </summary>
    /// <param name="content"></param>
    /// <param name="lines"></param>
    /// <param name="dx2"></param>
    /// <returns></returns>
    public static string GetLineFromCharIndex(string content, List<string> lines, int dx2)
    {
        var lineIndex = GetLineIndexFromCharIndex(content, dx2);
        return lines[lineIndex];
    }
    /// <summary>
    ///     Return index, therefore x-1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pos"></param>
    public static int GetLineIndexFromCharIndex(string input, int pos)
    {
        var lineNumber = input.Take(pos).Count(input => input == '\n') + 1;
        return lineNumber - 1;
    }
    /// <summary>
    /// Finds the next non-letter-or-digit character from the start index.
    /// </summary>
    public static int AnotherOtherThanLetterOrDigit(string content, int startIndex)
    {
        var currentIndex = startIndex;
        for (; currentIndex < content.Length; currentIndex++)
            if (!char.IsLetterOrDigit(content[currentIndex]))
                //currentIndex--;
                return currentIndex;
        //currentIndex--;
        return currentIndex--;
    }
    /// <summary>
    /// Processes or retrieves content from the end of the string.
    /// </summary>
    public static string LastChars(string strategy, int input)
    {
        return strategy.Substring(strategy.Length - input);
        //mystring.Substring(Math.Max(0, mystring.Length - 4));
    }
    /// <summary>
    /// Processes tab-related content in the string.
    /// </summary>
    public static string TabToNewLine(string input)
    {
        //Environment.NewLine
        input = input.Replace("\t", "\r");
        var list = SHGetLines.GetLines(input);
        CA.Trim(list);
        list = list.Where(data => data.Trim() != string.Empty).ToList();
        return string.Join(Environment.NewLine, list);
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsAllLower(string ext)
    {
        return IsAllLower(ext, char.IsLower);
    }
    private static bool IsAllLower(string ext, Func<char, bool> isLower)
    {
        for (var i = 0; i < ext.Length; i++)
            if (!isLower(ext[i]))
                return false;
        return true;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsAllUpper(string ext)
    {
        return IsAllLower(ext, char.IsUpper);
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsBracket(string temp, bool mustBeLeftAndRight = false)
    {
        List<char>? left = null;
        List<char>? right = null;
        return ContainsBracket(temp, ref left, ref right, mustBeLeftAndRight);
    }
    /// <summary>
    /// Indicates whether the current UI culture is Czech.
    /// </summary>
    protected static bool S_Cs;
    static SH()
    {
        S_Cs = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();
    }
    /// <summary>
    /// Indexes Of Chars operation on the input.
    /// </summary>
    public static List<int> IndexesOfChars(string input, char searchChar)
    {
        return IndexesOfCharsList(input, new List<char>(searchChar));
    }
    /// <summary>
    ///     IndexesOfChars - char
    ///     ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="input"></param>
    /// <param name="whiteSpaceChars"></param>
    /// <returns></returns>
    public static List<int> IndexesOfCharsList(string input, List<char> whiteSpaceChars)
    {
        var indices = new List<int>();
        foreach (var item in whiteSpaceChars) indices.AddRange(ReturnOccurencesOfString(input, item.ToString()));
        indices.Sort();
        return indices;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsBracket(string temp, ref List<char>? left, ref List<char>? right,
        bool mustBeLeftAndRight = false)
    {
        left = ContainsAnyChar(temp, false, AllLists.LeftBrackets);
        right = ContainsAnyChar(temp, false, AllLists.LeftBrackets);
        if (mustBeLeftAndRight)
        {
            if (left.Count > 0 && right.Count > 0) return true;
        }
        else
        {
            if (left.Count > 0 || right.Count > 0) return true;
        }
        return false;
    }
    /// <summary>
    /// Gets the closing bracket character for the given opening bracket.
    /// </summary>
    public static char ClosingBracketFor(char openingBracket)
    {
        foreach (var item in BracketsLeft)
            if (item.Value == openingBracket)
                return BracketsRight[item.Key];
        ThrowEx.IsNotAllowed(openingBracket + " as bracket");
        return char.MaxValue;
    }
    /// <summary>
    ///     Get text after cz#cd => #cd
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="after"></param>
    public static string TextAfter(string strategy, string after)
    {
        var dex = strategy.IndexOf(after);
        if (dex != -1) return strategy.Substring(dex + after.Length);
        return string.Empty;
    }
    /// <summary>
    /// Pads the string to the specified length.
    /// </summary>
    public static string PadRight(string baseString, string newLine, int input)
    {
        var stringBuilder = new StringBuilder(baseString);
        for (var i = 0; i < input; i++) stringBuilder.Append(newLine);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static void RemoveLastCharSb(StringBuilder stringBuilder)
    {
        if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1);
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static string RemoveUselessWhitespaces(string innerText)
    {
        var parameter = SHSplit.SplitChar(innerText);
        return string.Join("", parameter);
    }
    /// <summary>
    ///     Is used in btnShortTextOfLyrics
    ///     Short text but always keep whole paragraps
    ///     Can be use also for non paragraph strings abcd->ab
    /// </summary>
    /// <param name="c"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string ShortToLengthByParagraph(string strategy, int maxLength)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        //var delimiter = SH.PadRight(string.Empty, Environment.NewLine, 2);
        var parameter = SHSplit.SplitChar(strategy, whitespaceChar.WhiteSpaceChars.ToArray());
        while (strategy.Length + parameter.Count > maxLength)
            if (parameter.Count > 1)
            {
                parameter.RemoveAt(parameter.Count - 1);
                strategy = string.Join(" ", parameter);
            }
            else
            {
                //c = SHSubstring.SubstringIfAvailable(text, maxLength);
                strategy = strategy.Substring(0, maxLength);
                break;
            }
        if (maxLength < strategy.Length)
        {
        }
        return strategy;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public static T ToNumber<T>(Func<string, T> parse, string input)
    {
        return parse.Invoke(input);
    }
    /// <summary>
    /// Repairs malformed content in the string.
    /// </summary>
    public static string RepairQuotes(string strategy)
    {
        strategy = strategy.Replace("�", "\"");
        strategy = strategy.Replace("�", "\"");
        strategy = strategy.Replace("�", "'");
        strategy = strategy.Replace("�", "'");
        return strategy;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsNumbered(string input)
    {
        var i = 0;
        foreach (var item in input)
            if (char.IsNumber(item))
            {
                i++;
            }
            else if (item == '.')
            {
                if (i > 0) return true;
            }
            else
            {
                return false;
            }
        return false;
    }
    /// <summary>
    /// Inserts content into the string at the specified position.
    /// </summary>
    public static string InsertEndingBracket(string input, char startingBracket)
    {
        var closingBracket = ClosingBracketFor(startingBracket);
        var occB = ReturnOccurencesOfString(input, startingBracket.ToString());
        var occE = ReturnOccurencesOfString(input, closingBracket.ToString());
        return InsertEndingBracket(input, occB, occE, startingBracket);
    }
    private static string InsertEndingBracket(string input, List<int> countStart, List<int> countEnd,
        char startingBracket)
    {
        return InsertEndingBracketWorker(input, countStart.Count, countEnd.Count, new List<char>(), startingBracket);
    }
    /// <summary>
    /// Inserts content into the string at the specified position.
    /// </summary>
    public static string InsertEndingBracket(string input, List<char> countStart, List<char> countEnd)
    {
        return InsertEndingBracketWorker(input, countStart.Count, countEnd.Count, countStart, char.MaxValue);
    }
    /// <summary>
    /// Inserts content into the string at the specified position.
    /// </summary>
    public static string InsertEndingBracketWorker(string input, int countStartCount, int countEndCount,
        List<char> countStart, char startingBracket)
    {
        var min = Math.Min(countStartCount, countEndCount);
        var max = Math.Max(countStartCount, countEndCount);
        if (countStartCount < countEndCount) return input;
        if (startingBracket != char.MaxValue)
        {
            var to = max - min;
            countStart.Clear();
            for (var i = 0; i < to; i++) countStart.Add(startingBracket);
        }
        input = InsertEndingBrackets(input, countStart, min, max);
        return input;
    }
    private static string InsertEndingBrackets(string input, List<char> countStart, int min, int max)
    {
        var to = max - 1;
        var isMultiline = input.Contains(Environment.NewLine);
        for (var i = min; i < to; i++)
        {
            if (isMultiline) input += Environment.NewLine;
            input += BracketsRight[GetBracketFromBegin(countStart[i])];
        }
        return input;
    }
    /// <summary>
    /// Processes paired bracket elements in the string.
    /// </summary>
    public static string PairsBracketsToCompleteBlock(string input)
    {
#if DEBUG
        if (input.Contains("name, price,"))
        {
        }
#endif
        var add = new List<char>();
        foreach (var character in input)
        {
            if (BracketsLeftList.Contains(character)) add.Add(character);
            if (BracketsRightList.Contains(character))
            {
                var builder = GetBracketFromBegin(character);
                var bracketIndex = add.IndexOf(BracketsLeft[builder]);
                if (bracketIndex != -1) add.RemoveAt(bracketIndex);
            }
        }
        var stringBuilder = new StringBuilder(input);
        if (add.Count > 0)
        {
            stringBuilder.AppendLine();
            for (var i = add.Count - 1; i >= 0; i--)
            {
                var builder = GetBracketFromBegin(add[i]);
                stringBuilder.Append(BracketsRight[builder]);
            }
            stringBuilder.Append(';');
        }
        var result = stringBuilder.ToString();
        if (input == result) result = result.TrimEnd(',');
        return result.Replace("\r\n", "\n");
    }
    private static Brackets GetBracketFromBegin(char bracket)
    {
        var end = false;
        return GetBracketFromBegin(bracket, ref end, false);
    }
    private static Brackets GetBracketFromBegin(char bracket, ref bool end, bool throwExIsNotBracket)
    {
        end = true;
        switch (bracket)
        {
            case '(':
                end = false;
                return Brackets.Normal;
            case '{':
                end = false;
                return Brackets.Curly;
            case '[':
                end = false;
                return Brackets.Square;
            case ')':
                return Brackets.Normal;
            case '}':
                return Brackets.Curly;
            case ']':
                return Brackets.Square;
            default:
                if (throwExIsNotBracket) ThrowEx.NotImplementedCase(bracket);
                break;
        }
        return Brackets.None;
    }
    /// <summary>
    /// Include Brackets operation on the input.
    /// </summary>
    public static List<char> IncludeBrackets(string strategy, bool starting)
    {
        var containsBracket = new List<char>();
        if (starting)
        {
            foreach (var character in strategy)
                if (BracketsLeftList.Contains(character))
                    containsBracket.Add(character);
        }
        else
        {
            foreach (var character in strategy)
                if (BracketsRightList.Contains(character))
                    containsBracket.Add(character);
        }
        return containsBracket;
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsValidISO(string input)
    {
        // ISO-8859-1 je to samé jako latin1 https://en.wikipedia.org/wiki/ISO/IEC_8859-1
        var bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
        var result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
        return string.Equals(input, result);
    }
    private static bool IsInFirstXCharsTheseLetters(string parameter, int parameterLength, params char[] letters)
    {
        for (var i = 0; i < parameterLength; i++)
            foreach (var letter in letters)
                if (parameter[i] == letter)
                    return true;
        return false;
    }
    private static string ShortForLettersCount(string parameter, int maxLetterCount, out bool shouldAddEllipsis)
    {
        shouldAddEllipsis = false;
        // Vše tu funguje výborně
        parameter = parameter.Trim();
        var parameterLength = parameter.Length;
        var isLongerOrEqual = maxLetterCount <= parameterLength;
        if (isLongerOrEqual)
        {
            if (IsInFirstXCharsTheseLetters(parameter, maxLetterCount, ' '))
            {
                var spaceIndex = 0;
                var data = parameter; //parameter.Substring(parameter.Length - zkratitO);
                var to = data.Length;
                var characterCount = 0;
                for (var i = 0; i < to; i++)
                {
                    characterCount++;
                    if (data[i] == ' ')
                    {
                        if (characterCount >= maxLetterCount) break;
                        spaceIndex = i;
                    }
                }
                data = data.Substring(0, spaceIndex + 1);
                if (data.Trim() != "") shouldAddEllipsis = true;
                //d = data ;
                return data;
                //}
            }
            shouldAddEllipsis = true;
            return parameter.Substring(0, maxLetterCount);
        }
        return parameter;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsOnlyCase(string between, bool upper, bool ignoreOtherThanLetters = false)
    {
        var isLetter = false;
        foreach (var character in between)
        {
            isLetter = char.IsLetter(character);
            if (isLetter || (!isLetter && ignoreOtherThanLetters))
            {
                if (upper)
                {
                    if (!char.IsUpper(character)) return false;
                }
                else
                {
                    if (!char.IsLower(character)) return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Shortens the string to the specified length.
    /// </summary>
    public static string ShortForLettersCount(string parameter, int maxLetterCount)
    {
        var shouldAddEllipsis = false;
        return ShortForLettersCount(parameter, maxLetterCount, out shouldAddEllipsis);
    }
    /// <summary>
    ///     Insert prefix starting with +
    /// </summary>
    /// <param name="value"></param>
    public static string TelephonePrefixToBrackets(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        input = NormalizeString(input);
        var parameter = SHSplit.Split(input, " ");
        parameter[0] = "(" + parameter[0] + ")";
        return string.Join(" ", parameter);
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsVariable(string innerHtml)
    {
        return ContainsVariable('{', '}', innerHtml);
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsVariable(char parameter, char closingChar, string innerHtml)
    {
        if (string.IsNullOrEmpty(innerHtml)) return false;
        var unprocessedVariableContent = new StringBuilder();
        var processedContent = new StringBuilder();
        var inVariable = false;
        foreach (var character in innerHtml)
            if (character == parameter)
            {
                inVariable = true;
            }
            else if (character == closingChar)
            {
                if (inVariable) inVariable = false;
                var consecutiveCount = 0;
                if (int.TryParse(unprocessedVariableContent.ToString(), out consecutiveCount))
                    return true;
                processedContent.Append(parameter + unprocessedVariableContent.ToString() + closingChar);
                unprocessedVariableContent.Clear();
            }
            else if (inVariable)
            {
                unprocessedVariableContent.Append(character);
            }
            else
            {
                processedContent.Append(character);
            }
        return false;
    }
    /// <summary>
    /// Get Variables In String operation on the input.
    /// </summary>
    public static List<int> GetVariablesInString(string innerHtml)
    {
        return GetVariablesInString('{', '}', innerHtml);
    }
    /// <param name="parameter">The opening bracket character.</param>
    /// <param name="closingChar">The closing bracket character.</param>
    /// <param name="innerHtml">The text to search for variable references.</param>
    public static List<int> GetVariablesInString(char parameter, char closingChar, string innerHtml)
    {
        // Vrátí mi formáty, které jsou v A1 od 0 do A2-1
        // A1={0} {2} {3} A2=3 G=0,2
        var variableIndices = new List<int>();
        var unprocessedVariableContent = new StringBuilder();
        //StringBuilder processedContent = new StringBuilder();
        var inVariable = false;
        foreach (var character in innerHtml)
            if (character == parameter)
            {
                inVariable = true;
            }
            else if (character == closingChar)
            {
                if (inVariable) inVariable = false;
                var consecutiveCount = 0;
                if (int.TryParse(unprocessedVariableContent.ToString(), out consecutiveCount)) variableIndices.Add(consecutiveCount);
                unprocessedVariableContent.Clear();
            }
            else if (inVariable)
            {
                unprocessedVariableContent.Append(character);
            }
        return variableIndices;
    }
    /// <summary>
    ///     Really return list, for string join value
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
    ///     G zda je jedinz znak v A1 text dia.
    /// </summary>
    public static bool ContainsDiacritic(string word)
    {
        return word != TextWithoutDiacritic(word);
    }

    /// <summary>
    ///     Pokud je poslední znak v A1 A2, odstraním ho
    /// </summary>
    /// <param name="pluralWord"></param>
    /// <param name="p"></param>
    public static string ConvertPluralToSingleEn(string pluralWord)
    {
        if (pluralWord[pluralWord.Length - 1] == 's')
        {
            if (pluralWord[pluralWord.Length - 2] == 'e')
                if (pluralWord[pluralWord.Length - 3] == 'i')
                    return pluralWord.Substring(0, pluralWord.Length - 3) + "y";
            return pluralWord.Substring(0, pluralWord.Length - 1);
        }
        return pluralWord;
    }
    // takhle to bylo předtím ale teď to tu mám 2x se stejnými parametry
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static string WrapWith(string value, char v, bool _trimWrapping = false)
    //{
    //    // TODO: Make with StringBuilder, because of SH.WordAfter and so
    //    return WrapWith(value, v.ToString(), _trimWrapping);
    //}
    /// <summary>
    ///     Vše tu funguje výborně
    ///     Metoda pokud chci vybrat ze textu A1 posledních p_2 znaků které jsou v celku(oddělené mezerami) argument vložit před ně
    ///     ...
    /// </summary>
    /// <param name="p"></param>
    /// <param name="maxLetterCount"></param>
    public static string ShortForLettersCountThreeDots(string parameter, int maxLetterCount)
    {
        var shouldAddEllipsis = false;
        var result = ShortForLettersCount(parameter, maxLetterCount, out shouldAddEllipsis);
        if (shouldAddEllipsis) result += " ... ";
        result = result.Replace("\"", string.Empty);
        return result;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsOtherChatThanLetterAndDigit(string parameter)
    {
        foreach (var item in parameter)
            if (!char.IsLetterOrDigit(item))
                return true;
        return false;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetOddIndexesOfWord(string input)
    {
        var half = input.Length / 2;
        half = half / 2;
        half += half / 2;
        var stringBuilder = new StringBuilder(half);
        var stepSize = 2;
        for (var i = 0; i < input.Length; i += stepSize) stringBuilder.Append(input[i]);
        return stringBuilder.ToString();
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
    ///     Údajně detekuje i japonštinu argument podpobné jazyky
    /// </summary>
    /// <param name="strategy"></param>
    public static bool IsChinese(string strategy)
    {
        var hiragana = GetCharsInRange(strategy, 0x3040, 0x309F);
        if (hiragana) return true;
        var katakana = GetCharsInRange(strategy, 0x30A0, 0x30FF);
        if (katakana) return true;
        var kanji = GetCharsInRange(strategy, 0x4E00, 0x9FBF);
        if (kanji) return true;
        if (strategy.Any(input => input >= 0x20000 && input <= 0xFA2D)) return true;
        return false;
    }
    /// <summary>
    ///     Nevraci znaky na indexech ale zda nektere znaky maji rozsah char definovany v A2,3
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static bool GetCharsInRange(string strategy, int min, int max)
    {
        return strategy.Where(e => e >= min && e <= max).Count() != 0;
    }
    ///// <param name="nazevPP"></param>
    ///// <param name="only"></param>
    //public static string FirstCharUpper(string nazevPP, bool only = false)
    //{
    //    return se.SH.FirstCharUpper(nazevPP, only);
    //}
    /// <summary>
    /// Remove Duplicates None operation on the input.
    /// </summary>
    public static List<string> RemoveDuplicatesNone(string input, string delimiter)
    {
        var split = SHSplit.SplitNone(input, delimiter);
        split = split.Distinct().ToList();
        return split;
    }
    /// <summary>
    ///     Most of Love me like you do have in title - from Fifty shades of grey
    /// </summary>
    /// <param name="title"></param>
    /// <param name="squareBrackets"></param>
    /// <param name="parentheses"></param>
    /// <param name="braces"></param>
    /// <param name="afterSds"></param>
    public static string RemoveBracketsAndHisContent(string input, bool squareBrackets, bool parentheses, bool braces,
        bool afterSdsFrom)
    {
        if (squareBrackets) input = RemoveBetweenAndEdgeChars(input, "]", "[");
        if (parentheses) input = RemoveBetweenAndEdgeChars(input, "(", ")");
        if (braces) input = RemoveBetweenAndEdgeChars(input, "{", "}");
        if (afterSdsFrom)
        {
            var fromClauseIndex = input.IndexOf(" - from");
            if (fromClauseIndex == -1) fromClauseIndex = input.IndexOf(SunamoNotTranslateAble.From);
            if (fromClauseIndex != -1) input = input.Substring(0, fromClauseIndex + 1);
        }
        input = input.Replace(" ", string.Empty)
            .Trim(); //SHReplace.ReplaceAll(input, "", "").Trim();
        return input;
    }
    /// <summary>
    ///     A2,3 can be string or char
    /// </summary>
    /// <param name="s"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static string RemoveBetweenAndEdgeChars(string strategy, string begin, string end)
    {
        var regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
        return regex.Replace(strategy, string.Empty);
    }
    /// <summary>
    ///     Je dobré před voláním této metody převést bílé znaky v A1 na mezery
    /// </summary>
    /// <param name="textContent"></param>
    /// <param name="middleIndex"></param>
    /// <param name="charactersPerSide"></param>
    public static string XCharsBeforeAndAfterWholeWords(string textContent, int middleIndex, int charactersPerSide)
    {
        var rightSide = new StringBuilder();
        var currentWord = new StringBuilder();
        // Teď to samé udělám i pro levou stranu
        var leftSide = new StringBuilder();
        for (var i = middleIndex - 1; i >= 0; i--)
        {
            var currentChar = textContent[i];
            if (currentChar == ' ')
            {
                var wordString = currentWord.ToString();
                currentWord.Clear();
                if (wordString != "")
                {
                    leftSide.Insert(0, wordString + " ");
                    if (leftSide.Length + " ".Length + wordString.Length > charactersPerSide) break;
                }
            }
            else
            {
                currentWord.Insert(0, currentChar);
            }
        }
        var list = currentWord + " " + leftSide.ToString().TrimEnd(' ');
        list = list.TrimEnd(' ');
        charactersPerSide += charactersPerSide - list.Length;
        currentWord.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (var i = middleIndex; i < textContent.Length; i++)
        {
            var currentChar = textContent[i];
            if (currentChar == ' ')
            {
                var wordString = currentWord.ToString();
                currentWord.Clear();
                if (wordString != "")
                {
                    rightSide.Append(" " + wordString);
                    if (rightSide.Length + " ".Length + wordString.Length > charactersPerSide) break;
                }
            }
            else
            {
                currentWord.Append(currentChar);
            }
        }
        var parameter = rightSide.ToString().TrimStart(' ') + "" + currentWord;
        parameter = parameter.TrimStart(' ');
        var result = "";
        if (textContent.Contains(list + " ") && textContent.Contains(" " + parameter))
            result = list + "" + parameter;
        else
            result = list + parameter;
        return result;
    }
    /// <summary>
    ///     Vše tu funguje výborně
    ///     G text z A1, ktery bude obsahovat max A2 písmen - ne slov, protože někdo tam může vložit příliš dlouhé slova argument
    ///     nevypadalo by to pak hezky.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="maxLetterCount"></param>
    public static string ShortForLettersCountThreeDotsReverse(string parameter, int maxLetterCount)
    {
        parameter = parameter.Trim();
        var parameterLength = parameter.Length;
        var isLongerOrEqual = maxLetterCount <= parameterLength;
        if (isLongerOrEqual)
        {
            if (IsInLastXCharsTheseLetters(parameter, maxLetterCount, ' '))
            {
                var spaceIndex = 0;
                var data = parameter; //parameter.Substring(parameter.Length - zkratitO);
                var to = data.Length;
                var characterCount = 0;
                for (var i = to - 1; i >= 0; i--)
                {
                    characterCount++;
                    if (data[i] == ' ')
                    {
                        if (characterCount >= maxLetterCount) break;
                        spaceIndex = i;
                    }
                }
                data = data.Substring(spaceIndex + 1);
                if (data.Trim() != "") data = " ... " + data;
                return data;
                //}
            }
            return " ... " + parameter.Substring(parameter.Length - maxLetterCount);
        }
        return parameter;
    }
    /// <summary>
    /// Return Occurences Of String From To Word operation on the input.
    /// </summary>
    public static List<FromToWordString> ReturnOccurencesOfStringFromToWord(string textContent,
        params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0) return new List<FromToWordString>();
        textContent = textContent.ToLower();
        var wordRanges = new List<FromToWordString>();
        var list = textContent.Length;
        for (var i = 0; i < list; i++)
            foreach (var searchWord in hledaneSlova)
            {
                var allCharactersMatch = true;
                var offset = 0;
                while (offset < searchWord.Length)
                {
                    var dex = i + offset;
                    if (list > dex)
                    {
                        if (textContent[dex] != searchWord[offset])
                        {
                            allCharactersMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        allCharactersMatch = false;
                        break;
                    }
                    offset++;
                }
                if (allCharactersMatch)
                {
                    var ftw = new FromToWordString();
                    ftw.From = i;
                    ftw.To = i + offset - 1;
                    ftw.Word = searchWord;
                    wordRanges.Add(ftw);
                    i += offset;
                    break;
                }
            }
        return wordRanges;
    }
    private static bool IsInLastXCharsTheseLetters(string parameter, int parameterLength, params char[] letters)
    {
        for (var i = parameter.Length - 1; i >= parameterLength; i--)
            foreach (var letter in letters)
                if (parameter[i] == letter)
                    return true;
        return false;
    }
    //
    /// <summary>
    ///     Oddělovač může být pouze jediný znak, protože se to pak předává do metody text parametrem int!
    ///     If A1 dont have index A2, all chars
    /// </summary>
    /// <param name="input"></param>
    /// <param name="deli"></param>
    public static string GetFirstPartByLocation(string input, char deli)
    {
        var delimiterIndex = input.IndexOf(deli);
        return GetFirstPartByLocation(input, delimiterIndex);
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetFirstPartByLocation(string input, int delimiterIndex)
    {
        string parameter, remainder;
        parameter = input;
        if (delimiterIndex < input.Length) GetPartsByLocation(out parameter, out remainder, input, delimiterIndex);
        return parameter;
    }
    /// <summary>
    ///     return whether A1 ends with anything with A2
    /// </summary>
    /// <param name="source"></param>
    /// <param name="suffixes"></param>
    public static bool EndsWithArray(string source, params string[] suffixes)
    {
        foreach (var item in suffixes)
            if (source.EndsWith(item))
                return true;
        return false;
    }
    /// <summary>
    ///     Auto trim
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <returns></returns>
    public static string GetTextBetweenSimple(string parameter, string after, string before,
        bool throwExceptionIfNotContains = true)
    {
        var dxOfFounded = int.MinValue;
        var temp = GetTextBetween(parameter, after, before, out dxOfFounded, 0, throwExceptionIfNotContains);
        return temp;
    }
    /// <summary>
    ///     Auto trim
    /// </summary>
    /// <param name="p"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="dxOfFounded"></param>
    /// <param name="startSearchingAt"></param>
    /// <param name="throwExceptionIfNotContains"></param>
    /// <returns></returns>
    public static string GetTextBetween(string parameter, string after, string before, out int dxOfFounded,
        int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string? vr = null;
        dxOfFounded = parameter.IndexOf(after, startSearchingAt);
        var p3 = parameter.IndexOf(before, dxOfFounded + after.Length);
        var b2 = dxOfFounded != -1;
        var b3 = p3 != -1;
        if (b2 && b3)
        {
            dxOfFounded += after.Length;
            p3 -= 1;
            // When I return between ( ), there must be +1
            var length = p3 - dxOfFounded + 1;
            if (length < 1)
            {
                // Takhle to tu bylo předtím ale logicky je to nesmysl.
                //return parameter;
            }
            vr = parameter.Substring(dxOfFounded, length);
        }
        else
        {
            if (throwExceptionIfNotContains)
                ThrowEx.NotContains(parameter, after, before);
            else
                // 24-1-21 return null instead of parameter
                return null!;
            //vr = parameter;
        }
        // Na co to tady trimovat? Např. při vrácení zpět na hard coded se mi zničil kód tím že jsem místo AllStrings.space vkládal "". Částečně se mi zničili i tam kde bylo AllChars.space. Doteď jsem vůbec nevěděl co to způsobuje argument to jsem už tímhle opravil celé pinp.
        return vr!;
    }
    /// <summary>
    /// Checks if the string ends with the specified suffix.
    /// </summary>
    public static bool EndsWith(string input, string endsWith)
    {
        return input.EndsWith(endsWith);
    }
    /// <summary>
    /// Removes specified content from the string.
    /// </summary>
    public static bool RemovePrefix(ref string strategy, string prefix)
    {
        if (strategy.StartsWith(prefix))
        {
            strategy = strategy.Substring(prefix.Length);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Retrieves the specified portion or data from the string.
    /// </summary>
    public static string GetToFirstChar(string input, int indexOfChar)
    {
        if (indexOfChar != -1) return input.Substring(0, indexOfChar + 1);
        return input;
    }
    /// <summary>
    ///     Tato metoda byla výchozí, jen se jmenovala NullToString
    ///     OrEmpty pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="value"></param>
    public static string NullToStringOrEmpty(object value)
    {
        if (value == null) return "";
        var strategy = value.ToString()!;
        return strategy;
    }
    /// <summary>
    /// Checks if the input string contains the specified content.
    /// </summary>
    public static bool ContainsFromEnd(string input, char character, out int ContainsFromEndResult)
    {
        for (var i = input.Length - 1; i >= 0; i--)
            if (input[i] == character)
            {
                ContainsFromEndResult = i;
                return true;
            }
        ContainsFromEndResult = -1;
        return false;
    }
    /// <summary>
    /// Processes or retrieves content from the beginning of the string.
    /// </summary>
    public static string FirstWhichIsNotEmpty(params string[] strategy)
    {
        foreach (var item in strategy)
            if (item != "")
                return item;
        return "";
    }
    /// <summary>
    ///     Whether A1 is under A2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mask"></param>
    public static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }
    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat) return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, ".");
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        var pattern = new Regex(pat);
        return pattern.IsMatch(str);
    }
    /// <summary>
    ///     Return joined with space
    /// </summary>
    /// <param name="value"></param>
    public static string FirstCharOfEveryWordUpperDash(string input)
    {
        return FirstCharOfEveryWordUpper(input, '-');
    }
    /// <summary>
    ///     Return joined with space
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dash"></param>
    private static string FirstCharOfEveryWordUpper(string input, char dash)
    {
        var parameter = SHSplit.SplitChar(input, dash);
        for (var i = 0; i < parameter.Count; i++) parameter[i] = FirstCharUpper(parameter[i]);
        //p = CAChangeContent.ChangeContent0(null, parameter, FirstCharUpper);
        return string.Join(" ", parameter);
    }
    /// <summary>
    /// Determines whether the string matches the specified condition.
    /// </summary>
    public static bool IsNullOrWhiteSpace(string strategy)
    {
        if (strategy != null)
        {
            strategy = strategy.Trim();
            return strategy == "";
        }
        return true;
    }
    //public static bool HasTextRightFormat(string result, TextFormatDataString tfd)
    //{
    //    if (tfd.TrimBefore) result = result.Trim();
    //    long tfdOverallLength = 0;
    //    foreach (var item in tfd) tfdOverallLength += item.FromTo.To - item.FromTo.From + 1;
    //    var partsCount = tfd.Count;
    //    var actualCharFormatData = 0;
    //    var actualFormatData = tfd[actualCharFormatData];
    //    var followingFormatData = tfd[actualCharFormatData + 1];
    //    //int charCount = result.Length;
    //    //if (tfd.RequiredLength != -1)
    //    //{
    //    //    if (result.Length != tfd.RequiredLength)
    //    //    {
    //    //        return false;
    //    //    }
    //    //    charCount = Math.Min(result.Length, tfd.RequiredLength);
    //    //}
    //    var actualChar = 0;
    //    var processed = 0;
    //    var from = actualFormatData.FromTo.FromL;
    //    var remains = actualFormatData.FromTo.ToL;
    //    var tfdCountM1 = tfd.Count - 1;
    //    while (true)
    //    {
    //        var canBeAnyChar =
    //            actualFormatData.MustBe == null ||
    //            actualFormatData.MustBe.Length == 0; //SunamoCollectionsShared.CA.IsEmptyOrNull();
    //        var isRightChar = false;
    //        if (canBeAnyChar)
    //        {
    //            isRightChar = true;
    //            remains--;
    //        }
    //        else
    //        {
    //            if (result.Length <= actualChar) return false;
    //            isRightChar = actualFormatData.MustBe.Any(data => data == result[actualChar]); //CAG.IsEqualToAnyElement<char>(, );
    //            if (isRightChar && !canBeAnyChar)
    //            {
    //                actualChar++;
    //                processed++;
    //                remains--;
    //            }
    //        }
    //        if (!isRightChar)
    //        {
    //            if (result.Length <= actualChar) return false;
    //            isRightChar =
    //                followingFormatData.MustBe.Any(data => data == result[actualChar]); //CAG.IsEqualToAnyElement<char>(, );
    //            if (!isRightChar) return false;
    //            if (remains != 0 && processed < from) return false;
    //            if (isRightChar && !canBeAnyChar)
    //            {
    //                actualCharFormatData++;
    //                processed++;
    //                actualChar++;
    //                if (!CA.HasIndex(actualCharFormatData, tfd) && result.Length > actualChar) return false;
    //                actualFormatData = tfd[actualCharFormatData];
    //                if (CA.HasIndex(actualCharFormatData + 1, tfd))
    //                    followingFormatData = tfd[actualCharFormatData + 1];
    //                else
    //                    followingFormatData = CharFormatDataString.Templates.Any;
    //                processed = 0;
    //                remains = actualFormatData.FromTo.To;
    //                remains--;
    //            }
    //        }
    //        if (actualChar == tfdOverallLength)
    //            if (actualChar == result.Length)
    //                //break;
    //                return true;
    //        if (remains == 0)
    //        {
    //            ++actualCharFormatData;
    //            if (!CA.HasIndex(actualCharFormatData, tfd) && result.Length > actualChar) return false;
    //            actualFormatData = tfd[actualCharFormatData];
    //            if (CA.HasIndex(actualCharFormatData + 1, tfd))
    //                followingFormatData = tfd[actualCharFormatData + 1];
    //            else
    //                followingFormatData = CharFormatDataString.Templates.Any;
    //            processed = 0;
    //            remains = actualFormatData.FromTo.To;
    //        }
    //    }
    //}
    /// <param name="strategy"></param>
    /// <param name="append"></param>
    public static string AppendIfDontEndingWith(string strategy, string append)
    {
        if (strategy.EndsWith(append)) return strategy;
        return strategy + append;
    }
    #endregion
}
