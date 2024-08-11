using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class DateOfPayment : System.Web.UI.Page
    {
        static string className = "";
        static string batchName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "DateOfPayment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadFeesCategory(dlFeeCatName);
                    loadDateOfPayment("");
                }
            loadFeesCategoryInfo("");
            checkEmpty();
        }
        private void loadFeesCategory(DropDownList dl)
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select Distinct FeeCatId, FeeCatName From v_FeesCatDetails order by FeeCatName desc", dt);
            dl.DataSource = dt;
            dl.DataValueField = "FeeCatId";
            dl.DataTextField = "FeeCatName";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select Category...", "0"));
        }

        private void checkEmpty()
        {
            try
            {
                if (dlFeeCatName.Text == "...Select Fee Category...")
                {
                    string divInfo = "";
                    divInfo = "<div class='noData'> Select Category Name</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
                }
            }
            catch { }
        }

        private Boolean saveDateOfPayment()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeeCatName.SelectedItem.Text) };
                sqlDB.fillDataTable("Select FeeCatId from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dt);

                SqlCommand cmd = new SqlCommand("Insert into  DateOfPayment (FeeCatId, DateOfStart, DateOfEnd,IsActive)  values (@FeeCatId, @DateOfStart, "
                + "@DateOfEnd,@IsActive) ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@FeeCatId", dt.Rows[0]["FeeCatId"].ToString());
                cmd.Parameters.AddWithValue("@DateOfStart", DateTime.Parse(txtDateStart.Text.Trim()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@DateOfEnd", DateTime.Parse(txtDateEnd.Text.Trim()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@IsActive", "True");

                int result = (int)cmd.ExecuteNonQuery();

                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "warning->Unable to save";

                saveStudentPayment(); // Save Student  payment 

                loadDateOfPayment("");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
                return false;
            }
        }

        private Boolean saveStudentPayment()
        {
            try
            {
                DataTable dtfc = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeeCatName.SelectedItem.Text) };
                sqlDB.fillDataTable("Select FeeCatId from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dtfc);


                DataTable dtst = new DataTable();
                SqlParameter[] prmsst = { new SqlParameter("@ClassName", className) };
                sqlDB.fillDataTable("Select StudentId,SectionName,Shift,RollNo from CurrentStudentInfo where ClassName=@ClassName AND BatchName= '" + batchName + "' ", prmsst, dtst);

                for (int i = 0; i < dtst.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("saveStudentPayment", DbConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClassName", className);
                    cmd.Parameters.AddWithValue("@SectionName", dtst.Rows[i]["SectionName"].ToString());
                    cmd.Parameters.AddWithValue("@Shift", dtst.Rows[i]["Shift"].ToString());
                    cmd.Parameters.AddWithValue("@BatchName", batchName);
                    cmd.Parameters.AddWithValue("@FeeCatId", dtfc.Rows[0]["FeeCatId"].ToString());
                    cmd.Parameters.AddWithValue("@StudentId", dtst.Rows[i]["StudentId"].ToString());
                    cmd.Parameters.AddWithValue("@RollNo", dtst.Rows[i]["RollNo"].ToString());
                    cmd.Parameters.AddWithValue("@DateOfPayment", DateTime.Parse(txtDateStart.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@PayStatus", "0");
                    cmd.Parameters.AddWithValue("@AmountPaid", "0");
                    cmd.Parameters.AddWithValue("@FineAmount", "0");
                    cmd.Parameters.AddWithValue("@DiscountStatus", "0");
                    cmd.Parameters.AddWithValue("@DiscountTK", "0");
                    cmd.Parameters.AddWithValue("@GrandTotal", "0");

                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                    else lblMessage.InnerText = "error->Unable to save";
                }
                className = "";
                batchName = "";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
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
            if (lblDateOfPayment.Value.Length==0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadDateOfPayment(""); return; }
                saveDateOfPayment();
            }
         else
            {
                updateDateOfPayment();
            }
        }


        private void loadFeesCategoryInfo(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select FeeCatId, BatchName, Amount,PName from v_FeesCatDetails where FeeCatName='"
                    + dlFeeCatName.SelectedItem.Text + "' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                className = new String(dt.Rows[0]["BatchName"].ToString().Where(Char.IsLetter).ToArray());
                batchName = dt.Rows[0]["BatchName"].ToString();

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
                divInfo += "<th>Amount</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["FeeCatId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["BatchName"].ToString(), 12) + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Amount"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";


                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void dlFeeCatName_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDateOfPayment("");
        }

        private void loadDateOfPayment(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select DateOfPaymentId,DateOfStart,DateOfEnd,FeeCatName,FeeCatId from v_DateOfPaymentDetails";
                lblMessage.InnerHtml = "";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Date of Payment</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divDateOfPaymentList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Fees Category</th>";
                divInfo += "<th>Date Of Start</th>";
                divInfo += "<th>Date Of End</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";
                string FeeCatId = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    FeeCatId=dt.Rows[x]["FeeCatId"].ToString();
                    id = dt.Rows[x]["DateOfPaymentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["FeeCatName"].ToString() + "</td>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["DateOfStart"].ToString(), 12) + "</td>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["DateOfEnd"].ToString(), 12) + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                        divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editDateOfPayment(" + id + "," + FeeCatId + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";


                divDateOfPaymentList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }


        private Boolean updateDateOfPayment()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update DateOfPayment  Set DateOfStart=@DateOfStart, DateOfEnd=@DateOfEnd where DateOfPaymentId=@DateOfPaymentId ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@DateOfPaymentId", lblDateOfPayment.Value.ToString());
                cmd.Parameters.AddWithValue("@DateOfStart", DateTime.Parse(txtDateStart.Text.Trim()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@DateOfEnd", DateTime.Parse(txtDateEnd.Text.Trim()).ToString("yyyy-MM-dd"));                
                if (bool.Parse(cmd.ExecuteNonQuery().ToString()))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                loadDateOfPayment("");
                // loadFeesCategoryInfo("");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
    }
}