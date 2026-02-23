namespace SunamoString._public;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class StringOrStringList
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public StringOrStringList(string stringValue)
    {
        String = stringValue;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public StringOrStringList(List<string> list)
    {
        List = list;
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public string String { get; private set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<string> List { get; private set; } = null!;
    /// <summary>
    /// Performs an operation.
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
    /// Performs an operation.
    /// </summary>
    public List<string> GetList()
    {
        if (String != null)
        {
            if (List == null)
            {
                var nonLetterNumberChars = String.Where(ch => !char.IsLetterOrDigit(ch)).ToArray();
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
