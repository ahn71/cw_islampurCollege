using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;
using ComplexScriptingSystem;
using DS.BLL;

namespace DS.Forms
{
    public partial class OffDaysSet : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                loadOffDateList("");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (hfStatus.Value.ToString().Equals("Save"))
            {
                if (saveOffDay() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
                }
            }
            else { updateOffDay(); } 
        }

        private Boolean saveOffDay()
        {
            try
            {
                byte result=0;
                if (allFriday.Count.Equals(0))
                {
                    cmd = new SqlCommand("insert into OffdaySettings (OffDate,Purpose,updatedby,OffDateYear) values (@OffDate,@Purpose,@UpdateBy,@OffDateYear)", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@OffDate",convertDateTime.getCertainCulture( txtDate.Text));
                    cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                    cmd.Parameters.AddWithValue("@updateBy", 1);
                    cmd.Parameters.AddWithValue("@OffDateYear", TimeZoneBD.getCurrentTimeBD().Year.ToString());
                    result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                }
                else
                {
                    SqlCommand cmdDelete = new SqlCommand("Delete From OffdaySettings where OffDateYear=" + TimeZoneBD.getCurrentTimeBD().Year + " and Purpose='Weekly Holiday'", sqlDB.connection);
                    cmdDelete.ExecuteNonQuery();
                    for (int i = 0; i < allFriday.Count; i++)
                    {
                        cmd = new SqlCommand("insert into OffdaySettings (OffDate,Purpose,updatedby,OffDateYear) values (@OffDate,@Purpose,@UpdateBy,@OffDateYear)", sqlDB.connection);
                        cmd.Parameters.AddWithValue("@OffDate", convertDateTime.getCertainCulture(allFriday[i].ToString()));
                        cmd.Parameters.AddWithValue("@Purpose","Weekly Holiday");
                        cmd.Parameters.AddWithValue("@updateBy", 1);
                        cmd.Parameters.AddWithValue("@OffDateYear", TimeZoneBD.getCurrentTimeBD().Year.ToString());
                        result = byte.Parse(cmd.ExecuteNonQuery().ToString());
                    }
                }

                if (result > 1)
                {
                    lblMessage.InnerText = "success->Successfully set";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }

                loadOffDateList("");
                return true;
                
            }
            catch(Exception ex)
            { 
                loadOffDateList("");
                lblMessage.InnerText = "warning->This date is already added as a off day";
                return false; }
        }

        private void updateOffDay()
        {
            try
            {
                cmd = new SqlCommand("update offDaySettings set OffDate=@OffDate, Purpose=@Purpose where OffDateId=@OffDateId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@OffDate",convertDateTime.getCertainCulture(txtDate.Text));
                cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                cmd.Parameters.AddWithValue("@OffDateId", hfOffDateId.Value.ToString());             
                byte result=byte.Parse(cmd.ExecuteNonQuery().ToString());
                if (result > 0)
                {
                    lblMessage.InnerText = "success->Successfully Updated";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    loadOffDateList("");
                }
            }
            catch { }
        }

        private void loadOffDateList(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select OffDateId,CONVERT(VARCHAR(11), OffDate, 105) as OffDate,Purpose from OffdaySettings Order by offDateYear";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Entry any off day</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divOffDayList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo = " <table id='tblOffDayList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Off Day</th>";
                divInfo += "<th>Purpose</th>";
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["OffDateId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='font-weight: bold;' >" + dt.Rows[x]["OffDate"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Purpose"].ToString() + "</td>";
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editOffDate(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divOffDayList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnFridayGenerate_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            FridayGenerate();
        }

        ArrayList allFriday = new ArrayList();
        private void FridayGenerate()
        {
            try
            {
                DateTime begin = new DateTime(TimeZoneBD.getCurrentTimeBD().Year, 1, 1);
                DateTime end = new DateTime(TimeZoneBD.getCurrentTimeBD().Year, 12, 31);
                DataTable dtWeekend = new DataTable();
                sqlDB.fillDataTable("Select Distinct Weekend From ShiftConfiguration ", dtWeekend);                
                while (begin <= end)
                {
                    if (begin.DayOfWeek == ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dtWeekend.Rows[0]["Weekend"].ToString()))) allFriday.Add(begin.ToString("dd-MM-yyyy"));
                    begin = begin.AddDays(1);
                }
                saveOffDay();
            }
            catch { }
        }
    }
}