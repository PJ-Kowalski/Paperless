using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    [Serializable]
    public class AnyEvent
    {
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string EventType { get; set; }
        public string Content { get; set; }
        public string SourceName { get; set; }
        public string Location { get; set; }

        public object[] GetDefaultRow()
        {
            return new object[] { CreationDate, CreatedBy, EventType, Content, SourceName, Location };
        }
    }
}
