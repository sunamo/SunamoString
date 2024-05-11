namespace SunamoString;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SunamoString._sunamo
//{
internal class CASH
{
    internal static bool HasIndex(int dex, Array col)
    {
        return col.Length > dex;
    }
    internal static bool HasIndex(int p, IList nahledy)
    {
        if (p < 0)
        {
            throw new Exception("Chybn\u00FD parametr p");
        }
        if (nahledy.Count > p)
        {
            return true;
        }
        return false;
    }

    //        internal static Func<Object[], Object[]> ConvertListStringWrappedInArray;
    //        internal static Func<int, IList, bool> HasIndex;
    //        internal static Func<List<int>, int, int> FirstValueHigherThan;


    //    }
}
