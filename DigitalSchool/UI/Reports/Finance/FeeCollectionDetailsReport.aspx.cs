using DS.BLL;
using DS.BLL.ControlPanel;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Finance
{
    public partial class FeeCollectionDetailsReport : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        FeesCollectionEntry fc;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "FeeCollectionDetails.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        //........Student Wise Collection Details........
                        BatchEntry.GetDropdownlist(ddlBatchS, "True");
                        ddlBatchS.Items.Insert(1, new ListItem("All", "All"));
                        ddlBatchS.SelectedValue = "All";
                        ddlSectionS.Items.Insert(0, new ListItem("All", "All"));
                        ddlSectionS.SelectedValue = "All";
                        ddlGroup.Items.Insert(0, new ListItem("All", "All"));
                        ddlGroup.SelectedValue = "All";
                        ShiftEntry.GetDropDownList(ddlShiftS);
                        ddlFeeCatS.Items.Insert(0, new ListItem("All", "All"));
                        ddlFeeCatS.SelectedValue = "All";                        
                        ddlRollS.Items.Insert(0, new ListItem("All", "All"));
                        ddlRollS.SelectedValue = "All";
                    //........Collection Details...................
                    commonTask.LoadFeesCategoryPaymentForType(ddlPaymentFor);
                    BatchEntry.GetDropdownlist(ddlCBatch, "True");
                        ddlCBatch.Items.Insert(1, new ListItem("All", "All"));
                        ddlCBatch.SelectedValue = "All";
                        ddlCSection.Items.Insert(0, new ListItem("All", "All"));
                        ddlCSection.SelectedValue = "All";
                        ddlGroup.Items.Insert(0, new ListItem("All", "All"));
                        ddlGroup.SelectedValue = "All";
                        ShiftEntry.GetDropDownList(ddlCShift);
                        ddlCShift.Items.Insert(1, new ListItem("All", "All"));
                        ddlCShift.SelectedValue = "All";

                        //...........Due List............................
                        BatchEntry.GetDropdownlist(dlBatchDueList, "True");
                        dlBatchDueList.Items.Insert(1, new ListItem("All", "All"));
                        dlBatchDueList.SelectedValue = "All";
                        dlSectionDueList.Items.Insert(0, new ListItem("All", "All"));
                        dlSectionDueList.SelectedValue = "All";
                        dlGroupDueList.Items.Insert(0, new ListItem("All", "All"));
                        dlGroupDueList.SelectedValue = "All";
                        ShiftEntry.GetDropDownList(dlShiftDueList);
                        dlShiftDueList.Items.Insert(1, new ListItem("All", "All"));
                        dlShiftDueList.SelectedValue = "All";

                        //........Particular Report..............
                        BatchEntry.GetDropdownlist(ddlBatchP, "True");
                        ClassEntry.GetEntitiesData(ddlClass);
                        string index = Request.QueryString["back"];
                        if (index == "csr")
                        {
                            TabContainer.ActiveTabIndex = 1;
                        }
                        if (index == "ftd")
                        {
                            TabContainer.ActiveTabIndex = 2;
                        }
                        ddlCFeeCat.Items.Insert(0, new ListItem("All", "All"));
                        dlFeesCategoryDueList.Items.Insert(0, new ListItem("All", "All"));
                    //------ start by Nayem----
                    txtFeeCollectionToDate.Text=txtFeeCollectionDate.Text= txtTransactionToDate.Text =  txtTransactionDate.Text = TimeZoneBD.getCurrentTimeBD().ToString("dd-MM-yyyy");
                   
                    loadDailyTransaction(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));

                    commonTask.loadYearFromBatch(ddlYearFeeCollection);
                    commonTask.loadClassFromBatch(ddlClassFeeCollection);
                    //------ end by Nayem----
                }
                lblMessage.InnerText = "";
            }
            catch { }

        }
        private void loadDailyTransaction(string Date,string ToDate)
        {
            try {
                dt = new DataTable();
//                dt = CRUD.ReturnTableNull(@"with dft as (
//select StoreNameKey, StoreTitle from PaymentStores where StoreNameKey = 'islampurcollegeedubd'
//)
//select format(p.CreatedAt,'dd-MM-yyyy HH:mm:ss') as CreatedAt,format(p.UpdatedAt, 'dd-MM-yyyy HH:mm:ss') as UpdatedAt, p.OrderNo as InvoiceNo,p.PaidAmount,p.paymentRefId,p.clientMobileNo, format(p.orderDateTime, 'dd-MM-yyyy HH:mm:ss') as orderDateTime,format(p.issuerPaymentDateTime, 'dd-MM-yyyy HH:mm:ss') as issuerPaymentDateTime, p.issuerPaymentRefNo,p.status,p.PaymentType, case when p.PaymentType = 'nagad' then 'nagad' else p.PaymentMedia end as PaymentMedia,isnull(str.StoreTitle, dft.StoreTitle) as Store from PaymentInfo p left join PaymentStores str on p.StoreNameKey = str.StoreNameKey cross join dft  where status='success' and format( p.CreatedAt,'yyyy-MM-dd')>='" + Date + "' and  format( p.CreatedAt,'yyyy-MM-dd') <='" + ToDate + "' ");
                dt = CRUD.ReturnTableNull(@"with dft as (
select StoreNameKey, StoreTitle from PaymentStores where StoreNameKey = 'islampurcollegeedubd'
)
select format(p.CreatedAt,'dd-MM-yyyy HH:mm:ss') as CreatedAt,format(p.UpdatedAt, 'dd-MM-yyyy HH:mm:ss') as UpdatedAt, p.OrderNo as InvoiceNo,p.PaidAmount,p.status,p.PaymentType, case when p.PaymentType = 'nagad' then 'nagad' else p.PaymentMedia end as PaymentMedia,isnull(str.StoreTitle, dft.StoreTitle) as Store from PaymentInfo p left join PaymentStores str on p.StoreNameKey = str.StoreNameKey cross join dft  where status='success' and format( p.CreatedAt,'yyyy-MM-dd')>='" + Date + "' and  format( p.CreatedAt,'yyyy-MM-dd') <='" + ToDate + "' ");
                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();
            }
            catch(Exception ex ) { }
        }
        private void ViewFeeCollections()
        {
            try
            {
                string CatIds = "";
                if (ddlCategoriesFeeCollection.SelectedValue == "0")
                {
                    foreach (ListItem li in ddlCategoriesFeeCollection.Items)
                    {
                        CatIds += "," + li.Value;
                    }
                    CatIds = CatIds.Remove(0,1);
                }
                else
                    CatIds = ddlCategoriesFeeCollection.SelectedValue;



                dt = new DataTable();
                dt = loadFeeCollectionData(ddlPaymentFor.SelectedValue, ddlClassFeeCollection.SelectedItem.Text+ddlYearFeeCollection.SelectedValue, CatIds, commonTask.ddMMyyyyToyyyyMMdd(txtFeeCollectionDate.Text.Trim()), commonTask.ddMMyyyyToyyyyMMdd(txtFeeCollectionToDate.Text.Trim()));
                gvFeeCollections.DataSource = dt;
                gvFeeCollections.DataBind();
                Decimal TotalPrice = Convert.ToDecimal(dt.Compute("SUM(PaidAmount)", string.Empty));
                Label lblTotalAmount=(Label) gvFeeCollections.HeaderRow.FindControl("lblTotalAmount");
                lblTotalAmount.Text = TotalPrice.ToString();
            }
            catch (Exception ex) { }
        }
       
        protected void ddlCBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlCBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
            ClassSectionEntry.GetEntitiesData(ddlCSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            ddlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (ddlGroup.Enabled == true)
                ddlGroup.SelectedValue = "All";
            ddlCSection.Items.Insert(1, new ListItem("All", "All"));
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            fc.LoadFeesCategory(ddlCFeeCat, BatchClsID[0]);
            ddlCFeeCat.Items.Insert(1, new ListItem("All", "All"));
            ddlCFeeCat.SelectedValue = "All";
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlCBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (ddlGroup.SelectedValue != "All")
            {
                GroupId = ddlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(ddlCSection, int.Parse(BatchClsID[1]), GroupId);
            ddlCSection.Items.Insert(1, new ListItem("All", "All"));
        }
        protected void btnCSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string condition = "";

                DataTable dt = new DataTable();
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(ddlCShift.SelectedValue, ddlCBatch.SelectedValue, ddlGroup.SelectedValue, ddlCSection.SelectedValue);
                if (ddlCFeeCat.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND FeeCatId='" + ddlCFeeCat.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE FeeCatId='" + ddlCFeeCat.SelectedValue + "'";
                    }
                }
                if (chkCTodayCollect.Checked)
                {
                    if (condition != "")
                    {
                        condition += " AND Convert(datetime,DateOfPayment,105)=Convert(datetime,'" + DateTime.Now.ToString("dd-MM-yyyy") + "',105)";
                    }
                    else
                    {
                        condition = " WHERE Convert(datetime,DateOfPayment,105)=Convert(datetime,'" + DateTime.Now.ToString("dd-MM-yyyy") + "',105)";
                    }
                }
                else
                {
                    if (condition != "")
                    {
                        condition += " AND Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtCFrom.Text + "',105) AND Convert(datetime,'" + txtCTo.Text + "',105)";
                    }
                    else
                    {
                        condition = " WHERE Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtCFrom.Text + "',105) AND Convert(datetime,'" + txtCTo.Text + "',105)";
                    }
                }
                if (condition != "")
                {
                    condition += " AND BatchID!='0' AND PayStatus='True'";
                }
                else
                {
                    condition = " WHERE BatchID!='0' AND PayStatus='True'";
                }

                dt = new DataTable();
                dt = fc.LoadFeeCollection(condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__CollectionDetails__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=CollectionDetails');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Fees Collection";
            }
            catch { }
        }
        protected void btnSearchDetails_Click(object sender, EventArgs e)
        {
            if (dlFeeCategoryDetails.Text == "All") loadDetailsCollection("");
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
                for (byte b = 0; b < dlFeeCategoryDetails.Items.Count - 1; b++)
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
                    + "v_CollectionDetails where BatchName='" + dlBatchDetails.Text + "' and FeeCatName='" + dlFeeCategoryDetails.Items[i + 1].Value
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
                            var CatTotalAmount = ds.Tables[i].Compute("sum (Amount)", "");
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
                + "FeeCatName,FullName from v_CollectionDetails where BatchName='" + dlBatchDetails.Text + "' and FeeCatName='" + dlFeeCategoryDetails.Text
                + "'  and SectionName='" + dlSectionDetails.Text + "' and Shift='" + dlShiftForDetails.Text + "'  and DateOfPayment between '" + txtFromDateDetails.Text
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
                sqlDB.loadDropDownList("Select FeeCatName from FeesCategoryInfo where BatchName='" + dlBatchDetails.Text + "' ", dlFeeCategoryDetails);
                dlFeeCategoryDetails.Items.Add(new ListItem("...Select...", "0"));
                dlFeeCategoryDetails.SelectedIndex = dlFeeCategoryDetails.Items.Count - 1;
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
                    + dlFeeCategoryDetails.Text + " ');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }
        protected void dlBatchDueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchDueList.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroupDueList);
            ClassSectionEntry.GetEntitiesData(dlSectionDueList, int.Parse(BatchClsID[1]), dlGroupDueList.SelectedValue);
            dlGroupDueList.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroupDueList.Enabled == true)
                dlGroupDueList.SelectedValue = "All";
            dlSectionDueList.Items.Insert(1, new ListItem("All", "All"));
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            fc.LoadFeesCategory(dlFeesCategoryDueList, BatchClsID[0]);
            dlFeesCategoryDueList.Items.Insert(1, new ListItem("All", "All"));
            dlFeesCategoryDueList.SelectedValue = "All";
        }

        protected void dlGroupDueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchDueList.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroupDueList.SelectedValue != "All")
            {
                GroupId = dlGroupDueList.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSectionDueList, int.Parse(BatchClsID[1]), GroupId);
            dlSectionDueList.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void btnPrintPreviewDueList_Click(object sender, EventArgs e)
        {
            try
            {
                string condition = "";

                DataTable dt = new DataTable();
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(dlShiftDueList.SelectedValue,
                    dlBatchDueList.SelectedValue, dlGroupDueList.SelectedValue, dlSectionDueList.SelectedValue);
                if (ddlCFeeCat.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND FeeCatId='" + ddlCFeeCat.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE FeeCatId='" + ddlCFeeCat.SelectedValue + "'";
                    }
                }

                if (condition != "")
                {
                    condition += " AND BatchID!='0' AND PayStatus='False'";
                }
                else
                {
                    condition = " WHERE BatchID!='0' AND PayStatus='False'";
                }

                dt = new DataTable();
                dt = fc.LoadDueList(condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__DueList__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DueList');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Due List";
            }
            catch { }
        }

        protected void btnPrintParticular_Click(object sender, EventArgs e)
        {
            LoadParticularInfo();
        }
        private void LoadParticularInfo()
        {
            try
            {
                string[] batchID=ddlBatchP.SelectedValue.Split('_');
                dt = CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName,PName,Amount,BatchName,BatchId,FeeAmount,"
                + "FeeFine,StdTypeName FROM v_FeesCatDetails WHERE BatchId='" + batchID[0] + "' AND FeeCatId='" + ddlCategoryP.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    Session["__BatchWiseCategory__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=BatchWiseCategory');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->No Particular Found";
                }
            }
            catch { }
        }

        protected void ddlBatchP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (ddlBatchP.SelectedValue != "0")
                    {
                        LoadBatchwiseFeeCat(ddlBatchP.SelectedValue, ddlCategoryP);                      
                    }
                }
                catch { }
            }
            catch { }
        }
        private void LoadBatchwiseFeeCat(string batchId, DropDownList dl)
        {
            string[] batchclsID = batchId.Split('_');
            dt = new DataTable();
            sqlDB.fillDataTable("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId = '" + batchclsID[0] + "'  order by FeeCatId ASC", dt);
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdmFeesCategoresEntry.GetDropDownList(ddlCategoryA, ddlClass.SelectedValue);
        }

        protected void btnPreviewP_Click(object sender, EventArgs e)
        {
            LoadAdmissionParticularInfo();
        }
        private void LoadAdmissionParticularInfo()
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT AdmFeeCatId as AdmCatPId,FeeCatName,PName,Amount,ClassName,ClassID"
                + " FROM v_Adm_FeesCatDetails WHERE ClassID='" + ddlClass.SelectedValue + "' AND AdmFeeCatId='" + ddlCategoryA.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    Session["__AdmissionWiseCategory__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AdmissionWiseCategory');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->No Particular Found";
                }
            }
            catch { }
        }

        protected void ddlBatchS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatchS.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroupS);
            ClassSectionEntry.GetEntitiesData(ddlSectionS, int.Parse(BatchClsID[1]), ddlGroupS.SelectedValue);
            ddlGroupS.Items.Insert(1, new ListItem("All", "All"));
            if (ddlGroupS.Enabled == true)
            {
                ddlGroupS.SelectedValue = "All";
                tdGroupS.Visible = true;
                tdGroupSH.Visible = true;
            }
            else
            {
                tdGroupS.Visible = false;
                tdGroupSH.Visible = false;
            }
            ddlSectionS.Items.Insert(1, new ListItem("All", "All"));
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            fc.LoadFeesCategory(ddlFeeCatS, BatchClsID[0]);
            ddlFeeCatS.Items.Insert(1, new ListItem("All", "All"));
            ddlFeeCatS.SelectedValue = "All";
        }

        protected void ddlGroupS_SelectedIndexChanged(object sender, EventArgs e)
        {
             string[] BatchClsID = ddlBatchS.SelectedValue.Split('_');
            string GroupId = "0";
            if (ddlGroupS.SelectedValue != "All")
            {
                GroupId = ddlGroupS.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(ddlSectionS, int.Parse(BatchClsID[1]), GroupId);
            ddlSectionS.Items.Insert(1, new ListItem("All", "All"));
        }
        protected void ddlSectionS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSectionS.SelectedValue == "All")
            {
                ddlRollS.Items.Clear();
                ddlRollS.Items.Insert(0, new ListItem("...Select...", "0"));
                ddlRollS.Items.Insert(1, new ListItem("All", "All"));
                ddlRollS.SelectedValue = "All";
            }
            else
            {
                string[] BatchClsID = ddlBatchS.SelectedValue.Split('_');
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT DISTINCT StudentId,RollNo FROM v_StudentPaymentDetails WHERE ShiftID='"
                    + ddlShiftS.SelectedValue + "' AND BatchID='" + BatchClsID[1] + "' AND ClsGrpID='"
                    + ddlGroupS.SelectedValue + "' AND ClsSecID='" + ddlSectionS.SelectedValue + "'");
                ddlRollS.DataSource = dt;
                ddlRollS.DataTextField = "RollNo";
                ddlRollS.DataValueField = "StudentId";
                ddlRollS.DataBind();
                ddlRollS.Items.Insert(0, new ListItem("All", "All"));
            }
        }

        protected void btnStudentPaymentDetails_Click(object sender, EventArgs e)
        {
            LoadStudentPaymentDetails();
        }
        private void LoadStudentPaymentDetails()
        {
            try
            {
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            dt = new DataTable();
            string condition = "";
            condition = fc.GetSearchCondition(ddlShiftS.SelectedValue,
                ddlBatchS.SelectedValue, ddlGroupS.SelectedValue, ddlSectionS.SelectedValue);
            condition += " AND PayStatus='True'";
                if (ddlRollS.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND StudentId='" + ddlRollS.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE StudentId='" + ddlRollS.SelectedValue + "'";
                    }
                }
                if (ddlFeeCatS.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND FeeCatId='" + ddlFeeCatS.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE FeeCatId='" + ddlFeeCatS.SelectedValue + "'";
                    }
                }
              
            dt = fc.LoadStudentPaymentDetails(condition);
            if (dt.Rows.Count > 0)
            {
                Session["__StudentPaymentDetails__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentPaymentDetails');", true);  //Open New Tab for Sever side code
            }
            else
            {
                lblMessage.InnerText = "warning->Student Payment Not Available";
            }
            }
            catch { }
        }

        protected void btnReprint_Click(object sender, EventArgs e)
        {
            try
            {
                loadCollectionReprint();
            }
            catch { }
        }
        private void loadCollectionReprint()
        {
            try
            {
                DataTable dtPL;
                float total=0;
                string sqlCmd = string.Empty;
                sqlCmd = "Select FullName,ClassName,RollNo,TransactionNo,AdmCatPId, FeeCatName,PName, Amount from v_Adm_Payment_Reprint where AdmissionNo='" + txtAdmNoorTransactionNo.Text + "' AND BatchID='0' ";                
                dtPL = CRUD.ReturnTableNull(sqlCmd);
                if(dtPL.Rows.Count==0)
                {
                    sqlCmd = "Select FullName,ClassName,RollNo,TransactionNo,AdmCatPId, FeeCatName,PName, Amount from v_Adm_Payment_Reprint where TransactionNo='" + txtAdmNoorTransactionNo.Text + "' AND BatchID='0'";
                    dtPL = CRUD.ReturnTableNull(sqlCmd);
                }
                int totalRows = dtPL.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    lblMessage.InnerText = "warning->Admission Payment Not Found";
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
                    id = dtPL.Rows[x]["AdmCatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtPL.Rows[x]["Amount"].ToString() + "</td>";
                    total += float.Parse(dtPL.Rows[x]["Amount"].ToString());
                }
                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                Session["__dtPL__"] = dtPL; 
                Session["__Total__"] = total;                
                Session["__FeeCollectionRePrint__"] = divInfo;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmCollectionReprint.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadDailyTransaction(Classes.commonTask.ddMMyyyyToyyyyMMdd(txtTransactionDate.Text.Trim()), Classes.commonTask.ddMMyyyyToyyyyMMdd(txtTransactionToDate.Text.Trim()));
        }
        private DataTable loadFeeCollectionData(string PaymentFor, string BatchName,string FeeCatId, string Date,string ToDate)
        {
           

            if(PaymentFor=="openPayment")
                return CRUD.ReturnTableNull("select p.OrderNo as InvoiceNo,p.PaidAmount,p.clientMobileNo, format( p.orderDateTime,'dd-MM-yyyy HH:mm:ss') as orderDateTime,format( p.issuerPaymentDateTime,'dd-MM-yyyy HH:mm:ss') as issuerPaymentDateTime,format( p.CreatedAt,'dd-MM-yyyy HH:mm:ss') as CreatedAt,format( p.UpdatedAt,'dd-MM-yyyy HH:mm:ss') as UpdatedAt, p.issuerPaymentRefNo,p.status,c.ClassName+convert(varchar, st.Year) as BatchName,st.StudentName as FullName,st.RegNo as AdmissionNo,st.RegistrationNo,st.RollNo,g.GroupName,'' as Gender,ct.FeeCatName from PaymentInfo p left join PaymentOpenStudentInfo st on p.OpenStudentId=st.Id left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId  left join Tbl_Class_Group cg on st.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID left join Classes c on st.ClassID=c.ClassID where status='success' and format( p.CreatedAt,'yyyy-MM-dd')>='" + Date + "' and  format( p.CreatedAt,'yyyy-MM-dd') <='" + ToDate + "'  and p.FeeCatId in(" + FeeCatId+ ") order by p.FeeCatId,p.CreatedAt");
            else
            return CRUD.ReturnTableNull("select p.OrderNo as InvoiceNo,p.PaidAmount,p.clientMobileNo, format( p.orderDateTime,'dd-MM-yyyy HH:mm:ss') as orderDateTime,format( p.issuerPaymentDateTime,'dd-MM-yyyy HH:mm:ss') as issuerPaymentDateTime,format( p.CreatedAt,'dd-MM-yyyy HH:mm:ss') as CreatedAt,format( p.UpdatedAt,'dd-MM-yyyy HH:mm:ss') as UpdatedAt, p.issuerPaymentRefNo,p.status,isnull(st.BatchName,c.ClassName+convert(varchar,adm.AdmissionYear)) as BatchName,isnull(st.FullName,adm.FullName) as FullName,isnull(st.AdmissionNo,adm.AdmissionFormNo) as AdmissionNo,st.RegistrationNo,st.RollNo,isnull(st.GroupName,g.GroupName) as GroupName,isnull(st.Gender,adm.Gender) as Gender,ct.FeeCatName from PaymentInfo p left join v_CurrentStudentInfo st on p.BatchID=st.BatchID and p.StudentId=st.StudentId left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId left join Student_AdmissionFormInfo adm on p.AdmissionFormNo=adm.AdmissionFormNo left join Tbl_Class_Group cg on adm.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID left join Classes c on adm.ClassID=c.ClassID where status='success' and format( p.CreatedAt,'yyyy-MM-dd')>='" + Date + "' and  format( p.CreatedAt,'yyyy-MM-dd') <='" + ToDate + "' and p.BatchId in (select BatchId from Batchinfo where BatchName='"+ BatchName + "') and p.FeeCatId in(" + FeeCatId + ") order by p.FeeCatId,p.CreatedAt");
        }

        protected void btnSearchFeeCollections_Click(object sender, EventArgs e)
        {
            ViewFeeCollections();

        }

        protected void ddlYearFeeCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            commonTask.LoadBatchwiseFeeCatWithAll(ddlPaymentFor.SelectedValue, ddlClassFeeCollection.SelectedValue +ddlYearFeeCollection.SelectedValue, ddlCategoriesFeeCollection);
        }

        protected void ddlClassFeeCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            commonTask.LoadBatchwiseFeeCatWithAll(ddlPaymentFor.SelectedValue,ddlClassFeeCollection.SelectedValue + ddlYearFeeCollection.SelectedValue, ddlCategoriesFeeCollection);
        }

        protected void btnPrintFeeCollections_Click(object sender, EventArgs e)
        {
            
        }

        protected void ddlPaymentFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentFor.SelectedValue == "openPayment")
            {
                pnlAcademicInfo.Visible = false;
            }
            else
                pnlAcademicInfo.Visible = true;

        commonTask.LoadBatchwiseFeeCatWithAll(ddlPaymentFor.SelectedValue, ddlClassFeeCollection.SelectedValue + ddlYearFeeCollection.SelectedValue, ddlCategoriesFeeCollection);
        }
    }
}