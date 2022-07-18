using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    public class OnPremiseInfo
    {
        public long AllianceId { get; set; }
        public decimal Certainity { get; set; }
        public string Gate { get; set; }
        public string Region { get; set; }
        public DateTime GateTime { get; set; }
    }
}
