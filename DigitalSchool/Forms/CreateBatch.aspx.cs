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
    public partial class CreateBatch : System.Web.UI.Page
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
                    loadBatch("");
                    Classes.commonTask.loadClass(ddlClassName);
                    ddlClassName.SelectedItem.Text = "---Select Class---";
                    Classes.commonTask.loadSession(ddlSession);
                   
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveBatchInfo();
        }
        private Boolean saveBatchInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  BatchInfo  (BatchName,IsUsed)  values (@BatchName,@IsUsed) ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@BatchName",ddlClassName.Text+ddlSession.Text);
                cmd.Parameters.AddWithValue("@IsUsed",0);
                if(cmd.ExecuteNonQuery()>0)
                {
                    loadBatch("");
                    lblMessage.InnerText = "success->Successfully saved";
                return true;
                }
                else 
                {
                    lblMessage.InnerText = "error->Unable to save";
                    return false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void loadBatch(string sqlcmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select * from BatchInfo  Order by BatchId ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Batch available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divBatchList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblBatch' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Batch Name</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["BatchId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["BatchName"].ToString() + "</td>";                 
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divBatchList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}