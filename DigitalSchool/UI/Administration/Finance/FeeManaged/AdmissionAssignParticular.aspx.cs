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
using DS.BLL.Finance;
using DS.PropertyEntities.Model.Finance;
using DS.BLL.ControlPanel;
using ComplexScriptingSystem;
using DS.DAL;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class AdmissionAssignParticular : System.Web.UI.Page
    {
        string sql = string.Empty;
        DataTable dt;
        AdmFeesCategoresEntry AdmFeesEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;            
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AdmissionAssignParticular.aspx", btnSave, btnEdit, "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    ClassEntry.GetEntitiesData(ddlClass);
                    ClassEntry.GetEntitiesData(dlClassFilter);
                    ClassEntry.GetEntitiesData(ddlClassName);  
                    Classes.commonTask.loadParticularInfo(dlParticular);                   
                    AddColumns();
                    if (btnSave.Text == "Update") btnSave.Enabled = false;
                }
        }      
        private void AddColumns()
        {
            try
            {
                gvParticulars.DataSource = new object[] { null };
                gvParticulars.Columns[0].Visible = false;
                gvParticulars.RowStyle.HorizontalAlign = HorizontalAlign.Center;
                gvParticulars.DataBind();
            }
            catch { }
        }       
        protected void dlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                loadParticularDetails("");
            }
            catch { }
        }       
        private void AddGridview()
        {
            try
            {
                lblMessage.InnerText = "";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select FeeCatId, PId from ParticularsCategory where FeeCatId=" + dlCategory.SelectedValue + " and PId=" + dlParticular.SelectedValue + " ", dt);
                if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning-> Already add this particulars";
                    return;
                }
                if (txtAmount.Text.Length == 0)
                {
                    lblMessage.InnerText = "warning->Add Amount";
                    txtAmount.Focus();
                    return;
                }
                DataTable dt2 = (DataTable)ViewState["__tableInfo__"];
                if (dt2 != null)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dlParticular.SelectedItem.Text == dt2.Rows[i]["PName"].ToString())
                        {
                            lblMessage.InnerText = "warning->Already add this particulars";
                            return;
                        }
                    }
                }
                addRowsAndColumns();
            }
            catch { }
        }
        private void addRowsAndColumns()
        {
            try
            {
                string[] value = new string[3];
                value[0] = dlParticular.SelectedValue;
                value[1] = dlParticular.SelectedItem.Text;
                value[2] = txtAmount.Text.Trim();
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)ViewState["__tableInfo__"];
                    if (dt == null) dt = new DataTable();
                }
                catch { }
                if (dt.Columns.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("PId");
                    dt.Columns.Add("PName");
                    dt.Columns.Add("Amount");
                }
                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;
                gvParticulars.DataSource = dt;
                gvParticulars.Columns[0].Visible = true;
                gvParticulars.DataBind();
            }
            catch { }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
            AddGridview();
            txtAmount.Text = "";
        }
        private void loadParticularDetails(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select PName, Amount from v_Adm_FeesCatDetails where AdmFeeCatId='" + dlFilter.SelectedValue + "' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric'>Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                int id = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = x + 1;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Amount"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void gvParticulars_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            loadParticularDetails("");
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (dt == null) return;
                List<DataRow> rowsToDelete = new List<DataRow>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        DataRow row = dt.Rows[i];
                        rowsToDelete.Add(row);
                    }
                }
                //Deleting the rows 
                foreach (DataRow row in rowsToDelete)
                {
                    dt.Rows.Remove(row);
                }
                dt.AcceptChanges();
                ViewState["__tableInfo__"] = dt;
                gvParticulars.DataSource = dt;
                gvParticulars.DataBind();
                if (gvParticulars.Rows.Count == 0)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                lblMessage.InnerText = "warning->Please Add Particulars";
                return;
            }
            if (btnSave.Text == "Save")
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; return; }
                if (saveParticularsCategory() == true)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                    AdmFeesCategoresEntry.GetDropDownList(dlFilter, dlClassFilter.SelectedValue);
                    loadParticularDetails("");
                    lblMessage.InnerText = "success->Save Successfully ";
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete From Tbl_Adm_ParticularsCategory where AdmFeeCatId=" + dlCategory.SelectedValue + " ", DbConnection.Connection);
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (saveParticularsCategory() == true)
                    {
                        ViewState["__tableInfo__"] = null;
                        AddColumns();                        
                        loadParticularDetails("");
                        lblMessage.InnerText = "success->Update Successfully";
                    }
                }
            }
            DataTable dtAmount = new DataTable();
            sqlDB.fillDataTable("Select sum(Amount) as Amount From Tbl_Adm_ParticularsCategory where AdmFeeCatId=" + dlCategory.SelectedValue + "", dtAmount);
            if (dtAmount.Rows.Count == 0) return;
            else
            {
                SqlCommand cmd = new SqlCommand("Update Tbl_Adm_FeesCategory set FeeAmount=" + dtAmount.Rows[0]["Amount"].ToString() + " where AdmFeeCatId=" + dlCategory.SelectedValue + " ", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
        }
        private Boolean saveParticularsCategory()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["__tableInfo__"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("Insert into  Tbl_Adm_ParticularsCategory (AdmFeeCatId, PId, Amount)  values (@AdmFeeCatId, @PId, @Amount) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@AdmFeeCatId", dlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@PId", dt.Rows[i]["PId"].ToString());
                    cmd.Parameters.AddWithValue("@Amount", dt.Rows[i]["Amount"].ToString());
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {           
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select PId,PName,Amount From v_Adm_FeesCatDetails where AdmFeeCatId=" + dlFilter.SelectedValue + "", dt);
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning->No Particulars Found";
                AddColumns();
                return;
            }
            btnSave.Text = "Update";
            btnSave.Enabled = true;
            ViewState["__tableInfo__"] = dt;
            gvParticulars.DataSource = dt;
            gvParticulars.Columns[0].Visible = true;
            gvParticulars.DataBind();
            ddlClass.SelectedValue = dlClassFilter.SelectedValue;
            AdmFeesCategoresEntry.GetDropDownList(dlCategory, ddlClass.SelectedValue);
            dlCategory.SelectedValue = dlFilter.SelectedValue;
            
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ViewState["__tableInfo__"] = null;
            btnSave.Text = "Save";
            AddColumns();
            txtAmount.Text = "";
            lblMessage.InnerText = "";
        }

        // Start Development by Md. Arafat Rahman Rana
        protected void btnAddFeesCat_Click(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "0")
            {
                clearAddFeesCat();
                ddlClassName.SelectedValue = ddlClass.SelectedValue;
                ShowFeeCatModal.Show();
            }
        }
        private void clearAddFeesCat()
        {
            ddlClassName.SelectedValue = "0";
            txtFeesCatName.Text = string.Empty;
            txtDateStart.Text = string.Empty;
            txtDateEnd.Text = string.Empty;
            txtFeesFine.Text = "0";
        }
        protected void btnFeeCatSave_Click(object sender, EventArgs e)
        {
            if (saveFeesCategoryInfo())
            {
                AdmFeesCategoresEntry.GetDropDownList(dlCategory, ddlClass.SelectedValue);
            }
        }
        private Boolean saveFeesCategoryInfo()
        {
            using (AdmFeesCategoriesEntities AdmFeesE = GetFormData())
            {
                bool result = true;
                if (AdmFeesEntry == null)
                {
                    AdmFeesEntry = new AdmFeesCategoresEntry();
                }
                AdmFeesEntry.AddEntities = AdmFeesE;
               
                    result = AdmFeesEntry.Insert();
                    if (result == true)
                    {
                        lblMessage.InnerText = "success->Save Successfully";

                        return true;
                    }
                    else
                    {
                      return  false;
                    }
            }
        }
        private AdmFeesCategoriesEntities GetFormData()
        {
            AdmFeesCategoriesEntities AdmFeesEntry = new AdmFeesCategoriesEntities();            
            AdmFeesEntry.FeeCatName = txtFeesCatName.Text.Trim();
            AdmFeesEntry.ClassID = int.Parse(ddlClassName.SelectedValue);
            AdmFeesEntry.DateOfCreation = DateTime.Now;
            AdmFeesEntry.DateOfStart = convertDateTime.getCertainCulture(txtDateStart.Text);
            AdmFeesEntry.DateOfEnd = convertDateTime.getCertainCulture(txtDateEnd.Text);
            AdmFeesEntry.IsActive = true;
            return AdmFeesEntry;
        }      
        protected void btnAddParticular_Click(object sender, EventArgs e)
        {
            txtFeesType.Text = string.Empty;
            showAddParticularModal.Show();
        }
        protected void btnAddParticularSave_Click(object sender, EventArgs e)
        {
            if (txtFeesType.Text != string.Empty)
            {
                saveFeesType();
                Classes.commonTask.loadParticularInfo(dlParticular);
            }
        }
        private Boolean saveFeesType()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into  ParticularsInfo   values (@PName) ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@PName", txtFeesType.Text.Trim());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Save Successfully";
                }
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdmFeesCategoresEntry.GetDropDownList(dlCategory,ddlClass.SelectedValue);
        }

        protected void dlClassFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdmFeesCategoresEntry.GetDropDownList(dlFilter, dlClassFilter.SelectedValue);
        }
    }
}