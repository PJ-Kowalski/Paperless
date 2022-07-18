using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    [Serializable]
    public class Alert
    {
        public decimal Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string AlertType { get; set; }
        public string Content { get; set; }
        public string SourceName { get; set; }
        public string Location { get; set; }
        public string AckBy { get; set; }
        public DateTime? AckDate { get; set; }
        public decimal? TabletId { get; set; }

        public object[] GetDefaultRow()
        {
            return new object[] { CreationDate, CreatedBy, AlertType, Content, SourceName, Location, AckBy, AckDate.HasValue ? AckDate.Value.ToString() : null };
        }
    }
}
