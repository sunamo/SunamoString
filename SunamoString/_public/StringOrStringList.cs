namespace SunamoString._public;

/// <summary>
/// Represents the string or string list type.
/// </summary>
public class StringOrStringList
{
    /// <summary>
    /// Initializes a new instance of the string or string list class.
    /// </summary>
    /// <param name="text">The text.</param>
    public StringOrStringList(string text)
    {
        String = text;
    }

    /// <summary>
    /// Initializes a new instance of the string or string list class.
    /// </summary>
    /// <param name="list">The list.</param>
    public StringOrStringList(List<string> list)
    {
        List = list;
    }

    /// <summary>
    /// Gets or sets the string.
    /// </summary>
    public string String { get; private set; } = null!;

    /// <summary>
    /// Gets or sets the list.
    /// </summary>
    public List<string> List { get; private set; } = null!;

    /// <summary>
    /// Get string.
    /// </summary>
    /// <returns>The get string result.</returns>
    public string GetString()
    {
        if (String != null)
        {
            return String;
        }
        if (List != null)
        {
            if (String == null)
            {
                String = string.Join(" ", List);
            }
            return String;
        }
        throw new Exception("Both is null");
    }

    /// <summary>
    /// Get list.
    /// </summary>
    /// <param name="null"">The null.</param>
    /// <returns>The get list result.</returns>
    public List<string> GetList()
    {
        if (String != null)
        {
            if (List == null)
            {
                var nonLetterNumberChars = String.Where(character => !char.IsLetterOrDigit(character)).ToArray();
                List = SHSplit.SplitChar(String, nonLetterNumberChars);
            }
            return List;
        }
        if (List != null)
        {
            return List;
        }
        throw new Exception("Both is null");
    }
}
