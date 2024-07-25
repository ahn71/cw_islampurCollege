using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.SMS;
using DS.PropertyEntities.Model.SMS;
using DS.BLL.ManagedBatch;
using DS.PropertyEntities.Model.ManagedBatch;
using System.Data;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System.Globalization;
using DS.BLL.Attendace;
using DS.PropertyEntities.Model.Attendance;
using DS.BLL.HR;
using DS.PropertyEntities.Model.HR;
using System.Web.UI.HtmlControls;
using DS.BLL.ManagedClass;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using ASPSnippets.SmsAPI;
using System.Collections.Specialized;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.IO;
//using System.Net.Http;
using System.Runtime.Serialization;

using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

using System.Net;
using Newtonsoft.Json;
using DS.BLL.SMS;

namespace DS.UI.Notification
{
    public partial class SendSMS : System.Web.UI.Page
    {
        SMSBodyTitleEntry smsBodyTitle;
        List<SMSBodyTitleEntities> SmsTitleList;
        AbsentStudents absentStd;
        List<AbsentStudentsEntities> absentStdList;
        Students std;
        List<StdEntities> StdList;
        EmployeeEntry emp;
        List<Employee> empList;
        SMSEntites smsEntities;
        List<SMSEntites> smsList;
        SMSReportEntry smsReport;
        FailedStudent faildStd;
        List<ExamInfo> examInfoList;
        List<FailedStdEntities> failedStdList;
        PhnGrpEntry phnGrpEntry;
        PhnBookEntry phnBookEntry;
        List<PhoneBook> phnBookList;
        FeesCollectionEntry fc;
        ClassGroupEntry clsgrpEntry;
        SMSTxLogEntry stlEntry;
        //For Post Method
        private System.Collections.Specialized.NameValueCollection Inputs
        = new System.Collections.Specialized.NameValueCollection();

        public string Url = "";
        public string Method = "post";
        public string FormName = "form1";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
                if (!Page.IsPostBack)
                {
                    txtMsg.Attributes.Add("MaxLength", "2000");
                    txtMsgBody.Attributes.Add("MaxLength", "2000");

                    GetSMSTitle();
                    BatchEntry.GetDropdownlist(dlBatch, "True");
                    dlBatch.Items.Insert(1, new ListItem("All", "All"));
                    BatchEntry.GetDropdownlist(dlBatchN, true);
                    dlBatchN.Items.Insert(1, new ListItem("All", "All"));
                    ShiftEntry.GetDropDownList(dlShift);
                    dlShift.Items.Insert(1, new ListItem("All", "All"));
                    ShiftEntry.GetDropDownList(dlEShiftN);
                    ShiftEntry.GetDropDownList(dlEShiftN);
                    dlEShiftN.Items.Insert(1, new ListItem("All", "All"));
                    ShiftEntry.GetDropDownList(dlShiftN);
                    dlShiftN.Items.Insert(1, new ListItem("All", "All"));
                    txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtFDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtTDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    GetDeptment();
                    GetDesignation();
                    AbsentTable(string.Empty);
                    FailedTable(string.Empty);
                    NoticeTable(string.Empty);
                    GreetingsTable(string.Empty);
                    GetPhnGrp();
                    string tabindex = Request.QueryString["TabIndex"];
                    ActiveTab(tabindex);
                    lblHidetabIndex.Value = tabindex;
                }
        }
        private void ActiveTab(string Tabindex)
        {
            switch (Tabindex)
            {
                case "0":
                    alitab1.Attributes["class"] = "active";
                    alitab2.Attributes["class"] = "";
                    alitab3.Attributes["class"] = "";
                    alitab4.Attributes["class"] = "";
                    home.Attributes["class"] = "tab-pane active";
                    profile.Attributes["class"] = "tab-pane";
                    messages.Attributes["class"] = "tab-pane";
                    settings.Attributes["class"] = "tab-pane";
                    break;
                case "1":
                    alitab1.Attributes["class"] = "";
                    alitab2.Attributes["class"] = "active";
                    alitab3.Attributes["class"] = "";
                    alitab4.Attributes["class"] = "";
                    home.Attributes["class"] = "tab-pane";
                    profile.Attributes["class"] = "tab-pane active";
                    messages.Attributes["class"] = "tab-pane";
                    settings.Attributes["class"] = "tab-pane";
                    break;
                case "2":
                    alitab1.Attributes["class"] = "";
                    alitab2.Attributes["class"] = "";
                    alitab3.Attributes["class"] = "active";
                    alitab4.Attributes["class"] = "";
                    home.Attributes["class"] = "tab-pane ";
                    profile.Attributes["class"] = "tab-pane";
                    messages.Attributes["class"] = "tab-pane active";
                    settings.Attributes["class"] = "tab-pane";
                    break;
                case "3":
                    alitab1.Attributes["class"] = "";
                    alitab2.Attributes["class"] = "";
                    alitab3.Attributes["class"] = "";
                    alitab4.Attributes["class"] = "active";
                    home.Attributes["class"] = "tab-pane";
                    profile.Attributes["class"] = "tab-pane";
                    messages.Attributes["class"] = "tab-pane";
                    settings.Attributes["class"] = "tab-pane active";
                    break;
            }
        }       
        private void GetDeptment()
        {
            DepartmentEntry.GetDropdownlist(dlDeptN);
        }
        private void GetDesignation()
        {
            DesignationEntry.GetDropdownlist(dlDesignationN);
        }
        private void GetPhnGrp()
        {
            PhnGrpEntry.GetDropdownlist(dlPhnGrp);
        }
        #region Absent Student List
        private void AbsentTable(string msg)
        {
            if(msg == string.Empty)
            {
                msg = "Please search for absent student";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Student Name</th>";
            divInfo += "<th>Roll</th>";
            divInfo += "<th>Class</th>";
            divInfo += "<th>Section</th>";
            divInfo += "<th>Shift</th>";
            divInfo += "<th>Guardiant Mobile</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='7'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            absentgridPanel.Controls.Add(new LiteralControl(divInfo));
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            ddlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (ddlGroup.Enabled == true)
            {
                ddlGroup.SelectedValue = "All";
                tdgrplbl.Visible = true;
                tdgrp.Visible = true;
            }
            else
            {
                tdgrplbl.Visible = false;
                tdgrp.Visible = false;
            }
            dlSection.Items.Insert(1, new ListItem("All", "All"));
            AbsentTable(string.Empty);
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (absentStd == null)
                {
                    absentStd = new AbsentStudents();
                }
                string condition = "";
                absentStdList = null;
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                condition = fc.GetSearchCondition(dlShift.SelectedValue, BatchClsID[0], ddlGroup.SelectedValue, dlSection.SelectedValue);
                if (condition != "")
                {
                    condition += " AND Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtDate.Text + "',105) ORDER BY RollNo,ClassOrder";
                }
                else
                {
                    condition = " Where  Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtDate.Text + "',105) ORDER BY RollNo,ClassOrder";
                }
                absentStdList = absentStd.GetEntitiesData(condition);
                adsentStdView.DataSource = absentStdList;
                adsentStdView.DataBind();
                if (absentStdList == null)
                {
                    AbsentTable("Your selected date has no absent students");
                }
            }
            catch { }
        }
        #endregion Absent Student List
        #region Failed Student
        private void FailedTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please search for Failed student";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Student Name</th>";
            divInfo += "<th>Roll</th>";
            divInfo += "<th>Class</th>";
            divInfo += "<th>Section</th>";
            divInfo += "<th>Shift</th>";
            divInfo += "<th>Guardiant Mobile</th>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th>Marks</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='9'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            FailgridPanel.Controls.Add(new LiteralControl(divInfo));
        }
        protected void btnSearchExmID_Click(object sender, EventArgs e)
        {
            if(txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                if(faildStd == null)
                {
                    faildStd = new FailedStudent();
                }
                examInfoList = faildStd.GetExamInfoId(txtFDate.Text.Trim(),txtTDate.Text.Trim());                                                      
                if (examInfoList != null)
                {
                    dlExmID.DataTextField = "ExInId";
                    dlExmID.DataValueField = "ExInSI";
                    dlExmID.DataSource = examInfoList;
                    dlExmID.DataBind();
                    dlExmID.Items.Insert(0, new ListItem("...Select...", "0"));
                }
                else
                {
                    FailedTable(string.Empty);
                    lblMessage.InnerText = "warning-> FROM " + txtFDate.Text.Trim() + " Date To " + txtTDate.Text.Trim() + 
                                           " Date has not found exam information";
                }                               
            }
        }
        protected void btnSearchFailStd_Click(object sender, EventArgs e)
        {
            if (txtFDate.Text.Trim() != string.Empty &&
                txtTDate.Text.Trim() != string.Empty &&
                dlExmID.SelectedValue != "0")
            {
                if (faildStd == null)
                {
                    faildStd = new FailedStudent();
                }
                failedStdList = faildStd.GetAllFailedStd(dlExmID.SelectedItem.Text.Trim());
                if (failedStdList != null)
                {
                    failStdView.DataSource = failedStdList;
                    failStdView.DataBind();
                }
                else
                {
                    FailedTable(dlExmID.SelectedItem.Text + " exam has no fail student");
                    lblMessage.InnerText = "warning-> " + dlExmID.SelectedItem.Text + " exam has no fail student";
                }
            }
        }
        #endregion
        #region Notice Student/Emp List
        private void NoticeTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please search for Gaurdiant And Teacher or Employee";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList1' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%'>";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Name</th>";
            divInfo += "<th>Mobile Number</th>";            
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='3'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            noticePanel.Controls.Add(new LiteralControl(divInfo));
        }
        protected void dlBatchN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                NoticeTable(string.Empty);
                if (dlBatchN.SelectedValue == "All")
                {
                    ddlGroupN.Items.Clear();
                    dlSectionN.Items.Clear();
                    ddlGroupN.Items.Insert(0, new ListItem("All", "All"));
                    dlSectionN.Items.Insert(0, new ListItem("All", "All"));                    
                    return;
                }
                dlSectionN.Items.Clear();
                BatchEntry.loadGroupByBatchId(ddlGroupN, dlBatchN.SelectedValue.ToString());

                if (ddlGroupN.Items.Count == 1)
                {
                    trGroupN.Visible = false;
                    // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                    ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(dlSectionN, dlBatchN.SelectedValue.ToString(), ddlGroupN.SelectedItem.Value);
                }
                else
                {
                    trGroupN.Visible = true;
                }
                ddlGroupN.Items.Insert(1, new ListItem("All", "All"));
                dlSectionN.Items.Insert(1, new ListItem("All", "All"));               
            }
            catch { }
        }
        protected void btnSTDSearchN_Click(object sender, EventArgs e)
        {
            noticeEmpView.DataSource = null;
            noticeEmpView.DataBind();
            string condition = "";
            if (std == null)
            {
                std = new Students();
            }
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            condition = fc.GetSearchCondition(dlShiftN.SelectedValue, dlBatchN.SelectedValue, ddlGroupN.SelectedValue, dlSectionN.SelectedValue);

            condition += " ORDER By ClassOrder";

            StdList = std.GetEntitiesData(condition);
            noticePanel.Visible = false;
            noticeSTDGridPanel.Visible = true;
            noticeEmpGridPanel.Visible = false;
            noticeSTDView.DataSource = StdList;
            noticeSTDView.DataBind();
           

        }
        protected void btnESearchN_Click(object sender, EventArgs e)
        {
            noticeSTDView.DataSource = null;
            noticeSTDView.DataBind();
            if (emp == null)
            {
                emp = new EmployeeEntry();
            }
            string dept = (dlDeptN.SelectedValue != "All") ? dlDeptN.SelectedValue : null;
            string designation = (dlDesignationN.SelectedValue != "All") ? dlDesignationN.SelectedValue : null;
            string shift = (dlEShiftN.SelectedValue != "All") ? dlEShiftN.SelectedValue : null;
            empList = emp.GetEntitiesData(dept, designation, shift);
            noticePanel.Visible = false;
            noticeSTDGridPanel.Visible = false;
            noticeEmpGridPanel.Visible = true;
            noticeEmpView.DataSource = empList;
            noticeEmpView.DataBind();
        }
        #endregion
        #region Greetings
        private void GreetingsTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please search for SMS Recipient";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList4' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%'>";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Name</th>";
            divInfo += "<th>Number</th>";
            divInfo += "<th>Details</th>";            
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='4'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            GrpNumPanel.Controls.Add(new LiteralControl(divInfo));
        }
        protected void btnAddGrp_Click(object sender, EventArgs e)
        {
            txtGrpName.Text = string.Empty;
            txtGrpDetails.Text = string.Empty;
            phnGrpModal.Show();           
        }
        protected void btnAddGrpName_Click(object sender, EventArgs e)
        {
            if(txtGrpName.Text != string.Empty)
            {
                try
                {
                    using (PhoneGroup entities = GetPhnGrpData())
                    {
                        if (phnGrpEntry == null)
                        {
                            phnGrpEntry = new PhnGrpEntry();
                        }
                        phnGrpEntry.AddEntities = entities;
                        bool result = phnGrpEntry.Insert();
                        PhnGrpEntry.GetDropdownlist(dlPhnGrp);
                        GreetingsTable(string.Empty);
                        if (!result)
                        {
                            lblMessage.InnerText = "error-> Unable to save";
                            return;
                        }
                        lblMessage.InnerText = "success-> Save successfully"; 
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.InnerText = "error->" + ex.Message;                    
                }
            }
        }
        private PhoneGroup GetPhnGrpData()
        {
            PhoneGroup phnGrp = new PhoneGroup();
            phnGrp.GrpID = 0;
            phnGrp.GrpName = txtGrpName.Text.Trim();
            phnGrp.Details = txtGrpDetails.Text.Trim();
            return phnGrp;
        }
        protected void dlPhnGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dlPhnGrp.SelectedValue != "0")
            {
                GetRecipentNum(int.Parse(dlPhnGrp.SelectedValue));
            }
        }
        private void GetRecipentNum(int number)
        {
            if (phnBookEntry == null)
            {
                phnBookEntry = new PhnBookEntry();
            }
            phnBookList = phnBookEntry.GetAllPhnBookNum(number);
            if (phnBookList != null)
            {
                GrpNumView.DataSource = phnBookList;
                GrpNumView.DataBind();
                GrpNumView.HeaderRow.TableSection = TableRowSection.TableHeader;                
            }
            else
            {
                GrpNumView.DataSource = null;
                GrpNumView.DataBind();
                GreetingsTable("Recipient Number has not been found");
            }
            
        }
        protected void AddNumber_Click(object sender, EventArgs e)
        {
            if(dlPhnGrp.SelectedValue != "0")
            {
                PhnGrpEntry.GetDropdownlist(dlGrpName);
                dlGrpName.SelectedValue = dlPhnGrp.SelectedValue;
                txtName.Text = string.Empty;
                txtNumber.Text = string.Empty;
                txtNumDetails.Text = string.Empty;
                addNumModal.Show();
            }
        }
        protected void btnAddNumber_Click(object sender, EventArgs e)
        {
            if (
                dlGrpName.SelectedValue != "0" &&
                txtName.Text != string.Empty &&
                txtNumber.Text != string.Empty)
            {
                try
                {
                    //----------Validation----------
                    if (txtNumber.Text.Trim().Length != 11)
                    {
                        lblMessage.InnerText = "warning-> Mobile No Must be 11 Digits";
                        txtNumber.Focus();
                        return;
                    }
                    if (!"017,019,018,016,015,013,014".Contains(txtNumber.Text.Trim().Substring(0, 3)))
                    {
                        lblMessage.InnerText = "warning-> Mobile No is Invalid";
                        txtNumber.Focus();
                        return;
                    }
                    //------------------------------
                    using (PhoneBook entities = GetPhnBkData())
                    {
                        if (phnBookEntry == null)
                        {
                            phnBookEntry = new PhnBookEntry();
                        }
                        phnBookEntry.AddEntities = entities;
                        bool result = phnBookEntry.Insert();
                        GetRecipentNum(int.Parse(dlPhnGrp.SelectedValue));
                        if (!result)
                        {
                            lblMessage.InnerText = "error-> Unable to save";
                            return;
                        }
                        lblMessage.InnerText = "success-> Save successfully";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.InnerText = "error->" + ex.Message;
                }
            }
        }
        private PhoneBook GetPhnBkData()
        {
            PhoneBook phnBk = new PhoneBook();
            phnBk.NumID = 0;
            phnBk.Group = new PhoneGroup()
            {
                GrpID = int.Parse(dlGrpName.SelectedValue)
            };
            phnBk.Name = txtName.Text.Trim();
            phnBk.Number = lblMobile.Text.Trim()+txtNumber.Text.Trim();
            phnBk.Details = txtNumDetails.Text.Trim();
            return phnBk;
        }
        private SMSTransactionLog GetSMSLogData(string insertedSmsIds,string SMSType)
        {
            SMSTransactionLog stl = new SMSTransactionLog();
            stl.SL = 0;
            stl.insertedSmsIds = insertedSmsIds;
            stl.SMStype = SMSType;
            stl.Template = dlSMSTemplate.SelectedValue;
            stl.SendingTime = DateTime.Now.ToString();
            return stl;
        }
        #endregion
        #region Template code
        private void GetSMSTitle()
        {
            if (smsBodyTitle == null)
            {
                smsBodyTitle = new SMSBodyTitleEntry();
            }
            SmsTitleList = smsBodyTitle.GetEntitiesData();
            if (SmsTitleList != null)
            {
                dlSMSTemplate.DataTextField = "Title";
                dlSMSTemplate.DataValueField = "TitleID";
                dlSMSTemplate.DataSource = SmsTitleList;
                dlSMSTemplate.DataBind();
                dlSMSTemplate.Items.Insert(0, new ListItem("...Select...", "0"));
                dlSMSTemplate.SelectedValue = "0";
            }
        }
        protected void btnAddNewTemplate_Click(object sender, EventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtMsg.Text = string.Empty;
            showAddMsgBody.Show();
        }
        private SMSBodyTitleEntities GetFormData()
        {
            SMSBodyTitleEntities smsBodyTitle = new SMSBodyTitleEntities();
            smsBodyTitle.TitleID = int.Parse(lblSmsBodyTitle.Value);
            smsBodyTitle.Title = txtTitle.Text.Trim();
            smsBodyTitle.Body = txtMsg.Text.Trim();
            return smsBodyTitle;
        }
        protected void btnAddMsg_Click(object sender, EventArgs e)
        {
            if (lblSmsBodyTitle.Value.ToString() == string.Empty)
            {
                lblSmsBodyTitle.Value = "0";
                if (SaveName() == true)
                {
                    GetSMSTitle();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
        private Boolean SaveName()
        {
            try
            {
                using (SMSBodyTitleEntities entities = GetFormData())
                {
                    if (smsBodyTitle == null)
                    {
                        smsBodyTitle = new SMSBodyTitleEntry();
                    }
                    smsBodyTitle.AddEntities = entities;
                    bool result = smsBodyTitle.Insert();
                    lblSmsBodyTitle.Value = string.Empty;
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean UpdateName()
        {
            try
            {
                using (SMSBodyTitleEntities entities = GetFormData())
                {
                    if (smsBodyTitle == null)
                    {
                        smsBodyTitle = new SMSBodyTitleEntry();
                    }
                    smsBodyTitle.AddEntities = entities;
                    bool result = smsBodyTitle.Update();
                    lblSmsBodyTitle.Value = string.Empty;
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to update";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        protected void dlSMSTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlSMSTemplate.SelectedValue != "0")
            {
                if (smsBodyTitle == null)
                {
                    smsBodyTitle = new SMSBodyTitleEntry();
                }
                if (SmsTitleList == null)
                {
                    SmsTitleList = smsBodyTitle.GetEntitiesData();
                }
                txtMsgBody.Text = SmsTitleList.Find(c => c.TitleID == int.Parse(dlSMSTemplate.SelectedValue)).Body;
                lblCharCount.Text = txtMsgBody.Text.Length.ToString();
            }            
        }
        #endregion Template code
        #region SMS sending Zone
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string mblNo = string.Empty;
            string invalidMbNo = string.Empty;
            if (lblHidetabIndex.Value == "0") // Absent Student
            {
                if (adsentStdView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = new List<SMSEntites>();
                    foreach (GridViewRow row in adsentStdView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {
                            smsEntities = new SMSEntites();
                            ++cout;
                          //  mblNo += row.Cells[7].Text.Trim().ToString() + ",";
                            //----------Validation----------
                            try
                            {
                                string MobileNo = row.Cells[7].Text.Trim().ToString().Replace("+88", "");
                                if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                {
                                    //int a = int.Parse(MobileNo);
                                    //mblNo += row.Cells[7].Text.Trim().ToString() + ",";
                                    string resopse = API.SMSSend(string.Format(txtMsgBody.Text), row.Cells[7].Text.Trim().ToString());
                                    string[] r = resopse.Split('|');
                                    smsEntities.Status = API.MsgStatus(int.Parse(r[0]));
                                }
                                else
                                {
                                    smsEntities.Status = "Invalid Mobile";
                                    invalidMbNo += row.Cells[7].Text.Trim().ToString() + ",";
                                }
                            }
                            catch 
                            {
                                invalidMbNo += row.Cells[7].Text.Trim().ToString() + ",";
                                smsEntities.Status = "Invalid Mobile";
                            }
                            //------------------------------
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = row.Cells[7].Text.Trim().ToString();
                            smsEntities.MessageBody = string.Format(txtMsgBody.Text +
                                                                    " Name: {0}, Class: {1}, Roll: {2}, Section: {3}, Shift: {4}.",
                                                                    row.Cells[2].Text.ToString(),
                                                                    row.Cells[4].Text.ToString(),
                                                                    row.Cells[3].Text.ToString(),
                                                                    row.Cells[5].Text.ToString(),
                                                                    row.Cells[6].Text.ToString());
                            smsEntities.Purpose = "Absent";
                            smsEntities.SentTime = DateTime.Now;
                            smsList.Add(smsEntities);
                        }
                    }
                    if (cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    //mblNo = mblNo.Remove(mblNo.Length-1);
                    //sending(smsList, mblNo, invalidMbNo);    
                    sendingReport(smsList);
                   
                }
                else
                {
                    //lblMessage.InnerText = "warning->Please Searching Absent Student before sending SMS";
                }

            }
            if (lblHidetabIndex.Value == "1") // Failed Student
            {
                if (failStdView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = new List<SMSEntites>();
                    foreach (GridViewRow row in failStdView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {
                            Label lblStdName = row.FindControl("lblStdName") as Label;
                            Label lblStdRoll = row.FindControl("lblStdRoll") as Label;
                            Label lblStdClsName = row.FindControl("lblStdClsName") as Label;
                            Label lblStdSection = row.FindControl("lblStdSection") as Label;
                            Label lblStdShift = row.FindControl("lblStdShift") as Label;                            
                            Label lblMobile = row.FindControl("lblGuardiantMobile") as Label;
                            Label lblSubName = row.FindControl("lblSubName") as Label;
                            Label lblMarks = row.FindControl("lblMarks") as Label;
                            smsEntities = new SMSEntites();
                            ++cout;
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = lblMobile.Text.Trim().ToString();
                           // mblNo += lblMobile.Text.Trim().ToString() + ",";
                            //----------Validation----------
                            try
                            {
                                string MobileNo = lblMobile.Text.Trim().ToString().Replace("+88", "");
                                if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                {
                                    //int a = int.Parse(MobileNo);
                                    //mblNo += lblMobile.Text.Trim().ToString() + ",";
                                    string resopse = API.SMSSend(string.Format(txtMsgBody.Text), lblMobile.Text.Trim().ToString());
                                    string[] r = resopse.Split('|');
                                    smsEntities.Status = API.MsgStatus(int.Parse(r[0]));
                                }
                                else
                                {
                                    smsEntities.Status = "Invalid Mobile";
                                    invalidMbNo += lblMobile.Text.Trim().ToString() + ",";
                                }
                            }
                            catch 
                            { 
                                invalidMbNo += lblMobile.Text.Trim().ToString() + ",";
                                smsEntities.Status = "Invalid Mobile";
                            }
                            //------------------------------
                            smsEntities.MessageBody = string.Format(txtMsgBody.Text +
                                                                    " Name: {0}, Class: {1}, Roll: {2}, Section: {3}, Shift: {4}, Subject: {5}, Marks: {6}.",
                                                                    lblStdName.Text.Trim(),
                                                                    lblStdClsName.Text.Trim(),
                                                                    lblStdRoll.Text.Trim(),
                                                                    lblStdSection.Text.Trim(),
                                                                    lblStdShift.Text.Trim(),
                                                                    lblSubName.Text.Trim(),
                                                                    lblMarks.Text.Trim());
                            smsEntities.Purpose = "Failed";
                            smsEntities.SentTime = DateTime.Now;
                            smsList.Add(smsEntities);
                        }
                    }
                    if (cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    //mblNo = mblNo.Remove(mblNo.Length - 1);
                    //sending(smsList, mblNo, invalidMbNo); 
                    sendingReport(smsList);
                }
                else
                {
                    lblMessage.InnerText = "warning->Please Searching failed Student before sending SMS";
                }               
            }
            if (lblHidetabIndex.Value == "2") // Notice
            {
                if (noticeSTDView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = new List<SMSEntites>();
                    foreach (GridViewRow row in noticeSTDView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {
                            string mobileNo = "";
                            if(rblSendToN.SelectedValue=="student")
                                mobileNo= row.Cells[7].Text.Trim().ToString(); 
                            else
                                mobileNo= row.Cells[8].Text.Trim().ToString(); 
                            smsEntities = new SMSEntites();
                            ++cout;
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = mobileNo;
                           // mblNo += row.Cells[7].Text.Trim().ToString() + ",";

                            //----------Validation----------
                            try
                            {
                                string MobileNo = mobileNo.Replace("+88", "");
                                if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                {
                                    //int a = int.Parse(MobileNo);
                                    //mblNo += row.Cells[7].Text.Trim().ToString() + ",";
                                    string resopse = API.SMSSend(string.Format(txtMsgBody.Text), mobileNo);
                                    string[] r = resopse.Split('|');
                                    smsEntities.Status =API.MsgStatus(int.Parse(r[0]));
                                }
                                else
                                {
                                    invalidMbNo += mobileNo + ",";
                                    smsEntities.Status = "Invalid Mobile";
                                }
                            }
                            catch 
                            {
                                invalidMbNo += mobileNo + ",";
                                smsEntities.Status = "Invalid Mobile";
                            }
                            //------------------------------

                            smsEntities.MessageBody = string.Format(txtMsgBody.Text);
                            smsEntities.Purpose = "Notice";
                            smsEntities.SentTime = DateTime.Now;
                            smsList.Add(smsEntities);
                        }
                    }
                    if (cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    //mblNo = mblNo.Remove(mblNo.Length - 1);
                    //sending(smsList, mblNo, invalidMbNo); 
                    sendingReport(smsList);
                }
                else if (noticeEmpView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = new List<SMSEntites>();
                    foreach (GridViewRow row in noticeEmpView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {
                            smsEntities = new SMSEntites();
                            ++cout;
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = row.Cells[6].Text.Trim().ToString();
                           // mblNo += row.Cells[6].Text.Trim().ToString() + ",";

                            //----------Validation----------
                            try
                            {
                                string MobileNo = row.Cells[6].Text.Trim().ToString().Replace("+88", "");
                                if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                {
                                    //int a = int.Parse(MobileNo);
                                    //mblNo += row.Cells[6].Text.Trim().ToString() + ",";
                                    string resopse = API.SMSSend(string.Format(txtMsgBody.Text), row.Cells[6].Text.Trim().ToString());
                                    string[] r = resopse.Split('|');
                                    smsEntities.Status = API.MsgStatus(int.Parse(r[0]));
                                }
                                else 
                                {
                                    smsEntities.Status = "Invalid Mobile";
                                    invalidMbNo += row.Cells[6].Text.Trim().ToString() + ",";
                                }
                            }
                            catch { invalidMbNo += row.Cells[6].Text.Trim().ToString() + ",";
                            smsEntities.Status = "Invalid Mobile";
                            }
                            //------------------------------

                            smsEntities.MessageBody = string.Format(txtMsgBody.Text);
                            smsEntities.Purpose = "Notice";
                            smsEntities.SentTime = DateTime.Now;
                            smsList.Add(smsEntities);
                        }
                    }
                    if (cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    //mblNo = mblNo.Remove(mblNo.Length - 1);
                    //sending(smsList, mblNo, invalidMbNo); 
                    sendingReport(smsList);
                }
                else
                {
                    lblMessage.InnerText = "warning->Please Searching Student or Employee or Teacher before sending SMS";
                }

            }
            if (lblHidetabIndex.Value == "3") // Greetings
            {
                if (GrpNumView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = new List<SMSEntites>();
                    foreach (GridViewRow row in GrpNumView.Rows)
                    {

                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {

                            smsEntities = new SMSEntites();
                            ++cout;
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = row.Cells[3].Text.Trim().ToString();
                            //----------Validation----------
                            try
                            {
                                string MobileNo = row.Cells[3].Text.Trim().ToString().Replace("+88", "");
                                if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                {                                  
                                 string resopse= API.SMSSend(string.Format(txtMsgBody.Text), row.Cells[3].Text.Trim().ToString());
                                    //string[] r = resopse.Split('|');
                                    //smsEntities.Status =API.MsgStatus(int.Parse(r[0]));

                                    Dictionary<string, object> responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(resopse);
                                    int responseCode = Convert.ToInt32(responseDictionary["response_code"]);
                                    Console.WriteLine("responseCode: " + responseCode);
                                    smsEntities.Status = API.MsgStatus(responseCode);
                                }
                                else
                                {
                                    invalidMbNo += row.Cells[3].Text.Trim().ToString() + ",";
                                    smsEntities.Status = "Invalid Mobile";
                                }
                            }
                            catch
                            {
                                invalidMbNo += row.Cells[3].Text.Trim().ToString() + ",";
                                smsEntities.Status = "Invalid Mobile";
                            }
                            //------------------------------

                            smsEntities.MessageBody = string.Format(txtMsgBody.Text);
                            smsEntities.Purpose = "Greetings";
                            smsEntities.SentTime = DateTime.Now;
                            smsList.Add(smsEntities);
                        }
                    }
                    if (cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    sendingReport(smsList);
                    lblMessage.InnerText = "success->Successfully sent.";// +API.SMSSend(txtMsgBody.Text.Trim(), mblNo);
                }
                else
                {
                    lblMessage.InnerText = "warning->Please Searching SMS Recipient before sending SMS";
                }
            } 
            if (lblHidetabIndex.Value == "4") // Others
            {
                
            }            
            
        }
        private void sendingReport(List<SMSEntites> smsList)
        {
            if (smsList != null)
            {
                if (smsReport == null)
                {
                    smsReport = new SMSReportEntry();
                }
                smsReport.BulkInsert(smsList);
                smsReportView.DataSource = smsList;
                smsReportView.DataBind();
                smsReportModal.Show();

            }
        }
        private void sending(List<SMSEntites> smsList,string mblNo,string invalidMblNo)
        {
            try
            {              

                string status = "";

                SMSGetEntities1 jason = new SMSGetEntities1();
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["username"] = API.userID;
                    values["sercet_key"] = "cw_ppc";
                    mblNo = mblNo + txtTestMobileNo.Text.Trim();
                    values["number"] = mblNo;
                    values["text"] = txtMsgBody.Text.Trim();

                    var response = client.UploadValues(API.url, values);

                    var responseString = Encoding.Default.GetString(response);
                    status = responseString.ToString();
               //  List<SMSGetEntities> lstValues=new List<SMSGetEntities>();

                    jason = JsonConvert.DeserializeObject<SMSGetEntities1>(status);

                    //------------------------------------------------
                    var values1 = new NameValueCollection();
                    values1["username"] = "cw_user";
                    values1["sercet_key"] = "cw_ppc";
                  //  values1["deliveryReport"] =jason.serverResponse.insertedSmsIds;
                    values1["pageIndex"] = "0";
                    //----------------------------------------------------------

                    
                }

                //JavaScriptSerializer j = new JavaScriptSerializer();
                //object a = j.Deserialize(status, typeof(object));
               //-> this bellow block is comment by nayem date: 18-03-2018
                //try 
                //{
                //    if (jason.serverResponse.isError != "false")
                //    {
                //        lblMessage.InnerText = "error->SMS Not Send!";
                //        return;
                //    }    
                //}
                //catch { }

                using (var client = new WebClient())
                {
                    var values1 = new NameValueCollection();
                    values1["username"] = "cw_user";
                    values1["sercet_key"] = "cw_ppc";
                    //   values1["deliveryReport"] = jason.serverResponse.insertedSmsIds; ->this line is comment by nayem date: 18-03-2018
                    values1["pageIndex"] = "0";

                    var response = client.UploadValues("http://mark8king.com/sms_api/ ", values1);

                    var responseString = Encoding.Default.GetString(response);
                    status = responseString.ToString();
                    //  List<SMSGetEntities> lstValues=new List<SMSGetEntities>();        


                }

                if (smsList != null)
                {
                    if (smsReport == null)
                    {
                        smsReport = new SMSReportEntry();
                    }
                    smsReport.BulkInsert(smsList);
                    smsReportView.DataSource = smsList;
                    smsReportView.DataBind();
                    smsReportModal.Show();
                 
                }
              //SaveSMSTransactionLog(jason.serverResponse.insertedSmsIds,smsList[0].Purpose);  ->this line is comment by nayem date: 18-03-2018
                //if(jason.invalidNumber.Count()<1)
                //lblMessage.InnerText = "success->SMS Successfully Send " + jason.valid.Count(); 
                //else
                if (invalidMblNo.Trim().ToString() == "")
                    lblMessage.InnerText = "success->SMS Successfully Send ";
                else
                    lblMessage.InnerText = "success->SMS Successfully Send " + jason.valid.Count() + " and Invalid Number List " + invalidMblNo; 
            }
            catch {  }
        }
        private void SaveSMSTransactionLog(string insertedSmsIds, string SMSType)
        {
            try
            {

                SMSTransactionLog entities = GetSMSLogData(insertedSmsIds, SMSType);
                {
                    if (stlEntry == null)
                    {
                        stlEntry = new SMSTxLogEntry();
                    }
                    stlEntry.AddEntities = entities;
                    bool result = stlEntry.Insert();                   
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save Transaction Log";
                        return;
                    }
                    lblMessage.InnerText = "success-> Save successfully Transaction Log";
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private string MsgStatus1(int value)
        {            
            string returnMsg = string.Empty;
            switch (value)
            {
                case 1701:
                    returnMsg = "Success, Message Submitted Successfully ";
                    break;
                case 1702:
                    returnMsg = "Invalid URL Error, This means that one of  the parameters was not provided";
                    break;
                case 1703:
                    returnMsg = "Invalid value in username or password field";
                    break;
                case 1704:
                    returnMsg = "Invalid value in 'type' field ";
                    break;
                case 1705:
                    returnMsg = "Invalid Message";
                    break;
                case 1706:
                    returnMsg = "Invalid Destination";
                    break;
                case 1707:
                    returnMsg = "Invalid Source (Sender)";
                    break;
                case 1708:
                    returnMsg = "Invalid value for 'dlr' field";
                    break;
                case 1709:
                    returnMsg = "User validation failed";
                    break;
                case 1710:
                    returnMsg = "Internal Error";
                    break;
                case 1025:
                    returnMsg = "Insufficient Credit ";
                    break;
                case 1032:
                    returnMsg = "Number is in DND";
                    break;
            }
            return returnMsg;
        }
        
        #endregion SMS sending Zone

        protected void btnSMSReport_Click(object sender, EventArgs e)
        {
            SMSReportEntry smsREntry = new SMSReportEntry();
            smsReportView.DataSource = smsREntry.GetEntitiesData();
            smsReportView.DataBind();
            smsReportModal.Show();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (ddlGroup.SelectedValue != "All")
            {
                GroupId = ddlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            dlSection.Items.Insert(1, new ListItem("All", "All"));
            AbsentTable(string.Empty);
        }

        protected void ddlGroupN_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoticeTable(string.Empty);
            if (ddlGroupN.SelectedValue == "All")
            {
                dlSectionN.Items.Clear();
                dlSectionN.Items.Insert(0,new ListItem("All","All"));
                return;
            }
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(dlSectionN, dlBatchN.SelectedValue.ToString(), ddlGroupN.SelectedItem.Value);
            dlSectionN.Items.Insert(1, new ListItem("All", "All"));           
        }
        //For Post Method
        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }
        public void Post()
        {
            System.Web.HttpContext.Current.Response.Clear();

            System.Web.HttpContext.Current.Response.Write("<html><head>");

            System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));

            System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >",

           FormName, Method, Url));
            for (int i = 0; i < Inputs.Keys.Count; i++)
            {
                System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            }
            System.Web.HttpContext.Current.Response.Write("</form>");
            System.Web.HttpContext.Current.Response.Write("</body></html>");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}