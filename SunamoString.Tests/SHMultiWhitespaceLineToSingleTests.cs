namespace sunamo.Tests.Helpers.Text;

/// <summary>
/// Tests for SH.MultiWhitespaceLineToSingle.
/// </summary>
public class SHMultiWhitespaceLineToSingleTests
{
    /// <summary>
    /// Tests that a run of blank lines collapses into its first line.
    /// </summary>
    [Fact]
    public void CollapsesRunsOfBlankLines()
    {
        var result = SH.MultiWhitespaceLineToSingle(["a", "", "  ", "b", "", "c"]);

        Assert.Equal(string.Join(Environment.NewLine, "a", "", "b", "", "c"), result);
    }
}