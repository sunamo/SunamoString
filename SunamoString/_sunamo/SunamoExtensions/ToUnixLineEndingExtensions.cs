namespace SunamoString._sunamo.SunamoExtensions;

internal static class ToUnixLineEndingExtensions
{
}
internal static class StringExtensions
{
    internal static string ToUnixLineEnding(this string input)
    {
        return input.ReplaceLineEndings("\n");
    }
}