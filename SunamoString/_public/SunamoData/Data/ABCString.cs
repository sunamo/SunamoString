namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Represents a collection of <see cref="ABString"/> key-value pairs.
/// </summary>
public class ABCString : List<ABString>
{
    /// <summary>
    /// An empty <see cref="ABCString"/> collection.
    /// </summary>
    public static ABCString Empty = new ABCString();

    /// <summary>
    /// Initializes a new empty <see cref="ABCString"/> collection.
    /// </summary>
    public ABCString()
    {
    }

    /// <summary>
    /// Returns a comma-separated string representation of all key-value pairs.
    /// </summary>
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var abString in this)
        {
            stringBuilder.Append(abString.ToString() + ",");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the number of elements in the collection (alias for Count).
    /// </summary>
    public int Length
    {
        get
        {
            return Count;
        }
    }

    /// <summary>
    /// Initializes a new <see cref="ABCString"/> with a specified capacity, filled with null entries.
    /// </summary>
    /// <param name="capacity">The number of null entries to pre-fill.</param>
    public ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null!);
        }
    }

    /// <summary>
    /// Initializes a new <see cref="ABCString"/> from name-value pairs or existing ABString instances.
    /// </summary>
    /// <param name="nameValuePairs">Array of objects interpreted as name-value pairs or ABString instances.</param>
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
    /// Initializes a new <see cref="ABCString"/> from an array of <see cref="ABString"/> items.
    /// </summary>
    /// <param name="abStringItems">The ABString items to add to the collection.</param>
    public ABCString(params ABString[] abStringItems)
    {
        this.AddRange(abStringItems);
    }

    /// <summary>
    /// Returns an array containing only the B (value) components.
    /// </summary>
    public Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }

    /// <summary>
    /// Returns a list containing only the B (value) components.
    /// </summary>
    public List<object> OnlyBsList()
    {
        List<object> values = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            values.Add(this[i].B);
        }
        return values;
    }

    /// <summary>
    /// Returns a list containing only the A (key) components.
    /// </summary>
    public List<string> OnlyAs()
    {
        List<string> keys = new List<string>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            keys[i] = this[i].A;
        }
        return keys;
    }

    /// <summary>
    /// Returns a list containing only the B (value) components from the specified list.
    /// </summary>
    /// <param name="list">The list of ABString items to extract values from.</param>
    public static List<object> OnlyBs(List<ABString> list)
    {
        return list.Select(abString => abString.B).ToList();
    }
}
