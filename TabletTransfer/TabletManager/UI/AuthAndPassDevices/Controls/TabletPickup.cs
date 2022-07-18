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
    public partial class TabletPickup : UserControl
    {
        Subject<bool> okClicked = new Subject<bool>();
        
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IObservable<bool> OkClicked => okClicked.AsObservable();
        public TabletPickup()
        {
            InitializeComponent();

            Observable
                .FromEventPattern(h => button1.Click += h, h => button1.Click -= h)
                .Subscribe(x => {
                    okClicked.OnNext(textBox1.Text == textBox1.Tag.ToString());
                });
        }
        public enum State
        { 
            Unauthorized,
            Enabled,
            NothingToPickup
        }
        public void SetExpectedTabletCount(BehaviorSubject<int> state) =>
            state.Subscribe((x) => {
                if (x == 0)
                {
                    Clear("Brak tabletów do przekazania, możesz zamknąć to okno i kontynuować pracę w aplikacji.");
                }
                else if (x < 0)
                {
                    Clear("Odbiór jest możliwy dopiero po autoryzacji zmiennika.");
                }
                else
                {
                    lPlaceholder.Visible = false;
                    textBox1.Tag = x;
                }
            });
        public void Clear(string msg)
        {
            textBox1.Text = "";
            textBox1.Tag = null;

            lPlaceholder.Visible = true;
            lPlaceholder.Text = msg;
            lPlaceholder.Dock = DockStyle.Fill;
            lPlaceholder.BringToFront();
        }
    }
}
