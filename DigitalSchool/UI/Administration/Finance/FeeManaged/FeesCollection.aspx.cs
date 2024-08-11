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
using DS.BLL.Finance;
using DS.BLL.ManagedClass;
using DS.BLL.Admission;
using DS.PropertyEntities.Model.Finance;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class FeesCollection : System.Web.UI.Page
    {
        DataTable dt;
        static float total = 0;
        static float fineTotal = 0;
        static string StudentId;
        FeesCollectionEntry feescollectionent;
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        StudentFine stdFine;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = string.Empty;
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "FeesCollection.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        BatchEntry.GetDropdownlist(dlBatch, "True");
                        ShiftEntry.GetDropDownList(dlShift);
                        stdtypeEntry.GetEntitiesData(ddlStudentType);
                    }
                }                
            catch { }
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                if (feescollectionent == null)
                {
                    feescollectionent = new FeesCollectionEntry();
                }
                feescollectionent.LoadFeesCategory(dlFeesCategory, BatchClsID[0]);
                
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
                ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
            }
            catch { }
        }
        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
        } 
        protected void dlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                currentstdEntry.GetRollNo(dlRollNo, dlShift.SelectedValue,
                    BatchClsID[0], dlGroup.SelectedValue, dlSection.SelectedValue,ddlStudentType.SelectedValue);
            }
            catch
            { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                total = 0;
                StoresFine();
                dt = new DataTable();
                sqlDB.fillDataTable("SELECT convert(varchar(11),DateOfStart,106) as DateOfStart, " +
                                    "convert(varchar(11),DateOfEnd,106) as DateOfEnd FROM v_DateOfPaymentDetails " +
                                    "WHERE FeeCatId='" + dlFeesCategory.SelectedValue + "'", dt);
                if (dt.Rows.Count > 0)
                    lblPaymentDateStatus.Text = "Payment start at " + dt.Rows[0]["DateOfStart"].ToString() + 
                                                " and end at " + dt.Rows[0]["DateOfEnd"].ToString();
                else
                    lblPaymentDateStatus.Text = string.Empty;
                loadParticularCategory("");
            }
            catch { }
        }
        private void StoresFine()
        {
            try
            {
                string[] batchID = dlBatch.SelectedValue.Split('_');
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StudentId,ShiftID,BatchId,ClsGrpID,ClsSecID,RollNo, FeeCatId, FeeFine, FeeCatName from v_CheckStudentFine where DateOfEnd < '"
                + System.DateTime.Now.ToShortDateString() + "' and PayStatus='False' AND IsActive=" + 1 + " AND BatchId = '" + batchID[0] + "' AND FeeCatId='"+dlFeesCategory.SelectedValue+"'", dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("Insert into  StudentFine (StudentId,ShiftID,BatchId,ClsGrpID,ClsSecID,RollNo, FinePurpose, Fineamount, Discount, FineamountPaid)  "
                    + "values (@StudentId,@ShiftID,@BatchId,@ClsGrpID,@ClsSecID,@RollNo, @FinePurpose, @Fineamount, @Discount, @FineamountPaid) ", sqlDB.connection);                    
                    cmd.Parameters.AddWithValue("@StudentId", dt.Rows[i]["StudentId"].ToString());
                    cmd.Parameters.AddWithValue("@ShiftID", dt.Rows[i]["ShiftID"].ToString());
                    cmd.Parameters.AddWithValue("@BatchId", dt.Rows[i]["BatchId"].ToString());
                    cmd.Parameters.AddWithValue("@ClsGrpID", dt.Rows[i]["ClsGrpID"].ToString());
                    cmd.Parameters.AddWithValue("@ClsSecID", dt.Rows[i]["ClsSecID"].ToString());
                    cmd.Parameters.AddWithValue("@RollNo", dt.Rows[i]["RollNo"].ToString());
                    cmd.Parameters.AddWithValue("@FinePurpose", dt.Rows[i]["FeeCatName"].ToString());
                    cmd.Parameters.AddWithValue("@Fineamount", dt.Rows[i]["FeeFine"].ToString());
                    cmd.Parameters.AddWithValue("@Discount", "0");
                    cmd.Parameters.AddWithValue("@FineamountPaid", "0");
                    int result = (int)cmd.ExecuteNonQuery();
                }
                sqlDB.fillDataTable("select distinct FeeCatId from v_CheckStudentFine where DateOfEnd < '" + System.DateTime.Now.ToShortDateString() + "' AND IsActive="
                    + 1 + " AND FeeCatId='" + dlFeesCategory.SelectedValue + "' ", dt = new DataTable());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("update DateOfPayment  Set IsActive=@IsActive where FeeCatId=@FeeCatId ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", dt.Rows[i]["FeeCatId"].ToString());
                    cmd.Parameters.AddWithValue("@IsActive", "False");
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }
        DataTable dtPL;   //PL=Particular List
        private void loadParticularCategory(string sqlCmd)
        {
            try
            {
                total = 0;
                if (string.IsNullOrEmpty(sqlCmd))
                    sqlCmd = "Select CatPId, FeeCatName,PName, Amount from v_FeesCatDetails where FeeCatId='" + dlFeesCategory.SelectedValue + "' and StdTypeId='"+ddlStudentType.SelectedValue+"' ";
                Session["__FeeCatName__"] = dlFeesCategory.SelectedItem.Text;                
                sqlDB.fillDataTable(sqlCmd, dtPL = new DataTable());
                int totalRows = dtPL.Rows.Count;
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
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                int sl=0 ;
                for (int x = 0; x < dtPL.Rows.Count; x++)
                {
                    sl= x + 1;
                    id = dtPL.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtPL.Rows[x]["Amount"].ToString() + "</td>";
                    total += float.Parse(dtPL.Rows[x]["Amount"].ToString());
                }
                divInfo += "</tr>";
                Session["__PreDue__"] = "0";
                if (dlRollNo.SelectedIndex > 0)
                {
                    DataTable dtPreDue;
                    sqlDB.fillDataTable("select DueAmount from StudentPayment where StudentId=" + dlRollNo.SelectedValue + 
                        " and  DateOfPayment in(select max(DateOfPayment) from StudentPayment where StudentId=" + dlRollNo.SelectedValue +
                        " and PayStatus=1 and FeeCatId<>"+dlFeesCategory.SelectedValue+" )", dtPreDue = new DataTable());
                    if (dtPreDue.Rows.Count > 0 && int.Parse(dtPreDue.Rows[0]["DueAmount"].ToString()) > 0)
                    {
                        Session["__PreDue__"] = dtPreDue.Rows[0]["DueAmount"].ToString();

                        divInfo += "<tr>";
                        divInfo += "<td class='numeric' >" + (sl+= 1).ToString() + "</td>";
                        divInfo += "<td>Previous due</td>";
                        divInfo += "<td style='font-weight: bold; text-align:center'>" + Session["__PreDue__"].ToString()+ "</td>";
                        divInfo += "</tr>";
                        total = total + int.Parse(Session["__PreDue__"].ToString());
                    }
                }

              

                divInfo += "<tr>";
                divInfo += "<td class='numeric' >" + (sl + 1).ToString() + "</td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> <input  id='txtOthersText' value='Others' type='text' onchange='Otherstext(this);' autocomplete='off'  onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\"   class='input controlLength textalign' style='width:100%; text-align:left; float:left'/></td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input  id='txtOthers' value='0' type='text' onchange='OthersAddition(this);' autocomplete='off'  onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\"  class='input controlLength textalign'/></td>";
                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                //divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input disabled='true'  id='Total' value='" + total + "' type='text' class='input controlLength textalign'/></td>";
                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Discount :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input  id='Discount' value='0' type='text' onchange='Commonfunction(this);' autocomplete='off'  onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\"  class='input controlLength textalign'/></td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Payble :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input disabled='true'  id='Payble' value='" + total + "' type='text' class='input controlLength textalign'/></td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Paid :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input  id='Paid' value='0' type='text' onchange='Commonfunction(this);' autocomplete='off'  onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\" class='input controlLength textalign'/></td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Due :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input disabled='true' id='Due' value='" + total + "' type='text' class='input controlLength textalign'/></td>";
                divInfo += "</tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                Session["__OthersText__"] = "Others";
                Session["__Others__"] = "0";
               
                Session["__Total__"] = total;
                Session["__PreTotal__"] = total;
                Session["__Discount__"] = "0";
                Session["__Payble__"] = total;
                Session["__Paid__"] = "0";
                Session["__Due__"] = total;               
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                Session["__FeeCollectionReport__"] = divInfo;
            }
            catch { }
        }       
        protected void btnSearchByRoll_Click(object sender, EventArgs e)
        {
            if (dlBatch.SelectedValue != "0" && 
                dlFeesCategory.SelectedValue != "0" && 
                dlSection.SelectedValue != "0" && 
                dlRollNo.SelectedValue != "0")
            {
                searchByRoll();
                fineTotal = 0;
            }            
        }
        DataTable dtDis = new DataTable();
        private void searchByRoll()
        {
            try
            {
                DataTable dt = new DataTable();
                string[] batchID=dlBatch.SelectedValue.Split('_');
                sqlDB.fillDataTable("Select StudentId,FullName from v_CurrentStudentInfo where StudentId='" +dlRollNo.SelectedValue + "' ", dt);
                lblMessage.InnerHtml = "";
                if (dt.Rows.Count > 0)
                {
                    lblName.Text = "Name : " + dt.Rows[0]["FullName"].ToString();
                    Session["__FullName__"] = "Name : " + dt.Rows[0]["FullName"].ToString();
                    Session["__Roll__"] = "Roll Number : " + dlRollNo.SelectedItem.Text;
                    Session["__Class__"] = "Class : " + new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSection.SelectedItem.Text + " )";
                    Session["__FeeCategory__"] = "Category : " + dlFeesCategory.SelectedItem.Text;

                    sqlDB.fillDataTable("select PName,Discount from v_DiscountParticularDetails where BatchId='" + batchID[0] + "' AND StudentId='"+dlRollNo.SelectedValue+"' ", dtDis);
                    if (dtDis.Rows.Count > 0) 
                    {
                        loadParticularCategoryWithDiscountList("");
                    }                       
                    else
                    {
                        total = 0;
                        loadParticularCategory("");
                    }
                    loadStudentFineInfo();
                }
                else
                {
                    lblName.Text = "Roll not found";
                    lblFineNote.Text = string.Empty;
                }
            }
            catch { }
        }
        private void loadParticularCategoryWithDiscountList(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select  CatPId, FeeCatName,PName, Amount from v_FeesCatDetails where FeeCatId='"
                + dlFeesCategory.SelectedValue + "' ";
                Session["__FeeCatName__"] = dlFeesCategory.SelectedItem.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dtPL = new DataTable());
                int totalRows = dtPL.Rows.Count;
                string divInfo = "";
                total = 0;
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
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dtPL.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dtPL.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    float[] getCVal = checkDiscountParticular((byte)x);
                    if ((getCVal[0] == 0.0) && (getCVal[1] == 0.0) && (getCVal[2] == 0.0))
                    {
                        divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dtPL.Rows[x]["Amount"].ToString() + "</td>";
                        total += float.Parse(dtPL.Rows[x]["Amount"].ToString());
                    }
                    else
                    {
                        divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + " <span style='color:red; font-weight: bold;'>(D. " + getCVal[2] + "% "
                        + dtPL.Rows[x]["Amount"].ToString() + "-" + getCVal[0] + ") </span>" + "</td>";
                        divInfo += "<td class='numeric'>" + getCVal[1] + "</td>";
                        total += getCVal[1];
                    }
                }
                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table><br/>";
                Session["__Total__"] = total;                
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                Session["__FeeCollectionReport__"] = divInfo;
            }
            catch { }
        }
        private void loadStudentFineInfo() // Check student fine information
        {
            try
            {
                string[] batchID = dlBatch.SelectedValue.Split('_');
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select  sum(Fineamount) as Fineamount from StudentFine where BatchId='" + batchID[0] + "' and Studentid='"
                    + dlRollNo.SelectedValue + "' and (FineamountPaid is null or FineamountPaid =0)  and PayDate is null ", dt);
                if (stdFine == null)
                {
                    stdFine = new StudentFine();
                }
                DataTable dtabsent = new DataTable();
                dtabsent = stdFine.GetAbsentFine(dlRollNo.SelectedValue);
                if ((dt.Rows[0]["Fineamount"].ToString() == "") && (dtabsent.Rows[0]["Fineamount"].ToString() == ""))
                {
                    lblFineNote.Text = "You have no fine.";
                }
                else
                {
                    //if (dt.Rows[0]["Fineamount"].ToString() != "")
                    //{
                    //    fineTotal += float.Parse(dt.Rows[0]["Fineamount"].ToString());
                        
                    //}
                    loadFeeFine("");   // generate studentfine information if his already fined
                    lblFineNote.Text = "Total Fine : " + fineTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private void loadFeeFine(string sqlCmd)   // generate studentfine information if his already finded
        {
            try
            {
                Session["__storeFineInfo__"] = "";
                if (stdFine == null)
                {
                    stdFine = new StudentFine();
                }
                dt = new DataTable();
                dt = stdFine.GetStudentFine(dlRollNo.SelectedValue);               
                string divInfo = "";               
                divInfo = " <table id='tblFine' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Fine Purpose</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Select</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Fine Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                int sl = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    sl = x + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                    divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='" + dt.Rows[x]["FinePurpose"].ToString() + "_"
                    + dt.Rows[x]["FineId"].ToString() + "'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                    fineTotal += float.Parse(dt.Rows[x]["Fineamount"].ToString());
                }
                dt = new DataTable();
                dt = stdFine.GetAbsentFine(dlRollNo.SelectedValue);
                if (dt.Rows[0]["Fineamount"].ToString() != "")
                {                   
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        sl = x + 1;
                        divInfo += "<tr>";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td >Absent</td>";
                        divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='absent_0'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                        fineTotal += float.Parse(dt.Rows[x]["Fineamount"].ToString());
                    }
                }
                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='border-left:none'></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + fineTotal + "</td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";                
                divFineInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e) // Pay Now Event
        {
            if (checkInput() == true) updateStudentPayment();
        }
        bool checkInput()
        {
            try
            {
                if (total > 0 && dlRollNo.SelectedValue != "0")
                {
                    return true;
                }
                lblMessage.InnerText = "warning->Input roll number";
                return false;
            }
            catch { return false; }
        }
        private Boolean updateStudentPayment()
        {
            try
            {
                string[] batchID = dlBatch.SelectedValue.Split('_');                
                DataTable dtst = new DataTable();
                sqlDB.fillDataTable("Select  PayStatus from StudentPayment where StudentId='" + dlRollNo.SelectedValue + "' and BatchID='" + batchID[0] + "' and FeeCatId='"
                + dlFeesCategory.SelectedValue + "' ", dtst);
                if (dtst.Rows.Count > 0)
                {
                    if (dtst.Rows[0]["PayStatus"].ToString() == "True")
                    {
                        lblMessage.InnerText = "warning->Already paid this category";
                        Session["__PayStatus__"] = "Already Paid";
                        return false;
                    }
                }
                if (dtst.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("Update StudentPayment  Set DateOfPayment=@DateOfPayment, PayStatus=@PayStatus, AmountPaid=@AmountPaid, "
                    + "DiscountStatus=@DiscountStatus, DiscountTK=@DiscountTK, FineAmount=@FineAmount,DueAmount=@DueAmount,OthersParticular=@OthersParticular,OthersAmount=@OthersAmount,PreDueAmount=@PreDueAmount,GrandTotal=@GrandTotal where BatchID=@BatchID and StudentId=@StudentId and FeeCatId=@FeeCatId ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@BatchID", batchID[0]);
                    cmd.Parameters.AddWithValue("@StudentId",dlRollNo.SelectedValue);                    
                    cmd.Parameters.AddWithValue("@FeeCatId",dlFeesCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateOfPayment", DateTime.Parse(System.DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@PayStatus", "True");
                    if (Session["__TotalFine__"] == null)
                    {
                        cmd.Parameters.AddWithValue("@AmountPaid", Session["__Paid__"].ToString());
                        cmd.Parameters.AddWithValue("@FineAmount", 0);
                    }
                    else
                    {
                        string p = Session["__TotalFine__"].ToString();
                        cmd.Parameters.AddWithValue("@AmountPaid", float.Parse(Session["__Paid__"].ToString()) + float.Parse(Session["__TotalFine__"].ToString()));
                        cmd.Parameters.AddWithValue("@FineAmount", Session["__TotalFine__"].ToString());
                    }

                    if (float.Parse(Session["__Discount__"].ToString()) > 0)
                    {
                        cmd.Parameters.AddWithValue("@DiscountStatus", 1);
                        cmd.Parameters.AddWithValue("@DiscountTK", Session["__Discount__"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DiscountStatus", 0);
                        cmd.Parameters.AddWithValue("@DiscountTK", 0);
                    }
                    cmd.Parameters.AddWithValue("@DueAmount", Session["__Due__"].ToString());
                    cmd.Parameters.AddWithValue("@OthersParticular",Session["__OthersText__"].ToString());
                    cmd.Parameters.AddWithValue("@OthersAmount", Session["__Others__"].ToString());
                    cmd.Parameters.AddWithValue("@PreDueAmount", Session["__PreDue__"].ToString());
                    cmd.Parameters.AddWithValue("@GrandTotal", Session["__Total__"].ToString());
                    int result=(int)cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        bool status = true;
                        //-----------------------------------------------------------------------------------------------------------------
                        dt = new DataTable();
                        try
                        {
                            dt = (DataTable)Session["__storeFineInfo__"];   //Load From ajax.aspx 
                        }
                        catch { status = false; }
                        if (dt !=null )
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                if (dt.Rows[x]["Id"].ToString() == "0")
                                {
                                    if (stdFine == null)
                                    {
                                        stdFine = new StudentFine();
                                    }
                                    StudentFineEntities stdfineEntities = new StudentFineEntities();
                                    stdfineEntities.PayDate = DateTime.Now;
                                    stdFine.AddEntities = stdfineEntities;
                                    stdFine.AbsentUpdate(dlRollNo.SelectedValue);

                                }
                                else
                                {
                                    if (stdFine == null)
                                    {
                                        stdFine = new StudentFine();
                                    }
                                    StudentFineEntities stdfineEntities = new StudentFineEntities();
                                    stdfineEntities.FineId = int.Parse(dt.Rows[x]["Id"].ToString());
                                    stdfineEntities.FineamountPaid = double.Parse(dt.Rows[x]["FineAmount"].ToString());
                                    stdfineEntities.PayDate = DateTime.Now;
                                    stdFine.AddEntities = stdfineEntities;
                                    stdFine.Update();
                                }
                            }
                        }
                    }
                    Session["__TotalFine__"] = null;
                    lblMessage.InnerText = "success-> Payment Successfull";
                   
                    dt = new DataTable();
                    AdmFeesCategoresEntry AdmFee = new AdmFeesCategoresEntry();
                    dt = AdmFee.MoneyReceiptReportData(dlRollNo.SelectedValue,dlFeesCategory.SelectedValue,ddlStudentType.SelectedValue);
                    if(dt.Rows.Count>0)
                    {
                        dlRollNo.SelectedValue = "0";
                        lblFineNote.Text = "";
                        Session["__PayStatus__"] = "New Payment";
                        Session["__MoneyReceipt__"] = dt;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=MoneyReceipt');", true);  //Open New Tab for Sever side code
                    }
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        float discountTk = 0;
        private float[] checkDiscountParticular(byte rowIndex)
        {
            //try
            //{
            float[] getCalculationValue = new float[3];
            for (byte b = 0; b < dtDis.Rows.Count; b++)
            {
                if (dtPL.Rows[rowIndex]["PName"].ToString().Equals(dtDis.Rows[b]["PName"].ToString()))
                {
                    getCalculationValue[0] = (float.Parse(dtPL.Rows[rowIndex]["Amount"].ToString()) * float.Parse(dtDis.Rows[b]["Discount"].ToString())) / 100;
                    getCalculationValue[1] = (float.Parse(dtPL.Rows[rowIndex]["Amount"].ToString())) - getCalculationValue[0];
                    getCalculationValue[2] = float.Parse(dtDis.Rows[b]["Discount"].ToString());
                    discountTk += getCalculationValue[0];
                    break;
                }

            }
            Session["__DiscountTk__"] = discountTk;
            return getCalculationValue;
            //}
            //catch { return float [] val=new float [2];}
        }

        protected void dlRollNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadParticularCategory("");
        }            
    }
}