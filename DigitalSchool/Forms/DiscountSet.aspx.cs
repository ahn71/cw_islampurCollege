using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace DS.Forms
{
    public partial class DiscountSet : System.Web.UI.Page
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
                    lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(ddlBatchName);
                        loadSection(ddlSection);
                        LoadPatriculars(ddlParticulars);
                        AddColumns();
                        LoadBatchlist(ddlBatchlist);
                        LoadDiscountlist("");
                        //dgvCreatePackage.FirstDisplayedScrollingRowIndex = dgvCreatePackage.RowCount - 1;  // for scrolling on under
                    }
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
                dl.Items.Add("...Select Particulars...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }

        }
        private void loadSection(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                string cls = new String(ddlBatchName.Text.Where(Char.IsLetter).ToArray());
                string Session = new String(ddlBatchName.Text.Where(Char.IsNumber).ToArray());
                sqlDB.loadDropDownList("Select Distinct SectionName From CurrentStudentInfo where BatchName='" + ddlBatchName.SelectedItem.Text + "'  ", dl);
                dl.Items.Add("...Select...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText =ex.Message;
            }
        }
        private void loadRoll(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                string cls = new String(ddlBatchName.Text.Where(Char.IsLetter).ToArray());
                string Session = new String(ddlBatchName.Text.Where(Char.IsNumber).ToArray());
                sqlDB.loadDropDownList("Select RollNo From CurrentStudentInfo where BatchName='" + ddlBatchName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "' and Shift='"+dlShift.SelectedItem.Text+"' Order By RollNo ", dl);
                dl.Items.Add("...Select...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
          
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
                for (int i = 0; i < gvDiscount.Rows.Count; i++)
                {
                    if (ddlParticulars.Text == gvDiscount.Rows[i].Cells[1].Text.ToString())
                    {
                        lblMessage.InnerText = "warning->Already add ";
                        ddlParticulars.Focus();
                        return;
                    }
                }
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select PId from Discount where PId=" + ddlParticulars.SelectedValue + " and BatchName='" + ddlBatchName.Text + "' and SectionName='" + ddlSection.Text + "' and RollNo=" + ddlRoll.Text + " ", dt);
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
                for (int i = 0; i < vdt.Rows.Count; i++)
                {             
                   DataTable dt1 = new DataTable();
                   sqlDB.fillDataTable("select PId from Discount where PId=" + vdt.Rows[i]["PId"].ToString() + " and BatchName='" + ddlBatchName.Text + "' and SectionName='" + ddlSection.Text + "' and Shift='"+dlShift.SelectedItem.Text+"' and RollNo=" + ddlRoll.Text + " ", dt1);
                   DataTable dtsid = new DataTable();
                   sqlDB.fillDataTable("Select StudentId From CurrentStudentInfo where BatchName='" + ddlBatchName.SelectedItem.Text + "' and SectionName='" + ddlSection.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and RollNo=" + ddlRoll.Text + "", dtsid);
                    string StuId=dtsid.Rows[0]["StudentId"].ToString();
                    if (dt1.Rows.Count==0)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into  Discount (BatchName, Shift, SectionName, RollNo, PId, Discount,StudentId)  values (@BatchName, @Shift, @SectionName, @RollNo, @PId, @Discount,@StudentId) ", sqlDB.connection);

                        cmd.Parameters.AddWithValue("@BatchName", ddlBatchName.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Shift", dlShift.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SectionName", ddlSection.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RollNo", Int32.Parse(ddlRoll.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@PId", int.Parse(vdt.Rows[i]["PId"].ToString()));
                        cmd.Parameters.AddWithValue("@Discount", double.Parse(vdt.Rows[i]["Discount"].ToString()));
                        cmd.Parameters.AddWithValue("@StudentId",StuId);
                        result = (int)cmd.ExecuteNonQuery();
                    }
                    
                }

                if (result > 0)
                {
                    Allclear();
                    LoadBatchlist(ddlBatchlist);
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
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
                LoadDiscountlist("");
            }
            lblMessage.InnerText = "";
            if (btnSave.Text == "Save")
            {
                if (saveDiscount() == true)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete From Discount where BatchName='" + ddlBatchName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "' and RollNo=" + ddlRoll.SelectedItem.Text + "", sqlDB.connection);
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
                sqlDB.fillDataTable("Select PId From ParticularsInfo where PName='" +ddlParticulars.Text+ "'", dt);

                SqlCommand cmd = new SqlCommand("Update Discount set Discount=@Discount where BatchName='" + ddlBatchName.Text + "' and SectionName='" + ddlSection.Text + "' and RollNo=" + ddlRoll.Text + " and PId=" + dt.Rows[0]["PId"].ToString() + "", sqlDB.connection);
                cmd.Parameters.AddWithValue("@Discount",txtDiscountAmount.Text.Trim());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    LoadDiscountlist("");
                    lblMessage.InnerText = "success->Successfully updated";
                }
            }
            catch(Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void ddlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSection(ddlSection);
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt== null)
            {
                AddColumns();
            }
            loadBatchwiseDiscount("Select Distinct FullName,RollNo,SectionName From V_BatchwiseDiscount where BatchName='"+ddlBatchName.SelectedItem.Text+"'");
            divDiscount.Controls.Clear();
            string Batch=ddlBatchName.SelectedItem.Text;
            int count = 0;
            for (int i = 0; i < ddlBatchlist.Items.Count; i++)
            {
                if (ddlBatchlist.Items[i].Text == Batch)
                {
                    ddlBatchlist.SelectedIndex = i;
                    count++;
                }
                
            }
            if (count > 0)
            {
                LoadSectionlist(ddlSectionlist);
            }
            else
            {
                LoadBatchlist(ddlBatchlist);
                loadSection(ddlSectionlist);
            }
            
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRoll(ddlRoll);
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
            loadBatchwiseDiscount("Select Distinct FullName,RollNo,SectionName From V_BatchwiseDiscount where BatchName='" + ddlBatchName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "'");
            divDiscount.Controls.Clear();
            int count = 0;
            string section = ddlSection.SelectedItem.Text;
            for (int i = 0; i < ddlSectionlist.Items.Count; i++)
            {
                if (ddlSectionlist.Items[i].Text == section)
                {
                    ddlSectionlist.SelectedIndex = i;
                    count++;
                }

            }
            if (count > 0)
            {
                LoadRollList(ddlRolllist);
            }
            else
            {
                LoadSectionlist(ddlSectionlist);
                LoadRollList(ddlRolllist);
            }
        }
        private void LoadBatchlist(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select Distinct BatchName From Discount ", dl);
            dl.Items.Add("...Select...");
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        private void LoadSectionlist(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select Distinct SectionName From Discount where BatchName='" + ddlBatchlist.Text + "' ", dl);
            dl.Items.Add("...Select...");
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        private void LoadRollList(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                sqlDB.loadDropDownList("Select Distinct RollNo From Discount where BatchName='" + ddlBatchlist.Text + "' and SectionName='" + ddlSectionlist.Text + "' ", dl);
                dl.Items.Add("...Select...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
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
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select PName,Discount,Shift from V_Discount where BatchName='" + ddlBatchlist.Text + "' and SectionName='" + ddlSectionlist.Text + "' and RollNo=" + ddlRolllist.Text + " ";
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
                divInfo += "<th>Shift</th>";
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
                    divInfo += "<td >" + dt.Rows[x]["Shift"].ToString() + "</td>";
                    
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
                       
                // ddlRolllist.Items.Clear();
                LoadSectionlist(ddlSectionlist);
                DataTable dt = (DataTable)ViewState["__tableInfo__"];
                if (dt == null)
                {
                    AddColumns();
                }

        }

        protected void ddlSectionlist_SelectedIndexChanged(object sender, EventArgs e)
        {          
            LoadRollList(ddlRolllist);
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }
        protected void ddlParticulars_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void ddlRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadPreviousData();
            DataTable dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }
        //private void LoadPreviousData()
        //{
        //    DataTable dt=new DataTable();
        //    sqlDB.fillDataTable("Select PName,Discount from V_Discount where BatchName='" + ddlBatchName.Text + "' and SectionName='" + ddlSection.Text + "' and RollNo=" + ddlRoll.Text + " ", dt);
        //    if (dt.Rows.Count == 0)
        //    {
        //        Allclear();
        //        return;
        //    }
        //    ViewState["__tableInfo__"] = dt;
        //    gvDiscount.DataSource = dt;
        //    gvDiscount.RowStyle.HorizontalAlign = HorizontalAlign.Center;

        //    gvDiscount.DataBind();
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ddlBatchlist.Text == "---Select---") || (ddlSectionlist.Text == "---Select---") || (ddlRolllist.Text == "---Select---"))
                {
                    loadblank();
                    return;
                }
                if (string.IsNullOrEmpty(ddlBatchlist.ToString()))
                {
                    return;
                }
                LoadDiscountlist("");
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select PId,PName,Discount from V_Discount where BatchName='" + ddlBatchlist.SelectedItem.Text + "' and SectionName='" + ddlSectionlist.SelectedItem.Text + "' and RollNo=" + ddlRolllist.SelectedItem.Text + "", dt);
                if (dt.Rows.Count == 0) return;
                ViewState["__tableInfo__"] = dt;
                gvDiscount.DataSource = dt;
                gvDiscount.Columns[0].Visible = true;
                gvDiscount.DataBind();
                LoadDiscountlist("");
                btnSave.Text = "Update";
                string Batch = ddlBatchlist.Text;
                for (int i = 0; i < ddlBatchName.Items.Count; i++)
                {
                    if (ddlBatchName.Items[i].Text == Batch)
                    {
                        ddlBatchName.SelectedIndex = i;
                    }
                }
                ddlSection.SelectedItem.Text = ddlSectionlist.Text;
                ddlRoll.SelectedItem.Text = ddlRolllist.Text;
            }
            catch { }
            
        }

        protected void gvDiscount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
                    divInfo += "<td ><span id=section"+ id +">" + dt.Rows[x]["SectionName"].ToString() + "</span></td>";

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
            try
            {
                LoadSectionlist(ddlSectionlist);
                LoadRollList(ddlRolllist);

                if ((ddlBatchName.SelectedItem.Text != "...Select...") && (ddlSection.SelectedItem.Text != "...Select..."))
                    loadBatchwiseDiscount("Select Distinct FullName,RollNo,SectionName From V_BatchwiseDiscount where BatchName='" + ddlBatchName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "'");
                else
                {
                    loadBatchwiseDiscount("Select Distinct FullName,RollNo,SectionName From V_BatchwiseDiscount where BatchName='" + ddlBatchName.SelectedItem.Text + "'");
                }
            }
            catch { }
        }

        protected void dlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRoll(ddlRoll);
        }
       
    }
}