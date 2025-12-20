// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._sunamo;

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
    ///     Used in enigma
    /// </summary>
    internal readonly List<char> SpecialCharsAll;
    internal readonly List<char> SpecialCharsWhite = new(new[] { space });
    internal readonly List<char> SpecialCharsNotEnigma = new(new[] { nonBreakingSpace, copyright });
    private const char leftApostrophe = '\u2018';
    private const char rightApostrophe = '\u2019';
    private const char comma = ',';
    private const char space = ' ';
    private static char nonBreakingSpace = (char)160;
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
    private const char leftParenthesis = '(';
    private const char solidus = '/';
    private const char underscore = '_';
    private const char lessThan = '<';
    /// <summary>
    ///     skip in SpecialChars2 - already as equal
    /// </summary>
    private const char equals = '=';
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
    #region MyRegion
    private const char leftQuote = '"';
    private const char rightQuote = '"';
    #region Generic chars
    private const char zero = '0';
    #endregion
    #region Names here must be the same as in Consts
    private const char modulo = '%';
    private const char dash = '-';
    #endregion
    private const char tabChar = '\t';
    private const char newlineChar = '\n';
    private const char carriageReturnChar = '\r';
    private const char asterisk = '*';
    private const char apostropheChar = '\'';
    private const char semicolonChar = ';';
    /// <summary>
    ///     quotation marks
    /// </summary>
    private const char quotationMark = '"';
    /// <summary>
    ///     Question mark
    /// </summary>
    private const char questionChar = '?';
    /// <summary>
    ///     Left bracket
    /// </summary>
    private const char leftBracket = '(';
    private const char rightBracket = ')';
    private const char slash = '/';
    /// <summary>
    ///     backspace
    /// </summary>
    private const char backspace = '\b';
    #endregion
}