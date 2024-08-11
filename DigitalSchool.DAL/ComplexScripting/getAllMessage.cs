using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class getAllMessage
    {
        public static System.Timers.Timer timer = new System.Timers.Timer();
        public static System.Timers.Timer timer1 = new System.Timers.Timer();
        public static byte b = (byte)0;
        public static Control GetTool;

        public static bool getSaveMessage()
        {
            try
            {
                return MessageBox.Show("Do you want to save this record ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }
            catch
            {
                return false;
            }
        }

        public static bool getUpdateMessage()
        {
            try
            {
                return MessageBox.Show("Do you want to update this record ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }
            catch
            {
                return false;
            }
        }

        public static bool getDeleteMessage()
        {
            try
            {
                return MessageBox.Show("Do you want to delete this record ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }
            catch
            {
                return false;
            }
        }

        public static string getNumericMessage()
        {
            return "Please type the numeric value";
        }

        public static void getMessageDisplaBag(Control getTool, bool forError, string setStatusMessage)
        {
            try
            {
                getAllMessage.GetTool = getTool;
                getAllMessage.timer.Elapsed += new ElapsedEventHandler(getAllMessage.timer_Elapsed);
                getAllMessage.timer.Interval = 1000.0;
                if (!forError)
                {
                    getTool.ForeColor = Color.Green;
                    getTool.Text = setStatusMessage;
                }
                else
                {
                    getTool.ForeColor = Color.Red;
                    getTool.Text = setStatusMessage;
                }
                getAllMessage.GetTool.Text = setStatusMessage;
                getAllMessage.timer.Start();
            }
            catch
            {
            }
        }

        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ++getAllMessage.b;
                getAllMessage.GetTool.Text = "";
                if ((int)getAllMessage.b == 6)
                {
                    getAllMessage.timer.Enabled = false;
                    getAllMessage.timer.Stop();
                    getAllMessage.GetTool.Text = "";
                }
            }
            catch
            {
                getAllMessage.timer.Stop();
            }
        }

        public static void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }
    }
}
