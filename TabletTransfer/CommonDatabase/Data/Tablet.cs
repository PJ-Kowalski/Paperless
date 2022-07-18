using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    [Serializable]
    public class Tablet
    {
        public decimal Id { get; set; }
        public string WindowsName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChange { get; set; }
        public Operator Responsible { get; set; }
        public string Location { get; set; }
        public Operator LockedBy { get; set; }
        public DateTime? LockedOn { get; set; }
        public string LockedReason { get; set; }
        public LockType LockType { get; set; }
        public bool Locked { get { return LockedBy != null; } }
        public string Barcode { get; set; }

        public object[] GetDefaultRow()
        {
            return new object[] { WindowsName, Barcode, Responsible, LockType };
        }
    }
}
