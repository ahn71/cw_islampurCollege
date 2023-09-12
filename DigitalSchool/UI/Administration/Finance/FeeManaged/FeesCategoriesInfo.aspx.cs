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
using ComplexScriptingSystem;
using DS.BLL.ManagedClass;
using DS.BLL.Examinition;
using DS.Classes;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class FeesCategoriesInfo : System.Web.UI.Page
    {
        string sqlCmd = string.Empty;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "FeesCategoriesInfo.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                commonTask.LoadFeesCategoryPaymentForType(ddlPaymentFor);
                commonTask.LoadPaymentStores(ddlPaymentStore);
                BatchEntry.GetDropdownlist(dlBatchName, "True");
                ClassGroupEntry.GetDropDownWithAll(ddlGroup, -1);//-1 as All
                ExamInfoEntry.GetExamIdListWithExInSl(ddlExam, "All");
                //loadFeesCategoryInfo();
                //stdtypeEntry.GetEntitiesData(ddlStudentType);
            }
        }
        private void loadFeesCategoryInfo()
        {
            try
            {
                divFeesCategoryList.Controls.Add(new LiteralControl(""));
                string condition = "";
                if (ddlPaymentFor.SelectedIndex > 0)
                {
                    condition+=" Where IsNull(fc.PaymentFor,'regular')='" + ddlPaymentFor.SelectedValue + "'";
                }                 
                if (dlBatchName.SelectedIndex>0)
                {
                    string[] batchClsID = dlBatchName.SelectedValue.Split('_');
                    if(condition=="")
                        condition += " Where FC.BatchId='" + batchClsID[0] + "'";
                    else
                        condition += " and FC.BatchId='" + batchClsID[0] + "'";
                }
                              
               sqlCmd = @"with dft as (select StoreNameKey, StoreTitle from PaymentStores where StoreNameKey = 'islampurcollegeedubd')
                 Select FC.FeeCatId,BatchInfo.BatchName,IsNull(BatchInfo.BatchId, 0) as BatchId,ISNULL(BatchInfo.ClassID, 0) as ClassID, convert(varchar(10), FC.DateOfCreation, 105) as DateOfCreation, FC.FeeFine, FC.FeeCatName, DP.DateOfPaymentId, convert(varchar(10), DP.DateOfStart, 105) as 'Start Date', convert(varchar(10), DP.DateOfEnd, 105) as 'End Date', DP.IsActive,ex.ExInId,IsNull(ex.ExInSl, 0) as ExInSl,IsNull(fc.PaymentFor, 'regular') as PaymentFor,IsNull(fc.ClsGrpId, 0) as ClsGrpId,IsNull(FC.StoreNameKey, dft.StoreNameKey) as StoreNameKey,IsNull(str.StoreTitle, dft.StoreTitle) as StoreTitle from FeesCategoryInfo FC INNER JOIN DateOfPayment DP ON(FC.FeeCatId = DP.FeeCatId) Left JOIN BatchInfo ON BatchInfo.BatchId = FC.BatchId left join ExamInfo ex on fc.ExInSl = ex.ExInSl left join PaymentStores as str on FC.StoreNameKey = str.StoreNameKey cross join dft " + condition + " order by FC.FeeCatId desc";                                 
            

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='tblParticularCategory' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Batch Name</th>";
                divInfo += "<th>Fee CatName</th>";
                divInfo += "<th>Start Date</th>";
                divInfo += "<th>End Date</th>";
                divInfo += "<th class='numeric'>Fee Fine</th>";
                divInfo += "<th>Creation Date</th>";
                divInfo += "<th>Exam</th>";
                divInfo += "<th>Store</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (totalRows == 0)
                {
                    divInfo += "<tbody></tbody></table>";
                    divFeesCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo += "<tbody>";
                string id = "";
                string BatchId = "";
                string ClassId = "";
                string ExInSl = "0";
                string PaymentFor = "";
                string ClsGrpId = "0";
                string StoreNameKey = "0";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    PaymentFor = dt.Rows[x]["PaymentFor"].ToString();
                    BatchId = dt.Rows[x]["BatchId"].ToString();
                    ClassId = dt.Rows[x]["ClassID"].ToString();
                    ExInSl = dt.Rows[x]["ExInSl"].ToString();
                    id = dt.Rows[x]["FeeCatId"].ToString();
                    ClsGrpId = dt.Rows[x]["ClsGrpId"].ToString();
                    StoreNameKey = dt.Rows[x]["StoreNameKey"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["BatchName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FeeCatName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Start Date"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["End Date"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["FeeFine"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DateOfCreation"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ExInId"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["StoreTitle"].ToString() + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                        divInfo += "<td>" +
                            "<img style='width:20px;' src='/Images/gridImages/edit.png'  onclick=\"editFeesCategory(" + id + "," + BatchId + "," + ClassId + "," + ExInSl + ",'"+ PaymentFor + "',"+ ClsGrpId + ",'"+ StoreNameKey + "');\"  />";
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
            if (lblFeesCateId.Value.ToString().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadFeesCategoryInfo(); return; }
                if(!existAdmissionFee())
                     saveFeesCategoryInfo();
            }
            else updateFeesCategoryInfo();
        }
        private bool existAdmissionFee()
        {
            try {
                if (ddlPaymentFor.SelectedValue == "openPayment") return false;
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select FeeCatId,BatchId,DateOfCreation,FeeFine,FeeCatName,ExInSl,IsDemo,PaymentFor,ClsGrpId,StoreNameKey from FeesCategoryInfo where PaymentFor='admission' and BatchId=" + dlBatchName.SelectedValue.Split('_')[1]+" and ClsGrpId="+ddlGroup.SelectedValue);
                if (dt == null)
                {
                    lblMessage.InnerText = "error-> Technical Error!!!";
                    return true;
                }
                else if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning-> Admission Category Exist! ("+dt.Rows[0]["FeeCatName"].ToString()+")";
                    return true;
                }
                else
                    return false;
                   
            } catch(Exception ex) {
                lblMessage.InnerText = "error-> Technical Error!!! ex>"+ex.Message;
                return true;
            }
        }
        private Boolean saveFeesCategoryInfo()
        {
            int result = 0;
            try
            {
                string[] batchClsID = dlBatchName.SelectedValue.Split('_');
                SqlCommand cmd = new SqlCommand("Insert into FeesCategoryInfo( BatchId, DateOfCreation, FeeFine, FeeCatName,ExInSl,PaymentFor,ClsGrpId,StoreNameKey) values " +
                                                "(@BatchId, @DateOfCreation, @FeeFine, @FeeCatName,@ExInSl,@PaymentFor,@ClsGrpId,@StoreNameKey); SELECT SCOPE_IDENTITY();", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@BatchId", batchClsID[0]);
                cmd.Parameters.AddWithValue("@DateOfCreation", DateTime.Now);
                cmd.Parameters.AddWithValue("@FeeFine", txtFeesFine.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeCatName", txtFeesCatName.Text.Trim());
                cmd.Parameters.AddWithValue("@ExInSl", ddlExam.SelectedValue);
                cmd.Parameters.AddWithValue("@PaymentFor",ddlPaymentFor.SelectedValue);
                cmd.Parameters.AddWithValue("@ClsGrpId",ddlGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@StoreNameKey",ddlPaymentStore.SelectedValue);
                int FeeCatId = Convert.ToInt32(cmd.ExecuteScalar());
                if (FeeCatId > 0)
                {
                    cmd = new SqlCommand("Insert into  DateOfPayment(FeeCatId, DateOfStart, DateOfEnd,IsActive)  values (@FeeCatId, @DateOfStart, "
                    + "@DateOfEnd,@IsActive) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", FeeCatId.ToString());
                    cmd.Parameters.AddWithValue("@DateOfStart",Classes.commonTask.ddMMyyyyToyyyyMMdd(txtDateStart.Text));
                    cmd.Parameters.AddWithValue("@DateOfEnd", Classes.commonTask.ddMMyyyyToyyyyMMdd(txtDateEnd.Text));
                    cmd.Parameters.AddWithValue("@IsActive", "True");
                    result = (int)cmd.ExecuteNonQuery();
                }
                if (result > 0)
                {
                  //  saveStudentPayment(FeeCatId.ToString(), dlBatchName.SelectedValue);
                    Clear();
                    loadFeesCategoryInfo();                   
                }
                else lblMessage.InnerText = "error->Unable to save";

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                loadFeesCategoryInfo();
                return false;
            }
        }
        private Boolean saveStudentPayment(string FeeCatId, string batchID)
        {
            try
            {
                string[] batchClsID = batchID.Split('_');
                DataTable dtst = new DataTable();
                sqlDB.fillDataTable("Select StudentId,ConfigId,ClsGrpID,ClsSecID,RollNo from CurrentStudentInfo where BatchId='" + batchClsID[0] + "' ", dtst);

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
                    cmd.Parameters.AddWithValue("@DateOfPayment", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PayStatus", "0");
                    cmd.Parameters.AddWithValue("@AmountPaid", "0");
                    cmd.Parameters.AddWithValue("@FineAmount", "0");
                    cmd.Parameters.AddWithValue("@DiscountStatus", "0");
                    cmd.Parameters.AddWithValue("@DiscountTK", "0");
                    cmd.Parameters.AddWithValue("@GrandTotal", "0");
                    cmd.Parameters.AddWithValue("@DueAmount", "0");
                    cmd.Parameters.AddWithValue("@StdTypeId","0");

                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0) lblMessage.InnerText = "success->Save Successfully ";
                    else lblMessage.InnerText = "error->Unable to save";
                }            
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        

        private Boolean updateFeesCategoryInfo()
        {
            try
            {
                string[] batchClsID = dlBatchName.SelectedValue.Split('_');
                SqlCommand cmd = new SqlCommand(" update FeesCategoryInfo  Set FeeFine=@FeeFine, " +
                                                "FeeCatName=@FeeCatName,ExInSl=@ExInSl,PaymentFor=@PaymentFor,ClsGrpId=@ClsGrpId,StoreNameKey=@StoreNameKey where FeeCatId=@FeeCatId ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@FeeCatId", lblFeesCateId.Value.ToString());
               // cmd.Parameters.AddWithValue("@BatchId", batchClsID[0]);
                cmd.Parameters.AddWithValue("@FeeFine", txtFeesFine.Text.Trim());
                cmd.Parameters.AddWithValue("@FeeCatName", txtFeesCatName.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentFor",ddlPaymentFor.SelectedValue);
                cmd.Parameters.AddWithValue("@ClsGrpId",ddlGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@ExInSl",ddlExam.SelectedValue);               
                cmd.Parameters.AddWithValue("@StoreNameKey", ddlPaymentStore .SelectedValue);             
                cmd.ExecuteNonQuery();
                //int FeeCatId = Convert.ToInt32(cmd.ExecuteScalar());
                //if (FeeCatId > 0)
                //{
                    cmd = new SqlCommand("update DateOfPayment set DateOfStart=@DateOfStart,DateOfEnd=@DateOfEnd,IsActive=@IsActive where FeeCatId=@FeeCatId", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", lblFeesCateId.Value.ToString());
                    cmd.Parameters.AddWithValue("@DateOfStart", convertDateTime.getCertainCulture(txtDateStart.Text));
                    cmd.Parameters.AddWithValue("@DateOfEnd", convertDateTime.getCertainCulture(txtDateEnd.Text));
                    cmd.Parameters.AddWithValue("@IsActive", "True");
                    cmd.ExecuteNonQuery();
                //}
                loadFeesCategoryInfo();
                lblFeesCateId.Value = "";
                Clear();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                loadFeesCategoryInfo();
                return false;
            }
        }

        private void Clear()
        {
            txtFeesCatName.Text = string.Empty;
            txtDateStart.Text = string.Empty;
            txtDateEnd.Text = string.Empty;
            txtFeesFine.Text = string.Empty;
            ddlPaymentStore.SelectedValue = "0";
            if (pnlAcademicInfo.Visible)
                hfAcademicInfo.Value = "1";
            else
                hfAcademicInfo.Value = "0";
        }

        protected void dlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                if (dlBatchName.SelectedValue != "0")
                {
                    loadFeesCategoryInfo();
                    string[] batchClsID = dlBatchName.SelectedValue.Split('_');
                    ClassGroupEntry.GetDropDownWithAll(ddlGroup, int.Parse(batchClsID[1]));
                    ExamInfoEntry.GetExamIdListWithExInSl(ddlExam, batchClsID[0]);
                }                    
                else
                    loadFeesCategoryInfo();
                
            }
            catch{}
        }

        protected void ddlPaymentFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentFor.SelectedValue == "openPayment")
            {
                pnlAcademicInfo.Visible = false;
                hfAcademicInfo.Value = "0";
            }
            else
            {
                pnlAcademicInfo.Visible = true;
                hfAcademicInfo.Value = "1";
            }
               
            loadFeesCategoryInfo();
        }

        //protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
        //    if (dlBatchName.SelectedValue != "0")
        //    {
        //        string[] batchClsID = dlBatchName.SelectedValue.Split('_');
        //        ExamInfoEntry.GetExamIdListWithExInSl(ddlExam, batchClsID[0], ddlGroup.SelectedValue);
        //    }

        //}
    }
}