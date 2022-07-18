using CommonDatabase.Data;
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
using TabletManager.AI;

namespace TabletManager.UI.Controls
{
    public partial class UserInfo : UserControl
    {
        public UserInfo()
        {
            InitializeComponent();

            Observable
                .FromEventPattern(h => bChange.Click += h, h => bChange.Click -= h)
                .Subscribe((x) => {
                    operatorChangeRequest.OnNext(true);
                });
        }
        public void SetOperator(BehaviorSubject<Operator> subscribable_op) => 
            subscribable_op.Subscribe((op) =>
            {
                pictureBox1.Image = op?.Photo;
                label2.Text = op?.ToString();
                label3.Text = op?.PositionName;
                if (op != null)
                {
                    switch (op?.Position)
                    {
                        case Operator.PositionEnum.Service:
                            label3.BackColor = Color.Black;
                            label3.ForeColor = Color.White;
                            break;
                        case Operator.PositionEnum.TL:
                            label3.BackColor = Color.White;
                            label3.ForeColor = Color.Black;
                            break;
                        case Operator.PositionEnum.SL:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            break;
                        case Operator.PositionEnum.Normal:
                            label3.BackColor = Color.Blue;
                            label3.ForeColor = Color.White;
                            break;
                        default:
                            label3.BackColor = SystemColors.Control;
                            label3.ForeColor = SystemColors.ControlText;
                            break;
                    }
                }
            });

        private Subject<bool> operatorChangeRequest = new Subject<bool>();
        
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IObservable<bool> OperatorChangeRequested => operatorChangeRequest.AsObservable();
    }
}
