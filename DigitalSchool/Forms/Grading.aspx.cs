using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class Grading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadGrading();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblGId.Value.ToString().Length == 0)
            {
                if (saveGrading() == true)
                {
                    LoadGrading();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
                }
            }
            else
            {
                if (updateGrading() == true)
                {
                    LoadGrading();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }
        private Boolean saveGrading()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into  Grading (GName, GMarkMin, GMarkMax, GPointMin,GPointMax)  values (@GName, @GMarkMin, @GMarkMax, @GPointMin,@GPointMax) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@GName", txtGrade.Text.Trim());
                cmd.Parameters.AddWithValue("@GMarkMin", txtGradeMin.Text.Trim());
                cmd.Parameters.AddWithValue("@GMarkMax", txtGradeMax.Text.Trim());
                cmd.Parameters.AddWithValue("@GPointMin", txtGPointMin.Text.Trim());
                cmd.Parameters.AddWithValue("@GPointMax", txtGPointMax.Text.Trim());
                int result = (int)cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean updateGrading()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update Grading  Set GName=@GName, GMarkMin=@GMarkMin, GMarkMax=@GMarkMax, GPointMin=@GPointMin,GPointMax=@GPointMax where GId=@GId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@GId",lblGId.Value.ToString());
                cmd.Parameters.AddWithValue("@GName", txtGrade.Text.Trim());
                cmd.Parameters.AddWithValue("@GMarkMin", txtGradeMin.Text.Trim());
                cmd.Parameters.AddWithValue("@GMarkMax", txtGradeMax.Text.Trim());
                cmd.Parameters.AddWithValue("@GPointMin", txtGPointMin.Text.Trim());
                cmd.Parameters.AddWithValue("@GPointMax", txtGPointMax.Text.Trim());
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void LoadGrading()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select GId, GName, GMarkMin, GMarkMax, GPointMin,GPointMax from Grading Order by GId ASC", dt);

                string divInfo = "";
                int totalRows = dt.Rows.Count;
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Grading available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divGradingList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblClassList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='text-align:center'>Grade</th>";
                divInfo += "<th style='text-align:center'>Mark Min</th>";
                divInfo += "<th style='text-align:center'>Mark Max</th>";
                divInfo += "<th style='text-align:center'>Point Min</th>";
                divInfo += "<th style='text-align:center'>Point Max</th>";
                divInfo += "<th style='width:55px'>Edit</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                for (int x = 0; x < totalRows; x++)
                {
                    divInfo += "<tr id='r_" + dt.Rows[x]["GId"].ToString() + "'>";

                    divInfo += "<td style='text-align:center'>" + dt.Rows[x]["GName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center'>" + dt.Rows[x]["GMarkMin"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center'>" + dt.Rows[x]["GMarkMax"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center'>" + float.Parse(dt.Rows[x]["GPointMin"].ToString()).ToString("#.00") + "</td>";
                    divInfo += "<td style='text-align:center'>" + float.Parse(dt.Rows[x]["GPointMax"].ToString()).ToString("#.00") + "</td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editGrading(" + dt.Rows[x]["GId"].ToString() + " );'/></td>";                   
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divGradingList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}