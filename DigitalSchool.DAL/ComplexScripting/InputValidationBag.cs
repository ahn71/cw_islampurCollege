using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class InputValidationBag
    {
        public static void NumericValidationBasket_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar >= 48 & (int)e.KeyChar <= 57 || (int)e.KeyChar >= 96 & (int)e.KeyChar <= 105 || ((int)e.KeyChar == 8 || (int)e.KeyChar == 110) || (int)e.KeyChar == 190)
                    return;
                int num = (int)MessageBox.Show("Please type numeric value");
                e.Handled = true;
            }
            catch
            {
            }
        }

        public static bool ValidationBasket(Control[] getTools, string[] getDisplayMessage)
        {
            try
            {
                for (byte index = (byte)0; (int)index < getTools.Length; ++index)
                {
                    if (getTools[(int)index].Text.Trim() == "")
                    {
                        int num = (int)MessageBox.Show(getDisplayMessage[(int)index], "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        getTools[(int)index].Focus();
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidationBasket(Control[] getTools, string[] getDisplayMessage, Control getMessageBar)
        {
            try
            {
                for (byte index = (byte)0; (int)index < getTools.Length; ++index)
                {
                    if (getTools[(int)index].Text.Trim() == "")
                    {
                        getMessageBar.Text = getDisplayMessage[(int)index];
                        getTools[(int)index].Focus();
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidationBasket(Control[] getTools, string[] getDisplayMessage, ToolStripMenuItem getMessageBar)
        {
            try
            {
                for (byte index = (byte)0; (int)index < getTools.Length; ++index)
                {
                    if (getTools[(int)index].Text.Trim() == "")
                    {
                        getMessageBar.Text = getDisplayMessage[(int)index];
                        getTools[(int)index].Focus();
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EmptyStringValidationBasket(string Value)
        {
            try
            {
                return Value.Trim().Equals("");
            }
            catch
            {
                return false;
            }
        }

        public static string test()
        {
            return int.Parse(((object)"").ToString()).ToString();
        }
    }
}
