namespace SunamoString;


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
    /// <summary>
    /// Musí být public kvůli Brackets které nemůžu importovat přímo z SunamoString kvůli Cycle detected
    /// </summary>
    /// <param name="b2"></param>
    /// <param name="end"></param>
    /// <param name="i"></param>
    /// <param name="line"></param>
    public void Add(/*Brackets*/ Object b2, bool end, int i, int line)
    {
        // čti koment ve SquareMap.Add proč Object a zakomentováno zde.
        // Dalo by se to vyřešit tím že Brackets dám do samostatného nugetu ale už  to tu mám že mám v nugetu jen 1 enum
        // chce to vyřešit lépe 
        //Dictionary<int, List<int>> dict = null;
        //if (end)
        //{
        //    switch (b2)
        //    {
        //        case Brackets.Curly:
        //            dict = ecub;
        //            break;
        //        case Brackets.Square:
        //            dict = esqb;
        //            break;
        //        case Brackets.Normal:
        //            dict = eb;
        //            break;
        //        default:
        //            ThrowEx.NotImplementedCase(b);
        //            break;
        //    }
        //}
        //else
        //{
        //    switch (b2)
        //    {
        //        case Brackets.Curly:
        //            dict = cub;
        //            break;
        //        case Brackets.Square:
        //            dict = sqb;
        //            break;
        //        case Brackets.Normal:
        //            dict = b;
        //            break;
        //        default:
        //            ThrowEx.NotImplementedCase(b);
        //            break;
        //    }
        //}
        //DictionaryHelper.AddOrCreate<int, int>(dict, line, i);
    }
}