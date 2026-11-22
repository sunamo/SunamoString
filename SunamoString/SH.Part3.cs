namespace SunamoString;

public partial class SH
{

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
    /// Returns the text between two delimiters without throwing: the whole text when the opening delimiter is missing, the rest after the opening delimiter when the closing one is missing, an empty string for null.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="after">The opening delimiter.</param>
    /// <param name="before">The closing delimiter.</param>
    /// <returns>The text between the delimiters.</returns>
    public static string GetTextBetweenOrRest(string text, string after, string before)
    {
        if (text == null) return string.Empty;
        var startIndex = text.IndexOf(after, StringComparison.Ordinal);
        if (startIndex == -1) return text;
        startIndex += after.Length;
        var endIndex = text.IndexOf(before, startIndex, StringComparison.Ordinal);
        return endIndex == -1 ? text.Substring(startIndex) : text.Substring(startIndex, endIndex - startIndex);
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