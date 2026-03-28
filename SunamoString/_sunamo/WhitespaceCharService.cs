namespace SunamoString._sunamo;

/// <summary>
/// Provides lists of whitespace characters and their Unicode code points.
/// </summary>
internal class WhitespaceCharService
{
    internal List<char> WhitespaceChars = null!;
    internal readonly List<int> WhitespaceCodes = new(new[]
    {
        9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
        8232, 8233, 8239, 8287, 12288
    });
}
