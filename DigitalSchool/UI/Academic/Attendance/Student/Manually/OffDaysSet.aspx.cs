using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.Timetable;
using DS.BLL.Timetable;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Academics.Attendance.Student.Manually
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
                   
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "OffDaysSet.aspx", btnSave, btnWeekendDaysGenerator)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadOffDateList("");
                
        }
        private void loadOffDateList(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "SELECT OffDateId,CONVERT(VARCHAR(11), OffDate, 105) as OffDate,Purpose,DayName,Month " +
                                                           "FROM OffdaySettings ORDER BY offDateYear";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='tblOffDayList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Off Day Date</th>";
                divInfo += "<th>Day</th>";
                divInfo += "<th>Month</th>";
                divInfo += "<th>Purpose</th>";
                if (Session["__Update__"].ToString().ToLower().Equals("true")) divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "<tr><td colspan='5'>Off Days has not found</td></tr></tbody></table>";
                    divOffDayList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["OffDateId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='font-weight: bold;' >" + dt.Rows[x]["OffDate"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DayName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Month"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Purpose"].ToString() + "</td>";

                    if (Session["__Update__"].ToString().ToLower().Equals("true")) divInfo += "<td style='max-width:20px;' class='numeric control' >" + 
                        "<img src='/Images/gridImages/edit.png'  onclick='editOffDate(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divOffDayList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (hfStatus.Value.ToString().Equals("Save"))
            {
                if (Session["__Save__"].ToString().Equals("false"))
                {
                    lblMessage.InnerHtml = "warning-> Sorry you have not save peivilege ! ";
                    return;
                }

                if (saveOffDay() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
                }
            }
            else { updateOffDay(); }
        }
        private void updateOffDay()
        {
            try
            {
                cmd = new SqlCommand("UPDATE offDaySettings SET OffDate=@OffDate, Purpose=@Purpose, DayName=@DayName, Month=@Month " +
                                     "WHERE OffDateId=@OffDateId", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(txtDate.Text));
                cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                cmd.Parameters.AddWithValue("@DayName", convertDateTime.getCertainCulture(txtDate.Text).ToString("dddd"));
                cmd.Parameters.AddWithValue("@Month", convertDateTime.getCertainCulture(txtDate.Text).ToString("MMMM"));
                cmd.Parameters.AddWithValue("@OffDateId", hfOffDateId.Value.ToString());
                byte result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    loadOffDateList("");
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
                lblMessage.InnerHtml = "success-> Saved successfully";
                DataBindToChkControl();
                showWeeklyDaysModal.Show();
            }
        }
        protected void btnConfirmGenerator_Click(object sender, EventArgs e)
        {
            GenerateWeekendDays();
        }
        private void GenerateWeekendDays()
        {
            try
            {
                DateTime begin = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
                DataTable dtWeekend = new DataTable();
                sqlDB.fillDataTable("SELECT DayName From Tbl_Weekly_days WHERE Status = 'false'", dtWeekend);
                while (begin <= end)
                {
                    for (int i = 0; i < dtWeekend.Rows.Count; i++)
                    {
                        if (begin.DayOfWeek == ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dtWeekend.Rows[i]["DayName"].ToString())))
                        {
                            allWeekendDays.Add(begin.ToString("dd-MM-yyyy"));
                        }
                    }
                    begin = begin.AddDays(1);
                }                
                if (saveOffDay())
                {
                    lblMessage.InnerHtml = "success-> Saved successfully";
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
                    cmd = new SqlCommand("INSERT INTO OffdaySettings(OffDate,Purpose,DayName,Month,OffDateYear) VALUES " +
                                         "(@OffDate,@Purpose,@DayName,@Month,@OffDateYear)", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(txtDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text.Trim());
                    cmd.Parameters.AddWithValue("@DayName", convertDateTime.getCertainCulture(txtDate.Text.Trim()).ToString("dddd"));
                    cmd.Parameters.AddWithValue("@Month", convertDateTime.getCertainCulture(txtDate.Text.Trim()).ToString("MMMM"));
                    cmd.Parameters.AddWithValue("@OffDateYear", DateTime.Now.Year.ToString());
                    result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                }
                else
                {
                    SqlCommand cmdDelete = new SqlCommand("DELETE FROM OffdaySettings WHERE OffDateYear=" + DateTime.Now.Year + " AND " +
                                                          "Purpose='Weekly Holiday'", DbConnection.Connection);
                    cmdDelete.ExecuteNonQuery();
                    for (int i = 0; i < allWeekendDays.Count; i++)
                    {
                        cmd = new SqlCommand("INSERT INTO OffdaySettings (OffDate,Purpose,DayName,Month,OffDateYear) VALUES " +
                                             "(@OffDate,@Purpose,@DayName,@Month,@OffDateYear)", DbConnection.Connection);
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
    }
}