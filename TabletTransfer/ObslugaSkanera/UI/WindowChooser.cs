using ObslugaSkanera.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObslugaSkanera
{
    public partial class WindowChooser : UserControl
    {
        public WindowChooser()
        {
            InitializeComponent();
        }
        Cursor previous;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            previous = Cursor.Current;
            Cursor.Current = Cursors.Cross;
            pictureBox1.Capture = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = previous;
            ChoosenWindowTitle = WinAPI.GetWindowTitle(MousePosition);
            OnChoosenWindowSelected?.Invoke(this, ChoosenWindowTitle);
        }
        public string ChoosenWindowTitle { get; set; }
        
        public delegate void ChoosenWindowSelectedHandler(object sender, string title);
        public event ChoosenWindowSelectedHandler OnChoosenWindowSelected;
    }
}
