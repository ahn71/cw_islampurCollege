using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DS.DAL.AdviitDAL
{
    public static class adviitValidate
    {
        public static bool validateText(TextBox txt, HtmlGenericControl lblMessage, string warningMessage)
        {
            try
            {
                if (txt.Text.Trim().Length != 0)
                    return true;
                lblMessage.InnerText = warningMessage;
                txt.Attributes["style"] = "1px solid red";
                txt.Focus();
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool validateText(TextBox txt, int minLength, HtmlGenericControl lblMessage, string warningMessage)
        {
            try
            {
                if (txt.Text.Trim().Length >= minLength)
                    return true;
                lblMessage.InnerText = warningMessage;
                txt.Focus();
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool validateText(TextBox txt, int minLength, int maxLength, HtmlGenericControl lblMessage, string warningMessage)
        {
            try
            {
                if (txt.Text.Trim().Length < minLength)
                {
                    lblMessage.InnerText = warningMessage;
                    txt.Attributes["style"] = "1px solid red";
                    txt.Focus();
                    return false;
                }
                else
                {
                    if (txt.Text.Trim().Length <= maxLength)
                        return true;
                    lblMessage.InnerText = "Larger than " + (object)maxLength + " characters is not allowed";
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool compare2Inputs(TextBox txt1stInput, TextBox txt2ndInput, int minLength)
        {
            try
            {
                if (txt1stInput.Text.Trim().Length < minLength)
                {
                    txt1stInput.Focus();
                    return false;
                }
                else if (txt2ndInput.Text.Trim().Length < minLength)
                {
                    txt2ndInput.Focus();
                    return false;
                }
                else
                {
                    if (txt1stInput.Text.CompareTo(txt2ndInput.Text) == 0)
                        return true;
                    txt1stInput.Focus();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool validateCombo(DropDownList dl, HtmlGenericControl lblMessage, string defaultValue, string warningMessage)
        {
            if (((object)dl.SelectedValue).ToString().Length == 0)
            {
                lblMessage.InnerText = "warning->" + warningMessage;
                dl.Focus();
                return false;
            }
            else
            {
                if (!(((object)dl.SelectedValue).ToString() == defaultValue))
                    return true;
                lblMessage.InnerText = "warning->" + warningMessage;
                dl.Focus();
                return false;
            }
        }

        public static bool checkLength(TextBox txt, int minLength, int maxLength, HtmlGenericControl lblMessage)
        {
            try
            {
                if (txt.Text.Length <= maxLength && txt.Text.Length >= minLength)
                    return true;
                lblMessage.InnerText = "warning->Input must be (" + (object)minLength + "-" + (string)(object)maxLength + ") characters long";
                txt.Attributes["style"] = "1px solid red";
                txt.Focus();
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isDateLargerThanYesterday(TextBox txt, HtmlGenericControl lblMessage)
        {
            try
            {
                if (DateTime.Parse(txt.Text).Subtract(DateTime.Now).Days >= 0)
                    return true;
                lblMessage.InnerText = "warning->Date must be larger than yesterday";
                txt.Attributes["style"] = "1px solid red";
                txt.Focus();
                return false;
            }
            catch
            {
                lblMessage.InnerText = "warning->Date must be larger than yesterday";
                txt.Attributes["style"] = "1px solid red";
                txt.Focus();
                return false;
            }
        }
    }
}