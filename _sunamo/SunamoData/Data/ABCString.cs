namespace SunamoString;


public class ABCString : List<ABString>//, IList<AB>
{
    internal static ABCString Empty = new ABCString();
    internal ABCString()
    {
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in this)
        {
            sb.Append(item.ToString() + ",");
        }
        return sb.ToString();
    }
    internal int Length
    {
        get
        {
            return Count;
        }
    }
    internal ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null);
        }
    }
    internal ABCString(params Object[] setsNameValue)
    {
        if (setsNameValue.Length == 0)
        {
            return;
        }
        var o = setsNameValue[0];
        var t = o.GetType();
        Type t2 = t;
        if (o is IList)
        {
            var s = o as IList;
            var o2 = s.Count != 0 ? s[0] : null;
            t2 = o2.GetType();
        }
        //var t2 = setsNameValue[0][0].GetType();
        if (t2 == typeof(ABString))
        {
            //var abc = null;
            //if (true)
            //{
            //}
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                var snv = setsNameValue[i];
                t2 = snv.GetType();
                if (t2 == ABString.type)
                {
                    this.Add((ABString)snv);
                }
                else
                {
                    var ie = (IList)snv;
                    foreach (var item in ie)
                    {
                        var ab = (ABString)item;
                        Add(ab);
                    }
                }
            }
        }
        else if (t == typeof(ABCString))
        {
            var abc = (ABCString)o;
            this.AddRange(abc);
        }
        else
        {
            // Dont use like idiot TwoDimensionParamsIntoOne where is not needed - just iterate. Must more use radio and less blindness
            //var setsNameValue = CA.TwoDimensionParamsIntoOne(setsNameValue2);
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                this.Add(ABString.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
            }
        }
    }
    internal ABCString(params ABString[] abc)
    {
        // TODO: Complete member initialization
        this.AddRange(abc);
    }
    /// <summary>
    /// Must be [] due to SQL viz  https://stackoverflow.com/questions/9149919/no-mapping-exists-from-object-type-system-collections-generic-list-when-executin
    /// </summary>
    internal Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }
    internal List<object> OnlyBsList()
    {
        List<object> o = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o.Add(this[i].B);
        }
        return o;
    }
    internal List<string> OnlyAs()
    {
        List<string> o = new List<string>(this.Count);
        CASunamoExceptions.InitFillWith(o, this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o[i] = this[i].A;
        }
        return o;
    }
    internal static List<object> OnlyBs(List<ABString> arr)
    {
        return arr.Select(d => d.B).ToList();
    }
}