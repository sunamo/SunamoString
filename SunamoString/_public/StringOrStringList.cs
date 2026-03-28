namespace SunamoString._public;

/// <summary>
/// Wraps either a single string or a list of strings, with lazy conversion between the two.
/// </summary>
public class StringOrStringList
{
    /// <summary>
    /// Initializes a new instance wrapping a single string.
    /// </summary>
    /// <param name="text">The string value.</param>
    public StringOrStringList(string text)
    {
        String = text;
    }

    /// <summary>
    /// Initializes a new instance wrapping a list of strings.
    /// </summary>
    /// <param name="list">The list of strings.</param>
    public StringOrStringList(List<string> list)
    {
        List = list;
    }

    /// <summary>
    /// The stored string value, or null if initialized with a list.
    /// </summary>
    public string String { get; private set; } = null!;

    /// <summary>
    /// The stored list of strings, or null if initialized with a string.
    /// </summary>
    public List<string> List { get; private set; } = null!;

    /// <summary>
    /// Gets the value as a single string. If initialized with a list, joins the list elements with spaces.
    /// </summary>
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
    /// Gets the value as a list of strings. If initialized with a string, splits it by non-letter-or-digit characters.
    /// </summary>
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
