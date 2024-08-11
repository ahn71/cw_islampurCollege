using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.Timetable;
using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.Timetable;
using DS.SysErrMsgHandler;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class CreateWeekdays : System.Web.UI.Page
    {
        WeeklyDaysBLL Wdays;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerHtml = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "CreateWeekdays.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindToChkControl();
            }
        }

        private void DataBindToChkControl()
        {
           
            if (Wdays == null)
            {
                Wdays = new WeeklyDaysBLL();
            }
            IList<WeeklyDaysEntities> chkStatus = Wdays.GetWDaysEntities();
            if (chkStatus == null) return;
            foreach (var b in chkStatus)
            {
                if (b.status)
                {
                    chkboxSelection(b.DayName);
                }
            }
        }

        private void chkboxSelection(string name)
        {
            switch (name)
            {
                case "Saturday":
                    Sat.Checked = true;
                    break;
                case "Sunday":
                    Sun.Checked = true;
                    break;
                case "Monday":
                    Mon.Checked = true;
                    break;
                case "Tuesday":
                    Tue.Checked = true;
                    break;
                case "Wednesday":
                    Wed.Checked = true;
                    break;
                case "Thursday":
                    Thu.Checked = true;
                    break;
                case "Friday":
                    Fri.Checked = true;
                    break;
            }
        }        

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            bool result;
            using (WeeklyDaysEntities weeklyDaysEntities = new WeeklyDaysEntities())
            {
                CheckBox chk = (CheckBox)sender;
                weeklyDaysEntities.status = chk.Checked;
                weeklyDaysEntities.DayShortName = chk.ID;                
                if (Wdays == null)
                {
                    Wdays = new WeeklyDaysBLL();
                }
                result = Wdays.UpdateDaySeletion(weeklyDaysEntities);
                if(!result)
                {
                    lblMessage.InnerHtml = "error-> Unable to save";
                    return;
                }
                lblMessage.InnerHtml = "success-> Saved successfully";
                DataBindToChkControl();
            }
        }
    }
}