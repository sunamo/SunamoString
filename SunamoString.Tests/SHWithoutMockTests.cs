namespace sunamo.Tests.Helpers.Text;
// variables names: ok

/// <summary>
/// Tests for string helper methods that do not require mocking.
/// </summary>
public class SHWithoutMockTests
{
    /// <summary>
    /// Tests that Contains correctly finds a term in the input text using FixedSpace strategy.
    /// </summary>
    [Fact]
    public void ContainsFixedSpaceTest()
    {
        string input = "Hello World";
        string term = "World";

        bool result = SH.Contains(input, term, SearchStrategy.FixedSpace);

        Assert.True(result);
    }

    /// <summary>
    /// Tests that Contains returns false when the term is not in the input text.
    /// </summary>
    [Fact]
    public void ContainsFixedSpaceNotFoundTest()
    {
        string input = "Hello World";
        string term = "Earth";

        bool result = SH.Contains(input, term, SearchStrategy.FixedSpace);

        Assert.False(result);
    }

    /// <summary>
    /// Tests that FirstCharUpper correctly uppercases the first character of the text.
    /// </summary>
    [Fact]
    public void FirstCharUpperTest()
    {
        string input = "hello world";

        string result = SH.FirstCharUpper(input);

        Assert.Equal("Hello world", result);
    }

    /// <summary>
    /// Tests that ContainsAll returns true when the input contains all specified words.
    /// </summary>
    [Fact]
    public void ContainsAllTest()
    {
        string input = "The quick brown fox";
        var words = new List<string> { "quick", "fox" };

        bool result = SH.ContainsAll(input, words);

        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsNullOrWhiteSpace returns true for null, empty, and whitespace strings.
    /// </summary>
    [Fact]
    public void IsNullOrWhiteSpaceTest()
    {
        Assert.True(SH.IsNullOrWhiteSpace(null!));
        Assert.True(SH.IsNullOrWhiteSpace(""));
        Assert.True(SH.IsNullOrWhiteSpace("   "));
        Assert.False(SH.IsNullOrWhiteSpace("hello"));
    }
}
