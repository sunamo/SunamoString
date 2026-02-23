namespace SunamoString._public.SunamoData.Data;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class ABCString : List<ABString>
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public static ABCString Empty = new ABCString();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public ABCString()
    {
    }
    /// <summary>
    /// Performs an operation.
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
    /// Performs an operation.
    /// </summary>
    public int Length
    {
        get
        {
            return Count;
        }
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null!);
        }
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
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
                var currentItem = nameValuePairs[i];
                actualType = currentItem.GetType();
                if (actualType == ABString.Type)
                {
                    this.Add((ABString)currentItem);
                }
                else
                {
                    var itemList = (IList)currentItem;
                    foreach (var abStringElement in itemList)
                    {
                        var abStringItem = (ABString)abStringElement;
                        Add(abStringItem);
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
    /// Performs an operation.
    /// </summary>
    public ABCString(params ABString[] abStringItems)
    {

        this.AddRange(abStringItems);
    }
    
    
    
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }
    /// <summary>
    /// Performs an operation.
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
    /// Performs an operation.
    /// </summary>
    public List<string> OnlyAs()
    {
        List<string> keys = new List<string>(this.Count);
        //CA.InitFillWith(keys, this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            keys[i] = this[i].A;
        }
        return keys;
    }
    /// <summary>
    /// Only Bs operation on the input.
    /// </summary>
    public static List<object> OnlyBs(List<ABString> abStringList)
    {
        return abStringList.Select(abString => abString.B).ToList();
    }
}
