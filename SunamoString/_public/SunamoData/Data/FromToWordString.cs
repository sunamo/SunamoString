namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents a word with its position (from-to indices) in a text.
/// </summary>
public class FromToWordString
{
    /// <summary>
    /// The starting index of the word.
    /// </summary>
    public int From { get; set; } = 0;

    /// <summary>
    /// The ending index of the word.
    /// </summary>
    public int To { get; set; } = 0;

    /// <summary>
    /// The word text.
    /// </summary>
    public string Word { get; set; } = "";
}
