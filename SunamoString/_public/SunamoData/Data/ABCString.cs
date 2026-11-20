namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents the ABC string type.
/// </summary>
public class ABCString : List<ABString>
{
    /// <summary>
    /// The empty.
    /// </summary>
    public static ABCString Empty = new ABCString();

    /// <summary>
    /// ABC string.
    /// </summary>
    /// <returns>The ABC string result.</returns>
    public ABCString()
    {
    }

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns>The to string result.</returns>
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var abString in this)
        {
            stringBuilder.Append(abString.ToString() + ",");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    public int Length => Count;

    /// <summary>
    /// ABC string.
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    /// <returns>The ABC string result.</returns>
    public ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null!);
        }
    }

    /// <summary>
    /// Initializes a new instance of the ABC string class.
    /// </summary>
    /// <param name="i">The i.</param>
    public ABCString(params Object[] nameValuePairs)
    {
        if (nameValuePairs.Length == 0)
        {
            return;
        }
        var firstElement = nameValuePairs[0];
        var elementType = firstElement.GetType();
        Type actualType = elementType;
        if (firstElement is IList)
        {
            var listItems = firstElement as IList;
            var firstListElement = listItems!.Count != 0 ? listItems[0] : null;
            actualType = firstListElement!.GetType();
        }

        if (actualType == typeof(ABString))
        {
            for (int i = 0; i < nameValuePairs.Length; i++)
            {
                var currentElement = nameValuePairs[i];
                actualType = currentElement.GetType();
                if (actualType == ABString.Type)
                {
                    this.Add((ABString)currentElement);
                }
                else
                {
                    var elementList = (IList)currentElement;
                    foreach (var abStringElement in elementList)
                    {
                        var abString = (ABString)abStringElement;
                        Add(abString);
                    }
                }
            }
        }
        else if (elementType == typeof(ABCString))
        {
            var abcStringCollection = (ABCString)firstElement;
            this.AddRange(abcStringCollection);
        }
        else
        {
            for (int i = 0; i < nameValuePairs.Length; i++)
            {
                this.Add(ABString.Get(nameValuePairs[i].ToString()!, nameValuePairs[++i]));
            }
        }
    }

    /// <summary>
    /// ABC string.
    /// </summary>
    /// <param name="nameValuePairs[++i])">The name value pairs i.</param>
    /// <returns>The ABC string result.</returns>
    public ABCString(params ABString[] abStringItems)
    {
        this.AddRange(abStringItems);
    }

    /// <summary>
    /// Initializes a new instance of the only bs class.
    /// </summary>
    /// <param name="abStringItems">The ab string items.</param>
    public Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }

    /// <summary>
    /// Only bs list.
    /// </summary>
    /// <returns>The only bs list result.</returns>
    public List<object> OnlyBsList()
    {
        var values = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            values.Add(this[i].B);
        }
        return values;
    }

    /// <summary>
    /// Only as.
    /// </summary>
    /// <param name="this[i].B">The this i b.</param>
    /// <returns>The only as result.</returns>
    public List<string> OnlyAs()
    {
        var keys = new List<string>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            keys[i] = this[i].A;
        }
        return keys;
    }

    /// <summary>
    /// Only bs.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <returns>The only bs result.</returns>
    public static List<object> OnlyBs(List<ABString> list)
    {
        return list.Select(abString => abString.B).ToList();
    }
}
