using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabletManager.UI.Controls
{
    public partial class Events : UserControl, ILocationAwareControl
    {
        private Subject<bool> unackedExist = new Subject<bool>();
        public IObservable<bool> UnackedExist => unackedExist.AsObservable();

        private string location = "";
        public Events()
        {
            InitializeComponent();
            
            tabletHistoryGrid1.Filter
                .Where(x => !DesignMode)
                .Where(x => location != "")
                .Subscribe((f) => {
                    tabletHistoryGrid1.Data = CommonDatabase.CommonDbAccess.GetHistoryForLocation(location, f.minimum, f.maximum);
                });
            
            alertsGrid1.Filter
                .Where(x => !DesignMode)
                .Where(x => location != "")
                .Subscribe((f) => {
                    alertsGrid1.Data = CommonDatabase.CommonDbAccess.GetAlertsForLocation(location, f.minimum);
                    
                    unackedExist.OnNext(alertsGrid1.Data.Any(x => !x.AckDate.HasValue));
                });
        }

        public void SetTabletLocation(string location)
        {
            this.location = location;
            tabletHistoryGrid1.Data = CommonDatabase.CommonDbAccess.GetHistoryForLocation(location, tabletHistoryGrid1.From, tabletHistoryGrid1.To);
            alertsGrid1.Data = CommonDatabase.CommonDbAccess.GetAlertsForLocation(location, alertsGrid1.From);

            unackedExist.OnNext(alertsGrid1.Data.Any(x => !x.AckDate.HasValue));
        }
    }
}
