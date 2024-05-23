namespace SunamoString;


public class ABC : List<AB>//, IList<AB>
{
    public static ABC Empty = new ABC();
    public ABC()
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
    public int Length
    {
        get
        {
            return Count;
        }
    }
    public ABC(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null);
        }
    }
    public ABC(params Object[] setsNameValue)
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
        if (t2 == typeof(AB))
        {
            //var abc = null;
            //if (true)
            //{
            //}
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                var snv = setsNameValue[i];
                t2 = snv.GetType();
                if (t2 == AB.type)
                {
                    this.Add((AB)snv);
                }
                else
                {
                    var ie = (IList)snv;
                    foreach (var item in ie)
                    {
                        var ab = (AB)item;
                        Add(ab);
                    }
                }
            }
        }
        else if (t == typeof(ABC))
        {
            var abc = (ABC)o;
            this.AddRange(abc);
        }
        else
        {
            // Dont use like idiot TwoDimensionParamsIntoOne where is not needed - just iterate. Must more use radio and less blindness
            //var setsNameValue = CA.TwoDimensionParamsIntoOne(setsNameValue2);
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                this.Add(AB.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
            }
        }
    }
    public ABC(params AB[] abc)
    {
        // TODO: Complete member initialization
        this.AddRange(abc);
    }
    /// <summary>
    /// Must be [] due to SQL viz  https://stackoverflow.com/questions/9149919/no-mapping-exists-from-object-type-system-collections-generic-list-when-executin
    /// </summary>
    public Object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }
    public List<object> OnlyBsList()
    {
        List<object> o = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o.Add(this[i].B);
        }
        return o;
    }
    public List<string> OnlyAs()
    {
        List<string> o = new List<string>(this.Count);
        CASunamoExceptions.InitFillWith(o, this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o[i] = this[i].A;
        }
        return o;
    }
    public static List<object> OnlyBs(List<AB> arr)
    {
        return arr.Select(d => d.B).ToList();
    }
}