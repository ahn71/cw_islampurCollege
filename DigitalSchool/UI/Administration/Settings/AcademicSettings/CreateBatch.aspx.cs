using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Settings.AcademicSettings.ManagedBatch
{
    public partial class CreateBatch : System.Web.UI.Page
    {
        BatchEntry batchEntry;
        List<BatchEntities> batchlist = new List<BatchEntities>();
        protected void Page_Load(object sender, EventArgs e)
        {            
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "CreateBatch.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadBatch("");
                    ClassEntry.GetEntitiesData(ddlClassName);                
                    Classes.commonTask.loadSession(ddlSession);
                   

            }
        }
        private void loadBatch(string sqlcmd)
        {
            try
            {
                int totalRows = 0;
                try
                {
                    batchEntry = new BatchEntry();
                    batchlist = batchEntry.GetEntitiesData();
                    totalRows = batchlist.Count;
                }
                catch { }
                string divInfo = "";
                divInfo = " <table id='tblBatch' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>SL</th>";
                divInfo += "<th>Batch Name</th>";
                divInfo += "<th>Year</th>";
                divInfo += "<th>Is Use</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";                                  
                    divBatchList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }                
                string id = "";
                for (int x = 0; x < batchlist.Count; x++)
                {
                    id = batchlist[x].BatchId.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + (x + 1) + "</td>";
                    divInfo += "<td>" +batchlist[x].BatchName + "</td>";
                    divInfo += "<td>" + batchlist[x].Year + "</td>";
                    if (batchlist[x].IsUsed == true)
                    {
                        divInfo += "<td>True</td>";
                    }
                    else
                    {
                        divInfo += "<td>False</td>";
                    }
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divBatchList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (batchEntry == null)
            {
                batchEntry = new BatchEntry();
            }
            batchlist = batchEntry.GetEntitiesData();
            if (batchlist != null)
            {
                if (batchlist.FindAll(c => c.BatchName == ddlClassName.SelectedItem.Text + ddlSession.SelectedItem.Text).Count > 0)
                {
                    lblMessage.InnerText = "warning->This Batch Already Created";
                    loadBatch("");
                    return;
                }
            }
            if (saveBatchInfo())
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "success();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "fail();", true);
            }
        }
        private Boolean saveBatchInfo()
        {
            try
            {
                using (BatchEntities entities = GetFormData())
                {
                    if (batchEntry == null)
                    {
                        batchEntry = new BatchEntry();
                    }
                    batchEntry.AddEntities = entities;
                    bool result=batchEntry.Insert();
                    loadBatch("");
                    if (result == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }            
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private BatchEntities GetFormData()
        {
            BatchEntities batchEntities = new BatchEntities();
            batchEntities.BatchId = int.Parse(lblbatchId.Value);
            batchEntities.BatchName = ddlClassName.SelectedItem.Text + ddlSession.SelectedItem.Text;
            if (chkIsUse.Checked)
            {
                batchEntities.IsUsed = true;
            }
            else
            {
                batchEntities.IsUsed = false;
            }
            batchEntities.Year = int.Parse(ddlSession.SelectedItem.Text);
            batchEntities.ClassId = int.Parse(ddlClassName.SelectedValue);
            return batchEntities;
        }
        
    }
}