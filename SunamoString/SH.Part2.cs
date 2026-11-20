namespace SunamoString;

public partial class SH
{

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
}