using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class ParticularCategories : System.Web.UI.Page
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
                    Classes.commonTask.LoadFeesCategoryInfo(dlCategory);
                    Classes.commonTask.loadParticularInfo(dlParticular);
                    Classes.commonTask.loadFeesCategory(dlFilter);
                    AddColumns();
                    loadParticularDetails("");
                }
            }
        }
        private Boolean updateParticularsCategory()
        {
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlCategory.Text) };
                sqlDB.fillDataTable("Select FeeCatId  from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dt);
                DataTable dtp = new DataTable();
                SqlParameter[] prmsp = { new SqlParameter("@PName", dlParticular.Text) };
                sqlDB.fillDataTable("Select PId  from ParticularsInfo where PName=@PName ", prmsp, dtp);
                SqlCommand cmd = new SqlCommand(" update ParticularsCategory  Set FeeCatId=@FeeCatId,  PId=@PId, Amount=@Amount where CatPId=@CatPId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@CatPId", lblParticularCatId.Value.ToString());
                cmd.Parameters.AddWithValue("@FeeCatId", dt.Rows[0]["FeeCatId"].ToString());
                cmd.Parameters.AddWithValue("@PId", dtp.Rows[0]["PId"].ToString());
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                cmd.ExecuteNonQuery();
                lblParticularCatId.Value = "";               
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                
                return false;
            }
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

        private void AddGridview()
        {
            try
            {
                lblMessage.InnerText = "";                
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select FeeCatId, PId from ParticularsCategory where FeeCatId=" + dlCategory.SelectedValue + " and PId=" + dlParticular.SelectedValue+ " ", dt);               
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
               
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select PName, Amount from v_FeesCatDetails where FeeCatId='" + dlFilter.SelectedValue + "' ";

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
                if (saveParticularsCategory() == true)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                    Classes.commonTask.loadFeesCategory(dlFilter);
                    loadParticularDetails("");
                    lblMessage.InnerText = "success->Successfully saved";

                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete From ParticularsCategory where FeeCatId="+dlCategory.SelectedValue+" ", sqlDB.connection);
               int result=(int)cmd.ExecuteNonQuery();
               if (result > 0)
               {
                   if (saveParticularsCategory() == true)
                   {
                       ViewState["__tableInfo__"] = null;
                       AddColumns();
                       Classes.commonTask.loadFeesCategory(dlFilter);
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
                SqlCommand cmd = new SqlCommand("Update FeesCategoryInfo set FeeAmount=" + dtAmount.Rows[0]["Amount"].ToString() + " where FeeCatId=" + dlCategory.SelectedValue + " ", sqlDB.connection);
                cmd.ExecuteNonQuery();
                
            }

        }
        private Boolean saveParticularsCategory()
        {
            try
            {
                 DataTable  dt = (DataTable)ViewState["__tableInfo__"];
                 for (int i = 0; i < dt.Rows.Count;i++ )
                 {
                     SqlCommand cmd = new SqlCommand("Insert into  ParticularsCategory (FeeCatId, PId, Amount)  values (@FeeCatId, @PId, @Amount) ", sqlDB.connection);
                     cmd.Parameters.AddWithValue("@FeeCatId", dlCategory.SelectedValue);
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
            if (dlFilter.SelectedItem.Text == "---Select Fee Category---")
            {
                lblMessage.InnerText = "warning->Please Select Fees Category";
                return;
            }
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select PId,PName,Amount From v_FeesCatDetails where FeeCatId="+dlFilter.SelectedValue+"", dt);
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning->No Particulars Found";
                return;
            }
            btnSave.Text = "Update";
            ViewState["__tableInfo__"] = dt;
            gvParticulars.DataSource = dt;
            gvParticulars.Columns[0].Visible = true;
            gvParticulars.DataBind();
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
    }
}