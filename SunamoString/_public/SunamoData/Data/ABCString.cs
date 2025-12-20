// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoString._public.SunamoData.Data;

public class ABCString : List<ABString>
{
    public static ABCString Empty = new ABCString();
    public ABCString()
    {
    }
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var abString in this)
        {
            stringBuilder.Append(abString.ToString() + ",");
        }
        return stringBuilder.ToString();
    }
    public int Length
    {
        get
        {
            return Count;
        }
    }
    public ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null);
        }
    }
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
            var firstListElement = listItems.Count != 0 ? listItems[0] : null;
            actualType = firstListElement.GetType();
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
                this.Add(ABString.Get(nameValuePairs[i].ToString(), nameValuePairs[++i]));
            }
        }
    }
    public ABCString(params ABString[] abStringItems)
    {

        this.AddRange(abStringItems);
    }
    
    
    
    public Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }
    public List<object> OnlyBsList()
    {
        List<object> values = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            values.Add(this[i].B);
        }
        return values;
    }
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
    public static List<object> OnlyBs(List<ABString> abStringList)
    {
        return abStringList.Select(abString => abString.B).ToList();
    }
}
