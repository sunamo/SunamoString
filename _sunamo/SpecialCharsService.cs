namespace SunamoString._sunamo;
using System.Collections.Generic;

internal class SpecialCharsService
{
    internal readonly List<char> specialChars = new(new[]
        { excl, commat, num, dollar, percnt, Hat, amp, ast, quest, lowbar, tilda });

    internal readonly List<char> specialChars2 = new(new[]
    {
        lq, rq, dash, la, ra,
        comma, period, colon, apos, rpar, sol, lt, gt, lcub, rcub, lsqb, verbar, semi, plus, rsqb,
        ndash, slash
    });

    /// <summary>
    ///     Used in enigma
    /// </summary>
    internal readonly List<char> specialCharsAll;

    internal readonly List<char> specialCharsWhite = new(new[] { space });
    internal readonly List<char> specialCharsNotEnigma = new(new[] { space160, copy });

    private const char la = '‘';
    private const char ra = '’';
    private const char comma = ',';
    private const char space = ' ';
    private static char space160 = (char)160;
    private const char dollar = '$';
    private const char Hat = '^';
    private const char ast = '*';
    private const char quest = '?';
    private const char tilda = '~';
    private const char period = '.';
    private const char colon = ':';
    private const char excl = '!';
    private const char apos = '\'';
    private const char rpar = ')';
    private const char lpar = '(';
    private const char sol = '/';
    private const char lowbar = '_';
    private const char lt = '<';

    /// <summary>
    ///     skip in specialChars2 - already as equal
    /// </summary>
    private const char equals = '=';

    private const char gt = '>';
    private const char amp = '&';
    private const char lcub = '{';
    private const char rcub = '}';
    private const char lsqb = '[';
    private const char verbar = '|';
    private const char semi = ';';
    private const char commat = '@';
    private const char plus = '+';
    private const char rsqb = ']';
    private const char num = '#';
    private const char percnt = '%';
    private const char ndash = '–';
    private const char copy = '©';


    #region MyRegion

    private const char lq = '“';
    private const char rq = '”';

    #region Generic chars

    private const char zero = '0';

    #endregion

    #region Names here must be the same as in Consts

    private const char modulo = '%';
    private const char dash = '-';

    #endregion

    private const char tab = '\t';
    private const char nl = '\n';
    private const char cr = '\r';
    private const char asterisk = '*';
    private const char apostrophe = '\'';
    private const char sc = ';';

    /// <summary>
    ///     quotation marks
    /// </summary>
    private const char qm = '"';

    /// <summary>
    ///     Question
    /// </summary>
    private const char q = '?';

    /// <summary>
    ///     Left bracket
    /// </summary>
    private const char lb = '(';

    private const char rb = ')';
    private const char slash = '/';

    /// <summary>
    ///     backspace
    /// </summary>
    private const char bs2 = '\b';

    #endregion
}