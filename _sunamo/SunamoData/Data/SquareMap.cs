namespace SunamoString;


internal class SquareMap
{
    internal List<int> cub = new List<int>();
    internal List<int> sqb = new List<int>();
    internal List<int> b = new List<int>();
    internal List<int> ecub = new List<int>();
    internal List<int> esqb = new List<int>();
    internal List<int> eb = new List<int>();
    internal void Add(Object /*Brackets*/ b2, bool end, int i)
    {
        /*Spíše než na spoléhaní na internal Brackets tak to dočasně zakomentuji.
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