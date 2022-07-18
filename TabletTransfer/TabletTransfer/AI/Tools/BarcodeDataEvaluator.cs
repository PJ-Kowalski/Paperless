using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TabletTransfer.AI.Tools
{
    static class BarcodeDataEvaluator
    {
        public static bool EvaluateBarcodeData(string data)
        {
            string pattern = "^@B";
            Match m =Regex.Match(data, pattern);
            if (m.Success)
            {
                return true;
            }
            return false;
        }
    }
}
