using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EApi;

namespace RESTApiDelo.Helpers
{
    public class Helper
    {
        public static bool CheckRepeat(int aOrderNum, Head head)
        {
            try
            {
                dynamic MyResultSet = head.GetResultSet();
                MyResultSet.Source = head.GetCriterion("Table");
                MyResultSet.Source.SetParameters("DocKind", "In");
                MyResultSet.Source.SetParameters("Rc.DocDate", "01/01/2021:31/12/2021");
                MyResultSet.Fill();

                List<int> ListOfNumbers = new List<int>();
                foreach (var a in MyResultSet)
                {
                    ListOfNumbers.Add(a.ordernum);
                }

                foreach (int i in ListOfNumbers)
                {
                    if (aOrderNum == i)
                    {
                        return true;
                    }

                }
                return false;
            }
            catch
            {
                return true;
            }

        }

        public static bool CheckCanBeUsed(int aOrderNum, Head head)
        {
            int lastFreeNum;
            string aFN = String.Empty;
            int? aON = 0;
            Procedures.reserve_num(head, "N", "0.2EZ47.2EZ49.", 2021, "0.", ref aON, ref aFN, null);
            lastFreeNum = int.Parse(aON.ToString());
            Procedures.return_num(head, "R", "0.2EZ47.2EZ49.", DateTime.Now.Year, ref aON);
            if (lastFreeNum <= aOrderNum)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
