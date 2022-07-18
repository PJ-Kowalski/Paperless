using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Tools
{
    public class DecodedOperatorBarcode
    {
        public string ACPNo { get; set; }
        public bool IsTemporary { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public override string ToString()
        {
            return ACPNo;
        }
    }
}
