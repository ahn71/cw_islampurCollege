using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.Finance;
using DS.BLL.Finance;
using DS.BLL.ControlPanel;
using ComplexScriptingSystem;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class AdmissionFeesCategories : System.Web.UI.Page
    {
        string sqlCmd = string.Empty;
        AdmFeesCategoresEntry AdmFeesEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AdmissionFeesCategories.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    ClassEntry.GetEntitiesData(ddlClass);
                    loadFeesCategoryInfo("0");
                }
        }
        private void loadFeesCategoryInfo(string classId)
        {
            try
            {
                divFeesCategoryList.Controls.Add(new LiteralControl(""));
                string divInfo = string.Empty;
                if (AdmFeesEntry == null)
                {
                    AdmFeesEntry = new AdmFeesCategoresEntry();
                }
                IList<AdmFeesCategoriesEntities> FessCat = AdmFeesEntry.GetEntitiesData(classId);

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                divInfo = " <table id='tblParticularCategory' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Class Name</th>";
                divInfo += "<th>Fee CatName</th>";
                divInfo += "<th>Start Date</th>";
                divInfo += "<th>End Date</th>";                
                divInfo += "<th>Creation Date</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (FessCat == null)
                {
                    divInfo += "<tbody></tbody></table>";
                    divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo += "<tbody>";
                string id = "";
                string clsId = "";
                for (int x = 0; x < FessCat.Count; x++)
                {
                    clsId = FessCat[x].ClassID.ToString();
                    id = FessCat[x].AdmFeeCatId.ToString();
                    divInfo += "<tr id='r_" + id + "'>";                  
                    divInfo += "<td >" + FessCat[x].ClassName.ToString() + "</td>";
                    divInfo += "<td >" + FessCat[x].FeeCatName.ToString() + "</td>";
                    divInfo += "<td >" + FessCat[x].DateOfStart.ToString("dd-MM-yyyy") + "</td>";
                    divInfo += "<td >" + FessCat[x].DateOfEnd.ToString("dd-MM-yyyy") + "</td>";
                    divInfo += "<td >" + FessCat[x].DateOfCreation.ToString("dd-MM-yyyy") + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" +
                        "<img src='/Images/gridImages/edit.png'  onclick='editFeesCategory(" + id + ","+clsId+");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (lblFeesCateId.Value == "0")
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadFeesCategoryInfo("0"); return; }
                saveFeesCategoryInfo("Save");
            }
            else saveFeesCategoryInfo("Update");
        }
        private Boolean saveFeesCategoryInfo(string Save)
        {
            try
            {
                using ( AdmFeesCategoriesEntities AdmFeesE= GetFormData())
                {
                    bool result = true;
                    if (AdmFeesEntry == null)
                    {
                        AdmFeesEntry = new AdmFeesCategoresEntry();
                    }
                    AdmFeesEntry.AddEntities = AdmFeesE;
                    if (Save.Equals("Save"))
                    {
                        result = AdmFeesEntry.Insert();
                        lblMessage.InnerText = "success->Save Successfully";
                    }
                    else
                    {
                        result = AdmFeesEntry.Update();
                        lblMessage.InnerText = "success->Update Successfully";
                    }
                    lblFeesCateId.Value = string.Empty;
                    loadFeesCategoryInfo(ddlClass.SelectedValue);
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    txtFeesCatName.Text = string.Empty;
                    txtDateStart.Text = string.Empty;
                    txtDateEnd.Text = string.Empty;                                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private AdmFeesCategoriesEntities GetFormData()
        {
            AdmFeesCategoriesEntities AdmFeesEntry = new AdmFeesCategoriesEntities();
            AdmFeesEntry.AdmFeeCatId = int.Parse(lblFeesCateId.Value);
            AdmFeesEntry.FeeCatName = txtFeesCatName.Text.Trim();
            AdmFeesEntry.ClassID = int.Parse(ddlClass.SelectedValue);
            AdmFeesEntry.DateOfCreation =convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy"));
            AdmFeesEntry.DateOfStart = convertDateTime.getCertainCulture(txtDateStart.Text);
            AdmFeesEntry.DateOfEnd = convertDateTime.getCertainCulture(txtDateEnd.Text);
            AdmFeesEntry.IsActive = true;
            return AdmFeesEntry;
        }
        

        private void Clear()
        {
            txtFeesCatName.Text = string.Empty;
            txtDateStart.Text = string.Empty;
            txtDateEnd.Text = string.Empty;
            ddlClass.SelectedValue = "0";
        }     

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                if (ddlClass.SelectedValue != "0")
                    loadFeesCategoryInfo(ddlClass.SelectedValue);
                else
                    loadFeesCategoryInfo("0");
            }
            catch { }
        }
    }
}