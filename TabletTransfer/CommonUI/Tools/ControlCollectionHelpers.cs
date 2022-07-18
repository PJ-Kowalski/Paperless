using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonUI.Tools
{
    public static class ControlCollectionHelpers
    {
        public static IEnumerable<Control> ProcessAllControls(this Control rootControl)
        {
            foreach (Control childControl in rootControl.Controls)
            {
                yield return childControl;
                
                foreach (var c in ProcessAllControls(childControl))
                {
                    yield return c;
                }
            }
        }
    }
}
