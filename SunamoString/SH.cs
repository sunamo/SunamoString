namespace SunamoString;

/// <summary>
/// Represents the SH type.
/// </summary>
public partial class SH
{
    /// <summary>
    /// Gets or sets the brackets left list.
    /// </summary>
    protected static List<char> BracketsLeftList { get; set; } = null!;
    /// <summary>
    /// Gets or sets the brackets right list.
    /// </summary>
    protected static List<char> BracketsRightList { get; set; } = null!;
    private static StringBuilder StringBuilder { get; set; } = new();
    /// <summary>
    /// The x mismatch count in input arrays of SH all have right format.
    /// </summary>
    public static string XMismatchCountInInputArraysOfSHAllHaveRightFormat =
        "MismatchCountInInputArraysOfSHAllHaveRightFormat";

    /// <summary>
    /// Contains cl.
    /// </summary>
    /// <returns>The contains cl result.</returns>
    public static bool ContainsCl(string input, StringOrStringList searchTerm, SearchStrategy searchStrategy = SearchStrategy.FixedSpace, bool caseSensitive = false, bool isEnoughPartialContainsOfSplitted = true)
    {
        string? term = null;
        if (!caseSensitive)
        {
            input = input.ToLower();
            term = searchTerm.GetString().ToLower();
        }
        if (searchStrategy == SearchStrategy.ExactlyName)
        {
            return input == term;
        }
        if (searchStrategy == SearchStrategy.AnySpaces)
        {
            var inputParts = input.Split(input.Where(character => !char.IsLetterOrDigit(character)).ToArray(), StringSplitOptions.RemoveEmptyEntries);
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
    /// White space from start.
    /// </summary>
    /// <param name="term!">The term.</param>
    /// <returns>The white space from start result.</returns>
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
    /// Contains bool bool.
    /// </summary>
    /// <returns>The contains bool bool result.</returns>
    public static bool ContainsBoolBool(string input, string term, bool enoughIsContainsAttribute, bool caseSensitive)
    {
        return Contains(input, term, enoughIsContainsAttribute ? SearchStrategy.AnySpaces : SearchStrategy.ExactlyName,
            caseSensitive);
    }
    /// <summary>
    /// Contains.
    /// </summary>
    /// <returns>The contains result.</returns>
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
                    .ToList();
                return ContainsAll(input, allWords);
            }
            else
            {
                var allWords = term.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                for (var i = 0; i < allWords.Count; i++) allWords[i] = allWords[i].ToLower();
                return ContainsAll(input.ToLower(), allWords);
            }
        }
        return false;
    }
    /// <summary>
    /// Is contained.
    /// </summary>
    /// <param name="}">The }.</param>
    /// <param name="StringSplitOptions.RemoveEmptyEntries">The string split options remove empty entries.</param>
    /// <returns>The is contained result.</returns>
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
    /// Contains all.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="contains">The contains.</param>
    /// <returns>The contains all result.</returns>
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
    /// Contains.
    /// </summary>
    /// <param name="ccm">The ccm.</param>
    /// <returns>The contains result.</returns>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        return Contains(input, term, searchStrategy, true);
    }
    /// <summary>
    /// Initializes a new instance of the prefix if not started with class.
    /// </summary>
    /// <param name="!input.Contains(word)">The input contains word.</param>
    public static string PrefixIfNotStartedWith(string text, string prefix, bool skipWhitespaces = false)
    {
        var whitespaces = string.Empty;
        if (skipWhitespaces)
        {
            whitespaces = WhiteSpaceFromStart(text);
            text = text.Substring(whitespaces.Length);
        }
        if (!text.StartsWith(prefix)) return whitespaces + prefix + text;
        return whitespaces + text;
    }
    /// <summary>
    /// Remove last char.
    /// </summary>
    /// <param name="skipWhitespaces">The skip whitespaces.</param>
    /// <returns>The remove last char result.</returns>
    public static string RemoveLastChar(string input)
    {
        return input.Substring(0, input.Length - 1);
    }
    /// <summary>
    /// Postfix if not empty.
    /// </summary>
    /// <param name="whitespaces.Length">The whitespaces length.</param>
    /// <returns>The postfix if not empty result.</returns>
    public static string PostfixIfNotEmpty(string input, string postfix)
    {
        if (input.Length != 0)
            if (!input.EndsWith(postfix))
                return input + postfix;
        return input;
    }
    /// <summary>
    /// Add before upper chars.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <param name="1">The 1.</param>
    /// <returns>The add before upper chars result.</returns>
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
    /// Initializes a new instance of the remove ending pair chars when dont have starting class.
    /// </summary>
    /// <param name="char.IsUpper(input[i])">The char is upper input i.</param>
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
    /// Get pairs start and end.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="rightBracket">The right bracket.</param>
    /// <returns>The get pairs start and end result.</returns>
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
                leftBracketOccurrences.RemoveAt(leftBracketOccurrences.Count - 1);
            }
            else
            {
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
            }
            alreadyProcessedItem1.Add(list[i].Item1);
        }

        addToAnotherCollection = addToAnotherCollection.Distinct().ToList();
        foreach (var index in addToAnotherCollection)
        {
            var occurrenceCount = alreadyProcessedItem1.Where(entry => entry == index).Count();
            if (occurrenceCount > 2)
            {
                var selectedPairs = additionalPairs.Where(entry => entry.Item1 == index).ToList();
                var leftBracketIndex = leftBracketOccurrences.IndexOf(selectedPairs[0].Item1);
                if (leftBracketIndex != -1)
                {
                    list.Add(new Tuple<int, int>(leftBracketOccurrences[leftBracketIndex - 1], selectedPairs[0].Item2));
                }
            }
        }
        leftBracketOccurrences.Sort();
        var result = list;
        var alreadyProcessed = new List<int>();
        var foundIndex = -1;
        for (var resultIndex = 0; resultIndex < result.Count; resultIndex++)
        {
            var pairEntry = result[resultIndex];
            var startIndex = pairEntry.Item1;
            if (alreadyProcessed.Contains(startIndex))
            {
                foundIndex = leftBracketOccurrences.IndexOf(startIndex);
                if (foundIndex != -1)
                {
                    startIndex = leftBracketOccurrences[foundIndex - 1];
                    result[startIndex] = new Tuple<int, int>(startIndex, result[resultIndex - 1].Item2);
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
    /// Remove and insert replace.
    /// </summary>
    /// <returns>The remove and insert replace result.</returns>
    public static string RemoveAndInsertReplace(string input, int startIndex, string oldValue, string newValue)
    {
        input = input.Remove(startIndex, oldValue.Length);
        input = input.Insert(startIndex, newValue);
        return input;
    }
    /// <summary>
    /// Initializes a new instance of the replace once class.
    /// </summary>
    public static string ReplaceOnce(string input, string oldValue, string replacement)
    {
        if (oldValue == "") return input;
        var position = input.IndexOf(oldValue);
        if (position == -1) return input;
        return input.Substring(0, position) + replacement + input.Substring(position + oldValue.Length);
    }
    /// <summary>
    /// Replace once if started with.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace once if started with result.</returns>
    public static string ReplaceOnceIfStartedWith(string input, string searchPrefix, string replacement)
    {
        bool replaced;
        return ReplaceOnceIfStartedWith(input, searchPrefix, replacement, out replaced);
    }
    /// <summary>
    /// Replace once if started with.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <returns>The replace once if started with result.</returns>
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
    /// Normalize string.
    /// </summary>
    /// <param name="input.StartsWith(searchPrefix)">The input starts with search prefix.</param>
    /// <returns>The normalize string result.</returns>
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
    /// Initializes a new instance of the return occurences of string class.
    /// </summary>
    /// <param name="input">The input.</param>
    public static List<int> ReturnOccurencesOfString(string searchText, string searchTerm)
    {
        var results = new List<int>();
        for (var index = 0; index < searchText.Length - searchTerm.Length + 1; index++)
        {
            var substring = searchText.Substring(index, searchTerm.Length);
            if (substring == searchTerm)
                results.Add(index);
        }
        return results;
    }
    /// <summary>
    /// Tab or space next to.
    /// </summary>
    /// <typeparam name="int">The type int.</typeparam>
    /// <returns>The tab or space next to result.</returns>
    public static List<int> TabOrSpaceNextTo(string input)
    {
        var tabs = ReturnOccurencesOfString(input, "\t");
        return tabs;
    }
    /// <summary>
    /// Initializes a new instance of the wrap with bs class.
    /// </summary>
    /// <param name="index">The index.</param>
    public static string WrapWithBs(string input)
    {
        return WrapWithChar(input, '\\');
    }
    /// <summary>
    /// Wrap with space.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The wrap with space result.</returns>
    public static string WrapWithSpace(string input)
    {
        return WrapWithChar(input, ' ');
    }
    /// <summary>
    /// Wrap with qm.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The wrap with qm result.</returns>
    public static string WrapWithQm(string input)
    {
        return WrapWithQm(input, true);
    }
    /// <summary>
    /// Wrap with if.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The wrap with if result.</returns>
    public static string WrapWithIf(string text, string wrapper, Func<string, string, bool> predicate)
    {
        if (predicate.Invoke(text, wrapper)) return WrapWith(text, wrapper);
        return text;
    }
    /// <summary>
    /// Wrap with qm.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The wrap with qm result.</returns>
    public static string WrapWithQm(string text, bool isWrappingWhitespaceOrEmpty = true)
    {
        return WrapWithChar(text, '"', isWrappingWhitespaceOrEmpty);
    }
    /// <summary>
    /// Occurences of string in.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="wrapper">The wrapper.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>The occurences of string in result.</returns>
    public static int OccurencesOfStringIn(string text, string searchTerm)
    {
        return text.Split(new[] { searchTerm }, StringSplitOptions.None).Length - 1;
    }
    /// <summary>
    /// Get parts by location.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isWrappingWhitespaceOrEmpty">The is wrapping whitespace or empty.</param>
    /// <returns>The get parts by location result.</returns>
    public static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = "";
        }
        else
        {
            before = text.Substring(0, position);
            if (text.Length > position + 1)
                after = text.Substring(position + 1);
            else
                after = string.Empty;
        }
    }
    /// <summary>
    /// Get parts by location no out int.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <param name="position">The position.</param>
    /// <returns>The get parts by location no out int result.</returns>
    public static (string, string) GetPartsByLocationNoOutInt(string text, int position)
    {
        string before, after;
        GetPartsByLocation(out before, out after, text, position);
        return (before, after);
    }
    /// <summary>
    /// Get parts by location no out.
    /// </summary>
    /// <param name="string">The string.</param>
    /// <returns>The get parts by location no out result.</returns>
    public static (string, string) GetPartsByLocationNoOut(string text, char delimiter)
    {
        GetPartsByLocation(out var before, out var after, text, delimiter);
        return (before, after);
    }
    /// <summary>
    /// Get parts by location.
    /// </summary>
    /// <param name="before">The before.</param>
    /// <param name="after">The after.</param>
    /// <param name="text">The text.</param>
    /// <param name="position">The position.</param>
    /// <returns>The get parts by location result.</returns>
    public static void GetPartsByLocation(out string before, out string after, string text, char delimiter)
    {
        var delimiterIndex = text.IndexOf(delimiter);
        GetPartsByLocation(out before, out after, text, delimiterIndex);
    }
    /// <summary>
    /// Not allowed in ranges.
    /// </summary>
    /// <param name="before">The before.</param>
    /// <param name="after">The after.</param>
    /// <param name="text">The text.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The not allowed in ranges result.</returns>
    public static bool NotAllowedInRanges(object rangeChecker, int indexToTest)
    {
        if (rangeChecker is Func<int, bool>)
        {
            var predicate = (Func<int, bool>)rangeChecker;
            return predicate(indexToTest);
        }
        ThrowEx.NotImplementedCase("NotAllowedInRanges: " + rangeChecker);
        return false;
    }
    /// <summary>
    /// Get text between two chars.
    /// </summary>
    /// <param name="bool>">The bool.</param>
    /// <returns>The get text between two chars result.</returns>
    public static string GetTextBetweenTwoChars(string text, char beginChar, char endChar,
        bool isThrowingIfNotContains = true, object? notAllowedInRanges = null, bool isUsingLastIndexOf = false)
    {
        var beginIndex = text.IndexOf(beginChar);
        var endIndex = -1;
        if (isUsingLastIndexOf)
        {
            endIndex = text.LastIndexOf(endChar);
        }
        else
        {
            endIndex = text.IndexOf(endChar, beginIndex + 1);
            if (notAllowedInRanges != null)
                while (endIndex != NumConsts.MOne && NotAllowedInRanges(notAllowedInRanges, endIndex))
                    endIndex = text.IndexOf(endChar, endIndex + 1);
        }
        if (beginIndex == NumConsts.MOne || endIndex == NumConsts.MOne)
        {
            if (isThrowingIfNotContains)
            {
                ThrowEx.NotContains(text, beginChar.ToString(), endChar.ToString());
            }
            else
            {
                if (endIndex == NumConsts.MOne) return null!;
            }
        }
        else
        {
            return GetTextBetweenTwoCharsInts(text, beginIndex, endIndex);
        }
        return text;
    }
    /// <summary>
    /// Initializes a new instance of the get text between two chars ints class.
    /// </summary>
    /// <param name="endIndex">The end index.</param>
    public static string GetTextBetweenTwoCharsInts(string text, int beginIndex, int endIndex)
    {
        if (endIndex > beginIndex)
            return text.Substring(beginIndex + 1, endIndex - beginIndex - 1);
        return text;
    }
    /// <summary>
    /// First char upper.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="beginIndex">The begin index.</param>
    /// <param name="endIndex">The end index.</param>
    /// <returns>The first char upper result.</returns>
    public static void FirstCharUpper(ref string text)
    {
        text = FirstCharUpper(text);
    }
    /// <summary>
    /// First char upper.
    /// </summary>
    /// <param name="beginIndex">The begin index.</param>
    /// <returns>The first char upper result.</returns>
    public static string FirstCharUpper(string text)
    {
        if (text.Length == 1) return text.ToUpper();
        var remainder = text.Substring(1);
        return text[0].ToString().ToUpper() + remainder;
    }
    /// <summary>
    /// Concat if before has value.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The concat if before has value result.</returns>
    public static string ConcatIfBeforeHasValue(params string[] array)
    {
        var result = new StringBuilder();
        for (var i = 0; i < array.Length; i++)
        {
            var evenElement = array[i];
            if (!string.IsNullOrWhiteSpace(evenElement))
                result.Append(evenElement + array[++i]);
        }
        return result.ToString();
    }
    /// <summary>
    /// From space 160 to 32.
    /// </summary>
    /// <returns>The from space 160 to 32 result.</returns>
    public static string FromSpace160To32(string text)
    {
        text = Regex.Replace(text, @"\p{Z}", " ");
        return text;
    }
    /// <summary>
    /// Initializes a new instance of the is number class.
    /// </summary>
    /// <param name="array[++i]">The array i.</param>
    public static bool IsNumber(string text, params char[] nextAllowedChars)
    {
        foreach (var character in text)
            if (!char.IsNumber(character))
                if (!nextAllowedChars.Contains(character))
                    return false;
        return true;
    }
    /// <summary>
    /// Make up to x chars.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="nextAllowedChars">The next allowed chars.</param>
    /// <returns>The make up to x chars result.</returns>
    public static string MakeUpToXChars(int number, int targetLength)
    {
        var stringBuilder = new StringBuilder();
        var numberText = number.ToString();
        var paddingCount = (number.ToString().Length - targetLength) * -1;
        for (var i = 0; i < paddingCount; i++) stringBuilder.Append(0);
        stringBuilder.Append(numberText);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Get first char.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="targetLength">The target length.</param>
    /// <returns>The get first char result.</returns>
    public static char GetFirstChar(string text)
    {
        return text[0];
    }
    /// <summary>
    /// To pascal case.
    /// </summary>
    /// <param name="targetLength">The target length.</param>
    /// <returns>The to pascal case result.</returns>
    public static string ToPascalCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        var words = text.Split(' ');
        for (var i = 0; i < words.Length; i++)
            if (words[i].Length > 0)
                words[i] = words[i][0].ToString().ToUpper() + words[i].Substring(1);
        return string.Join("", words);
    }
    /// <summary>
    /// Start with whitespace.
    /// </summary>
    /// <param name="string.IsNullOrEmpty(text)">The string is null or empty text.</param>
    /// <returns>The start with whitespace result.</returns>
    public static bool StartWithWhitespace(string text)
    {
        return text.TrimStart() != text;
    }
    /// <summary>
    /// Initializes a new instance of the detect newline class.
    /// </summary>
    /// <param name="i">The i.</param>
    public static string DetectNewline(string text)
    {
        if (text.Contains("\r\n")) return "\r\n";
        return "\n";
    }
    /// <summary>
    /// Remove last word.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The remove last word result.</returns>
    public static string RemoveLastWord(string text)
    {
        return SHParts.RemoveAfterLast(text.Trim(), " ");
    }
    /// <summary>
    /// Get indexes of lines starting with.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The get indexes of lines starting with result.</returns>
    public static List<int> GetIndexesOfLinesStartingWith(List<string> list, Func<string, bool> predicate)
    {
        var allIndices = list.Select((line, i) => new { Str = line, Index = i })
            .Where(element => predicate(element.Str))
            .Select(element => element.Index).ToList();
        return allIndices;
    }
    /// <summary>
    /// Remove lines which contains.
    /// </summary>
    /// <param name="text.Trim()">The text trim.</param>
    /// <param name=""">The ".</param>
    /// <returns>The remove lines which contains result.</returns>
    public static string RemoveLinesWhichContains(string text, string textToRemove)
    {
        var list = SHGetLines.GetLines(text);
        list = list.Where(line => !line.Contains(textToRemove)).ToList();
        var result = string.Join(Environment.NewLine, list);
        return result;
    }
    /// <summary>
    /// Add if not contains.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="textToRemove">The text to remove.</param>
    /// <returns>The add if not contains result.</returns>
    public static string AddIfNotContains(string text, string textToAdd, string? lowerCaseVersion = null)
    {
        if (lowerCaseVersion != null)
        {
            textToAdd = lowerCaseVersion;
            text = text.ToLower();
        }
        if (!text.Contains(textToAdd)) return text + " " + textToAdd;
        return text;
    }
    /// <summary>
    /// Switch swap.
    /// </summary>
    /// <param name="!">The !.</param>
    /// <returns>The switch swap result.</returns>
    public static string SwitchSwap(string text, string delimiter)
    {
        var parts = SHSplit.Split(text, delimiter);
        if (parts.Count == 2) return parts[1] + "," + parts[0];
        return null!;
    }
    /// <summary>
    /// Initializes a new instance of the insert before ending bracket class.
    /// </summary>
    /// <param name="!text.Contains(textToAdd)">The text contains text to add.</param>
    public static string InsertBeforeEndingBracket(string text, string textToInsert)
    {
        var bracketIndex = text.LastIndexOf(')');
        if (bracketIndex != -1) return text.Insert(bracketIndex, textToInsert);
        return text;
    }
    /// <summary>
    /// Initializes a new instance of the statistic letter chars class.
    /// </summary>
    /// <param name="parts.Count">The parts count.</param>
    public static Dictionary<char, int> StatisticLetterChars(string text, StatisticLetterCharsStrategy strategy,
        params char[] charsToStrategy)
    {
        List<char>? ignoreCompletely = null;
        if (strategy == StatisticLetterCharsStrategy.IgnoreCompletely) ignoreCompletely = new List<char>(charsToStrategy);
        var characterCounts = new Dictionary<char, int>();
        if (strategy == StatisticLetterCharsStrategy.AddAsFirst)
            foreach (var character in charsToStrategy)
                characterCounts.Add(character, 0);
        foreach (var character in text)
        {
            if (strategy == StatisticLetterCharsStrategy.IgnoreCompletely)
                if (ignoreCompletely!.Contains(character))
                    continue;
            DictionaryHelper.AddOrPlus(characterCounts, character, 1);
        }
        return characterCounts;
    }
    /// <summary>
    /// Initializes a new instance of the all brackets class.
    /// </summary>
    /// <param name="text">The text.</param>
    public static List<char> AllBrackets(string text)
    {
        var bracketChars = new List<char>();
        var isEnd = false;
        for (var i = 0; i < text.Length; i++)
        {
            var bracket = GetBracketFromBegin(text[i], ref isEnd, false);
            if (bracket != Brackets.None) bracketChars.Add(text[i]);
        }
        return bracketChars;
    }
    /// <summary>
    /// Indexes of brackets.
    /// </summary>
    /// <typeparam name="char">The type char.</typeparam>
    /// <returns>The indexes of brackets result.</returns>
    public static Tuple<SquareMap, SquareMapLines> IndexesOfBrackets(string text)
    {
        var squareMap = new SquareMap();
        var lineBasedMap = new SquareMapLines(squareMap);
        var isEnd = false;
        var lineNumber = 0;
        var isCarriageReturn = false;
        for (var i = 0; i < text.Length; i++)
        {
            var currentChar = text[i];
            if (isCarriageReturn)
            {
                isCarriageReturn = false;
                lineNumber++;
                if (currentChar == '\n') continue;
            }
            if (currentChar == '\n')
            {
                lineNumber++;
                continue;
            }
            if (currentChar == '\r')
            {
                isCarriageReturn = true;
                continue;
            }
            var bracket = GetBracketFromBegin(currentChar, ref isEnd, false);
            if (bracket != Brackets.None)
            {
                squareMap.Add(bracket, isEnd, i);
                lineBasedMap.Add(bracket, isEnd, i, lineNumber);
            }
        }
        return new Tuple<SquareMap, SquareMapLines>(squareMap, lineBasedMap);
    }
    /// <summary>
    /// Replace brackets.
    /// </summary>
    /// <param name="currentChar">The current char.</param>
    /// <param name="isEnd">The is end.</param>
    /// <param name="false">The false.</param>
    /// <returns>The replace brackets result.</returns>
    public static string ReplaceBrackets(string text, Brackets what, Brackets replacement)
    {
        text = text.Replace(BracketsLeft[what], BracketsLeft[replacement]);
        text = text.Replace(BracketsRight[what], BracketsRight[replacement]);
        return text;
    }
    /// <summary>
    /// Contains any from element.
    /// </summary>
    /// <param name="squareMap">The square map.</param>
    /// <param name="lineBasedMap">The line based map.</param>
    /// <returns>The contains any from element result.</returns>
    public static List<int> ContainsAnyFromElement(StringBuilder stringBuilder, IList<string> list)
    {
        var result = new List<int>();
        var matchIndex = 0;
        foreach (var searchValue in list)
        {
            if (stringBuilder.ToString().Contains(searchValue)) result.Add(matchIndex);
            matchIndex++;
        }
        return result;
    }
    /// <summary>
    /// Find closing bracket index char.
    /// </summary>
    /// <typeparam name="int">The type int.</typeparam>
    /// <returns>The find closing bracket index char result.</returns>
    public static int FindClosingBracketIndexChar(StringBuilder stringBuilder, bool isRemovingBetween, string openedBracket = "{")
    {
        var index = stringBuilder.ToString().IndexOf(openedBracket);
        return FindClosingBracketIndex(stringBuilder, isRemovingBetween, stringBuilder[index]);
    }
    /// <summary>
    /// Find closing bracket index.
    /// </summary>
    /// <param name="stringBuilder">The string builder.</param>
    /// <param name="isRemovingBetween">The is removing between.</param>
    /// <param name="openedBracket">The opened bracket.</param>
    /// <returns>The find closing bracket index result.</returns>
    public static int FindClosingBracketIndex(StringBuilder stringBuilder, bool isRemovingBetween, int startIndex)
    {
        var openedBracket = stringBuilder[startIndex];
        var closedBracket = ClosingBracketFor(openedBracket);
        var start = startIndex;
        var bracketCount = 1;
        var currentChar = 'a';
        for (var i = startIndex + 1; i < stringBuilder.Length; i++)
        {
            currentChar = stringBuilder[i];
            if (currentChar == openedBracket)
                bracketCount++;
            else if (currentChar == closedBracket) bracketCount--;
            if (bracketCount == 0)
            {
                startIndex = i;
                break;
            }
        }
        if (isRemovingBetween) RemoveBetweenIndexes(stringBuilder, start, startIndex);
        return startIndex;
    }
    private static void RemoveBetweenIndexes(StringBuilder stringBuilder, int startIndex, int endIndex)
    {
        ThrowEx.StartIsHigherThanEnd(startIndex, endIndex);
        startIndex++;
        for (; startIndex < endIndex; startIndex++) stringBuilder[startIndex] = ' ';
    }
    /// <summary>
    /// Initializes a new instance of the check whether no bracked is before other 2 class.
    /// </summary>
    /// <param name="isRemovingBetween">The is removing between.</param>
    public static bool CheckWhetherNoBrackedIsBeforeOther2(string text)
    {
        return BalancedBrackets.AreBracketsBalanced(AllBrackets(text));
    }
    /// <summary>
    /// Check whether no bracked is before other 1.
    /// </summary>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">The end index.</param>
    /// <returns>The check whether no bracked is before other 1 result.</returns>
    public static bool CheckWhetherNoBrackedIsBeforeOther1(string text)
    {
        const string openBraces = "([{";
        const string closeBraces = ")]}";
        var stack = new Stack<char>();
        foreach (var character in text)
            if (openBraces.Contains(character))
                stack.Push(character);
            else if (stack.Count == 0 || openBraces.IndexOf(stack.Pop()) != closeBraces.IndexOf(character)) return false;
        return stack.Count == 0;
    }
    /// <summary>
    /// Convert whitespace to visible.
    /// </summary>
    /// <param name="closeBraces">The close braces.</param>
    /// <returns>The convert whitespace to visible result.</returns>
    public static string ConvertWhitespaceToVisible(string text)
    {
        text = text.Replace('\t', UnicodeWhiteToVisible.Tab);
        text = text.Replace('\n', UnicodeWhiteToVisible.NewLine);
        text = text.Replace('\r', UnicodeWhiteToVisible.CarriageReturn);
        text = text.Replace(' ', UnicodeWhiteToVisible.Space);
        return text;
    }
    /// <summary>
    /// Concat space.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The concat space result.</returns>
    public static string ConcatSpace(IList list)
    {
        var stringBuilder = new StringBuilder();
        foreach (string element in list) stringBuilder.Append(element + " ");
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Is null or white space range.
    /// </summary>
    /// <param name="'">The '.</param>
    /// <param name="UnicodeWhiteToVisible.Space">The unicode white to visible space.</param>
    /// <returns>The is null or white space range result.</returns>
    public static bool IsNullOrWhiteSpaceRange(params string[] array)
    {
        foreach (var text in array)
            if (IsNullOrWhiteSpace(text))
                return true;
        return false;
    }
    /// <summary>
    /// Is single line.
    /// </summary>
    /// <returns>The is single line result.</returns>
    public static bool IsSingleLine(string text)
    {
        return !text.Trim().Contains(Environment.NewLine);
    }
    /// <summary>
    /// Initializes a new instance of the get whitespace from beginning class.
    /// </summary>
    /// <param name="array">The array.</param>
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
    /// <summary>
    /// Chars before and after.
    /// </summary>
    /// <returns>The chars before and after result.</returns>
    public static string CharsBeforeAndAfter(string text, string centerString, int centerIndex, int before, int after)
    {
        var startIndex = centerIndex - before;
        var endIndex = centerIndex + centerString.Length + after;
        var stringBuilder = new StringBuilder();
        if (HasIndex(startIndex, text, false))
        {
            stringBuilder.Append(text.Substring(startIndex, before));
            stringBuilder.Append(" ");
        }
        stringBuilder.Append(centerString);
        if (HasIndex(endIndex, text, false))
        {
            stringBuilder.Append(text.Substring(endIndex, after));
            stringBuilder.Append(" ");
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Contains new line.
    /// </summary>
    /// <param name="centerString">The center string.</param>
    /// <returns>The contains new line result.</returns>
    public static bool ContainsNewLine(string text)
    {
        return text.Contains('\n') || text.Contains('\r');
    }
    /// <summary>
    /// Initializes a new instance of the change encoding process wrong characters class.
    /// </summary>
    /// <param name="after)">The after.</param>
    public static bool ChangeEncodingProcessWrongCharacters(ref string input)
    {
        return ChangeEncodingProcessWrongCharacters(ref input, Encoding.GetEncoding("latin1"));
    }
    /// <summary>
    /// Change encoding process wrong characters.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The change encoding process wrong characters result.</returns>
    public static bool ChangeEncodingProcessWrongCharacters(ref string input, Encoding oldEncoding)
    {
        if (IsValidISO(input))
        {
            var encodedBytes = oldEncoding.GetBytes(input);
            input = Encoding.UTF8.GetString(encodedBytes);
            return true;
        }
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
    /// Add space after first letter for every and sort.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The add space after first letter for every and sort result.</returns>
    public static List<string> AddSpaceAfterFirstLetterForEveryAndSort(List<string> input)
    {
        CA.Trim(input);
        for (var i = 0; i < input.Count; i++) input[i] = input[i].Insert(1, " ");
        input.Sort();
        return input;
    }
    /// <summary>
    /// Get last word.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The get last word result.</returns>
    public static string GetLastWord(string text, bool returnEmptyWhenDontHaveLenght = true)
    {
        text = text.Trim();
        var spaceIndex = text.LastIndexOf(' ');
        if (spaceIndex != -1) return text.Substring(spaceIndex).Trim();
        if (returnEmptyWhenDontHaveLenght) return string.Empty;
        return text;
    }
    /// <summary>
    /// Add space and dont duplicate.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="returnEmptyWhenDontHaveLenght">The return empty when dont have lenght.</param>
    /// <returns>The add space and dont duplicate result.</returns>
    public static string AddSpaceAndDontDuplicate(bool after, string text, string colon)
    {
        List<int>? colonPositions = null;
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(text);
        if (after)
        {
            colonPositions = ReturnOccurencesOfString(text, colon);
            for (var i = colonPositions.Count - 1; i >= 0; i--) stringBuilder.Insert(colonPositions[i] + 1, " ");
            colonPositions = ReturnOccurencesOfString(stringBuilder.ToString(), colon + "  ");
            for (var i = colonPositions.Count - 1; i >= 0; i--) stringBuilder.Remove(colonPositions[i] + 1, 1);
        }
        else
        {
            colonPositions = ReturnOccurencesOfString(text, colon);
            for (var i = colonPositions.Count - 1; i >= 0; i--) stringBuilder.Insert(colonPositions[i], " ");
            colonPositions = ReturnOccurencesOfString(stringBuilder.ToString(), "  " + colon);
            for (var i = colonPositions.Count - 1; i >= 0; i--) stringBuilder.Remove(colonPositions[i], 1);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Count of items.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="colon">The colon.</param>
    /// <returns>The count of items result.</returns>
    public static string CountOfItems(List<KeyValuePair<string, int>> counted)
    {
        var stringBuilder = new StringBuilder();
        foreach (var kvp in counted) stringBuilder.AppendLine(kvp.Value + "x " + kvp.Key);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Multi whitespace line to single.
    /// </summary>
    /// <returns>The multi whitespace line to single result.</returns>
    public static string MultiWhitespaceLineToSingle(List<string> lines)
    {
        var joinedText = string.Join(Environment.NewLine, lines);
        return joinedText;
    }
    /// <summary>
    /// Indent as previous line.
    /// </summary>
    /// <returns>The indent as previous line result.</returns>
    public static void IndentAsPreviousLine(List<string> lines)
    {
        var previousIndent = string.Empty;
        string? line = null;
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < lines.Count - 1; i++)
        {
            line = lines[i];
            if (line.Length > 0)
            {
                if (!char.IsWhiteSpace(line[0]))
                {
                    lines[i] = previousIndent + lines[i];
                }
                else
                {
                    stringBuilder.Clear();
                    foreach (var character in line)
                        if (char.IsWhiteSpace(character))
                            stringBuilder.Append(character);
                        else
                            break;
                    previousIndent = stringBuilder.ToString();

                    // Line consists only of whitespace - collapse it to empty instead of leaving stray whitespace
                    if (previousIndent.Length == line.Length)
                    {
                        lines[i] = string.Empty;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Contains line.
    /// </summary>
    /// <param name="previousIndent.Length">The previous indent length.</param>
    /// <returns>The contains line result.</returns>
    public static bool ContainsLine(string text, bool isCheckingCaseForSingleString, params string[] contains)
    {
        return ContainsLine2(text, isCheckingCaseForSingleString, contains);
    }
    /// <summary>
    /// Contains line 2.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isCheckingCaseForSingleString">The is checking case for single string.</param>
    /// <param name="contains">The contains.</param>
    /// <returns>The contains line 2 result.</returns>
    public static bool ContainsLine2(string text, bool isCheckingCaseForSingleString, IList<string> contains)
    {
        var hasLine = false;
        if (contains.Count() == 1)
        {
            if (isCheckingCaseForSingleString) hasLine = text.Contains(contains.First());
        }
        else
        {
            foreach (var searchTerm in contains)
                if (text.Contains(searchTerm)) {
                    hasLine = true;
                    break;
                }
        }
        return hasLine;
    }
    /// <summary>
    /// Word after.
    /// </summary>
    /// <param name="contains">The contains.</param>
    /// <returns>The word after result.</returns>
    public static string WordAfter(string input, string word)
    {
        input = WrapWithChar(input, ' ');
        var foundIndex = input.IndexOf(word);
        var nextSpaceIndex = input.IndexOf(' ', foundIndex + 1);
        var stringBuilder = new StringBuilder();
        if (nextSpaceIndex != -1)
        {
            nextSpaceIndex++;
            for (var i = nextSpaceIndex; i < input.Length; i++)
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
    /// Leading.
    /// </summary>
    /// <param name="!">The !.</param>
    /// <returns>The leading result.</returns>
    public static string Leading(string input, Func<char, bool> predicate)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in input)
            if (predicate.Invoke(character))
                stringBuilder.Append(character);
            else
                break;
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Is on index.
    /// </summary>
    /// <returns>The is on index result.</returns>
    public static bool IsOnIndex(string input, int index, Func<char, bool> predicate)
    {
        if (input.Length > index) return predicate.Invoke(input[index]);
        return false;
    }
    /// <summary>
    /// Count lines.
    /// </summary>
    /// <returns>The count lines result.</returns>
    public static int CountLines(string text)
    {
        return Regex.Matches(text, Environment.NewLine).Count;
    }
    /// <summary>
    /// Has letter.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="index">The index.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>The has letter result.</returns>
    public static bool HasLetter(string text)
    {
        foreach (var character in text)
            if (char.IsLetter(character))
                return true;
        return false;
    }
    /// <summary>
    /// Get texts between.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="Environment.NewLine">The environment new line.</param>
    /// <returns>The get texts between result.</returns>
    public static List<string> GetTextsBetween(string text, string afterDelimiter, string beforeDelimiter,
        bool isRequiringNonLetterBeforeMatch = false)
    {
        return GetTextsBetween(text, afterDelimiter, beforeDelimiter, isRequiringNonLetterBeforeMatch, out isRequiringNonLetterBeforeMatch);
    }
    /// <summary>
    /// Initializes a new instance of the get texts between class.
    /// </summary>
    /// <param name="char.IsLetter(character)">The char is letter character.</param>
    public static List<string> GetTextsBetween(string text, string afterDelimiter, string beforeDelimiter, bool isRequiringNonLetterBeforeMatch,
        out bool firstCharBeforeIsLetter)
    {
        firstCharBeforeIsLetter = false;
        var results = new List<string>();
        var indexesAfter = ReturnOccurencesOfString(text, afterDelimiter);
        var indexesBefore = ReturnOccurencesOfString(text, beforeDelimiter);
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
                    indexAfterFinal = indexesAfter[0] + afterDelimiter.Length;
                    indexBeforeFinal = indexesBefore.FirstOrDefault(data => data > indexAfterFinal) - 1;
                    if (indexBeforeFinal == 0)
                    {
                        throw new Exception("There is no number higher than " + indexAfterFinal);
                    }
                }
                indexBeforeToAccessCollection--;
                continue;
            }
            indexAfterFinal = indexAfter + afterDelimiter.Length;
            indexBeforeFinal = indexBefore - 1;
            var substringed = text.Substring(indexAfterFinal, indexBeforeFinal - indexAfterFinal + 1).Trim();
            if (isRequiringNonLetterBeforeMatch)
            {
                if (indexAfterFinal != 0)
                {
                    var charBeforeEnd = text[indexBeforeFinal - 1];
                    var charBeforeStart = text[indexAfterFinal - afterDelimiter.Length - 1];
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
    /// Remove last letters.
    /// </summary>
    /// <param name="substringed">The substringed.</param>
    /// <returns>The remove last letters result.</returns>
    public static string RemoveLastLetters(string input, int characterCount)
    {
        if (input.Length > characterCount) return input.Substring(0, input.Length - characterCount);
        return input;
    }
    /// <summary>
    /// Has char right format.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="characterCount">The character count.</param>
    /// <returns>The has char right format result.</returns>
    public static bool HasCharRightFormat(char character, CharFormatDataString charFormatData)
    {
        if (charFormatData.Upper.HasValue)
        {
            if (charFormatData.Upper.Value)
            {
                if (char.IsLower(character)) return false;
            }
            else
            {
                if (char.IsUpper(character)) return false;
            }
        }
        if (charFormatData.MustBe != null && charFormatData.MustBe.Length != 0)
        {
            foreach (var requiredChar in charFormatData.MustBe)
                if (requiredChar == character)
                    return true;
            return false;
        }
        return true;
    }
    /// <summary>
    /// Initializes a new instance of the get text in last square brackets and other class.
    /// </summary>
    /// <param name="!">The !.</param>
    public static bool GetTextInLastSquareBracketsAndOther(string text, out string mainText, out string bracketedText)
    {
        mainText = bracketedText = null!;
        text = text.Trim();
        if (text[text.Length - 1] != ']')
            return false;
        text = text.Substring(0, text.Length - 1);
        var bracketIndex = text.LastIndexOf(']');
        if (bracketIndex == -1)
            return false;
        if (bracketIndex != -1) SHSplit.SplitByIndex(text, bracketIndex, out mainText, out bracketedText);
        return true;
    }
    /// <summary>
    /// Initializes a new instance of the remove brackets with text case insensitive class.
    /// </summary>
    /// <param name="!">The !.</param>
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
    /// Remove brackets without text.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="patterns">The patterns.</param>
    /// <returns>The remove brackets without text result.</returns>
    public static string RemoveBracketsWithoutText(string input)
    {
        return SHReplace.ReplaceAll(input, "", "()", "[]");
    }
    /// <summary>
    /// Without special chars.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name=""">The ".</param>
    /// <returns>The without special chars result.</returns>
    public static string WithoutSpecialChars(string input, params char[] excludedCharacters)
    {
        SpecialCharsService specialCharsService = new();
        var stringBuilder = new StringBuilder();
        foreach (var character in input)
            if (!specialCharsService.SpecialChars.Contains(character) &&
                !excludedCharacters.Any(data => data == character))
                stringBuilder.Append(character);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Remove brackets from start.
    /// </summary>
    /// <returns>The remove brackets from start result.</returns>
    public static string RemoveBracketsFromStart(string text)
    {
        while (true)
        {
            var foundBracket = false;
            if (text.StartsWith("("))
            {
                var closingIndex = text.IndexOf(")");
                if (closingIndex != -1 && closingIndex != text.Length - 1)
                {
                    foundBracket = true;
                    text = text.Substring(closingIndex + 1);
                }
            }
            else if (text.StartsWith("["))
            {
                var closingIndex = text.IndexOf("]");
                if (closingIndex != -1 && closingIndex != text.Length - 1)
                {
                    foundBracket = true;
                    text = text.Substring(closingIndex + 1);
                }
            }
            if (!foundBracket) break;
        }
        return text;
    }
    /// <summary>
    /// Remove last char if is.
    /// </summary>
    /// <param name="1">The 1.</param>
    /// <returns>The remove last char if is result.</returns>
    public static string RemoveLastCharIfIs(string text, char character)
    {
        var lastIndex = text.Length - 1;
        if (text[lastIndex] == character) return text.Substring(0, lastIndex);
        return text;
    }
}
