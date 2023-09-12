using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using DS.BLL;

namespace DS.Forms
{
    public partial class AttendanceSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        loadAbsentFineAmount();
                        loadAttendanceCountType();
                    }
                }
            }
            catch { }
        }

        protected void chkAbsentFineCount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbsentFineCount.Checked) txtAbsentFineAmount.Visible = true;
            else txtAbsentFineAmount.Visible = false;
        }
        private void loadAttendanceCountType()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select CountType from AttendanceCountType",dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["CountType"].ToString().Equals("Manually System")) rbAttendanceCountType.SelectedIndex = 0;
                    else rbAttendanceCountType.SelectedIndex = 1;
                }
            }
            catch { }
        }
        protected void btnSaveFineAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAbsentFineAmount.Text != "" && lblAbsentId.Value.ToString().Length == 0)
                {
                    SqlCommand cmd = new SqlCommand("Insert Into AbsentFine (AbsentFineAmount,Date) values (@AbsentFineAmount,@Date) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@AbsentFineAmount", txtAbsentFineAmount.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date", convertDateTime.getCertainCulture(TimeZoneBD.getCurrentTimeBD().ToString("d-M-yyyy")));
                    cmd.ExecuteNonQuery();
                    txtAbsentFineAmount.Text = "";
                    loadAbsentFineAmount();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                }
                else if (txtAbsentFineAmount.Text != "" && lblAbsentId.Value.ToString().Length > 0)
                {
                    SqlCommand cmd = new SqlCommand("Update AbsentFine SET AbsentFineAmount=@AbsentFineAmount,Date=@Date Where AFId=@AFId ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@AFId", lblAbsentId.Value);
                    cmd.Parameters.AddWithValue("@AbsentFineAmount", txtAbsentFineAmount.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date", TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                    txtAbsentFineAmount.Text = "";
                    loadAbsentFineAmount();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
                else loadAbsentFineAmount();
            }
            catch { }
        }
        private void loadAbsentFineAmount()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select AFId,AbsentFineAmount,convert(varchar(11),Date,106) as Date From AbsentFine Order By AFId DESC", dt);

                string divInfo = "";
                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric'>Fine Amount</th>";
                divInfo += "<th >Date</th>";
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["AFId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["AbsentFineAmount"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Date"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editAbsentAmount(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divAbsentFineList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtAbsentFineAmount.Text = "";
            lblAbsentId.Value = "";
        }
        protected void btnAttendanceCountType_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    SQLOperation.forDelete("AttendanceCountType", sqlDB.connection);
                }
                catch { }
                string getType = (rbAttendanceCountType.SelectedItem.ToString().Trim().Equals("Manually System")) ? "Manually System" : "Machine System";
                string[] getColumns = { "CountType" };
                string[] getValues = {getType};
                if (SQLOperation.forSaveValue("AttendanceCountType", getColumns, getValues,sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success->Successfully "+rbAttendanceCountType.SelectedItem.ToString()+" type set";
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        
    }
}