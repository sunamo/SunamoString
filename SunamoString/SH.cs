namespace SunamoString;

/// <summary>
/// Represents the SH type.
/// </summary>
public class SH
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

    /// <summary>
    /// Get last part by string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="character">The character.</param>
    /// <returns>The get last part by string result.</returns>
    public static string GetLastPartByString(string input, string returnFromString)
    {
        var lastIndex = input.LastIndexOf(returnFromString);
        if (lastIndex == -1) return input;
        var start = lastIndex + returnFromString.Length;
        if (start < input.Length) return input.Substring(start);
        return input;
    }
    /// <summary>
    /// Add empty lines.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="returnFromString">The return from string.</param>
    /// <returns>The add empty lines result.</returns>
    public static string AddEmptyLines(string content, int addRowsDuringScrolling)
    {
        var lines = SHGetLines.GetLines(content);
        for (var i = 0; i < addRowsDuringScrolling; i++) lines.Add(string.Empty);
        return string.Join(Environment.NewLine, lines);
    }
    /// <summary>
    /// Initializes a new instance of the to case class.
    /// </summary>
    /// <param name="input.Length">The input length.</param>
    public static string ToCase(string input, bool? isUpperCase)
    {
        if (isUpperCase.HasValue)
        {
            if (isUpperCase.Value)
                return input.ToUpper();
            return input.ToLower();
        }
        return input;
    }
    /// <summary>
    /// Ends with number.
    /// </summary>
    /// <param name="isUpperCase.HasValue">The is upper case has value.</param>
    /// <returns>The ends with number result.</returns>
    public static bool EndsWithNumber(string input)
    {
        for (var i = 0; i < 10; i++)
            if (input.EndsWith(i.ToString()))
                return true;
        return false;
    }
    /// <summary>
    /// Null to string or null.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The null to string or null result.</returns>
    public static string? NullToStringOrNull(object? value)
    {
        if (value == null) return null;
        return value.ToString();
    }
    /// <summary>
    /// Initializes a new instance of the last char equals class.
    /// </summary>
    /// <param name="input.EndsWith(i.ToString())">The input ends with i to string.</param>
    public static bool LastCharEquals(string input, char delimiter)
    {
        if (!string.IsNullOrEmpty(input)) return false;
        var lastChar = input[input.Length - 1];
        if (lastChar == delimiter) return true;
        return false;
    }
    /// <summary>
    /// Get without last word.
    /// </summary>
    /// <returns>The get without last word result.</returns>
    public static string GetWithoutLastWord(string text)
    {
        text = text.Trim();
        var lastSpaceIndex = text.LastIndexOf(' ');
        if (lastSpaceIndex != -1) return text.Substring(0, lastSpaceIndex);
        return text;
    }
    /// <summary>
    /// Delete chars out of ascii.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The delete chars out of ascii result.</returns>
    public static string DeleteCharsOutOfAscii(string text)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in text)
        {
            int charValue = character;
            if (charValue < 128) stringBuilder.Append(character);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Remove diacritics.
    /// </summary>
    /// <returns>The remove diacritics result.</returns>
    public static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var character in normalizedString)
            switch (CharUnicodeInfo.GetUnicodeCategory(character))
            {
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    stringBuilder.Append(character);
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
    /// Strip functations and symbols.
    /// </summary>
    /// <param name="'_'">The '_'.</param>
    /// <returns>The strip functations and symbols result.</returns>
    public static string StripFunctationsAndSymbols(string text)
    {
        var stringBuilder = new StringBuilder();
        foreach (var character in text)
            if (!char.IsPunctuation(character) && !char.IsSymbol(character))
                stringBuilder.Append(character);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Return occurences of string from to.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The return occurences of string from to result.</returns>
    public static List<FromToString> ReturnOccurencesOfStringFromTo(string searchText, string searchTerm)
    {
        var searchLength = searchTerm.Length;
        var results = new List<FromToString>();
        for (var index = 0; index < searchText.Length - searchTerm.Length + 1; index++)
            if (searchText.Substring(index, searchTerm.Length) == searchTerm)
            {
                var range = new FromToString();
                range.From = index;
                range.To = index + searchLength - 1;
                results.Add(range);
            }
        return results;
    }
    /// <summary>
    /// Initializes a new instance of the get without first word class.
    /// </summary>
    /// <param name="searchTerm.Length)">The search term length.</param>
    public static string GetWithoutFirstWord(string text)
    {
        text = text.Trim();
        var spaceIndex = text.IndexOf(' ');
        if (spaceIndex != -1) return text.Substring(spaceIndex + 1);
        return text;
    }
    /// <summary>
    /// Ends with index.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The ends with index result.</returns>
    public static int EndsWithIndex(string source, params string[] endingsToCheck)
    {
        for (var i = 0; i < endingsToCheck.Length; i++)
            if (source.EndsWith(endingsToCheck[i]))
                return i;
        return -1;
    }
    /// <summary>
    /// Get to first.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="endingsToCheck">The endings to check.</param>
    /// <returns>The get to first result.</returns>
    public static string GetToFirst(string input, string searchFor)
    {
        var indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1) return input.Substring(0, indexOfChar + 1);
        return input;
    }

    /// <summary>
    /// First char lower.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="searchFor">The search for.</param>
    /// <returns>The first char lower result.</returns>
    public static string FirstCharLower(string input)
    {
        if (input.Length < 2) return input;
        var stringBuilder = input.Substring(1);
        return input[0].ToString().ToLower() + stringBuilder;
    }
    /// <summary>
    /// Convert typed whitespace to string.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The convert typed whitespace to string result.</returns>
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

    /// <summary>
    /// Null to string or default.
    /// </summary>
    /// <param name="nullableObject">The nullable object.</param>
    /// <returns>The null to string or default result.</returns>
    public static string NullToStringOrDefault(object nullableObject)
    {
        return nullableObject == null ? " " + "(null)" : " " + nullableObject;
    }
    /// <summary>
    /// Wrap with.
    /// </summary>
    /// <param name="nullableObject">The nullable object.</param>
    /// <returns>The wrap with result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }
    /// <summary>
    /// Wrap with char.
    /// </summary>
    /// <param name="null">The null.</param>
    /// <returns>The wrap with char result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWithChar(string value, char wrapperChar, bool shouldTrimWrapping = false,
        bool shouldIncludeWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !shouldIncludeWhitespaceOrEmpty) return string.Empty;
        // TODO: Make with StringBuilder, because of WordAfter and so
        return WrapWith(shouldTrimWrapping ? value.Trim() : value, wrapperChar.ToString());
    }
    /// <summary>
    /// The brackets left.
    /// </summary>
    protected static Dictionary<Brackets, char> BracketsLeft = null!;
    /// <summary>
    /// The brackets right.
    /// </summary>
    protected static Dictionary<Brackets, char> BracketsRight = null!;
    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="wrapperChar">The wrapper char.</param>
    /// <param name="shouldTrimWrapping">The should trim wrapping.</param>
    /// <param name="shouldIncludeWhitespaceOrEmpty">The should include whitespace or empty.</param>
    /// <returns>The init result.</returns>
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
    /// <summary>
    /// Count of.
    /// </summary>
    /// <param name="BracketsLeft">The brackets left.</param>
    /// <returns>The count of result.</returns>
    public static int CountOf(string input, char character)
    {
        var count = 0;
        foreach (var currentChar in input)
            if (currentChar == character)
                count++;
        return count;
    }
    /// <summary>
    /// Has index.
    /// </summary>
    /// <returns>The has index result.</returns>
    public static bool HasIndex(int parameter, string text, bool isThrowingExceptionOnInvalidIndex = true)
    {
        if (parameter < 0)
        {
            if (isThrowingExceptionOnInvalidIndex)
                throw new Exception("Invalid parameter");
            return false;
        }
        if (text.Length > parameter) return true;
        return false;
    }
    /// <summary>
    /// Initializes a new instance of the is negation class.
    /// </summary>
    /// <param name="currentChar">The current char.</param>
    public static bool IsNegation(string searchTerm)
    {
        if (searchTerm[0] == '!') return true;
        return false;
    }
    /// <summary>
    /// Is negation tuple.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <returns>The is negation tuple result.</returns>
    public static (bool, string) IsNegationTuple(string searchTerm)
    {
        if (searchTerm[0] == '!')
        {
            searchTerm = searchTerm.Substring(1);
            return (true, searchTerm);
        }
        return (false, searchTerm);
    }
    /// <summary>
    /// Is contained.
    /// </summary>
    /// <param name="searchTerm">The search term.</param>
    /// <returns>The is contained result.</returns>
    public static bool IsContained(string text, string searchTerm)
    {
        var (negation, extractedTerm) = IsNegationTuple(searchTerm);
        searchTerm = extractedTerm;
        if (negation && text.Contains(searchTerm))
            return false;
        if (!negation && !text.Contains(searchTerm)) return false;
        return true;
    }
    /// <summary>
    /// Equals one of this.
    /// </summary>
    /// <param name="1">The 1.</param>
    /// <returns>The equals one of this result.</returns>
    public static bool EqualsOneOfThis(string text, params string[] values)
    {
        foreach (var element in values)
            if (text == element)
                return true;
        return false;
    }
    /// <summary>
    /// Text without diacritic.
    /// </summary>
    /// <param name="negation">The negation.</param>
    /// <param name="extractedTerm">The extracted term.</param>
    /// <returns>The text without diacritic result.</returns>
    public static string TextWithoutDiacritic(string input)
    {
        return input.RemoveDiacritics();
    }
    /// <summary>
    /// Get first word.
    /// </summary>
    /// <param name="!text.Contains(searchTerm)">The text contains search term.</param>
    /// <returns>The get first word result.</returns>
    public static string GetFirstWord(string text, bool returnEmptyWhenDontHaveLenght = true)
    {
        text = text.Trim();
        var spaceIndex = text.IndexOf(' ');
        if (spaceIndex != -1) return text.Substring(0, spaceIndex);
        if (returnEmptyWhenDontHaveLenght) return string.Empty;
        return text;
    }
    /// <summary>
    /// Contains any bool.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The contains any bool result.</returns>
    public static bool ContainsAnyBool(string text, bool isCheckingInCaseOnlyOneString, IList<string> contains)
    {
        return ContainsAny(text, isCheckingInCaseOnlyOneString, contains).Count > 0;
    }
    /// <summary>
    /// First word which is number.
    /// </summary>
    /// <returns>The first word which is number result.</returns>
    public static int FirstWordWhichIsNumber(string input, int probablyIndex,
        bool shouldJoinAdjacentNumbers = false)
    {
        var words = SHSplit.Split(input, " ");
        if (words.Count > probablyIndex)
        {
            if (BTS.IsInt(words[probablyIndex]))
            {
                if (shouldJoinAdjacentNumbers)
                {
                    var concatenatedNumber = BTS.LastInt + NH.JoinAnotherTokensIfIsNumber(words, probablyIndex + 1);
                    return int.Parse(concatenatedNumber);
                }
                return BTS.LastInt;
            }
            return FirstWordWhichIsNumberAllIndexes(words, shouldJoinAdjacentNumbers);
        }
        return FirstWordWhichIsNumberAllIndexes(words, shouldJoinAdjacentNumbers);
    }
    /// <summary>
    /// Initializes a new instance of the first word which is number all indexes class.
    /// </summary>
    /// <param name="probablyIndex">The probably index.</param>
    public static int FirstWordWhichIsNumberAllIndexes(List<string> words, bool shouldJoinAdjacentNumbers = true)
    {
        var index = 0;
        foreach (var word in words)
            if (BTS.IsInt(word))
            {
                index++;
                if (shouldJoinAdjacentNumbers)
                {
                    var concatenatedNumber = BTS.LastInt + NH.JoinAnotherTokensIfIsNumber(words, index);
                    return int.Parse(concatenatedNumber);
                }
                return BTS.LastInt;
            }
        return int.MinValue;
    }
    /// <summary>
    /// Compare string ignore whitespaces 2.
    /// </summary>
    /// <param name="words">The words.</param>
    /// <returns>The compare string ignore whitespaces 2 result.</returns>
    public static bool CompareStringIgnoreWhitespaces2(string firstText, string secondText)
    {
        return string.Compare(firstText, secondText, CultureInfo.CurrentCulture,
            CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
    }
    /// <summary>
    /// Compare string ignore whitespaces.
    /// </summary>
    /// <param name="shouldJoinAdjacentNumbers">The should join adjacent numbers.</param>
    /// <returns>The compare string ignore whitespaces result.</returns>
    public static bool CompareStringIgnoreWhitespaces(string firstText, string secondText)
    {
        var firstNormalized = Regex.Replace(firstText, @"\s", "");
        var secondNormalized = Regex.Replace(secondText, @"\s", "");
        var stringEquals = string.Equals(
            firstNormalized,
            secondNormalized,
            StringComparison.OrdinalIgnoreCase);
        return stringEquals;
    }
    /// <summary>
    /// Contains only.
    /// </summary>
    /// <param name="firstText">The first text.</param>
    /// <param name="secondText">The second text.</param>
    /// <returns>The contains only result.</returns>
    public static bool ContainsOnly(string input, List<char> numericChars)
    {
        if (input.Length == 0) return false;
        foreach (var character in input)
            if (!numericChars.Contains(character))
                return false;
        return true;
    }
    /// <summary>
    /// First line.
    /// </summary>
    /// <param name="secondText">The second text.</param>
    /// <param name="@"\s"">The s.</param>
    /// <param name="""">The "".</param>
    /// <returns>The first line result.</returns>
    public static string FirstLine(string text)
    {
        var lines = SHGetLines.GetLines(text);
        return lines.Count == 0 ? string.Empty : lines[0];
    }
    /// <summary>
    /// First char upper.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="numericChars">The numeric chars.</param>
    /// <returns>The first char upper result.</returns>
    public static string FirstCharUpper(string text, bool isOnlyFirstLetter = false)
    {
        if (text != null)
        {
            var remainder = text.Substring(1);
            if (isOnlyFirstLetter) remainder = remainder.ToLower();
            return text[0].ToString().ToUpper() + remainder;
        }
        return null!;
    }
    /// <summary>
    /// In brackets.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The in brackets result.</returns>
    public static string InBrackets(string input)
    {
        return GetTextBetweenTwoCharsInts(input, input.IndexOf('('), input.IndexOf(')'));
    }
    /// <summary>
    /// Contains any.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isOnlyFirstLetter">The is only first letter.</param>
    /// <returns>The contains any result.</returns>
    public static List<string> ContainsAny(string text, bool isCheckingCaseForSingleString, IList<string> contains)
    {
        var matchedItems = new List<string>();
        if (contains.Count() == 1 && isCheckingCaseForSingleString)
            text.Contains(contains.First());
        else
            foreach (var searchItem in contains)
                if (text.Contains(searchItem))
                    matchedItems.Add(searchItem);
        return matchedItems;
    }
    /// <summary>
    /// Contains any char.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The contains any char result.</returns>
    public static List<char> ContainsAnyChar(string text, bool isCheckingCaseForSingleString, IList<char> contains)
    {
        var matchedItems = new List<char>();
        if (contains.Count() == 1 && isCheckingCaseForSingleString)
            text.Contains(contains.First());
        else
            foreach (var character in contains)
                if (text.Contains(character))
                    matchedItems.Add(character);
        return matchedItems;
    }
    /// <summary>
    /// Initializes a new instance of the contains any t class.
    /// </summary>
    /// <param name="text.Contains(searchItem)">The text contains search item.</param>
    public static List<T> ContainsAny<T>(
        bool isCheckingCaseForSingleString, T value, IList<T> contains)
    {
        throw new Exception(
            "This method is entirely broken, always use the non-generic ContainsAny method instead.");
    }
    /// <summary>
    /// Initializes a new instance of the get word on index class.
    /// </summary>
    /// <param name="contains.Count()">The contains count.</param>
    [Obsolete("This method relied on SHData.ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces which was overly complex. Do not restore, rewrite if needed.")]
    public static string? GetWordOnIndex(string line, int index)
    {
        return null;
    }
    /// <summary>
    /// Initializes a new instance of the contains upper class.
    /// </summary>
    /// <param name="text.Contains(character)">The text contains character.</param>
    public static bool ContainsUpper(string text)
    {
        return text.Any(char.IsUpper);
    }
    /// <summary>
    /// Contains lower.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="isCheckingCaseForSingleString">The is checking case for single string.</param>
    /// <param name="value">The value.</param>
    /// <param name="contains">The contains.</param>
    /// <returns>The contains lower result.</returns>
    public static bool ContainsLower(string text)
    {
        return text.Any(char.IsLower);
    }
    /// <summary>
    /// Get text between.
    /// </summary>
    /// <param name="restore">The restore.</param>
    /// <param name="needed."">The needed.</param>
    /// <returns>The get text between result.</returns>
    public static string GetTextBetween(string parameter, char after, char before,
        bool throwExceptionIfNotContains = true /*cant have implicit value*/,
        object? notAllowedInRanges = null /*cant have implicit value*/, bool endLastIndexOf = false)
    {
        return GetTextBetweenTwoChars(parameter, after, before, throwExceptionIfNotContains, notAllowedInRanges,
            endLastIndexOf);
    }
    /// <summary>
    /// Values between quotes.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The values between quotes result.</returns>
    public static List<string> ValuesBetweenQuotes(string text, bool isInsertingBackToQuotes, bool isUsingApostrophes = false)
    {
        var quoteString = "\"";
        if (isUsingApostrophes) quoteString = "'";
        return ValuesBetweenQuotesOrApos(text, isInsertingBackToQuotes, quoteString);
    }
    /// <summary>
    /// Values between quotes and apos.
    /// </summary>
    /// <param name="char.IsLower">The char is lower.</param>
    /// <returns>The values between quotes and apos result.</returns>
    public static List<string> ValuesBetweenQuotesAndApos(string text, bool isInsertingBackToQuotes,
        bool isFilteringUnmatched = false)
    {
        var sourceList = ValuesBetweenQuotesOrApos(text, isInsertingBackToQuotes, "\"", isFilteringUnmatched);
        sourceList.AddRange(ValuesBetweenQuotesOrApos(text, isInsertingBackToQuotes, "'", isFilteringUnmatched));
        return sourceList;
    }
    private static List<string> ValuesBetweenQuotesOrApos(string text, bool isInsertingBackToQuotes, string quoteString,
        bool isFilteringUnmatched = false)
    {
        var quoteChar = quoteString[0];
        var quotedTextRegex = new Regex(quoteString + ".*?" + quoteString);
        var matches = quotedTextRegex.Matches(text);
        var result = new List<string>(matches.Count);
        foreach (var item in matches)
        {
            var matchText = item.ToString()!;
            if (isFilteringUnmatched)
                if (text.Contains("t(" + matchText + ")"))
                    continue;
            if (isInsertingBackToQuotes)
                result.Add(matchText);
            else
                result.Add(matchText.TrimEnd(quoteChar).TrimStart(quoteChar));
        }
        return result;
    }
    /// <summary>
    /// Contains at least one.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The contains at least one result.</returns>
    public static bool ContainsAtLeastOne(string text, List<string> list)
    {
        foreach (var searchTerm in list)
            if (text.Contains(searchTerm))
                return true;
        return false;
    }
    /// <summary>
    /// First char of every word part.
    /// </summary>
    /// <param name="isInsertingBackToQuotes">The is inserting back to quotes.</param>
    /// <returns>The first char of every word part result.</returns>
    public static string FirstCharOfEveryWordPart(string text, string delimiter)
    {
        var parts = SHSplit.Split(text, delimiter);
        var stringBuilder = new StringBuilder();
        foreach (var part in parts) stringBuilder.Append(part[0].ToString());
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Increment last number.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="list">The list.</param>
    /// <returns>The increment last number result.</returns>
    public static void IncrementLastNumber(ref string input)
    {
        var lastChar = input[input.Length - 1];
        if (char.IsNumber(lastChar))
        {
            var number = int.Parse(lastChar.ToString());
            number++;
            input = input.Substring(0, input.Length - 1) + number;
            return;
        }
        input = input + "1";
    }
    /// <summary>
    /// Initializes a new instance of the get line from char index class.
    /// </summary>
    /// <param name="parts">The parts.</param>
    public static string GetLineFromCharIndex(string text, List<string> lines, int characterIndex)
    {
        var lineIndex = GetLineIndexFromCharIndex(text, characterIndex);
        return lines[lineIndex];
    }
    /// <summary>
    /// Get line index from char index.
    /// </summary>
    /// <param name="char.IsNumber(lastChar)">The char is number last char.</param>
    /// <returns>The get line index from char index result.</returns>
    public static int GetLineIndexFromCharIndex(string text, int characterPosition)
    {
        var lineNumber = text.Take(characterPosition).Count(character => character == '\n') + 1;
        return lineNumber - 1;
    }
    /// <summary>
    /// Another other than letter or digit.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <param name="1">The 1.</param>
    /// <returns>The another other than letter or digit result.</returns>
    public static int AnotherOtherThanLetterOrDigit(string text, int startIndex)
    {
        var currentIndex = startIndex;
        for (; currentIndex < text.Length; currentIndex++)
            if (!char.IsLetterOrDigit(text[currentIndex]))
                return currentIndex;
        return currentIndex--;
    }
    /// <summary>
    /// Last chars.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="characterPosition">The character position.</param>
    /// <returns>The last chars result.</returns>
    public static string LastChars(string text, int characterCount)
    {
        return text.Substring(text.Length - characterCount);
    }
    /// <summary>
    /// Tab to new line.
    /// </summary>
    /// <param name="characterPosition">The character position.</param>
    /// <returns>The tab to new line result.</returns>
    public static string TabToNewLine(string text)
    {
        text = text.Replace("\t", "\r");
        var list = SHGetLines.GetLines(text);
        CA.Trim(list);
        list = list.Where(element => element.Trim() != string.Empty).ToList();
        return string.Join(Environment.NewLine, list);
    }
    /// <summary>
    /// Is all lower.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="characterCount">The character count.</param>
    /// <returns>The is all lower result.</returns>
    public static bool IsAllLower(string text)
    {
        return IsAllLower(text, char.IsLower);
    }
    private static bool IsAllLower(string text, Func<char, bool> isLower)
    {
        for (var i = 0; i < text.Length; i++)
            if (!isLower(text[i]))
                return false;
        return true;
    }
    /// <summary>
    /// Initializes a new instance of the is all upper class.
    /// </summary>
    /// <param name="list">The list.</param>
    public static bool IsAllUpper(string text)
    {
        return IsAllLower(text, char.IsUpper);
    }
    /// <summary>
    /// Contains bracket.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The contains bracket result.</returns>
    public static bool ContainsBracket(string text, bool isRequiringBothSides = false)
    {
        List<char>? left = null;
        List<char>? right = null;
        return ContainsBracket(text, ref left, ref right, isRequiringBothSides);
    }
    /// <summary>
    /// The is czech culture.
    /// </summary>
    protected static bool IsCzechCulture;
    static SH()
    {
        IsCzechCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();
    }
    /// <summary>
    /// Indexes of chars.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="char.IsUpper">The char is upper.</param>
    /// <returns>The indexes of chars result.</returns>
    public static List<int> IndexesOfChars(string input, char searchChar)
    {
        return IndexesOfCharsList(input, new List<char>(searchChar));
    }
    /// <summary>
    /// Indexes of chars list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <param name="isRequiringBothSides">The is requiring both sides.</param>
    /// <returns>The indexes of chars list result.</returns>
    public static List<int> IndexesOfCharsList(string text, List<char> characters)
    {
        var indices = new List<int>();
        foreach (var character in characters) indices.AddRange(ReturnOccurencesOfString(text, character.ToString()));
        indices.Sort();
        return indices;
    }
    /// <summary>
    /// Contains bracket.
    /// </summary>
    /// <returns>The contains bracket result.</returns>
    public static bool ContainsBracket(string text, ref List<char>? left, ref List<char>? right,
        bool isRequiringBothSides = false)
    {
        left = ContainsAnyChar(text, false, AllLists.LeftBrackets);
        right = ContainsAnyChar(text, false, AllLists.LeftBrackets);
        if (isRequiringBothSides)
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
    /// Initializes a new instance of the closing bracket for class.
    /// </summary>
    public static char ClosingBracketFor(char openingBracket)
    {
        foreach (var bracketEntry in BracketsLeft)
            if (bracketEntry.Value == openingBracket)
                return BracketsRight[bracketEntry.Key];
        ThrowEx.IsNotAllowed(openingBracket + " as bracket");
        return char.MaxValue;
    }
    /// <summary>
    /// Initializes a new instance of the text after class.
    /// </summary>
    /// <param name="isRequiringBothSides">The is requiring both sides.</param>
    public static string TextAfter(string text, string after)
    {
        var foundIndex = text.IndexOf(after);
        if (foundIndex != -1) return text.Substring(foundIndex + after.Length);
        return string.Empty;
    }
    /// <summary>
    /// Initializes a new instance of the pad right class.
    /// </summary>
    /// <param name="0">The 0.</param>
    public static string PadRight(string baseString, string paddingText, int characterCount)
    {
        var stringBuilder = new StringBuilder(baseString);
        for (var i = 0; i < characterCount; i++) stringBuilder.Append(paddingText);
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Initializes a new instance of the remove last char sb class.
    /// </summary>
    /// <param name="BracketsLeft">The brackets left.</param>
    public static void RemoveLastCharSb(StringBuilder stringBuilder)
    {
        if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1);
    }
    /// <summary>
    /// Remove useless whitespaces.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="after">The after.</param>
    /// <returns>The remove useless whitespaces result.</returns>
    public static string RemoveUselessWhitespaces(string text)
    {
        var parts = SHSplit.SplitChar(text);
        return string.Join("", parts);
    }
    /// <summary>
    /// Initializes a new instance of the short to length by paragraph class.
    /// </summary>
    /// <param name="!">The !.</param>
    public static string ShortToLengthByParagraph(string text, int maxLength)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        var parts = SHSplit.SplitChar(text, whitespaceChar.WhitespaceChars.ToArray());
        while (text.Length + parts.Count > maxLength)
            if (parts.Count > 1)
            {
                parts.RemoveAt(parts.Count - 1);
                text = string.Join(" ", parts);
            }
            else
            {
                text = text.Substring(0, maxLength);
                break;
            }
        return text;
    }
    /// <summary>
    /// To number t.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxLength">The max length.</param>
    /// <returns>The to number t result.</returns>
    public static T ToNumber<T>(Func<string, T> parse, string input)
    {
        return parse.Invoke(input);
    }
    /// <summary>
    /// Repair quotes.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="whitespaceChar.WhitespaceChars.ToArray()">The whitespace char whitespace chars to array.</param>
    /// <returns>The repair quotes result.</returns>
    public static string RepairQuotes(string text)
    {
        text = text.Replace("�", "\"");
        text = text.Replace("�", "\"");
        text = text.Replace("�", "'");
        text = text.Replace("�", "'");
        return text;
    }
    /// <summary>
    /// Is numbered.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <param name="maxLength">The max length.</param>
    /// <returns>The is numbered result.</returns>
    public static bool IsNumbered(string input)
    {
        var digitCount = 0;
        foreach (var character in input)
            if (char.IsNumber(character))
            {
                digitCount++;
            }
            else if (character == '.')
            {
                if (digitCount > 0) return true;
            }
            else
            {
                return false;
            }
        return false;
    }
    /// <summary>
    /// Insert ending bracket.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The insert ending bracket result.</returns>
    public static string InsertEndingBracket(string input, char startingBracket)
    {
        var closingBracket = ClosingBracketFor(startingBracket);
        var openingBracketCount = ReturnOccurencesOfString(input, startingBracket.ToString());
        var closingBracketCount = ReturnOccurencesOfString(input, closingBracket.ToString());
        return InsertEndingBracket(input, openingBracketCount, closingBracketCount, startingBracket);
    }
    private static string InsertEndingBracket(string input, List<int> openingBrackets, List<int> closingBrackets,
        char startingBracket)
    {
        return InsertEndingBracketWorker(input, openingBrackets.Count, closingBrackets.Count, new List<char>(), startingBracket);
    }
    /// <summary>
    /// Insert ending bracket.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="startingBracket">The starting bracket.</param>
    /// <returns>The insert ending bracket result.</returns>
    public static string InsertEndingBracket(string input, List<char> openingBrackets, List<char> closingBrackets)
    {
        return InsertEndingBracketWorker(input, openingBrackets.Count, closingBrackets.Count, openingBrackets, char.MaxValue);
    }
    /// <summary>
    /// Insert ending bracket worker.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="startingBracket">The starting bracket.</param>
    /// <returns>The insert ending bracket worker result.</returns>
    public static string InsertEndingBracketWorker(string input, int openingCount, int closingCount,
        List<char> openingBrackets, char startingBracket)
    {
        var min = Math.Min(openingCount, closingCount);
        var max = Math.Max(openingCount, closingCount);
        if (openingCount < closingCount) return input;
        if (startingBracket != char.MaxValue)
        {
            var difference = max - min;
            openingBrackets.Clear();
            for (var i = 0; i < difference; i++) openingBrackets.Add(startingBracket);
        }
        input = InsertEndingBrackets(input, openingBrackets, min, max);
        return input;
    }
    private static string InsertEndingBrackets(string input, List<char> openingBrackets, int min, int max)
    {
        var lastIndex = max - 1;
        var isMultiline = input.Contains(Environment.NewLine);
        for (var i = min; i < lastIndex; i++)
        {
            if (isMultiline) input += Environment.NewLine;
            input += BracketsRight[GetBracketFromBegin(openingBrackets[i])];
        }
        return input;
    }
    /// <summary>
    /// Pairs brackets to complete block.
    /// </summary>
    /// <returns>The pairs brackets to complete block result.</returns>
    public static string PairsBracketsToCompleteBlock(string input)
    {
        var unmatchedCount = new List<char>();
        foreach (var character in input)
        {
            if (BracketsLeftList.Contains(character)) unmatchedCount.Add(character);
            if (BracketsRightList.Contains(character))
            {
                var bracketType = GetBracketFromBegin(character);
                var bracketIndex = unmatchedCount.IndexOf(BracketsLeft[bracketType]);
                if (bracketIndex != -1) unmatchedCount.RemoveAt(bracketIndex);
            }
        }
        var stringBuilder = new StringBuilder(input);
        if (unmatchedCount.Count > 0)
        {
            stringBuilder.AppendLine();
            for (var i = unmatchedCount.Count - 1; i >= 0; i--)
            {
                var bracketType = GetBracketFromBegin(unmatchedCount[i]);
                stringBuilder.Append(BracketsRight[bracketType]);
            }
            stringBuilder.Append(';');
        }
        var result = stringBuilder.ToString();
        if (input == result) result = result.TrimEnd(',');
        return result.Replace("\r\n", "\n");
    }
    private static Brackets GetBracketFromBegin(char bracket)
    {
        var isClosingBracket = false;
        return GetBracketFromBegin(bracket, ref isClosingBracket, false);
    }
    private static Brackets GetBracketFromBegin(char bracket, ref bool isClosingBracket, bool throwExIsNotBracket)
    {
        isClosingBracket = true;
        switch (bracket)
        {
            case '(':
                isClosingBracket = false;
                return Brackets.Normal;
            case '{':
                isClosingBracket = false;
                return Brackets.Curly;
            case '[':
                isClosingBracket = false;
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
    /// Include brackets.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="starting">The starting.</param>
    /// <returns>The include brackets result.</returns>
    public static List<char> IncludeBrackets(string text, bool starting)
    {
        var containsBracket = new List<char>();
        if (starting)
        {
            foreach (var character in text)
                if (BracketsLeftList.Contains(character))
                    containsBracket.Add(character);
        }
        else
        {
            foreach (var character in text)
                if (BracketsRightList.Contains(character))
                    containsBracket.Add(character);
        }
        return containsBracket;
    }
    /// <summary>
    /// Is valid ISO.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="starting">The starting.</param>
    /// <returns>The is valid ISO result.</returns>
    public static bool IsValidISO(string input)
    {
        // ISO-8859-1 is the same as latin1 https://en.wikipedia.org/wiki/ISO/IEC_8859-1
        var bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
        var result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
        return string.Equals(input, result);
    }
    private static bool IsInFirstXCharsTheseLetters(string text, int characterCount, params char[] letters)
    {
        for (var i = 0; i < characterCount; i++)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }
    private static string ShortForLettersCount(string text, int maxLetterCount, out bool shouldAddEllipsis)
    {
        shouldAddEllipsis = false;
        text = text.Trim();
        var textLength = text.Length;
        var isLongerOrEqual = maxLetterCount <= textLength;
        if (isLongerOrEqual)
        {
            if (IsInFirstXCharsTheseLetters(text, maxLetterCount, ' '))
            {
                var spaceIndex = 0;
                var processedText = text;
                var endIndex = processedText.Length;
                var processedCount = 0;
                for (var i = 0; i < endIndex; i++)
                {
                    processedCount++;
                    if (processedText[i] == ' ')
                    {
                        if (processedCount >= maxLetterCount) break;
                        spaceIndex = i;
                    }
                }
                processedText = processedText.Substring(0, spaceIndex + 1);
                if (processedText.Trim() != "") shouldAddEllipsis = true;
                return processedText;
            }
            shouldAddEllipsis = true;
            return text.Substring(0, maxLetterCount);
        }
        return text;
    }
    /// <summary>
    /// Initializes a new instance of the contains only case class.
    /// </summary>
    /// <param name="i">The i.</param>
    public static bool ContainsOnlyCase(string text, bool isUpper, bool isIgnoringOtherThanLetters = false)
    {
        var isLetter = false;
        foreach (var character in text)
        {
            isLetter = char.IsLetter(character);
            if (isLetter || (!isLetter && isIgnoringOtherThanLetters))
            {
                if (isUpper)
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
    /// Initializes a new instance of the short for letters count class.
    /// </summary>
    /// <param name="isIgnoringOtherThanLetters)">The is ignoring other than letters.</param>
    public static string ShortForLettersCount(string text, int maxLetterCount)
    {
        var shouldAddEllipsis = false;
        return ShortForLettersCount(text, maxLetterCount, out shouldAddEllipsis);
    }
    /// <summary>
    /// Telephone prefix to brackets.
    /// </summary>
    /// <param name="!char.IsLower(character)">The char is lower character.</param>
    /// <returns>The telephone prefix to brackets result.</returns>
    public static string TelephonePrefixToBrackets(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        input = NormalizeString(input);
        var parts = SHSplit.Split(input, " ");
        parts[0] = "(" + parts[0] + ")";
        return string.Join(" ", parts);
    }
    /// <summary>
    /// Contains variable.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxLetterCount">The max letter count.</param>
    /// <returns>The contains variable result.</returns>
    public static bool ContainsVariable(string text)
    {
        return ContainsVariable('{', '}', text);
    }
    /// <summary>
    /// Contains variable.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxLetterCount">The max letter count.</param>
    /// <returns>The contains variable result.</returns>
    public static bool ContainsVariable(char openingChar, char closingChar, string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        var unprocessedVariableContent = new StringBuilder();
        var processedContent = new StringBuilder();
        var inVariable = false;
        foreach (var character in text)
            if (character == openingChar)
            {
                inVariable = true;
            }
            else if (character == closingChar)
            {
                if (inVariable) inVariable = false;
                var consecutiveCount = 0;
                if (int.TryParse(unprocessedVariableContent.ToString(), out consecutiveCount))
                    return true;
                processedContent.Append(openingChar + unprocessedVariableContent.ToString() + closingChar);
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
    /// Get variables in string.
    /// </summary>
    /// <param name="inVariable">The in variable.</param>
    /// <returns>The get variables in string result.</returns>
    public static List<int> GetVariablesInString(string text)
    {
        return GetVariablesInString('{', '}', text);
    }
    /// <summary>
    /// Get variables in string.
    /// </summary>
    /// <param name="closingChar">The closing char.</param>
    /// <returns>The get variables in string result.</returns>
    public static List<int> GetVariablesInString(char openingChar, char closingChar, string text)
    {
        var variableIndices = new List<int>();
        var unprocessedVariableContent = new StringBuilder();
        var inVariable = false;
        foreach (var character in text)
            if (character == openingChar)
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
    /// Initializes a new instance of the remove duplicates class.
    /// </summary>
    /// <param name="text">The text.</param>
    public static List<string> RemoveDuplicates(string input, string delimiter)
    {
        var split = SHSplit.Split(input, delimiter);
        return split.Distinct().ToList();
    }
    /// <summary>
    /// Contains diacritic.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <returns>The contains diacritic result.</returns>
    public static bool ContainsDiacritic(string word)
    {
        return word != TextWithoutDiacritic(word);
    }

    /// <summary>
    /// Initializes a new instance of the convert plural to single en class.
    /// </summary>
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
    /// <summary>
    /// Short for letters count three dots.
    /// </summary>
    /// <returns>The short for letters count three dots result.</returns>
    public static string ShortForLettersCountThreeDots(string text, int maxLetterCount)
    {
        var shouldAddEllipsis = false;
        var result = ShortForLettersCount(text, maxLetterCount, out shouldAddEllipsis);
        if (shouldAddEllipsis) result += " ... ";
        result = result.Replace("\"", string.Empty);
        return result;
    }
    /// <summary>
    /// Contains other char than letter and digit.
    /// </summary>
    /// <param name="1]">The 1.</param>
    /// <returns>The contains other char than letter and digit result.</returns>
    public static bool ContainsOtherCharThanLetterAndDigit(string text)
    {
        foreach (var character in text)
            if (!char.IsLetterOrDigit(character))
                return true;
        return false;
    }
    /// <summary>
    /// Get odd indexes of word.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxLetterCount">The max letter count.</param>
    /// <returns>The get odd indexes of word result.</returns>
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
    /// <summary>
    /// Is chinese.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The is chinese result.</returns>
    public static bool IsChinese(string text)
    {
        var hiragana = GetCharsInRange(text, 0x3040, 0x309F);
        if (hiragana) return true;
        var katakana = GetCharsInRange(text, 0x30A0, 0x30FF);
        if (katakana) return true;
        var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF);
        if (kanji) return true;
        if (text.Any(character => character >= 0x20000 && character <= 0xFA2D)) return true;
        return false;
    }
    /// <summary>
    /// Get chars in range.
    /// </summary>
    /// <param name="half">The half.</param>
    /// <returns>The get chars in range result.</returns>
    public static bool GetCharsInRange(string text, int min, int max)
    {
        return text.Where(character => character >= min && character <= max).Count() != 0;
    }
    /// <summary>
    /// Initializes a new instance of the remove duplicates none class.
    /// </summary>
    /// <param name="i">The i.</param>
    public static List<string> RemoveDuplicatesNone(string input, string delimiter)
    {
        var split = SHSplit.SplitNone(input, delimiter);
        split = split.Distinct().ToList();
        return split;
    }
    /// <summary>
    /// Initializes a new instance of the remove brackets and his content class.
    /// </summary>
    /// <param name="hiragana">The hiragana.</param>
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
        input = input.Replace(" ", string.Empty).Trim();
        return input;
    }
    /// <summary>
    /// Remove between and edge chars.
    /// </summary>
    /// <returns>The remove between and edge chars result.</returns>
    public static string RemoveBetweenAndEdgeChars(string text, string begin, string end)
    {
        var regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
        return regex.Replace(text, string.Empty);
    }
    /// <summary>
    /// X chars before and after whole words.
    /// </summary>
    /// <param name="squareBrackets">The square brackets.</param>
    /// <returns>The x chars before and after whole words result.</returns>
    public static string XCharsBeforeAndAfterWholeWords(string textContent, int middleIndex, int charactersPerSide)
    {
        var rightSide = new StringBuilder();
        var currentWord = new StringBuilder();
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
        var leftText = currentWord + " " + leftSide.ToString().TrimEnd(' ');
        leftText = leftText.TrimEnd(' ');
        charactersPerSide += charactersPerSide - leftText.Length;
        currentWord.Clear();
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
        var rightText = rightSide.ToString().TrimStart(' ') + "" + currentWord;
        rightText = rightText.TrimStart(' ');
        var result = "";
        if (textContent.Contains(leftText + " ") && textContent.Contains(" " + rightText))
            result = leftText + "" + rightText;
        else
            result = leftText + rightText;
        return result;
    }
    /// <summary>
    /// Initializes a new instance of the short for letters count three dots reverse class.
    /// </summary>
    /// <param name="wordString">The word string.</param>
    public static string ShortForLettersCountThreeDotsReverse(string text, int maxLetterCount)
    {
        text = text.Trim();
        var textLength = text.Length;
        var isLongerOrEqual = maxLetterCount <= textLength;
        if (isLongerOrEqual)
        {
            if (IsInLastXCharsTheseLetters(text, maxLetterCount, ' '))
            {
                var spaceIndex = 0;
                var textToProcess = text;
                var lastIndex = textToProcess.Length;
                var characterCount = 0;
                for (var i = lastIndex - 1; i >= 0; i--)
                {
                    characterCount++;
                    if (textToProcess[i] == ' ')
                    {
                        if (characterCount >= maxLetterCount) break;
                        spaceIndex = i;
                    }
                }
                textToProcess = textToProcess.Substring(spaceIndex + 1);
                if (textToProcess.Trim() != "") textToProcess = " ... " + textToProcess;
                return textToProcess;
            }
            return " ... " + text.Substring(text.Length - maxLetterCount);
        }
        return text;
    }
    /// <summary>
    /// Return occurences of string from to word.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <returns>The return occurences of string from to word result.</returns>
    public static List<FromToWordString> ReturnOccurencesOfStringFromToWord(string textContent,
        params string[] searchedWords)
    {
        if (searchedWords == null || searchedWords.Length == 0) return new List<FromToWordString>();
        textContent = textContent.ToLower();
        var wordRanges = new List<FromToWordString>();
        var textLength = textContent.Length;
        for (var i = 0; i < textLength; i++)
            foreach (var searchWord in searchedWords)
            {
                var allCharactersMatch = true;
                var offset = 0;
                while (offset < searchWord.Length)
                {
                    var currentIndex = i + offset;
                    if (textLength > currentIndex)
                    {
                        if (textContent[currentIndex] != searchWord[offset])
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
                    var rangeResult = new FromToWordString();
                    rangeResult.From = i;
                    rangeResult.To = i + offset - 1;
                    rangeResult.Word = searchWord;
                    wordRanges.Add(rangeResult);
                    i += offset;
                    break;
                }
            }
        return wordRanges;
    }
    private static bool IsInLastXCharsTheseLetters(string text, int characterCount, params char[] letters)
    {
        for (var i = text.Length - 1; i >= characterCount; i--)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }
    /// <summary>
    /// Get first part by location.
    /// </summary>
    /// <param name="rangeResult">The range result.</param>
    /// <returns>The get first part by location result.</returns>
    public static string GetFirstPartByLocation(string input, char delimiter)
    {
        var delimiterIndex = input.IndexOf(delimiter);
        return GetFirstPartByLocation(input, delimiterIndex);
    }
    /// <summary>
    /// Get first part by location.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="characterCount">The character count.</param>
    /// <param name="letters">The letters.</param>
    /// <returns>The get first part by location result.</returns>
    public static string GetFirstPartByLocation(string input, int delimiterIndex)
    {
        string firstPart, remainder;
        firstPart = input;
        if (delimiterIndex < input.Length) GetPartsByLocation(out firstPart, out remainder, input, delimiterIndex);
        return firstPart;
    }
    /// <summary>
    /// Initializes a new instance of the ends with array class.
    /// </summary>
    /// <param name="i">The i.</param>
    public static bool EndsWithArray(string source, params string[] suffixes)
    {
        foreach (var suffix in suffixes)
            if (source.EndsWith(suffix))
                return true;
        return false;
    }
    /// <summary>
    /// Get text between simple.
    /// </summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The get text between simple result.</returns>
    public static string GetTextBetweenSimple(string text, string after, string before,
        bool throwExceptionIfNotContains = true)
    {
        var foundIndex = int.MinValue;
        var result = GetTextBetween(text, after, before, out foundIndex, 0, throwExceptionIfNotContains);
        return result;
    }
    /// <summary>
    /// Get text between.
    /// </summary>
    /// <param name="input.Length">The input length.</param>
    /// <returns>The get text between result.</returns>
    public static string GetTextBetween(string text, string after, string before, out int foundIndex,
        int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string? result = null;
        foundIndex = text.IndexOf(after, startSearchingAt);
        var closingIndex = text.IndexOf(before, foundIndex + after.Length);
        var isAfterFound = foundIndex != -1;
        var isBeforeFound = closingIndex != -1;
        if (isAfterFound && isBeforeFound)
        {
            foundIndex += after.Length;
            closingIndex -= 1;
            var length = closingIndex - foundIndex + 1;
            result = text.Substring(foundIndex, length);
        }
        else
        {
            if (throwExceptionIfNotContains)
                ThrowEx.NotContains(text, after, before);
            else
                return null!;
        }
        return result!;
    }
    /// <summary>
    /// Ends with.
    /// </summary>
    /// <param name="isBeforeFound">The is before found.</param>
    /// <returns>The ends with result.</returns>
    public static bool EndsWith(string input, string endsWith)
    {
        return input.EndsWith(endsWith);
    }
    /// <summary>
    /// Remove prefix.
    /// </summary>
    /// <param name="foundIndex">The found index.</param>
    /// <param name="length">The length.</param>
    /// <returns>The remove prefix result.</returns>
    public static bool RemovePrefix(ref string text, string prefix)
    {
        if (text.StartsWith(prefix))
        {
            text = text.Substring(prefix.Length);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Get to first char.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="endsWith">The ends with.</param>
    /// <returns>The get to first char result.</returns>
    public static string GetToFirstChar(string input, int indexOfChar)
    {
        if (indexOfChar != -1) return input.Substring(0, indexOfChar + 1);
        return input;
    }
    /// <summary>
    /// Null to string or empty.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="endsWith">The ends with.</param>
    /// <returns>The null to string or empty result.</returns>
    public static string NullToStringOrEmpty(object value)
    {
        if (value == null) return "";
        var result = value.ToString()!;
        return result;
    }
    /// <summary>
    /// Initializes a new instance of the contains from end class.
    /// </summary>
    /// <param name="text.StartsWith(prefix)">The text starts with prefix.</param>
    public static bool ContainsFromEnd(string input, char character, out int foundIndex)
    {
        for (var i = input.Length - 1; i >= 0; i--)
            if (input[i] == character)
            {
                foundIndex = i;
                return true;
            }
        foundIndex = -1;
        return false;
    }
    /// <summary>
    /// First which is not empty.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The first which is not empty result.</returns>
    public static string FirstWhichIsNotEmpty(params string[] values)
    {
        foreach (var value in values)
            if (value != "")
                return value;
        return "";
    }
    /// <summary>
    /// Match wildcard.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="character">The character.</param>
    /// <param name="foundIndex">The found index.</param>
    /// <returns>The match wildcard result.</returns>
    public static bool MatchWildcard(string text, string wildcardPattern)
    {
        return IsMatchRegex(text, wildcardPattern, '?', '*');
    }
    private static bool IsMatchRegex(string text, string wildcardPattern, char singleWildcard, char multipleWildcard)
    {
        if (text == wildcardPattern) return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        wildcardPattern = Regex.Escape(wildcardPattern);
        wildcardPattern = wildcardPattern.Replace(escapedSingle, ".");
        wildcardPattern = "^" + wildcardPattern.Replace(escapedMultiple, ".*") + "$";
        var pattern = new Regex(wildcardPattern);
        return pattern.IsMatch(text);
    }
    /// <summary>
    /// First char of every word upper dash.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="wildcardPattern">The wildcard pattern.</param>
    /// <returns>The first char of every word upper dash result.</returns>
    public static string FirstCharOfEveryWordUpperDash(string input)
    {
        return FirstCharOfEveryWordUpper(input, '-');
    }
    private static string FirstCharOfEveryWordUpper(string input, char delimiter)
    {
        var words = SHSplit.SplitChar(input, delimiter);
        for (var i = 0; i < words.Count; i++) words[i] = FirstCharUpper(words[i]);
        return string.Join(" ", words);
    }
    /// <summary>
    /// Is null or white space.
    /// </summary>
    /// <param name="1)">The 1.</param>
    /// <returns>The is null or white space result.</returns>
    public static bool IsNullOrWhiteSpace(string text)
    {
        if (text != null)
        {
            text = text.Trim();
            return text == "";
        }
        return true;
    }
    /// <summary>
    /// Append if dont ending with.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="'-'">The '-'.</param>
    /// <returns>The append if dont ending with result.</returns>
    public static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append)) return text;
        return text + append;
    }
    /// <summary>
    /// Substring.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The substring result.</returns>
    public static string Substring(string input, int startIndex, int length, bool untilEndIfShorter)
    {
        if (input == null) return input;
        if (startIndex >= input.Length) return string.Empty;
        if (untilEndIfShorter && startIndex + length > input.Length) length = input.Length - startIndex;
        return input.Substring(startIndex, length);
    }
}
