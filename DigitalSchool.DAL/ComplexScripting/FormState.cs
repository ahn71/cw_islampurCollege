using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class FormState
    {
        private bool IsMaximized = false;
        private FormWindowState winState;
        private FormBorderStyle brdStyle;
        private bool topMost;
        private Rectangle bounds;

        public void Maximize(Form targetForm)
        {
            if (this.IsMaximized)
                return;
            this.IsMaximized = true;
            this.Save(targetForm);
            targetForm.WindowState = FormWindowState.Maximized;
            targetForm.FormBorderStyle = FormBorderStyle.None;
            targetForm.TopMost = true;
            WinApi.SetWinFullScreen(targetForm.Handle);
        }

        public void Save(Form targetForm)
        {
            this.winState = targetForm.WindowState;
            this.brdStyle = targetForm.FormBorderStyle;
            this.topMost = targetForm.TopMost;
            this.bounds = targetForm.Bounds;
        }

        public void Restore(Form targetForm)
        {
            targetForm.WindowState = this.winState;
            targetForm.FormBorderStyle = this.brdStyle;
            targetForm.TopMost = this.topMost;
            targetForm.Bounds = this.bounds;
            this.IsMaximized = false;
        }
    }
}
