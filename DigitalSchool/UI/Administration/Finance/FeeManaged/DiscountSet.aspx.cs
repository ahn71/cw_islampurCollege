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
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class DiscountSet : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "DiscountSet.aspx",btnSave,btnEdit,"")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        BatchEntry.GetDropdownlist(ddlBatchName,"True");
                        ShiftEntry.GetDropDownList(dlShift);
                        BatchEntry.GetDropdownlist(ddlBatchlist, "True");
                        ShiftEntry.GetDropDownList(ddlShiftList);
                        LoadPatriculars(ddlParticulars);
                        AddColumns();                       
                        LoadDiscountlist("");
                        if (btnSave.Text == "Update") btnSave.Enabled = false;
                        //dgvCreatePackage.FirstDisplayedScrollingRowIndex = dgvCreatePackage.RowCount - 1;  // for scrolling on under
                    }
                }
            catch { }
        }
        private void LoadPatriculars(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select PName,PId From ParticularsInfo order by PName desc ", dt);
                dl.DataSource = dt;
                dl.DataTextField = "PName";
                dl.DataValueField = "PId";
                dl.DataBind();
                dl.Items.Insert(0,new ListItem("...Select...","0"));                
            }
            catch { }

        }
        protected void ddlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = ddlBatchName.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = ddlBatchName.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = ddlBatchName.SelectedValue.Split('_');
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            currentstdEntry.GetRollNo(ddlRoll, dlShift.SelectedValue, BatchClsID[0], ddlGroup.SelectedValue, ddlSection.SelectedValue);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);

            lblMessage.InnerText = "";
            if (btnAdd.Text == "Add")
                AddGridview();
            else Reomvefromgrid();
            LoadDiscountlist("");
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }

        }

        private void AddGridview()
        {
            try
            {

                if (txtDiscountAmount.Text.Length == 0)
                {
                    lblMessage.InnerText = "warning->Type Discount";
                    txtDiscountAmount.Focus();
                    return;
                }
                DataTable dt = (DataTable)ViewState["__tableInfo__"];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (ddlParticulars.SelectedValue == dt.Rows[i]["PId"].ToString())
                        {
                            lblMessage.InnerText = "warning->Already add ";
                            ddlParticulars.Focus();
                            return;
                        }
                    }
                }
                string[] batchID = ddlBatchName.SelectedValue.Split('_');
                dt = new DataTable();
                sqlDB.fillDataTable("Select PId from Discount where ShiftID='"+dlShift.SelectedValue+"' AND PId="
                 + ddlParticulars.SelectedValue + " and BatchId='" + batchID[0] + "' AND ClsGrpID='"+ddlGroup.SelectedValue
                 + "' and ClsSecID='" + ddlSection.SelectedValue + "' and StudentId=" + ddlRoll.SelectedValue + " ", dt);
                if (dt.Rows.Count > 0)
                {
                    if (ddlParticulars.SelectedValue == dt.Rows[0]["PId"].ToString())
                    {
                        lblMessage.InnerText = "warning->Already Inserted ";
                        ddlParticulars.Focus();
                        return;
                    }
                }
                addRowsAndColumns();
                txtDiscountAmount.Text = "";
            }
            catch { }
        }

        private void addRowsAndColumns()
        {
            try
            {
                string[] value = new string[3];
                value[0] = ddlParticulars.SelectedValue;
                value[1] = ddlParticulars.SelectedItem.Text;
                value[2] = txtDiscountAmount.Text;

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
                    dt = new DataTable();
                    dt.Columns.Add("PId");
                    dt.Columns.Add("PName");
                    dt.Columns.Add("Discount");
                }

                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;

                gvDiscount.DataSource = dt;
                gvDiscount.Columns[0].Visible = true;
                gvDiscount.DataBind();
            }
            catch { }
        }

        private void addRowsAndColumnforFirstTimeShow()
        {
            try
            {
                string[] value = new string[1];
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)ViewState["__tableInf__"];
                    if (dt == null) dt = new DataTable();
                }
                catch { }

                if (dt.Columns.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Particulars Name");
                    dt.Columns.Add("Amount");
                }
                dt.Rows.Add(value);
                ViewState["__tableInf__"] = dt;
                gvDiscount.DataSource = dt;
                gvDiscount.DataBind();
            }
            catch { }
        }

        private void AddColumns()
        {
            try
            {
                gvDiscount.DataSource = new object[] { null };
                gvDiscount.Columns[0].Visible = false;

                gvDiscount.RowStyle.HorizontalAlign = HorizontalAlign.Center;

                gvDiscount.DataBind();

            }
            catch { }
        }

        private void Reomvefromgrid()
        {
            try
            {
                CheckBox chk = new CheckBox();
                DataTable dt = (DataTable)ViewState["__tableInfo__"];
                for (int x = 0; x < gvDiscount.Rows.Count; x++)
                {
                    chk = (CheckBox)gvDiscount.Rows[x].Cells[0].FindControl("chkRow");
                    if (chk.Checked == true)
                    {
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                dt.Rows.Remove(dt.Rows[x]);
                            }
                        }
                        catch { }
                        ViewState["__tableInfo__"] = dt;
                    }
                }

                gvDiscount.DataSource = (DataTable)ViewState["__tableInfo__"];
                gvDiscount.DataBind();
                if (gvDiscount.Rows.Count == 0)
                {
                    AddColumns();
                }
                btnAdd.Text = "Add";
            }
            catch { }
        }

        private Boolean saveDiscount()
        {
            try
            {
                DataTable vdt = (DataTable)ViewState["__tableInfo__"];
                if (vdt == null)
                {
                    lblMessage.InnerText = "warning->Please Add Particulars";
                    return false;
                }
                int result = 0;
                string[] batchID = ddlBatchName.SelectedValue.Split('_');
                for (int i = 0; i < vdt.Rows.Count; i++)
                {

                    SqlCommand cmd = new SqlCommand("Insert into  Discount (StudentId,ShiftID,BatchId,ClsGrpID,ClsSecID,RollNo, PId, Discount)  values (@StudentId, @ShiftID, @BatchId, @ClsGrpID,@ClsSecID,@RollNo, @PId, @Discount) ", DbConnection.Connection);
                        cmd.Parameters.AddWithValue("@StudentId", ddlRoll.SelectedValue);
                        cmd.Parameters.AddWithValue("@ShiftID", dlShift.SelectedValue);
                        cmd.Parameters.AddWithValue("@BatchId", batchID[0]);
                        cmd.Parameters.AddWithValue("@ClsGrpID", ddlGroup.SelectedValue);
                        cmd.Parameters.AddWithValue("@ClsSecID",ddlSection.SelectedValue );
                        cmd.Parameters.AddWithValue("@RollNo", Int32.Parse(ddlRoll.SelectedValue));
                        cmd.Parameters.AddWithValue("@PId",int.Parse(vdt.Rows[i]["PId"].ToString()));
                        cmd.Parameters.AddWithValue("@Discount", double.Parse(vdt.Rows[i]["Discount"].ToString()));                       
                        result = (int)cmd.ExecuteNonQuery();           

                }

                if (result > 0)
                {
                    Allclear();                    
                    LoadDiscountlist("");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] batchID = ddlBatchName.SelectedValue.Split('_');
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
                LoadDiscountlist("");
            }
            lblMessage.InnerText = "";
            if (btnSave.Text == "Save")
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; return; }
                if (saveDiscount() == true)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete From Discount where StudentId='"
                    + ddlRoll.SelectedValue + "' AND ShiftID='" + dlShift.SelectedValue + "' AND BatchId='"
                    + batchID[0] + "' and ClsGrpID='" + ddlGroup.SelectedValue + "' AND ClsSecID='"
                    + ddlSection.SelectedValue + "'", DbConnection.Connection);
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (saveDiscount() == true)
                    {
                        lblMessage.InnerText = "success->Successfully Updated";
                    }
                }

            }

        }
        private void updateDiscount()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select PId From ParticularsInfo where PName='" + ddlParticulars.Text + "'", dt);

                SqlCommand cmd = new SqlCommand("Update Discount set Discount=@Discount where BatchName='" + ddlBatchName.Text + "' and SectionName='" + ddlSection.Text + "' and RollNo=" + ddlRoll.Text + " and PId=" + dt.Rows[0]["PId"].ToString() + "", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@Discount", txtDiscountAmount.Text.Trim());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    LoadDiscountlist("");
                    lblMessage.InnerText = "success->Successfully updated";
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
       
        private void loadblank()
        {
            string divInfo = "";
            divInfo = "<div class='noData'>No Discount List available</div>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
            divDiscountList.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadDiscountlist(string sqlcmd)
        {
            try
            {
                string[] batchID = ddlBatchlist.SelectedValue.Split('_');
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select PName,Discount from V_Discount where StudentId='" 
                    + ddlRolllist.SelectedValue + "' AND ShiftID='" + ddlShiftList.SelectedValue + "' AND BatchId='"
                    + batchID[0] + "' and ClsGrpID='" + ddlGroupList.SelectedValue + "' AND ClsSecID='" 
                    + ddlSectionlist.SelectedValue + "'";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                divDiscountList.Controls.Clear();
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Discount List available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divDiscountList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblBatch' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Particulars Name</th>";
                divInfo += "<th>Discount (%)</th>";                
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                int id = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id++;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";

                    divInfo += "<td >" + dt.Rows[x]["Discount"].ToString() + "</td>";                    

                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divDiscountList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void ddlBatchlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                string[] BatchClsID = ddlBatchlist.SelectedValue.Split('_');
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroupList);
                ClassSectionEntry.GetEntitiesData(ddlSectionlist, int.Parse(BatchClsID[1]), ddlGroupList.SelectedValue);
            }
            catch { }
        }

        protected void ddlSectionlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                string[] BatchClsID = ddlBatchlist.SelectedValue.Split('_');
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                currentstdEntry.GetRollNo(ddlRolllist, ddlShiftList.SelectedValue,
                    BatchClsID[0], ddlGroupList.SelectedValue, ddlSectionlist.SelectedValue);
            }
            catch { }
        }
        protected void ddlParticulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            txtDiscountAmount.Focus();
            LoadDiscountlist("");
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            Allclear();
        }
        private void Allclear()
        {
            lblMessage.InnerText = "";
            btnAdd.Text = "Add";
            btnSave.Text = "Save";
            txtDiscountAmount.Text = "";
            lblid.Value = "";
            ViewState["__tableInfo__"] = null;
            AddColumns();

        }    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                LoadDiscountlist("");
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {                
                string sqlcmd = "";
                string[] batchID = ddlBatchlist.SelectedValue.Split('_');
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select PId, PName,Discount from V_Discount where StudentId='"
                    + ddlRolllist.SelectedValue + "' AND ShiftID='" + ddlShiftList.SelectedValue + "' AND BatchId='"
                    + batchID[0] + "' and ClsGrpID='" + ddlGroupList.SelectedValue + "' AND ClsSecID='"
                    + ddlSectionlist.SelectedValue + "'";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd,dt);
                if (dt.Rows.Count == 0) return;
                ViewState["__tableInfo__"] = dt;
                gvDiscount.DataSource = dt;
                gvDiscount.Columns[0].Visible = true;
                gvDiscount.DataBind();
                LoadDiscountlist("");
                btnSave.Text = "Update";
                btnSave.Enabled = true;
                ddlBatchName.SelectedValue = ddlBatchlist.SelectedValue;
                dlShift.SelectedValue = ddlShiftList.SelectedValue;
                string[] BatchClsID = ddlBatchName.SelectedValue.Split('_');
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
                ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
                if (ddlGroup.Enabled == true)
                {
                    ddlGroup.SelectedValue = ddlGroupList.SelectedValue;
                    ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
                    ddlSection.SelectedValue = ddlSectionlist.SelectedValue;
                }
                else
                {
                    ddlSection.SelectedValue = ddlSectionlist.SelectedValue;
                }               
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                currentstdEntry.GetRollNo(ddlRoll,
                    dlShift.SelectedValue, BatchClsID[0], ddlGroup.SelectedValue, ddlSection.SelectedValue);
                ddlRoll.SelectedValue = ddlRolllist.SelectedValue;
            }
            catch { }

        }

        protected void gvDiscount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                LoadDiscountlist("");
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
                    gvDiscount.DataSource = dt;
                    gvDiscount.DataBind();
                    if (gvDiscount.Rows.Count == 0)
                    {
                        ViewState["__tableInfo__"] = null;

                        AddColumns();
                    }
                }
            }
            catch { }
        }

        private void loadBatchwiseDiscount(string sqlcmd)
        {
            try
            {
                //if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select PName,Discount from V_Discount where BatchName='" + ddlBatchlist.Text + "' and SectionName='" + ddlSectionlist.Text + "' and RollNo=" + ddlRolllist.Text + " ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                divDiscountList.Controls.Clear();
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Discount List available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divDiscountList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblBatch' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Student Name</th>";
                divInfo += "<th>Roll</th>";
                divInfo += "<th>Section</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                int id = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = x + 1; ;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td ><a href='#' id='" + dt.Rows[x]["RollNo"].ToString() + "'  onclick='loadDiscount(" + id + ");'> " + dt.Rows[x]["FullName"].ToString() + "</a></td>";

                    divInfo += "<td ><span id=roll" + id + ">" + dt.Rows[x]["RollNo"].ToString() + "</span></td>";
                    divInfo += "<td ><span id=section" + id + ">" + dt.Rows[x]["SectionName"].ToString() + "</span></td>";

                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divDiscountList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            LoadDiscountlist("");
        }

        protected void ddlGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = ddlBatchlist.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(ddlSectionlist, int.Parse(BatchClsID[1]), ddlGroupList.SelectedValue);
        }          
    }
}