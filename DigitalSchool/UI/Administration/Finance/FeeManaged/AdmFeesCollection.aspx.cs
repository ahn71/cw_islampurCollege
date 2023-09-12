using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.Finance;
using DS.BLL.ManagedClass;
using DS.BLL.ManageUser;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.Finance;
using DS.PropertyEntities.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class AdmFeesCollection : System.Web.UI.Page
    {
        List<CurrentStdEntities> grpList;
        List<CurrentStdEntities> classIDList;
        
        bool result=false;
        public string classId;
        private static Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {           
            lblMessage.InnerText = string.Empty;           
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "AdmFeesCollection.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    string admissionNO = Request.QueryString["admissionNo"];
                    classId = Request.QueryString["classId"];
                    CurrentStdEntry.GetDropDownList(ddlAdmissionNo);
                    ClassEntry.GetEntitiesData(ddlClass);
                    btnClose.Visible = true;
                    if (classId != null)
                    {
                        for (int i = 0; i < ddlAdmissionNo.Items.Count; i++)
                        {
                            if (ddlAdmissionNo.Items[i].Text == admissionNO)
                            {
                                ddlAdmissionNo.SelectedIndex = i;
                                break;
                            }
                        }                  
                        ddlClass.SelectedValue = classId;
                        hdfstdId.Value = ddlAdmissionNo.SelectedValue;
                        AdmFeesCategoresEntry.GetDropDownList(dlFeesCategory, ddlClass.SelectedValue);
                        loadParticularCategory();
                        ddlAdmissionNo.Enabled = false;
                        ddlClass.Enabled = false;
                        btnClose.Visible = true;
                        string Page = Request.QueryString["Page"];
                        if (Page.Equals("stdadmdetails"))
                        {
                            btnClose.PostBackUrl = "~/UI/Academic/Students/AdmissionDetails.aspx";
                        }
                        else
                        {
                            btnClose.PostBackUrl = "~/UI/Academic/Students/StdAdmission.aspx";
                        }
                    }                   
            }
        }
        protected void ddlAdmissionNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentStdEntry currentstdent=new CurrentStdEntry();
            classIDList = currentstdent.GetEntitiesData(int.Parse(ddlAdmissionNo.SelectedValue));
            int clsId = 0;
            if (classIDList != null)
            {
                clsId = classIDList[0].ClassID;
            }
            ddlClass.SelectedValue = clsId.ToString();
            AdmFeesCategoresEntry.GetDropDownList(dlFeesCategory, clsId.ToString());
            loadParticularCategory();
            lblUserNamePassword.Text = "";
        }

        protected void dlFeesCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadParticularCategory();
        }
        DataTable dtPL;   //PL=Particular List
        float total=0;
        private void loadParticularCategory()
        {
            try
            {
                string sqlCmd = string.Empty;
                if (string.IsNullOrEmpty(sqlCmd))
                    sqlCmd = "Select AdmCatPId, FeeCatName,PName, Amount from v_Adm_FeesCatDetails where AdmFeeCatId='" + dlFeesCategory.SelectedValue + "' ";
                Session["__FeeCatName__"] = dlFeesCategory.Text;
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
                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Discount :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input  id='Discount' value='0' type='text' onchange='Commonfunction(this);' autocomplete='off' onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\" class='input controlLength textalign'/></td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Payble :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input disabled='true'  id='Payble' value='" + total + "' type='text' class='input controlLength textalign'/></td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Paid :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input  id='Paid' value='0' type='text' onchange='Commonfunction(this);' autocomplete='off' onkeyup=\"this.onchange();\" onpaste=\"this.onchange();\" oninput=\"this.onchange();\" class='input controlLength textalign'/></td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Due :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> <input disabled='true' id='Due' value='" + total + "' type='text' class='input controlLength textalign'/></td>";
                divInfo += "</tr>";


                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                Session["__Total__"] = total;
                Session["__Discount__"] ="0";
                Session["__Payble__"] = total;
                Session["__Paid__"] = "0";
                Session["__Due__"] = total;
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                Session["__FeeCollectionReport__"] = divInfo;
            }
            catch { }
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            if (saveStudentPayment())
            {                
                CurrentStdEntry EntryStudent = new CurrentStdEntry();
                if (classId == null)
                {
                    result = EntryStudent.UpdateStdStatus(ddlAdmissionNo.SelectedValue, "True");
                }
                else
                {
                    result = EntryStudent.UpdateStdStatus(hdfstdId.Value, "True");
                }
                if (result)
                {
                    if (classId == null)
                    {
                        result = EntryStudent.UpdateCurrentStudentActive(ddlAdmissionNo.SelectedValue, "True");
                    }
                    else
                    {
                        result = EntryStudent.UpdateCurrentStudentActive(hdfstdId.Value, "True");
                    }
                    if (result)
                    {
                        SaveUserAccount();                       
                        AdmFeesCategoresEntry AdmFee = new AdmFeesCategoresEntry();
                        DataTable dt = new DataTable();
                        if (classId == null)
                        {
                            dt = AdmFee.AdmMoneyReceiptReportData(ddlAdmissionNo.SelectedValue);
                        }
                        else
                        {
                            dt = AdmFee.AdmMoneyReceiptReportData(hdfstdId.Value);
                        }
                        CurrentStdEntry.GetDropDownList(ddlAdmissionNo);
                       if (dt.Rows.Count > 0)
                       {
                           Session["__AdmMoneyReceipt__"] = dt;
                           ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AdmMoneyReceipt');", true);  //Open New Tab for Sever side code
                       }
                            
                       // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmCollectionReport.aspx');", true);  //Open New Tab for Sever side code
                    }
                }                
            }
        }
        private Boolean saveStudentPayment()
        {
            try
            {
                CurrentStdEntry clsGrp = new CurrentStdEntry();
                if (grpList == null)
                {
                    grpList = clsGrp.GetEntitiesData();
                }
                if(classId==null)
                grpList=grpList.FindAll(c=>c.StudentID==int.Parse(ddlAdmissionNo.SelectedValue));
                else grpList = grpList.FindAll(c => c.StudentID == int.Parse(hdfstdId.Value)); 
                SqlCommand cmd = new SqlCommand("saveStudentPayment", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", grpList[0].StudentID);
                cmd.Parameters.AddWithValue("@ShiftID", grpList[0].ConfigId);
                cmd.Parameters.AddWithValue("@BatchId", grpList[0].BatchID);
                cmd.Parameters.AddWithValue("@ClassID", grpList[0].ClassID);
                cmd.Parameters.AddWithValue("@ClsGrpID", grpList[0].ClsGrpID);
                cmd.Parameters.AddWithValue("@ClsSecID", grpList[0].ClsSecID);
                cmd.Parameters.AddWithValue("@RollNo", grpList[0].RollNo);
                cmd.Parameters.AddWithValue("@FeeCatId",dlFeesCategory.SelectedValue);                
                cmd.Parameters.AddWithValue("@DateOfPayment", TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@PayStatus", "1");
                cmd.Parameters.AddWithValue("@AmountPaid", Session["__Paid__"].ToString());
                cmd.Parameters.AddWithValue("@FineAmount", "0");
                if(float.Parse(Session["__Discount__"].ToString())>0)
                {
                    cmd.Parameters.AddWithValue("@DiscountStatus", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DiscountStatus", "0");
                }
                
                cmd.Parameters.AddWithValue("@DiscountTK", Session["__Discount__"].ToString());
                cmd.Parameters.AddWithValue("@GrandTotal", Session["__Total__"].ToString());
                cmd.Parameters.AddWithValue("@DueAmount", Session["__Due__"].ToString());
                sqlDB.connectDB();
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    int tNO = TransactionEntry.GetMaxID();
                    SaveTransaction(tNO, int.Parse(grpList[0].StudentID.ToString()));
                    lblMessage.InnerText = "success->Successfully saved";
                    Session["__FullName__"] ="Name: "+ grpList[0].FullName.ToString();
                    Session["__Roll__"] = "Roll: "+grpList[0].RollNo.ToString();
                    Session["__Class__"] = "Class: "+grpList[0].ClassName.ToString();
                    Session["__FeeCategory__"] ="Category: "+ dlFeesCategory.SelectedItem.Text;
                    Session["__TransactionNO__"] = "Transaction No: " + tNO;
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
        private Boolean SaveTransaction(int trNo,int stdID)
        {
            using (TransactionEntities entities = GetFormData(trNo,stdID))
            {
                TransactionEntry tEntry = new TransactionEntry();
                tEntry.AddEntities = entities;
                return tEntry.Insert();
            }
          
        }
        private TransactionEntities GetFormData(int trNo,int stdID)
        {
            TransactionEntities trEntry = new TransactionEntities();
            trEntry.TransactionNo = trNo;
            trEntry.Purpose = "Admission Fee";
            trEntry.StudentID = stdID;
            trEntry.OthersID = "";
            trEntry.ReferenceID = "";
            trEntry.TransactionDate = TimeZoneBD.getCurrentTimeBD();
            return trEntry;
        }
        private void SaveUserAccount()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = AdmStdInfoEntry.GetAdmStudent(ddlAdmissionNo.SelectedValue);
                string FullName = dt.Rows[0].ItemArray[0].ToString();
                string[] Name = FullName.Split(' ');
                string FirstName = "", LastName = "";
                if (Name.Length == 1) FirstName = Name[0];
                else if (Name.Length == 2) { FirstName = Name[0]; LastName = Name[1]; }
                else if (Name.Length > 2) { for (int i = 0; i < Name.Length; i++) { if (Name.Length != i + 1) FirstName += Name[i]; else LastName = Name[i]; } }
                StringBuilder password = new StringBuilder();
                for (int i = 1; i <= 2; i++)
                {
                    char capitalLeter = GenerateChar(FirstName.ToUpper());
                    InsertAtRandomPositons(password, capitalLeter);
                }
                for (int i = 1; i <= 2; i++)
                {
                    char smallLetter = GenerateChar(FirstName.ToLower());
                    InsertAtRandomPositons(password, smallLetter);
                }
                char digit = GenerateChar(ddlAdmissionNo.SelectedItem.Text);
                InsertAtRandomPositons(password, digit);
                string pass = password.ToString();
                using (StudentUserAccountEntities entities = GetFormData(pass))
                {
                    StudentUserAccountEntry StdUserAccountEntry = new StudentUserAccountEntry();
                    StdUserAccountEntry.AddEntities = entities;
                    if (StdUserAccountEntry.Insert() == true)
                    {
                        lblUserNamePassword.Text = "UserName:" + ddlAdmissionNo.SelectedItem.Text + " , Password:" + pass + "";
                    }
                    else
                    {
                        lblUserNamePassword.Text = "";
                    }
                }
               
            }
            catch { }
        }
        private static void InsertAtRandomPositons(StringBuilder password, char character)
        {
            int randomPosition = rnd.Next(password.Length + 1);
            password.Insert(randomPosition, character);
        }
        private static char GenerateChar(string availableChars)
        {
            int randomIndex = rnd.Next(availableChars.Length);
            char randomChar = availableChars[randomIndex];
            return randomChar;
        }
        private StudentUserAccountEntities GetFormData(string password)
        {
            StudentUserAccountEntities trEntry = new StudentUserAccountEntities();
            trEntry.StudentId = int.Parse( ddlAdmissionNo.SelectedValue);
            trEntry.UserName = ComplexLetters.getTangledLetters(ddlAdmissionNo.SelectedItem.Text);
            trEntry.UserPassword = ComplexLetters.getTangledLetters(password);
            string u = Session["__UserId__"].ToString();
            if (Session["__UserId__"] == null)
            {
                trEntry.CreatedBy = 0;
            }
            else
            {
                trEntry.CreatedBy = int.Parse(Session["__UserId__"].ToString());
            }
            trEntry.CreatedOn = TimeZoneBD.getCurrentTimeBD();
            trEntry.Status = true;
            return trEntry;
        }
    }
}