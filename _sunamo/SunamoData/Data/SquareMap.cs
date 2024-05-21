namespace SunamoString;


public class SquareMap
{
    public List<int> cub = new List<int>();
    public List<int> sqb = new List<int>();
    public List<int> b = new List<int>();
    public List<int> ecub = new List<int>();
    public List<int> esqb = new List<int>();
    public List<int> eb = new List<int>();
    public void Add(Object /*Brackets*/ b2, bool end, int i)
    {
        /*Spíše než na spoléhaní na public Brackets tak to dočasně zakomentuji.
         * Je tu cycle detected mezi SunamoData a SunamoString 
         */
        //if (end)
        //{
        //    switch (b2)
        //    {
        //        case Brackets.Curly:
        //            ecub.Add(i);
        //            break;
        //        case Brackets.Square:
        //            esqb.Add(i);
        //            break;
        //        case Brackets.Normal:
        //            eb.Add(i);
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
        //            cub.Add(i);
        //            break;
        //        case Brackets.Square:
        //            sqb.Add(i);
        //            break;
        //        case Brackets.Normal:
        //            b.Add(i);
        //            break;
        //        default:
        //            ThrowEx.NotImplementedCase(b);
        //            break;
        //    }
        //}
    }
}