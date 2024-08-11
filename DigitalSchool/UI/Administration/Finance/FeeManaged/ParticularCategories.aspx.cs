using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ManagedBatch;
using DS.BLL.ControlPanel;
using DS.DAL;
using DS.BLL.ManagedClass;
namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class ParticularCategories : System.Web.UI.Page
    {
        string sql = string.Empty;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;          
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ParticularCategories.aspx", btnSave, btnEdit, "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    stdtypeEntry.GetEntitiesData(ddlStudentType);
                    stdtypeEntry.GetEntitiesData(ddlStudentTypeFilter);
                    BatchEntry.GetDropdownlist(dlBatch, "True");
                    BatchEntry.GetDropdownlist(dlBatchName, "True");
                    BatchEntry.GetDropdownlist(dlSearchBatch, "True");
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
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(dlBatch.SelectedValue != "0")
                {
                    LoadBatchwiseFeeCat(dlBatch.SelectedValue,dlCategory);                   
                    AddColumns();
                }
            }
            catch { }
        }
        private void LoadBatchwiseFeeCat(string batchId,DropDownList dl)
        {
            dt = new DataTable();
            if (ckbIsOpenPayemnt.Checked)
            {
                sqlDB.fillDataTable("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE PaymentFor='openPayment'  order by FeeCatId ASC", dt);               

            }
            else
            {
                string[] batchclsID = batchId.Split('_');
               
                sqlDB.fillDataTable("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId = '" + batchclsID[0] + "'  order by FeeCatId ASC", dt);
            }
            
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
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
                sqlDB.fillDataTable("Select FeeCatId, PId from ParticularsCategory where FeeCatId=" + dlCategory.SelectedValue + " and PId=" + dlParticular.SelectedValue + " and StdTypeId='"+ddlStudentType.SelectedValue+"' ", dt);
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

                sqlCmd = "Select PName, Amount from v_FeesCatDetails where FeeCatId='" + dlFilter.SelectedValue + "'  ";

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
                    loadParticularDetails("");
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete From ParticularsCategory where FeeCatId=" + dlCategory.SelectedValue + " ", DbConnection.Connection);
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (saveParticularsCategory() == true)
                    {
                        ViewState["__tableInfo__"] = null;
                        AddColumns();                       
                        loadParticularDetails("");
                        lblMessage.InnerText = "success->Successfully Updated";
                    }
                }
            }
            DataTable dtAmount = new DataTable();
            sqlDB.fillDataTable("Select sum(Amount) as Amount From ParticularsCategory where FeeCatId=" + dlCategory.SelectedValue + "", dtAmount);
            if (dtAmount.Rows.Count == 0) return;
            else
            {
                SqlCommand cmd = new SqlCommand("Update FeesCategoryInfo set FeeAmount=" + dtAmount.Rows[0]["Amount"].ToString() + " where FeeCatId=" + dlCategory.SelectedValue + " ", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            btnSave.Text = "Save";
        }
        private Boolean saveParticularsCategory()
        {
            try
            {
                SqlCommand cmd;
                DataTable dt = (DataTable)ViewState["__tableInfo__"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into  ParticularsCategory (FeeCatId, PId, Amount,StdTypeId)  values (@FeeCatId, @PId, @Amount,@StdTypeId) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", dlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@PId", dt.Rows[i]["PId"].ToString());
                    cmd.Parameters.AddWithValue("@Amount", dt.Rows[i]["Amount"].ToString());
                    cmd.Parameters.AddWithValue("@StdTypeId",ddlStudentType.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
                string[] batchid = dlBatch.SelectedValue.ToString().Split('_'); ;
                cmd = new SqlCommand("Update StudentPayment set StdTypeId='" + ddlStudentType.SelectedValue + "' where StudentId in (SELECT StudentId FROM CurrentStudentInfo where BatchId='" + batchid[0] + "') and FeeCatId='" + dlCategory.SelectedValue + "' and BatchId='" + batchid[0] + "' ", DbConnection.Connection);
                cmd.ExecuteNonQuery();
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
            sqlDB.fillDataTable("Select PId,PName,Amount From v_FeesCatDetails where FeeCatId=" + dlFilter.SelectedValue + " and StdTypeId='"+ddlStudentTypeFilter.SelectedValue+"'", dt);
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning->No Particulars Found";
                return;
            }
            btnSave.Text = "Update";
            btnSave.Enabled = true;
            ViewState["__tableInfo__"] = dt;
            gvParticulars.DataSource = dt;
            gvParticulars.Columns[0].Visible = true;
            gvParticulars.DataBind();
            dlBatch.SelectedValue = dlSearchBatch.SelectedValue;
            ddlStudentType.SelectedValue = ddlStudentTypeFilter.SelectedValue;
            LoadBatchwiseFeeCat(dlBatch.SelectedValue, dlCategory); 
            string FessCat = dlFilter.SelectedItem.Text;
            for (int i = 0; i < dlCategory.Items.Count; i++)
            {
                if (dlCategory.Items[i].Text == FessCat)
                {
                    dlCategory.SelectedIndex = i;
                }
            }
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
            if (dlBatch.SelectedValue != "0")
            {
                clearAddFeesCat();
                dlBatchName.SelectedValue = dlBatch.SelectedValue;                
                ShowFeeCatModal.Show();
            }            
        }
        private void clearAddFeesCat()
        {
            dlBatchName.SelectedValue = "0";
            txtFeesCatName.Text = string.Empty;
            txtDateStart.Text = string.Empty;
            txtDateEnd.Text = string.Empty;
            txtFeesFine.Text = "0";
        }
        protected void btnFeeCatSave_Click(object sender, EventArgs e)
        {
            if(saveFeesCategoryInfo())
            {
                try
                {
                    if (dlBatch.SelectedValue != "0")
                    {
                        string[] batchID = dlBatch.SelectedValue.Split('_');
                        dt = new DataTable();
                        sqlDB.fillDataTable("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId = '" + batchID[0] + "'  order by FeeCatId ASC", dt);
                        dlCategory.DataSource = dt;
                        dlCategory.DataTextField = "FeeCatName";
                        dlCategory.DataValueField = "FeeCatId";
                        dlCategory.DataBind();
                        dlCategory.Items.Add(new ListItem("...Select Fees Category...", "0"));
                        dlCategory.SelectedItem.Text = txtFeesCatName.Text;
                    }
                }
                catch { }
            }
        }
        private Boolean saveFeesCategoryInfo()
        {
            int result = 0;            
            try
            {
                string[] batchClsID = dlBatch.SelectedValue.Split('_');
                SqlCommand cmd = new SqlCommand("Insert into FeesCategoryInfo( BatchId, DateOfCreation, FeeFine, FeeCatName) values " +
                                                "(@BatchId, @DateOfCreation, @FeeFine, @FeeCatName); SELECT SCOPE_IDENTITY();", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@BatchId", batchClsID[0]);
                cmd.Parameters.AddWithValue("@DateOfCreation", DateTime.Parse(System.DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@FeeFine", txtFeesFine.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeCatName", txtFeesCatName.Text.Trim());
                int FeeCatId = Convert.ToInt32(cmd.ExecuteScalar());
                if (FeeCatId > 0)
                {
                    cmd = new SqlCommand("Insert into  DateOfPayment(FeeCatId, DateOfStart, DateOfEnd,IsActive)  values (@FeeCatId, @DateOfStart, "
                    + "@DateOfEnd,@IsActive) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", FeeCatId.ToString());
                    cmd.Parameters.AddWithValue("@DateOfStart", DateTime.Parse(txtDateStart.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DateOfEnd", DateTime.Parse(txtDateEnd.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@IsActive", "True");
                    result = (int)cmd.ExecuteNonQuery();
                }
                if (result > 0)
                {
                    saveStudentPayment(FeeCatId.ToString(), dlBatch.SelectedValue);                                      
                }
                else lblMessage.InnerText = "error->Unable to save";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;                
                return false;
            }
        }
        private Boolean saveStudentPayment(string FeeCatId, string batchID)
        {
            try
            {
                string[] batchClsID = batchID.Split('_');
                DataTable dtst = new DataTable();
                sqlDB.fillDataTable("Select StudentId,ConfigId,ClsGrpID,ClsSecID,RollNo from CurrentStudentInfo where BatchId='" + batchClsID[0] + "' and StdTypeId='"+ddlStudentType.SelectedValue+"'", dtst);

                for (int i = 0; i < dtst.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("saveStudentPayment", DbConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", dtst.Rows[i]["StudentId"].ToString());
                    cmd.Parameters.AddWithValue("@ShiftID", dtst.Rows[i]["ConfigId"].ToString());
                    cmd.Parameters.AddWithValue("@BatchId", batchClsID[0]);
                    cmd.Parameters.AddWithValue("@ClassID", batchClsID[1]);
                    cmd.Parameters.AddWithValue("@ClsGrpID", dtst.Rows[i]["ClsGrpID"].ToString());
                    cmd.Parameters.AddWithValue("@ClsSecID", dtst.Rows[i]["ClsSecID"].ToString());
                    cmd.Parameters.AddWithValue("@RollNo", dtst.Rows[i]["RollNo"].ToString());
                    cmd.Parameters.AddWithValue("@FeeCatId", FeeCatId);
                    cmd.Parameters.AddWithValue("@DateOfPayment", DateTime.Parse(txtDateStart.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@PayStatus", "0");
                    cmd.Parameters.AddWithValue("@AmountPaid", "0");
                    cmd.Parameters.AddWithValue("@FineAmount", "0");
                    cmd.Parameters.AddWithValue("@DiscountStatus", "0");
                    cmd.Parameters.AddWithValue("@DiscountTK", "0");
                    cmd.Parameters.AddWithValue("@GrandTotal", "0");
                    cmd.Parameters.AddWithValue("@DueAmount", "0");
                    cmd.Parameters.AddWithValue("@StdTypeId","0");

                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                    else lblMessage.InnerText = "error->Unable to save";
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        protected void btnAddParticular_Click(object sender, EventArgs e)
        {
            txtFeesType.Text = string.Empty;
            showAddParticularModal.Show();
        }       
        protected void btnAddParticularSave_Click(object sender, EventArgs e)
        {
            if(txtFeesType.Text != string.Empty)
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
                    lblMessage.InnerText = "success->Successfully saved";
                }
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;               
                return false;
            }
        }

        protected void dlSearchBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBatchwiseFeeCat(dlSearchBatch.SelectedValue,dlFilter);  
        }
        

        protected void ckbIsOpenPayemnt_CheckedChanged(object sender, EventArgs e)
        {
            checkIsOpenPayemnt();
        }
        void checkIsOpenPayemnt()
        {
            if (ckbIsOpenPayemnt.Checked)
            {
                pnlAcademicInfo.Visible = false;                
                pnlAcademicInfo1.Visible = false;                
            }
            else
            {
                pnlAcademicInfo.Visible = true;                
                pnlAcademicInfo1.Visible = true;                
            }
            LoadBatchwiseFeeCat(dlBatch.SelectedValue, dlCategory);
            LoadBatchwiseFeeCat(dlSearchBatch.SelectedValue, dlFilter);
        }
        // End Development by Md. Arafat Rahman Rana Date:08-03-2015 before
        //End Development by Md. Suman Miah Date:08-03-2015
    }
}