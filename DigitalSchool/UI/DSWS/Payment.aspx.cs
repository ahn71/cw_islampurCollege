using adviitRuntimeScripting;
using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using Newtonsoft.Json.Linq;
using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class Payment : System.Web.UI.Page
    {
        DataTable dt;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry resut;
        string query = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)

            {
               ViewState["__AdmsnNo__"] = "";
                hIsTest.Visible = false;
                ViewState["__IsLivePayment__"] = "True";
                try
                {
                    string qs = Request.QueryString["for"].ToString();
                    if (qs == "test")
                    {
                        hIsTest.Visible = true;
                        ViewState["__IsLivePayment__"] = "False";
                    }

                }
                catch (Exception ex) { }


                string[] path = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/');
                ViewState["__OpenPayment__"] = "False";
                hIsOpenPayment.Visible = false;

                if (path[path.Length - 1] == "open-payment")
                {
                    hIsOpenPayment.Visible = true;
                    ViewState["__OpenPayment__"] = "True";
                    rblPaywith.SelectedValue = "ssl";
                    pnlSearcingArea.Visible = false;
                    divExistingStudentViewArea.Visible = false;
                    pnlPayment.Visible = true;
                    btnPayment.Visible = false;
                    commonTask.loadClasses(ddlClassForOpen);
                    commonTask.LoadBatchwiseFeeCat("openPayment", "", "", ddlFeeCategories);

                }
                else
                {
                    try
                    {
                        divOpenStudentInfoArea.Visible = false;
                        int AdmissionNo = int.Parse(path[path.Length - 1].ToString());
                        ckbIsAdmission.Checked = true;
                        ckbIsAdmission.Enabled = false;
                        txtRollNo.Text = AdmissionNo.ToString();
                        txtRollNo.Visible = false;
                        lblAdmissionOrRollCaption.Visible = false;

                        pnlYearClassArea.Visible = false;
                        //lblAdmissionOrRollCaption.InnerText = "Admission No";
                        btnFind.Visible = false;


                        varifiAdmissionStudent();
                    }
                    catch (Exception ex)
                    {
                        commonTask.loadYearFromBatch(ddlYear);
                        commonTask.loadClassFromBatch(ddlBatch);

                    }
                }


                // BatchEntry.GetDropdownlist(ddlBatch, "True");

            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            hAlreadyPaid.Visible = false;
            if (ckbIsAdmission.Checked)
            {
                varifiAdmissionStudent();
                api_intigration();
            }
                
            else
            if (varifiEmp(ddlBatch.SelectedValue + ddlYear.SelectedValue))
            {
                commonTask.LoadBatchwiseFeeCat("regular", ddlBatch.SelectedValue + ddlYear.SelectedValue, ViewState["__ClsGrpId__"].ToString(), ddlFeeCategories);
                pnlPayment.Visible = true;
                setPyementMedia();

                api_intigration();
            }

            else
                pnlPayment.Visible = false;
          //  api_intigration();
        }
        private void setPyementMedia()
        {
            if (ViewState["__OpenPayment__"].ToString() == "True")
                return;
            //if (lblClass.Text.Trim().Contains("Eleven") || lblClass.Text.Trim().Contains("Twelve"))
            //{
            //    rblPaywith.SelectedValue = "nagad";
            //    btnPayment.Visible = true;
            //    btnPaymentSSL.Visible = false;
            //}
            //else
            //{
            rblPaywith.SelectedValue = "ssl";
            btnPayment.Visible = false;
            btnPaymentSSL.Visible = true;
            // }
        }



        protected void ddlFeeCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadParticularDetails();
        }
        private bool varifiEmp(string BatchName)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select AdmissionNo,BatchID, StudentId,FullName,FathersName,RollNo,ClassID,GroupName,ImageName,ClsGrpId,Mobile from v_CurrentStudentInfo where BatchName='" + BatchName + "' and (AdmissionNo=" + txtRollNo.Text.Trim() + " Or PSCRollNo=" + txtRollNo.Text.Trim() + " or RegistrationNo='" + txtRollNo.Text.Trim() + "')");
            if (dt != null && dt.Rows.Count > 0)
            {
                string imgName = (dt.Rows[0]["ImageName"].ToString() == "") ? "" : dt.Rows[0]["ImageName"].ToString();
                string imageInfo = "<img class='img-responsive' style='width:80px;height:100px;' src='../../Images/profileImages/" + imgName + "' alt='...' />";
                divImage.Controls.Add(new LiteralControl(imageInfo));
                ViewState["__BatchID__"] = dt.Rows[0]["BatchID"].ToString();
                ViewState["__StudentId__"] = dt.Rows[0]["StudentId"].ToString();

                ViewState["__AdmissionNo__"] = dt.Rows[0]["AdmissionNo"].ToString();

                ViewState["__AdmsnNo__"] = ViewState["__AdmissionNo__"].ToString();

                ViewState["__ClassID__"] = dt.Rows[0]["ClassID"].ToString();

                ViewState["__StudentAdmissionNo__"] = dt.Rows[0]["AdmissionNo"].ToString();
                ViewState["__StudentMobile__"] = dt.Rows[0]["Mobile"].ToString();
                ViewState["__StudentName__"] = dt.Rows[0]["FullName"].ToString();
                ViewState["__StudentEmail__"] = "";

                lblStudentName.Text = dt.Rows[0]["FullName"].ToString();
                lblFathersName.Text = dt.Rows[0]["FathersName"].ToString();
                lblClassRoll.Text = dt.Rows[0]["RollNo"].ToString();
                lblGroup.Text = dt.Rows[0]["GroupName"].ToString();
                ViewState["__ClsGrpId__"] = dt.Rows[0]["ClsGrpId"].ToString();
                lblYear.Text = ddlYear.SelectedValue;
                lblClass.Text = ddlBatch.SelectedItem.Text;
                lblAdmissionNo.Text = txtRollNo.Text;
                lblMsg.Text = "Verified!";
                return true;
            }
            lblMsg.Text = "Invalid!";
            return false;
        }
        private bool varifiAdmissionStudent()
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select AdmissionFormNo, FullName,FathersName,c.ClassName, AdmissionYear,adm.ClsGrpID,g.GroupName,ImageName,adm.Mobile,adm.ClassID from Student_AdmissionFormInfo adm left join Classes c on adm.ClassID=c.ClassID left join Tbl_Class_Group cg on adm.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID Where AdmissionFormNo=" + txtRollNo.Text.Trim());
            if (dt != null && dt.Rows.Count > 0)
            {

                string imgName = (dt.Rows[0]["ImageName"].ToString() == "") ? "" : dt.Rows[0]["ImageName"].ToString();
                string imageInfo = "<img class='img-responsive' style='width:80px;height:100px;' src='../../Images/studentAdmissionImages/" + imgName + "' alt='...' />";
                divImage.Controls.Add(new LiteralControl(imageInfo));

                ViewState["__AdmissionFormNo__"] = dt.Rows[0]["AdmissionFormNo"].ToString();
                ViewState["__AdmsnNo__"] = ViewState["__AdmissionFormNo__"].ToString();

                ViewState["__StudentAdmissionNo__"] = ViewState["__AdmissionFormNo__"].ToString();
                ViewState["__StudentMobile__"] = dt.Rows[0]["Mobile"].ToString();
                ViewState["__StudentName__"] = dt.Rows[0]["FullName"].ToString();
                ViewState["__StudentEmail__"] = "";

                ViewState["__ClassID__"] = dt.Rows[0]["ClassID"].ToString();

                lblStudentName.Text = dt.Rows[0]["FullName"].ToString();
                lblFathersName.Text = dt.Rows[0]["FathersName"].ToString();
                lblClassRoll.Text = "";
                lblGroup.Text = dt.Rows[0]["GroupName"].ToString();
                ViewState["__ClsGrpId__"] = dt.Rows[0]["ClsGrpId"].ToString();
                lblYear.Text = dt.Rows[0]["AdmissionYear"].ToString();
                lblClass.Text = dt.Rows[0]["ClassName"].ToString();
                lblAdmissionNo.Text = dt.Rows[0]["AdmissionFormNo"].ToString();
                lblMsg.Text = "Verified!";
                pnlPayment.Visible = true;
                setPyementMedia();
                ddlFeeCategories.Enabled = false;
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select cat.FeeCatId,cat.BatchId from FeesCategoryInfo cat left join DateOfPayment dp on cat.FeeCatId=dp.FeeCatId where  PaymentFor='admission' and DateOfEnd>='" + ServerTimeZone.GetBangladeshNowDate("yyyy-MM-dd") + "' and BatchId in(select BatchId from BatchInfo where BatchName='" + lblClass.Text + lblYear.Text + "') and (ClsGrpId=" + ViewState["__ClsGrpId__"].ToString() + " or ClsGrpId=0)");

                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["__BatchID__"] = dt.Rows[0]["BatchId"].ToString();
                    commonTask.LoadBatchwiseFeeCat("admission", lblClass.Text + lblYear.Text, ViewState["__ClsGrpId__"].ToString(), ddlFeeCategories);
                    ddlFeeCategories.SelectedValue = dt.Rows[0]["FeeCatId"].ToString();
                    loadParticularDetails();
                }
                else
                    divParticularCategoryList.Controls.Add(new LiteralControl("<div class='noData'>Admission Category Not Found!</div><div class='dataTables_wrapper'><div class='head'></div></div>"));
                api_intigration();
                return true;
            }
            lblMsg.Text = "Invalid!";
            return false;
        }

        private void loadParticularDetails()
        {
            try
            {

                hPreviousDue.Visible = false;
                dt = CRUD.ReturnTableNull("Select PName, Amount, isnull(StoreNameKey,'islampurcollegeedubd') as StoreNameKey from v_FeesCatDetails where FeeCatId='" + ddlFeeCategories.SelectedValue + "' ");
                ViewState["__ParticularDetails__"] = dt;
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                ViewState["__StoreNameKey__"] = dt.Rows[0]["StoreNameKey"].ToString();
                divInfo = "<h3 class='text-primary'>Payment Information</h3> ";
                divInfo = " <table id='tblParticularCategory' class='table table-bordered table-striped' > ";
                divInfo += "<thead>";
                divInfo += "<tr class='bg-success'>";
                divInfo += "<th class='text-center'>Particular Name</th>";
                divInfo += "<th class='numeric text-center'>Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                int id = 0;
                double total = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = x + 1;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric text-right'>" + dt.Rows[x]["Amount"].ToString() + "</td>";
                    total += int.Parse(dt.Rows[x]["Amount"].ToString());
                }
                ViewState["__Amount_"] = total;
                ViewState["__Discount__"] = 0;
                if (rblPaywith.SelectedValue == "ssl")
                {
                    ViewState["__OnlineCharge_"] = 0;
                    ViewState["__OnlineChargePer_"] = 0;
                    //ViewState["__OnlineCharge_"] = Math.Round(total * Classes.commonTask.OnlineChargPer_SSL, 2);
                    //ViewState["__OnlineChargePer_"] = Math.Round(100 * Classes.commonTask.OnlineChargPer_SSL, 2);

                }
                else
                {
                    ViewState["__OnlineCharge_"] = Math.Round(total * Classes.commonTask.OnlineChargPer_Nagad, 2);

                    ViewState["__OnlineChargePer_"] = Math.Round(100 * Classes.commonTask.OnlineChargPer_Nagad, 2);
                }



                ViewState["__TotalAmount_"] = Math.Round(total + double.Parse(ViewState["__OnlineCharge_"].ToString()), 0);



                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "<tr class='bg-info' id='r_0'><th class='text-right'>Sub Total</th><th class='numeric text-right'>" + total + "</th>";
                divInfo += "<tr id='r_0'><th class='text-right'>Discount</th><th class='numeric text-right'>" + ViewState["__Discount__"].ToString() + "</th>";
                if (rblPaywith.SelectedValue != "ssl")
                    divInfo += "<tr id='r_0'><th class='text-right'>Online Charge (" + ViewState["__OnlineChargePer_"].ToString() + " %)	</th><th class='numeric text-right'>" + ViewState["__OnlineCharge_"].ToString() + "</th>";

                divInfo += "<tr class='bg-info' id='r_0'><th class='text-right'>Total</th><th class='numeric text-right'>" + ViewState["__TotalAmount_"].ToString() + "</th>";
                divInfo += "</tfoot>";
                divInfo += "</table>";
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                if (IsPaid())
                    return;
                if (!IsExpired())
                    setPyementMedia();


            }
            catch { }
        }
        private bool IsPaid()
        {
            if (ViewState["__OpenPayment__"].ToString() == "True")
                return false;
            string OrderNo = "";
            if (ckbIsAdmission.Checked)
                OrderNo = commonTask.IsPaidReturnOrderNoByAdmissionFormNo(ddlFeeCategories.SelectedValue, ViewState["__AdmissionFormNo__"].ToString());
            else
                OrderNo = commonTask.IsPaidReturnOrderNo(ddlFeeCategories.SelectedValue, ViewState["__StudentId__"].ToString());
            if (OrderNo == "")
            {
                hAlreadyPaid.Visible = false;
                return false;
            }
            else
            {
                btnPayment.Visible = false;
                btnPaymentSSL.Visible = false;
                hAlreadyPaid.Visible = true;
                hPreviousDue.Visible = false;
                return true;
            }
        }
        private bool IsExpired()
        {

            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select convert(varchar(10), DateOfStart,120) as DateOfStart,convert(varchar(10), DateOfEnd,120) as DateOfEnd from DateOfPayment where FeeCatId=" + ddlFeeCategories.SelectedValue);
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime DateOfEnd = DateTime.Parse(dt.Rows[0]["DateOfEnd"].ToString());
                DateTime dateTimeNow = DateTime.Parse(ServerTimeZone.GetBangladeshNowDate("yyyy-MM-dd"));
                if (DateOfEnd < dateTimeNow)
                {
                    hPreviousDue.InnerText = "This Payment is Expired on " + DateOfEnd.ToString("dd-MM-yyyy") + "!";
                    hPreviousDue.Visible = true;
                    hAlreadyPaid.Visible = false;
                    btnPayment.Visible = false;
                    btnPaymentSSL.Visible = false;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
                return true;


        }
        private bool hasPreviousDue()
        {
            return false;// Validation is ignored. Date: 29-08-2022

            if (ViewState["__OpenPayment__"].ToString() == "True" || ckbIsAdmission.Checked)
                return false;

            // Recommended students are allowed  
            //switch (ViewState["__AdmissionNo__"].ToString())
            //{
            //    case "20200031":
            //        return false;
            //    case "20200150":
            //        return false;
            //    case "20210062":
            //        return false;
            //    case "20220584":
            //        return false;
            //    case "20200337":
            //        return false;
            //    case "20200343":
            //        return false;
            //    case "20200285":
            //        return false;
            //    case "20200291":
            //        return false;
            //    case "20220385":
            //        return false;
            //    case "20200267":
            //        return false;
            //    default:
            //        break;

            //}

            if (ViewState["__AdmissionNo__"].ToString() == "")
                dt = new DataTable();
            dt = commonTask.getFeeCatIdBatchwiseFeeCat(ddlBatch.SelectedValue + ddlYear.SelectedValue, ViewState["__ClsGrpId__"].ToString(), ddlFeeCategories.SelectedValue);
            string dueCat = "";
            if (dt.Rows.Count == 0)
                return false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string OrderNo = commonTask.IsPaidReturnOrderNo(dt.Rows[i]["FeeCatId"].ToString(), ViewState["__StudentId__"].ToString());
                if (OrderNo == "")
                {
                    dueCat += ", " + dt.Rows[i]["FeeCatName"].ToString();
                }

            }
            if (dueCat == "")
            {
                hPreviousDue.Visible = false;
                return false;
            }

            else
            {
                //hPreviousDue.InnerText = "Please, pay your previous due("+ dueCat.Remove(0,1) + ") first!";
                hPreviousDue.InnerText = "এই ফি প্রদা‌নের জন্য পূর্ব‌ের ব‌কেয়া(" + dueCat.Remove(0, 1) + ") প‌রিশ‌োধ করুন! (You can not pay this bill before making the payment for due amount of " + dueCat.Remove(0, 1) + "!)";
                hPreviousDue.Visible = true;
                hAlreadyPaid.Visible = false;
                return true;
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            if (!IsPaid() && !hasPreviousDue() && !IsExpired())
                SaveInvoice("nagad");
        }
        private bool SaveInvoiceDetails(string OrderID, string OrderNo, string Particular, string Amount)
        {
            return CRUD.ExecuteQuery(@"INSERT INTO [dbo].[PaymentInfoDetails]
           ([OrderID]
           ,[OrderNo]
           ,[Particular]
           ,[ParticularAmount]
           ,[Status])
            VALUES
           (" + OrderID + ",'" + OrderNo + "','" + Particular + "'," + Amount + ",1)");

        }
        private int SaveOpenStudentInfo()
        {
            try
            {
                if (ddlClassForOpen.SelectedIndex < 1)
                {
                    lblMessage.InnerText = "warning-> Please, Select Class!";
                    ddlClassForOpen.Focus();
                    return 0;
                }
                if (txtYear.Text.Trim().Length != 4)
                {
                    lblMessage.InnerText = "warning-> Please, Enter valid Session!";
                    lblYear.Focus();
                    return 0;
                }

                int Year = 0;
                try
                {
                    Year = int.Parse(txtYear.Text.Trim());
                }
                catch (Exception ex)
                {
                    lblMessage.InnerText = "warning-> Please, Enter valid Year!";
                    lblYear.Focus();
                    return 0;
                }
                if (ddlGroupForOpen.SelectedIndex < 1)
                {
                    lblMessage.InnerText = "warning-> Please, Select Group!";
                    ddlGroupForOpen.Focus();
                    return 0;
                }
                if (txtRegNo.Text.Trim().Length == 0)
                {
                    lblMessage.InnerText = "warning-> Please, Enter Registration Number!";
                    txtRegNo.Focus();
                    return 0;
                }
                if (txtClassRoll.Text.Trim().Length == 0)
                {
                    lblMessage.InnerText = "warning-> Please, Enter Class Roll!";
                    txtClassRoll.Focus();
                    return 0;
                }

                if (txtStudentName.Text.Trim().Length == 0)
                {
                    lblMessage.InnerText = "warning-> Please, Enter Full Name!";
                    txtStudentName.Focus();
                    return 0;
                }
                if (txtFathersName.Text.Trim().Length == 0)
                {
                    lblMessage.InnerText = "warning-> Please, Enter Father's Name!";
                    txtFathersName.Focus();
                    return 0;
                }
                if (txtStudentMobileNo.Text.Trim().Length != 11)
                {
                    lblMessage.InnerText = "warning-> Please, Enter valid Mobile No!";
                    txtStudentMobileNo.Focus();
                    return 0;
                }
                if (ddlFeeCategories.SelectedIndex < 1)
                {
                    lblMessage.InnerText = "warning-> Please, Select Fee Category!";
                    ddlFeeCategories.Focus();
                    return 0;
                }
                ViewState["__StudentMobile__"] = txtStudentMobileNo.Text.Trim();
                ViewState["__StudentName__"] = txtStudentName.Text.Trim();
                ViewState["__StudentEmail__"] = "";
                ViewState["__StudentAdmissionNo__"] = txtClassRoll.Text.Trim();
                query = @"INSERT INTO [dbo].[PaymentOpenStudentInfo]
           ([MobileNo]
           ,[StudentName]
           ,[ClassId]
           ,[ClsGrpId]
           ,[RegNo]
           ,[RollNo]           
           ,[Year]
           ,[FathersName]
           ,[FeeCatId])
     VALUES
           ('" + txtStudentMobileNo.Text.Trim() + "',N'" + txtStudentName.Text.Trim() + "'," + ddlClassForOpen.SelectedValue + "," + ddlGroupForOpen.SelectedValue + ",N'" + txtRegNo.Text.Trim() + "',N'" + txtClassRoll.Text.Trim() + "'," + Year.ToString() + ",N'" + txtFathersName.Text.Trim() + "'," + ddlFeeCategories.SelectedValue + "); SELECT SCOPE_IDENTITY()";
                int result = CRUD.GetMaxID(query);
                if (result == 0)
                    lblMessage.InnerText = "warning-> Student's Info May Be Wrong!";
                return result;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error-> " + ex.Message;
                return 0;
            }
        }
        private void SaveInvoice(string PaymentType)
        {
            try
            {


                int OpenStudentId = 0;
                if (ViewState["__OpenPayment__"].ToString() == "True")
                {
                    OpenStudentId = SaveOpenStudentInfo();
                    if (OpenStudentId == 0) return;

                }
                string store_name = ViewState["__StoreNameKey__"].ToString();
                string student_name = ViewState["__StudentName__"].ToString();
                string student_mobile = ViewState["__StudentMobile__"].ToString();
                string student_email = ViewState["__StudentEmail__"].ToString();
                string student_id = ViewState["__StudentAdmissionNo__"].ToString();

                string OrderNo = generateInvoiceNo();
                int OrderSL = int.Parse(OrderNo.Remove(0, 10));
                if (ViewState["__OpenPayment__"].ToString() == "True")
                    query = @"INSERT INTO [dbo].[PaymentInfo]
            ([FeeCatId],[OrderNo]
           ,[Amount],[Discount],[OnlineCharge],[OnlineChargePer],[TotalAmount]
           ,[IsPaid]
           ,[IsActive]
           ,[CreatedAt]          
           ,[OrderSL],[PaymentType],[OpenStudentId],[StoreNameKey])
     VALUES
           ('" + ddlFeeCategories.SelectedValue + "','" + OrderNo + "','" + ViewState["__Amount_"].ToString() + "','" + ViewState["__Discount__"].ToString() + "','" + ViewState["__OnlineCharge_"].ToString() + "','" + ViewState["__OnlineChargePer_"].ToString() + "','" + ViewState["__TotalAmount_"].ToString() + "',0,1,'" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:mm:ss") + "'," + OrderSL + ",'" + PaymentType + "'," + OpenStudentId + ",'" + store_name + "'); SELECT SCOPE_IDENTITY()";
                else if (ckbIsAdmission.Checked)
                    query = @"INSERT INTO [dbo].[PaymentInfo]
            ([BatchID],[FeeCatId],[OrderNo]
           ,[Amount],[Discount],[OnlineCharge],[OnlineChargePer],[TotalAmount]
           ,[IsPaid]
           ,[IsActive]
           ,[CreatedAt]          
           ,[OrderSL],[PaymentType],[AdmissionFormNo],[StoreNameKey])
     VALUES
           ('" + ViewState["__BatchID__"].ToString() + "','" + ddlFeeCategories.SelectedValue + "','" + OrderNo + "','" + ViewState["__Amount_"].ToString() + "','" + ViewState["__Discount__"].ToString() + "','" + ViewState["__OnlineCharge_"].ToString() + "','" + ViewState["__OnlineChargePer_"].ToString() + "','" + ViewState["__TotalAmount_"].ToString() + "',0,1,'" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:mm:ss") + "'," + OrderSL + ",'" + PaymentType + "'," + ViewState["__AdmissionFormNo__"].ToString() + ",'" + store_name + "'); SELECT SCOPE_IDENTITY()";
                else
                    query = @"INSERT INTO [dbo].[PaymentInfo]
            ([StudentId],[BatchID],[FeeCatId],[OrderNo]
           ,[Amount],[Discount],[OnlineCharge],[OnlineChargePer],[TotalAmount]
           ,[IsPaid]
           ,[IsActive]
           ,[CreatedAt]          
           ,[OrderSL],[PaymentType],[StoreNameKey])
     VALUES
           ('" + ViewState["__StudentId__"].ToString() + "','" + ViewState["__BatchID__"].ToString() + "','" + ddlFeeCategories.SelectedValue + "','" + OrderNo + "','" + ViewState["__Amount_"].ToString() + "','" + ViewState["__Discount__"].ToString() + "','" + ViewState["__OnlineCharge_"].ToString() + "','" + ViewState["__OnlineChargePer_"].ToString() + "','" + ViewState["__TotalAmount_"].ToString() + "',0,1,'" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:mm:ss") + "'," + OrderSL + ",'" + PaymentType + "','" + store_name + "'); SELECT SCOPE_IDENTITY()";
                int OrderID = CRUD.GetMaxID(query);
                if (OrderID > 0)
                {
                    dt = new DataTable();
                    dt = (DataTable)ViewState["__ParticularDetails__"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                            SaveInvoiceDetails(OrderID.ToString(), OrderNo, dt.Rows[i]["PName"].ToString(), dt.Rows[i]["Amount"].ToString());
                    }

                    //Response.Redirect("https://blaisetyre.cwprojects.xyz/?amount=" + ViewState["__TotalAmount_"].ToString() + "&invoice=" + OrderNo + "&payment_type=" + PaymentType + "&store_name=" + store_name + "&student_name=" + student_name + "&student_mobile=" + student_mobile + "&student_email=" + student_email + "&student_id=" + student_id + "");

                    //Response.Redirect("https://app.idesk360.live/securepayment/?amount=" + ViewState["__TotalAmount_"].ToString() + "&invoice=" + OrderNo + "&payment_type=" + PaymentType + "&store_name=" + store_name + "&student_name=" + student_name + "&student_mobile=" + student_mobile + "&student_email=" + student_email + "&student_id=" + student_id + "");

                    //if (ViewState["__IsLivePayment__"].ToString() == "True")
                    //{
                    //    Response.Redirect("https://www.kazigroup.net/securepayment/?amount=" + ViewState["__TotalAmount_"].ToString() + "&invoice=" + OrderNo + "&payment_type=" + PaymentType + "&store_name=" + store_name + "&student_name=" + student_name + "&student_mobile=" + student_mobile + "&student_email=" + student_email + "&student_id=" + student_id + "");

                    //}
                    //else if (ViewState["__IsLivePayment__"].ToString() == "False")
                    //{
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect(baseUrl + "UI/PaymentMethod/SSLCommerzInfos/Default.aspx?amount=" + ViewState["__TotalAmount_"].ToString() + "&invoice=" + OrderNo + "&payment_type=" + PaymentType + "&store_name=" + store_name + "&student_name=" + student_name + "&student_mobile=" + student_mobile + "&student_email=" + student_email + "&student_id=" + student_id + "");
                    //}



                }
            }
            catch (Exception ex) { }
        }

        private string generateInvoiceNo()
        {
            try
            {
                DateTime today = TimeZoneBD.getCurrentTimeBD();
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ISNULL( max(OrderSL),0) as OrderSL  from PaymentInfo where  convert(varchar(10),CreatedAt,120) ='" + today.ToString("yyyy-MM-dd") + "'");
                string OrderSL = (int.Parse(dt.Rows[0]["OrderSL"].ToString()) + 1).ToString();

                if (OrderSL.Length < 4)
                {
                    if (OrderSL.Length == 3)
                        OrderSL = "0" + OrderSL;
                    else if (OrderSL.Length == 2)
                        OrderSL = "00" + OrderSL;
                    else if (OrderSL.Length == 1)
                        OrderSL = "000" + OrderSL;
                }

                string OrderNo = "ICCW" + today.ToString("yyMMdd") + OrderSL;
                return OrderNo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        protected void btnPaymentSSL_Click(object sender, EventArgs e)
        {
            //if (ckbIsAdmission.Checked)
            //{
            //    api_intigration();

            //    if (ViewState["__status__"].ToString() == "failed")
            //    {
            //        ViewState["__status__"] = "failed";
            //        ddlCatagory.Visible = false;
            //        btnPaymentSSL.Visible = false;
            //        btnSubsreicption.Attributes["href"] = "https://websupportbd.com/subscription/?url_adm_no=" + ViewState["__AdmsnNo__"].ToString();
            //        btnSubsreicption.Attributes["target"] = "_blank";
            //        subscriptionMessage.Visible = true;
            //        btnSubsreicption.Visible = true;
            //    }
            //}

            //else
            //{
                if (!IsPaid() && !hasPreviousDue())
                    SaveInvoice("ssl");
            //}
            
        }

        protected void ckbIsAdmission_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIsAdmission.Checked)
            {
                pnlYearClassArea.Visible = false;
                lblAdmissionOrRollCaption.InnerText = "Admission Form No";
            }
            else
            {
                pnlYearClassArea.Visible = true;
                lblAdmissionOrRollCaption.InnerText = "Admission Form No/SSC/HSC Roll";
            }
        }

        protected void rblPaywith_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadParticularDetails();
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            if (!IsPaid() && !hasPreviousDue())
                SaveInvoice(rblPaywith.SelectedValue);
        }

        protected void ddlClassForOpen_SelectedIndexChanged(object sender, EventArgs e)
        {
            commonTask.loadGroupsByClass(ddlGroupForOpen, ddlClassForOpen.SelectedValue);
        }
        public string getResponse(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //string urlTest = "https://www.websupportbd.com/subscription/api/payments/?admission_no=102031&class_id=250";
            //string ppp= "https://www.websupportbd.com/subscription/api/payments/?admission_no=20220632&class_id=232"

            WebRequest webrequest = WebRequest.Create(url);
            webrequest.Method = "GET";

            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebResponse = (HttpWebResponse)webrequest.GetResponse();

                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    string response = sr.ReadToEnd();
                    sr.Close();
                    return response;
                }

            }
            catch (WebException ex)
            {

                Console.WriteLine("Error: " + ex.Message);
                return "Api Error";
            }
        }
        public void  api_intigration()
        {
            ViewState["__status__"] = "";
            string ffff = ViewState["__ClassID__"].ToString();
            string url = "https://www.websupportbd.com/subscription/api/payments/?admission_no=" + ViewState["__AdmsnNo__"].ToString() + "&class_id=" + ViewState["__ClassID__"].ToString();
            var respose=getResponse(url);
        
                JArray jsonArray = JArray.Parse(respose);
                if (jsonArray.Count > 0)
                {
                    JObject firstItem = (JObject)jsonArray[0];
                    if(firstItem["status"]?.ToString()== "404")
                    {
                        ViewState["__status__"] = "failed";
                        ddlCatagory.Visible = false;
                        btnPaymentSSL.Visible = false;
                        btnSubsreicption.Attributes["href"] = "https://websupportbd.com/subscription/?url_adm_no=" + ViewState["__AdmsnNo__"].ToString();
                        btnSubsreicption.Attributes["target"] = "_blank";
                        subscriptionMessage.Visible = true;
                        btnSubsreicption.Visible = true;
                      
                  
                    }
                    else
                    {
                        string admissionNo = firstItem["admission_no"]?.ToString();
                        string studentName = firstItem["student_name"]?.ToString();
                        string studentPhone = firstItem["student_phone"]?.ToString();
                        string totalAmount = firstItem["total_amount"]?.ToString();
                        ddlCatagory.Visible = true;
                        btnPaymentSSL.Visible = true;
                        subscriptionMessage.Visible = false;
                        btnSubsreicption.Visible = false;
                        
                    }



               
                }
            

        }

    }
}