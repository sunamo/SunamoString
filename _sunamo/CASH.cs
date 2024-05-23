namespace SunamoString;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SunamoString
//{
public class CASH
{
    public static bool HasIndex(int dex, Array col)
    {
        return col.Length > dex;
    }
    public static bool HasIndex(int p, IList nahledy)
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

    //        public static Func<Object[], Object[]> ConvertListStringWrappedInArray;
    //        public static Func<int, IList, bool> HasIndex;
    //        public static Func<List<int>, int, int> FirstValueHigherThan;


    //    }
}
