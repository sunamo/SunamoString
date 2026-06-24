namespace SunamoString._public;

public class StringOrStringList
{
    public StringOrStringList(string text)
    {
        String = text;
    }

    public StringOrStringList(List<string> list)
    {
        List = list;
    }

    public string String { get; private set; } = null!;

    public List<string> List { get; private set; } = null!;

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
