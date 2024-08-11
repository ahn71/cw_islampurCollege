using DS.BLL;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class CollectionDetails : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(dlBatch);
                        Classes.commonTask.loadBatch(dlBatchSummary);

                        // Classes.commonTask.loadFeesCategoryWithCondition(dlCategory);

                        Classes.commonTask.loadSection(dlSectionSummary);
                        Classes.commonTask.loadSection(dlSectionDetails);
                        Classes.commonTask.loadBatch(dlBatchDetails);

                        Classes.commonTask.loadBatch(dlBatchDueList);
                        Classes.commonTask.loadSection(dlSectionDueList);

                        string index = Request.QueryString["back"];
                        if (index == "csr")
                        {
                            TabContainer.ActiveTabIndex = 1;
                        }
                        if (index == "ftd")
                        {
                            TabContainer.ActiveTabIndex = 2;
                        }
                    }
                }
            }
            catch { }

        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlCategory.Items.Clear();
                sqlDB.loadDropDownList("Select FeeCatName From FeesCategoryInfo where BatchName='" + dlBatch.Text + "'  ", dlCategory);
            }
            catch { }
        }

        private void loadFeesCategoryInfo(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select  ClassName, SectionName, BatchName,  RollNo, DateOfPayment, PayStatus, AmountPaid,  FeeCatName,FullName"
                +" from v_CollectionDetails where PayStatus='True' and BatchName='" + dlBatch.SelectedItem.Text + "' and FeeCatName='"+dlCategory.SelectedItem.Text
                +"' order by SectionName ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

             
                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divFeeCategory.Visible = false;
                    divInfo = "<div class='noData'>No Fee Collection Information</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divCollectionView.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                else
                {
                    divFeeCategory.Visible = true;
                    lblCategory.Text = dt.Rows[0]["FeeCatName"].ToString();
                }
                

                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th>Section</th>";
                divInfo += "<th class='numeric'>Paid Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                float totalPaid = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    int sl = x + 1;
                    divInfo += "<tr></tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["AmountPaid"].ToString() + "</td>";
                    totalPaid += float.Parse(dt.Rows[x]["AmountPaid"].ToString());
                }

                divInfo += "</tr>";

                divInfo += "<td ></td>";
                divInfo += "<td style='border-left:none;'></td>";
                divInfo += "<td style='border-left:none;'></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + totalPaid + "</td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divCollectionView.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadFeesCategoryInfo("");
        }

        protected void btnSearchSummary_Click(object sender, EventArgs e)
        {
            loadTotalCollectionSummary("");
        }

        private void loadStudentPaymentInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                    + "' and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ", dt);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void loadTotalCollectionSummary(string sqlCmd)
        {
            try
            {
                if(dlFeeCategorySummary.Text=="All")
                {
                    loalCollectionAllFeeCat(""); //for All category collection
                    return;
                }


                DataTable dtfeId = new DataTable();
		        SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeeCategorySummary.SelectedItem.Text) };
		        sqlDB.fillDataTable("Select FeeCatId  from FeesCategoryInfo where FeeCatName=@FeeCatName ",prms,dtfeId);


                if (string.IsNullOrEmpty(sqlCmd) && chkToday.Checked) sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" 
                    + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" 
                    + dlSectionSummary.SelectedItem.Text + "' and Shift='"+dlShift.SelectedItem.Text+"' and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd")
                    + "'  ";
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                    + "' and FeeCatId=" + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='"
                    + dlShift.SelectedItem.Text + "'   and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                //----------------------------Category Amount---------------------------------//
                DataTable dtCatAmount = new DataTable();
                sqlDB.fillDataTable("select FeeAmount from FeesCategoryInfo where FeeCatName='"+ dlFeeCategorySummary.SelectedItem.Text +"' ", dtCatAmount);
                //----------------------------End Category Amount---------------------------------//


                //----------------------------Count Paid Student ---------------------------------//
                DataTable dtCountSt = new DataTable();
                if(chkToday.Checked) sqlDB.fillDataTable("select distinct StudentId from StudentPayment where DateOfPayment = '"+ TimeZoneBD.getCurrentTimeBD().ToShortDateString()
                    +"' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[0]["FeeCatId"] + "' ", dtCountSt);
                else sqlDB.fillDataTable("select distinct StudentId from StudentPayment where  DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" 
                    + txtToDate.Text.ToString() + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[0]["FeeCatId"] + "' ", dtCountSt);
                //----------------------------End Count Paid Student---------------------------------//


                //----------------------------Load Fine Collection----------------------------//
                DataTable dtfine = new DataTable();
                if (chkToday.Checked) sqlCmd = "select sum(Fineamount) as Fineamount from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                    + "' and FeeCatId=" + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                    + dlShift.SelectedItem.Text + "'  and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD().ToShortDateString() + "' ";
                else sqlCmd = "select sum(Fineamount) as Fineamount from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" 
                    + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text 
                    + "'  and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                sqlDB.fillDataTable(sqlCmd, dtfine);

                DataTable dtFineStNum = new DataTable();
                if (chkToday.Checked) sqlDB.fillDataTable("select distinct StudentId from StudentPayment where Fineamount >0 and DateOfPayment = '" 
                    + TimeZoneBD.getCurrentTimeBD().ToShortDateString() + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[0]["FeeCatId"] + "' ", dtFineStNum);
                else sqlDB.fillDataTable("select distinct StudentId from StudentPayment where Fineamount >0 and DateOfPayment between '" + txtFromDate.Text.ToString() 
                    + "'  and  '" + txtToDate.Text.ToString() + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[0]["FeeCatId"] + "' ", dtFineStNum);
                //----------------------------End Fine Collection----------------------------//


                //----------------------------Load Discount Collection----------------------------//
                DataTable dtDis = new DataTable();
                if (chkToday.Checked) sqlCmd = "select sum(DiscountTK) as DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                    + "' and FeeCatId=" + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                    + dlShift.SelectedItem.Text + "'   and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD().ToShortDateString() + "' ";
                else sqlCmd = "select sum(DiscountTK) as DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" 
                    + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text 
                    + "'  and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                sqlDB.fillDataTable(sqlCmd, dtDis);

                DataTable dtDisCount = new DataTable();
                if (chkToday.Checked) sqlCmd = "select sum(DiscountTK) as DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                    + "' and FeeCatId=" + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                    + dlShift.SelectedItem.Text + "'   and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD().ToShortDateString() + "' ";
                else sqlCmd = "select DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" 
                    + dtfeId.Rows[0]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text 
                    + "'  and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                sqlDB.fillDataTable(sqlCmd, dtDisCount);
                //----------------------------End Discount Collection----------------------------//


                int totalRows = dt.Rows.Count;
                string divInfo = ""; 


                if (dt.Rows[0]["AmountPaid"].ToString() == "" || dt.Rows[0]["AmountPaid"].ToString() == "0")
                {
                    btnPreview.Visible = false;
                    divInfo = "<div class='noData'>No Fee Collection Information</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                btnPreview.Visible = true;
                lblClassName.Text = "Class : " + new String(dlBatchSummary.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSectionSummary.Text + " )";

               // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";


                divInfo = " <table id='tblParticularCategory' class='displayT'  style='width:595.28px;margin:0 auto;'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

               // divInfo += "<th>" + dlFeeCategorySummary.Text + "</th>";
               // divInfo += "<th class='numeric'>Amount</th>";


                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";


                divInfo += "<tr></tr>";

                divInfo += "<td > " + dlFeeCategorySummary.Text + " (Cat.)  </td>";

                divInfo += "<td style='text-align:right; width:91px'>" + dtCatAmount.Rows[0]["FeeAmount"].ToString() + " </td>";

                divInfo += "<tr></tr>";


                divInfo += "<td > Paid Student Amount ( " + dtCountSt.Rows.Count + " ) </td>";
                divInfo += "<td style='text-align:right'> x " + dtCountSt.Rows.Count + " </td>";

                divInfo += "<tr></tr>";
                divInfo += "<td style='text-align:right;'> </td>";
                float paidStudentAmount = float.Parse(dtCatAmount.Rows[0]["FeeAmount"].ToString()) * dtCountSt.Rows.Count;
                divInfo += "<td style='border-top:1px solid black; text-align:right' > Total :  " + paidStudentAmount + "</td>";

                divInfo += "<tr></tr>";
                divInfo += "<tr></tr>";
                divInfo += "<td>Total Fine Amount ( " + dtFineStNum.Rows.Count +" ) </td>";
                divInfo += "<td style='text-align:right'> + " + dtfine.Rows[0]["Fineamount"] + " </td>";

                divInfo += "<tr></tr>";
                divInfo += "<td style='text-align:right'> </td>";
                float totalCateAndFine = paidStudentAmount  + float.Parse(dtfine.Rows[0]["Fineamount"].ToString());
                divInfo += "<td  style='border-top:1px solid black; text-align:right;'>Total : " + totalCateAndFine + "</td>";


                divInfo += "<tr></tr>";
                divInfo += "<td> Total Discount Amount ( " + dtDisCount.Rows.Count + " ) </td>";
                divInfo += "<td style='text-align:right'> -" + dtDis.Rows[0]["DiscountTK"] + "</td>";

                divInfo += "<tr></tr>";
                divInfo += "<tr></tr>";
                divInfo += "<td style='border-top:1px solid black'>Total Amount Collected </td>";
                float TotalAmountCollected = totalCateAndFine - float.Parse(dtDis.Rows[0]["DiscountTK"].ToString());
                divInfo += "<td  style='border-top:1px solid black; text-align:right'> " + TotalAmountCollected + " </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
               // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                Session["__SummaryReport__"] = divInfo;
                divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        private void loalCollectionAllFeeCat(string sqlCmd)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtCateInfo;
                DataTable dtfeId = new DataTable(); //for Fee CategoryId Find
                DataTable dt = new DataTable(); //for Amount Paid
                DataTable dtCatAmount = new DataTable(); //for Category Amount
                DataTable dtfine = new DataTable(); // for Fine Collection
                DataTable dtFineStNum = new DataTable(); // count fine student number
                DataTable dtDis = new DataTable(); //for Discount Collection
                DataTable dtCountSt = new DataTable(); // for Paid Student Count

                DataTable rpt = new DataTable();
                rpt.Columns.Add("htmlCode");

                for (int b = 0; b < dlFeeCategorySummary.Items.Count; b++)
                {
                    dtCateInfo = new DataTable();
                    dtCateInfo.Columns.Add("CS", typeof(string));//CS = Category Summary
                    dtCateInfo.Columns.Add("Amount", typeof(float));
                    ds.Tables.Add(dtCateInfo);
                    if (dlFeeCategorySummary.Items[b].Text != "All")
                    {
                        SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeeCategorySummary.Items[b].Text) };
                        sqlDB.fillDataTable("Select FeeCatId  from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dtfeId);

                        if (string.IsNullOrEmpty(sqlCmd) && chkToday.Checked) sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" 
                            + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" 
                            + dlSectionSummary.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text + "'   and DateOfPayment = '" 
                            + TimeZoneBD.getCurrentTimeBD().ToShortDateString() + "' ";
                        if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" 
                            + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" 
                            + dlSectionSummary.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text + "'   and DateOfPayment between '" 
                            + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                        sqlDB.fillDataTable(sqlCmd, dt);

                        //----------------------------Category Amount---------------------------------//
                        sqlDB.fillDataTable("select FeeAmount from FeesCategoryInfo where FeeCatName='" + dlFeeCategorySummary.Items[b].Text + "' ", dtCatAmount);
                        ds.Tables[b].Rows.Add(dlFeeCategorySummary.Items[b].Text + "(Cat)", dtCatAmount.Rows[b-1]["FeeAmount"].ToString());
                        //----------------------------End Category Amount----------------------------//

                        //----------------------------Count Paid Student ---------------------------------//     
                        if (chkToday.Checked) sqlDB.fillDataTable("select distinct StudentId from StudentPayment where DateOfPayment = '"
                            + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[b - 1]["FeeCatId"] + "' ", dtCountSt);
                        else sqlDB.fillDataTable("select distinct StudentId from StudentPayment where DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" 
                            + txtToDate.Text.ToString() + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[b - 1]["FeeCatId"] + "' ", dtCountSt);
                        ds.Tables[b].Rows.Add("Paid Student Amount (" + dtCountSt.Rows.Count + ")", dtCountSt.Rows.Count);
                        //----------------------------End Count Paid Student---------------------------------//

                        //----------------------------Load Fine Collection----------------------------//                       
                        if (chkToday.Checked) sqlCmd = "select sum(Fineamount) as Fineamount from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                            + "' and FeeCatId=" + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='"
                            + dlShift.SelectedItem.Text + "'  and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' ";
                        else sqlCmd = "select sum(Fineamount) as Fineamount from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" 
                            + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                            + dlShift.SelectedItem.Text + "'  and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                        sqlDB.fillDataTable(sqlCmd, dtfine);

                        if (chkToday.Checked) sqlDB.fillDataTable("select distinct StudentId from StudentPayment where Fineamount >0 and DateOfPayment ='"
                            + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' and PayStatus='True' and FeeCatId='" + dtfeId.Rows[b - 1]["FeeCatId"] + "' ", dtFineStNum);
                        else sqlDB.fillDataTable("select distinct StudentId from StudentPayment where Fineamount >0 and DateOfPayment between '" 
                            + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' and PayStatus='True' and FeeCatId='" 
                            + dtfeId.Rows[b - 1]["FeeCatId"] + "' ", dtFineStNum);
                        ds.Tables[b].Rows.Add("Total Fine Amount (" + dtFineStNum.Rows.Count + ")", dtfine.Rows[b-1]["Fineamount"].ToString());
                        //----------------------------End Fine Collection----------------------------//

                        //----------------------------Load Discount Amount ----------------------------//                       
                        if (chkToday.Checked) sqlCmd = "select sum(DiscountTK) as DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text 
                            + "' and FeeCatId=" + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                            + dlShift.SelectedItem.Text + "'   and DateOfPayment = '" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' ";
                        else sqlCmd = "select sum(DiscountTK) as DiscountTK from StudentPayment where BatchName='" + dlBatchSummary.SelectedItem.Text + "' and FeeCatId=" 
                            + dtfeId.Rows[b - 1]["FeeCatId"].ToString() + " and SectionName='" + dlSectionSummary.SelectedItem.Text + "'  and Shift='" 
                            + dlShift.SelectedItem.Text + "'  and DateOfPayment between '" + txtFromDate.Text.ToString() + "'  and  '" + txtToDate.Text.ToString() + "' ";
                        sqlDB.fillDataTable(sqlCmd, dtDis);
                        ds.Tables[b].Rows.Add("Total Discount Amount (" + dtDis.Rows.Count + ")", dtDis.Rows[b-1]["DiscountTK"].ToString());
                        //----------------------------End Discount ----------------------------//

                        sqlCmd = "";
                    }
                }


                int totalRows = dt.Rows.Count;
                string divInfo = "";


                if (dt.Rows[0]["AmountPaid"].ToString() == "" || dt.Rows[0]["AmountPaid"].ToString() == "0")
                {
                    btnPreview.Visible = false;
                    divInfo = "<div class='noData'>No Fee Collection Information</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                btnPreview.Visible = true;
                lblClassName.Text = "Class : " + new String(dlBatchSummary.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSectionSummary.Text + " )";


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo = " <table id='tblParticularCategory' class='displayT'  style='width:595.28px;margin:0 auto;'  > ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                        {
                            divInfo += "<tr>";

                            divInfo += "<td > " + ds.Tables[i].Rows[j]["CS"] + "  </td>";
                            if (j == 0) divInfo += "<td style='text-align:right; width:110px;'>" + ds.Tables[i].Rows[j]["Amount"] + " </td>";
                            if (j == 1)
                            {
                                divInfo += "<td style='text-align:right; width:110px;'>x" + ds.Tables[i].Rows[j]["Amount"] + " </td>";
                                divInfo += "</tr> <tr>";
                                divInfo += "<td style='text-align:right' ></td>";
                                float tcMult = float.Parse(ds.Tables[i].Rows[j-1]["Amount"].ToString()) * float.Parse(ds.Tables[i].Rows[j]["Amount"].ToString());
                                ViewState["__tcMult__"] = tcMult.ToString();
                                divInfo += "<td style='border-top:1px solid black; text-align:right'> Total : " + tcMult + "</td>";
                                divInfo += "</tr>";
                                
                            }
                            if (j == 2)
                            {
                                divInfo += "<td style='text-align:right; width:110px;'>+" + ds.Tables[i].Rows[j]["Amount"] + " </td>";
                                divInfo += "</tr> <tr>";
                                divInfo += "<td style='text-align:right' > </td>";
                                float tcSum = float.Parse(ViewState["__tcMult__"].ToString()) + float.Parse(ds.Tables[i].Rows[j]["Amount"].ToString());
                                ViewState["__tcSum__"] = tcSum.ToString();
                                divInfo += "<td style='border-top:1px solid black; text-align:right'> Total : " + tcSum + "</td>";
                                divInfo += "</tr>";
                            }
                            if (j == 3)
                            {
                                divInfo += "<td style='text-align:right; width:110px;'>-" + ds.Tables[i].Rows[j]["Amount"] + " </td>";
                                divInfo += "</tr> <tr>";
                                divInfo += "<td > </td>";
                                float tcSub = float.Parse(ViewState["__tcSum__"].ToString()) - float.Parse(ds.Tables[i].Rows[j]["Amount"].ToString());
                                ViewState["__tcSub__"] = tcSub.ToString();
                                
                                divInfo += "</tr>";
                            }
                           if (j==0)divInfo += "</tr>";    
                     
                        }

                        divInfo += "<tr>";
                        divInfo += "<td style='border-top:1px solid black;'>Total Amount Collected  </td>";
                        divInfo += "<td style='border-top:1px solid black; text-align:right'>" + ViewState["__tcSub__"] .ToString()+ "</td>";
                        divInfo += "</tr>";


                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<br>";
                        Session["__FeeCatName__"] = "All";
                       
                        rpt.Rows.Add(divInfo);
                        divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                    }
                    Session["__SummaryReport__"] = rpt;
                }
            }
            catch { }
        
        } // for all Category Summary  Collection

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtfeId = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeeCategorySummary.Text) };
                sqlDB.fillDataTable("Select FeeCatId  from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dtfeId);

                Session["__Batch__"] = dlBatchSummary.Text;
                Session["__Section__"] = dlSectionSummary.Text;
                if (dtfeId.Rows.Count > 0) Session["__FeeCat__"] = dtfeId.Rows[0]["FeeCatId"].ToString();
                else Session["__FeeCat__"] = "All";
                Session["__FeeCatName__"] = dlFeeCategorySummary.Text;
                Session["__FromDate__"] = txtFromDate.Text.Trim();
                Session["__ToDate__"] = txtToDate.Text.Trim();

                if (chkToday.Checked)
                {
                    Session["__DateFt__"] = "Today";
                }
                else
                {
                    Session["__DateFrom__"] ="From : "+ txtFromDate.Text;
                    Session["__DateTo__"] ="To : "+ txtToDate.Text;
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/CollectionSummaryReport.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void dlBatchSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlFeeCategorySummary.Items.Clear();
                dlFeeCategorySummary.Items.Add("All");
                sqlDB.loadDropDownList("Select FeeCatName from FeesCategoryInfo where BatchName='" + dlBatchSummary.Text + "' ", dlFeeCategorySummary);
            }
            catch { }
        }

        protected void btnSearchDetails_Click(object sender, EventArgs e)
        {
            if (dlFeeCategoryDetails.Text == "All")loadDetailsCollection("");
            else loadDetailsCollectionSingle("");
        }

        string allCollectionDetailsReport;
        private void loadDetailsCollection(string sqlCmd)
        {
            try
            {
                string[,] array = new string[dlFeeCategoryDetails.Items.Count - 1, 2];

                DataTable dt = new DataTable();

                //------------------Enterd every record in specific table-------------------
                DataSet ds = new DataSet();

                for (byte b = 0; b < dlFeeCategoryDetails.Items.Count -1; b++)
                {
                    array[b, 0] = dlFeeCategoryDetails.Items[b + 1].Value;
                    //array[b, 1] = dt.Rows[b]["AmountPaid"].ToString();

                    DataTable Tables;
                    Tables = new DataTable();
                    ds.Tables.Add(Tables);

                    ds.Tables[b].Columns.Add("Sl", typeof(int));
                    ds.Tables[b].Columns.Add("Roll", typeof(int));
                    ds.Tables[b].Columns.Add("Name", typeof(string));
                    ds.Tables[b].Columns.Add("Amount", typeof(float));

                }
                //-----------------------------------------------------------------------------

                float getDetailsGrandTotal = 0;

                for (int i = 0; i < dlFeeCategoryDetails.Items.Count - 1; i++)
                {
                    dt.Rows.Clear();
                    string sqlcmd = "select ClassName, SectionName, BatchName, RollNo, DateOfPayment, PayStatus, AmountPaid,  FeeCatName,FullName from "
                    +"v_CollectionDetails where BatchName='" + dlBatchDetails.Text + "' and FeeCatName='" + dlFeeCategoryDetails.Items[i + 1].Value 
                    + "'  and SectionName='" + dlSectionDetails.Text + "'  and Shift='" + dlShiftForDetails.Text + "'  and DateOfPayment between '" 
                    + txtFromDateDetails.Text + "'  and  '" + txtToDateDetails.Text + "' and PayStatus='True'  ";
                    sqlDB.fillDataTable(sqlcmd, dt);
              
                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        return;
                    }

                    //--------------------------------Display Category-------------------------------------

                    divInfo = " <table  style='width:100%;margin-right:10px;text-align:left; font-family:Lucida Grande;'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th>" + array[i, 0] + "</th>";


                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    divInfo += "<tr></tr>";


                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
     
                    divCollectionDetails.Controls.Add(new LiteralControl(divInfo));

                    //---------------------------------------------------------------------



                    btnPreviewDetails.Visible = true;
                    lblClassDetails.Text = "Class : " + new String(dlBatchDetails.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSectionDetails.Text + " )";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";


                    divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th class='numeric'>SL</th>";
                    divInfo += "<th class='numeric'>Roll</th>";
                    divInfo += "<th>Name</th>";
                    divInfo += "<th class='numeric'>Paid Amount</th>";

                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int sl = x + 1;
                        divInfo += "<tr></tr>";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["AmountPaid"].ToString() + "</td>";
                        ds.Tables[i].Rows.Add(sl, int.Parse(dt.Rows[x]["RollNo"].ToString()), dt.Rows[x]["FullName"].ToString(), float.Parse(dt.Rows[x]["AmountPaid"].ToString()));
                        getDetailsGrandTotal += float.Parse(dt.Rows[x]["AmountPaid"].ToString());
                        if (x == dt.Rows.Count - 1)
                        {
                            var CatTotalAmount = ds.Tables[i].Compute("sum (Amount)","");
                            array[i, 1] = CatTotalAmount.ToString();
                        }
                    }
                    divInfo += "</tr>";
                    divInfo += "<td ></td>";
                    divInfo += "<td style='border-left:none; border-bottom:none'></td>";
                    divInfo += "<td style='text-align:right; font-weight: bold; border-left:none; border-bottom:none'>Total : </td>";
                    divInfo += "<td class='numeric' style='font-weight: bold;'>" + array[i, 1] + "</td>";

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table><br/>";
                    divCollectionDetails.Controls.Add(new LiteralControl(divInfo));

                    allCollectionDetailsReport += divInfo;
                    Session["__allCDR__"] = allCollectionDetailsReport;
                    Session["__batchName__"] = "Batch : " + dlBatchDetails.SelectedItem.Text + " (" + dlBatchDetails.SelectedItem.Text + ")";
                    Session["__CollectionDs__"] = ds;
                    Session["__arrayDetails__"] = array;

                }
            }
            catch { }

        }

        private void loadDetailsCollectionSingle(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select ClassName, SectionName, BatchName,  RollNo, DateOfPayment, PayStatus, AmountPaid,  "
                +"FeeCatName,FullName from v_CollectionDetails where BatchName='" + dlBatchDetails.Text + "' and FeeCatName='"+dlFeeCategoryDetails.Text
                +"'  and SectionName='" + dlSectionDetails.Text + "' and Shift='"+ dlShiftForDetails.Text +"'  and DateOfPayment between '" + txtFromDateDetails.Text
                + "'  and  '" + txtToDateDetails.Text + "' and PayStatus='True' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    btnPreviewDetails.Visible = false;
                    divInfo = "<div class='noData'>No Fee Collection Details</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divCollectionDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                btnPreviewDetails.Visible = true;
                lblClassDetails.Text = "Class : " + new String(dlBatchDetails.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSectionDetails.Text + " )";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th class='numeric'>Paid Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";


                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    int sl = x + 1;
                    divInfo += "<tr></tr>";
                    divInfo += "<td class='numeric' >" + sl + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["AmountPaid"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divCollectionDetails.Controls.Add(new LiteralControl(divInfo));

                Session["__reportType__"] = "Collection Details Report -";
                Session["__batchName__"] = "Batch : " + dlBatchDetails.SelectedItem.Text + " (" + dlBatchDetails.SelectedItem.Text + ")";
                Session["__allCDR__"] = divInfo;
            }
            catch { }

        }

        protected void dlBatchDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlFeeCategoryDetails.Items.Clear();
                dlFeeCategoryDetails.Items.Add("All");
                sqlDB.loadDropDownList("Select FeeCatName from FeesCategoryInfo where BatchName='" + dlBatchDetails.Text + "' ", dlFeeCategoryDetails);
            }
            catch { }
        }

        protected void btnPreviewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Session["__Batch__"] = dlBatchDetails.Text;
                Session["__Section__"] = dlSectionDetails.Text;
                //Response.Redirect("/Report/CollectionDetailsReport.aspx?val=" + dlFeeCategoryDetails.Text);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/CollectionDetailsReport.aspx?val=" 
                    + dlFeeCategoryDetails.Text +" ');", true);  //Open New Tab for Sever side code

            }
            catch { }
        }

        protected void btnSearchDueList_Click(object sender, EventArgs e)
        {
            loadDueList("");
        }

        protected void dlBatchDueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlFeesCategoryDueList.Items.Clear();
                sqlDB.loadDropDownList("Select FeeCatName From FeesCategoryInfo where BatchName='" + dlBatchDueList.SelectedItem.Text+ "'  ", dlFeesCategoryDueList);
            }
            catch { }
        }

        private void loadDueList(string sqlCmd)   //Load Due list 
        {
            try
            {
                DataTable dtFcAmount = new DataTable();
                sqlDB.fillDataTable("select FeeAmount From FeesCategoryInfo where FeeCatName='"+dlFeesCategoryDueList.SelectedItem.Text+"' ", dtFcAmount);

                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select RollNo,FullName from v_StudentPaymentInfo where BatchName='" + dlBatchDueList.SelectedItem.Text 
                    + "' and SectionName='" + dlSectionDueList.SelectedItem.Text + "' and  FeeCatName='" + dlFeesCategoryDueList.SelectedItem.Text + "' and Shift='" 
                    + dlShiftDueList.SelectedItem.Text + "' and PayStatus='False'  ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Due List</div>";
                    // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divDueList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblFine' class='display'  style='width:100%;margin:0px auto; ' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th class='numeric' style='width:100px'>Roll No</th>";
                divInfo += "<th > Name </th>";
                divInfo += "<th class='numeric'> Amount </th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                float total = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtFcAmount.Rows[0]["FeeAmount"].ToString() + "</td>";
                    total += float.Parse(dtFcAmount.Rows[0]["FeeAmount"].ToString());

                }
                divInfo += "</tr>";

                divInfo += "<td ></td>";
                divInfo += "<td style='border-left:none;'></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";


                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

               // divInfo += "<div style='margin-left:50%'> Total : "+total+" </div>";


                divDueList.Controls.Add(new LiteralControl(divInfo));

                Session["__DueListReport__"] = divInfo;
                lblBatch.Text = "Due List of " + dlBatchDueList.SelectedItem.Text;
                lblShift.Text = "Shift : " + dlShiftDueList.SelectedItem.Text;
                lblSection.Text = "Section : " + dlSectionDueList.SelectedItem.Text;
            }
            catch { }
        }

        protected void btnPrintPreviewDueList_Click(object sender, EventArgs e)
        {
            Session["__Class__"] = new String(dlBatchDueList.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            Session["__Section__"] = dlSectionDueList.SelectedItem.Text;
            Session["__FeeCate__"] = dlFeesCategoryDueList.SelectedItem.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/DueListReport.aspx');", true);  //Open New Tab for Sever side code
   
        }

    }
}