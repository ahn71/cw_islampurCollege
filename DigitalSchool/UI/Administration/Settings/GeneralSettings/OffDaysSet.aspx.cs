using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.ComplexScripting;
using DS.DAL.AdviitDAL;
using System.Collections;
using DS.BLL.Timetable;
using DS.PropertyEntities.Model.Timetable;
using DS.DAL;
using DS.UI.Administration.Settings.GeneralSettings;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class OffDaysSet : System.Web.UI.Page
    {       
        SqlCommand cmd;
        WeeklyDaysBLL Wdays;
        ArrayList allWeekendDays = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "OffDaysSet.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                Classes.commonTask.loadShift(ckbShiftList);
                if (ckbShiftList.Items.Count < 2)
                    ckbAll.Visible = false;
                loadOffDateList("");
            }
                    
        }
        private void loadOffDateList(string sqlCmd)
        {
            try
            {
               
               // if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "SELECT OffDateId,CONVERT(VARCHAR(11), OffDate, 105) as OffDate,Purpose,DayName,Month,ShiftID FROM OffdaySettings ORDER BY offDateYear";
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = " SELECT distinct ROW_NUMBER() Over (Order by OffDate) As OffDateId, CONVERT(VARCHAR(11), OffDate, 105) as OffDate,Purpose,DayName,Month,day(OffDate)  ,Month(OffDate),year(OffDate) FROM OffdaySettings "+
                    "group by OffDate,Purpose,DayName,Month "+
                    "ORDER by year(OffDate) desc ,Month(OffDate) desc, day(OffDate) desc";
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sqlCmd);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='tblOffDayList' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Off Day Date</th>";
                divInfo += "<th>Day</th>";
                divInfo += "<th>Month</th>";
                divInfo += "<th>Purpose</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";
                    divOffDayList.Controls.Clear();
                    divOffDayList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";
               
               
                for (int x = 0; x < dt.Rows.Count; x++)
                {          
                   
                    id = dt.Rows[x]["OffDateId"].ToString();
                    divInfo += "<tr id='r_" + id +"'>";
                    divInfo += "<td style='font-weight: bold;' >" + dt.Rows[x]["OffDate"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DayName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Month"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Purpose"].ToString() + "</td>";
                    if (dt.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday"))
                        divInfo += "<td ></td>";
                    else
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editOffDate(" + id + ","+ckbShiftList.Items.Count+");'  />";

                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divOffDayList.Controls.Clear();
                divOffDayList.Controls.Add(new LiteralControl(divInfo));
                
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (hfStatus.Value.ToString().Equals("Save"))
            {
                if (saveOffDay() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
                    ckbShiftList.ClearSelection();
                    ckbAll.Checked = false;
                }
            }
            else { updateOffDay(); }
        }
        private void updateOffDay()
        {
            try
            {
                string[] Date = hfDate.Value.Split('-') ;
                cmd = new SqlCommand("delete from OffdaySettings where OffDate='" + Date[2] + "-" + Date[1] + "-" + Date[0] + "'", DbConnection.Connection);

                //cmd = new SqlCommand("UPDATE offDaySettings SET OffDate=@OffDate, Purpose=@Purpose, DayName=@DayName, Month=@Month WHERE OffDateId=@OffDateId", DbConnection.Connection);
                //cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(txtDate.Text));
                //cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                //cmd.Parameters.AddWithValue("@DayName", convertDateTime.getCertainCulture(txtDate.Text).ToString("dddd"));
                //cmd.Parameters.AddWithValue("@Month", convertDateTime.getCertainCulture(txtDate.Text).ToString("MMMM"));
                //cmd.Parameters.AddWithValue("@OffDateId", hfOffDateId.Value.ToString());
                byte result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                if (result > 0)
                {
                    if (saveOffDay())
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    loadOffDateList("");
                    hfDate.Value = "";
                    ckbShiftList.ClearSelection();
                    ckbAll.Checked = false;
                }
            }
            catch { }
        }
        protected void btnWeekendDaysGenerator_Click(object sender, EventArgs e)
        {
            DataBindToChkControl();
            showWeeklyDaysModal.Show();            
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
                if (!result)
                {
                    lblMessage.InnerHtml = "error-> Unable to save";
                    return;
                }
                lblMessage.InnerHtml = "success-> Save successfully";
                DataBindToChkControl();
                showWeeklyDaysModal.Show();         
            }
        }
        protected void btnConfirmGenerator_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
            GenerateWeekendDays();
        }        
        private void GenerateWeekendDays()
        {
            try
            {                
                DateTime begin = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
                DataTable dtWeekend = new DataTable();
                dtWeekend = CRUD.ReturnTableNull("SELECT DayName From Tbl_Weekly_days WHERE Status = 'true'");
                while (begin <= end)
                {
                    for (int i = 0; i < dtWeekend.Rows.Count; i++ )
                    {
                        if (begin.DayOfWeek == ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dtWeekend.Rows[i]["DayName"].ToString())))
                        {
                            allWeekendDays.Add(begin.ToString("dd-MM-yyyy"));
                        }                            
                    }                        
                    begin = begin.AddDays(1);
                }
                if(saveOffDay())
                {
                    lblMessage.InnerHtml = "success-> Save successfully";
                }
            }
            catch { }
        }
        private Boolean saveOffDay()
        {
            try
            {
                
                byte result = 0;
                if (allWeekendDays.Count.Equals(0))
                {
                    int i = 0;
                    foreach (ListItem item in ckbShiftList.Items)
                    {
                        if (item.Selected == true)
                        { 
                            cmd = new SqlCommand("INSERT INTO OffdaySettings(OffDate,Purpose,DayName,Month,OffDateYear,ShiftID) VALUES (@OffDate,@Purpose,@DayName,@Month,@OffDateYear,@ShiftID)", DbConnection.Connection);
                        cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(txtDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text.Trim());
                        cmd.Parameters.AddWithValue("@DayName", convertDateTime.getCertainCulture(txtDate.Text.Trim()).ToString("dddd"));
                        cmd.Parameters.AddWithValue("@Month", convertDateTime.getCertainCulture(txtDate.Text.Trim()).ToString("MMMM"));
                        cmd.Parameters.AddWithValue("@OffDateYear", DateTime.Now.Year.ToString());
                        cmd.Parameters.AddWithValue("@ShiftID", ckbShiftList.Items[i].Value);
                        result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                        }
                        i++;
                    }
                }
                else
                {
                    SqlCommand cmdDelete = new SqlCommand("DELETE FROM OffdaySettings WHERE OffDateYear=" + DateTime.Now.Year + " AND Purpose='Weekly Holiday'", DbConnection.Connection);
                    cmdDelete.ExecuteNonQuery();
                    for (int i = 0; i < allWeekendDays.Count; i++)
                    {
                        cmd = new SqlCommand("INSERT INTO OffdaySettings (OffDate,Purpose,DayName,Month,OffDateYear) VALUES (@OffDate,@Purpose,@DayName,@Month,@OffDateYear)", DbConnection.Connection);
                        cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(allWeekendDays[i].ToString()));
                        cmd.Parameters.AddWithValue("@Purpose", "Weekly Holiday");
                        cmd.Parameters.AddWithValue("@DayName", convertDateTime.getCertainCulture(allWeekendDays[i].ToString()).ToString("dddd"));
                        cmd.Parameters.AddWithValue("@Month", convertDateTime.getCertainCulture(allWeekendDays[i].ToString()).ToString("MMMM"));
                        cmd.Parameters.AddWithValue("@OffDateYear", DateTime.Now.Year.ToString());
                        result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                    }
                }                
                loadOffDateList("");
                return true;
            }
            catch 
            {
                loadOffDateList("");
                lblMessage.InnerText = "warning->This date is already added as a off day";
                return false;
            }
        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in ckbShiftList .Items)
            {
                item.Selected = ckbAll.Checked;
            }
        }

        protected void ckbShiftList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAll=true;
            foreach (ListItem item in ckbShiftList.Items)
            {         if(item.Selected==false)       
                    isAll =false ;            
            }
            ckbAll.Checked = isAll;
        }
        [System.Web.Services.WebMethod]
        public static string[] getUpdateInfo(string Date)
        {
            try
            {

                 
                OffDaysSet obj = new OffDaysSet();
                string[] Date1 = Date.Split('-');
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("Select ShiftID FROM OffdaySettings WHERE OffDate='" + Date1[2] + "-" + Date1[1] + "-" + Date1[0] + "'");
                string[] shiftId = new string[dt.Rows.Count];
                if (dt.Rows.Count > 0)
                {
                   
                  for(int i=0;i<dt.Rows.Count;i++)
                  {
                      shiftId[i] = dt.Rows[i]["ShiftID"].ToString();
                  }
                }
                return shiftId;
            }
            catch { return null; }




        }
    }
}