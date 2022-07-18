using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    public class Location
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public object[] GetDefaultRow()
        {
            return new object[] { Name, Position };
        }
        public bool Dirty { get; set; } = false;
    }
}
