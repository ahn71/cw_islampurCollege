using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using DS.BLL;

namespace DS.Forms
{
    public partial class FeesCategoriesInfo : System.Web.UI.Page
    {
        // Developed By MD.Rohol Amin      
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
                    Classes.commonTask.loadBatch(dlBatchName);
                    loadFeesCategoryInfo("");
                }
            }
        }

        private Boolean saveFeesCategoryInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  FeesCategoryInfo ( BatchName, DateOfCreation, FeeFine, FeeCatName)  values (@BatchName, @DateOfCreation, @FeeFine, @FeeCatName) ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@BatchName", dlBatchName.Text);
                cmd.Parameters.AddWithValue("@DateOfCreation", TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@FeeFine", txtFeesFine.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeCatName", txtFeesCatName.Text.Trim());

                int result = (int)cmd.ExecuteNonQuery();
                loadFeesCategoryInfo("");
                lblFeesCateId.Value = "";
                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                loadFeesCategoryInfo("");
                return false;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblFeesCateId.Value.ToString().Length == 0) saveFeesCategoryInfo();
            else updateFeesCategoryInfo();
        }

        private void loadFeesCategoryInfo(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select FeeCatId, BatchName, convert(varchar(11),DateOfCreation,106) as DateOfCreation, FeeFine, FeeCatName from FeesCategoryInfo";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Batch Name</th>";
                divInfo += "<th>Date of Creation</th>";
                divInfo += "<th class='numeric'>Fee Fine</th>";
                divInfo += "<th>Fee CatName</th>";
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["FeeCatId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["BatchName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DateOfCreation"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["FeeFine"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FeeCatName"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editFeesCategory(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private Boolean updateFeesCategoryInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update FeesCategoryInfo  Set BatchName=@BatchName,  FeeFine=@FeeFine, FeeCatName=@FeeCatName where FeeCatId=@FeeCatId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@FeeCatId", lblFeesCateId.Value.ToString());
                cmd.Parameters.AddWithValue("@BatchName", dlBatchName.Text);
                cmd.Parameters.AddWithValue("@FeeFine", txtFeesFine.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeCatName", txtFeesCatName.Text.Trim());

                cmd.ExecuteNonQuery();
                loadFeesCategoryInfo("");
                lblFeesCateId.Value = "";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                loadFeesCategoryInfo("");
                return false;
            }
        }
    }
}