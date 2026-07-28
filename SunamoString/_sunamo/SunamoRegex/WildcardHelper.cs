namespace SunamoString._sunamo.SunamoRegex;

internal class WildcardHelper
{
    internal static bool IsWildcard(string text)
    {
        return text.ToCharArray().Any(character => character == '?') || text.ToCharArray().Any(character => character == '*');
    }
}
