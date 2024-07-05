namespace SunamoString._public.SunamoData.Data;


public class ABCString : List<ABString>
{
    public static ABCString Empty = new ABCString();
    public ABCString()
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
    public ABCString(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null);
        }
    }
    public ABCString(params Object[] setsNameValue)
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
        
        if (t2 == typeof(ABString))
        {
            
            
            
            
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
            
            
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                this.Add(ABString.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
            }
        }
    }
    public ABCString(params ABString[] abc)
    {
        
        this.AddRange(abc);
    }
    
    
    
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
    public static List<object> OnlyBs(List<ABString> arr)
    {
        return arr.Select(d => d.B).ToList();
    }
}