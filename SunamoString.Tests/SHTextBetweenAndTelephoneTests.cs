namespace sunamo.Tests.Helpers.Text;

/// <summary>
/// Tests for SH.GetTextBetweenOrRest and SH.TelephonePrefixToBrackets.
/// </summary>
public class SHTextBetweenAndTelephoneTests
{
    /// <summary>
    /// Tests that GetTextBetweenOrRest returns the text between both delimiters.
    /// </summary>
    [Fact]
    public void GetTextBetweenOrRest_BothDelimiters()
    {
        Assert.Equal("a@b.cz", SH.GetTextBetweenOrRest("mailto:a@b.cz?subject=x", "mailto:", "?"));
    }

    /// <summary>
    /// Tests that GetTextBetweenOrRest returns the rest of the text when the closing delimiter is missing.
    /// </summary>
    [Fact]
    public void GetTextBetweenOrRest_MissingClosingDelimiter()
    {
        Assert.Equal("a@b.cz", SH.GetTextBetweenOrRest("mailto:a@b.cz", "mailto:", "?"));
    }

    /// <summary>
    /// Tests that GetTextBetweenOrRest returns the whole text when the opening delimiter is missing and an empty string for null.
    /// </summary>
    [Fact]
    public void GetTextBetweenOrRest_MissingOpeningDelimiterOrNull()
    {
        Assert.Equal("a@b.cz?x", SH.GetTextBetweenOrRest("a@b.cz?x", "mailto:", "?"));
        Assert.Equal(string.Empty, SH.GetTextBetweenOrRest(null!, "mailto:", "?"));
    }

    /// <summary>
    /// Tests that TelephonePrefixToBrackets wraps only an international prefix starting with a plus sign.
    /// </summary>
    [Fact]
    public void TelephonePrefixToBrackets_WithPlusPrefix()
    {
        Assert.Equal("(+420) 777 123 456", SH.TelephonePrefixToBrackets(" +420 777 123 456 "));
        Assert.Equal("(+420) 777 123 456", SH.TelephonePrefixToBrackets("+420 777 123 456"));
    }

    /// <summary>
    /// Tests that TelephonePrefixToBrackets leaves a number without a plus prefix or without a space unchanged.
    /// </summary>
    [Fact]
    public void TelephonePrefixToBrackets_WithoutPrefix()
    {
        Assert.Equal("777 123 456", SH.TelephonePrefixToBrackets("777 123 456"));
        Assert.Equal("+420777123456", SH.TelephonePrefixToBrackets("+420777123456"));
        Assert.Equal(string.Empty, SH.TelephonePrefixToBrackets(null!));
    }
}
