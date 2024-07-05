namespace SunamoString._public.SunamoData.Data;


public class SquareMapLines
{
    public Dictionary<int, List<int>> cub;
    public Dictionary<int, List<int>> sqb;
    public Dictionary<int, List<int>> b;
    public Dictionary<int, List<int>> ecub;
    public Dictionary<int, List<int>> esqb;
    public Dictionary<int, List<int>> eb;
    void Init(int ccub, int csqb, int cb)
    {
        cub = new Dictionary<int, List<int>>(ccub);
        sqb = new Dictionary<int, List<int>>(csqb);
        b = new Dictionary<int, List<int>>(cb);
        ecub = new Dictionary<int, List<int>>(ccub);
        esqb = new Dictionary<int, List<int>>(csqb);
        eb = new Dictionary<int, List<int>>(cb);
    }
    public SquareMapLines(SquareMap m)
    {
        Init(m.cub.Count, m.sqb.Count, m.b.Count);
    }
    public SquareMapLines()
    {
        Init(0, 0, 0);
    }
    
    
    
    
    
    
    
    public void Add( Object b2, bool end, int i, int line)
    {
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}