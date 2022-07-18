using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    [Serializable]
    public class TabletEvent : AnyEvent
    {
        public decimal? TabletId { get; set; }

        new public object[] GetDefaultRow()
        {
            return new object[] { CreationDate, CreatedBy, EventType, Content, SourceName };
        }
    }
}
