namespace SunamoString._sunamo;

/// <summary>
/// Provides categorized lists of special characters for text processing.
/// </summary>
internal class SpecialCharsService
{
    internal readonly List<char> SpecialChars = new(new[]
        { exclamation, atSign, numberSign, dollar, percent, caret, ampersand, asteriskChar, questionMark, underscore, tilde });
    internal readonly List<char> SpecialChars2 = new(new[]
    {
        leftQuote, rightQuote, dash, leftApostrophe, rightApostrophe,
        comma, period, colon, apostrophe, rightParenthesis, solidus, lessThan, greaterThan, leftCurlyBracket, rightCurlyBracket, leftSquareBracket, verticalBar, semicolon, plus, rightSquareBracket,
        enDash, slash
    });
    /// <summary>
    /// Combined list of all special characters. Used in enigma.
    /// </summary>
    internal readonly List<char> SpecialCharsAll = null!;
    internal readonly List<char> SpecialCharsWhite = new(new[] { space });
    internal readonly List<char> SpecialCharsNotEnigma = new(new[] { nonBreakingSpace, copyright });
    private const char leftApostrophe = '\u2018';
    private const char rightApostrophe = '\u2019';
    private const char comma = ',';
    private const char space = ' ';
    private const char nonBreakingSpace = (char)160;
    private const char dollar = '$';
    private const char caret = '^';
    private const char asteriskChar = '*';
    private const char questionMark = '?';
    private const char tilde = '~';
    private const char period = '.';
    private const char colon = ':';
    private const char exclamation = '!';
    private const char apostrophe = '\'';
    private const char rightParenthesis = ')';
    private const char solidus = '/';
    private const char underscore = '_';
    private const char lessThan = '<';
    private const char greaterThan = '>';
    private const char ampersand = '&';
    private const char leftCurlyBracket = '{';
    private const char rightCurlyBracket = '}';
    private const char leftSquareBracket = '[';
    private const char verticalBar = '|';
    private const char semicolon = ';';
    private const char atSign = '@';
    private const char plus = '+';
    private const char rightSquareBracket = ']';
    private const char numberSign = '#';
    private const char percent = '%';
    private const char enDash = '–';
    private const char copyright = '©';
    private const char leftQuote = '\u201C';
    private const char rightQuote = '\u201D';
    private const char dash = '-';
    private const char slash = '/';
}
