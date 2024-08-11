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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if(!Page.IsPostBack)
            {
                GetSMSTitle();
                GetBatch();
                txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                GetDeptment();
                GetDesignation();
            }
        }
        private void GetBatch()
        {
            BatchEntry batchEntry = new BatchEntry();
            List<BatchEntities> batchList = batchEntry.GetEntitiesData().FindAll(c => c.IsUsed == true).ToList();
            dlBatch.DataTextField = "BatchName";
            dlBatch.DataValueField = "BatchId";
            dlBatch.DataSource = batchList;
            dlBatch.DataBind();
            dlBatch.Items.Insert(0, new ListItem("...Select...", "0"));
            dlBatch.Items.Insert(1, new ListItem("All", "All"));
            dlBatchN.DataTextField = "BatchName";
            dlBatchN.DataValueField = "BatchId";
            dlBatchN.DataSource = batchList;
            dlBatchN.DataBind();
            dlBatchN.Items.Insert(0, new ListItem("...Select...", "0"));
            dlBatchN.Items.Insert(1, new ListItem("All", "All"));
        }
        private void GetDeptment()
        {
            DepartmentEntry.GetDropdownlist(dlDeptN);
        }
        private void GetDesignation()
        {
            DesignationEntry.GetDropdownlist(dlDesignationN);
        }
        #region Absent Student List        
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dlBatch.SelectedValue != "0" && dlBatch.SelectedValue != "All")
            {
                lblGrpSection.Visible = true;
                dlSection.Visible = true;
                loadSectionClassWise();
            }
            else
            {
                lblGrpSection.Visible = false;
                dlSection.Visible = false;
            }
        }
        private void loadSectionClassWise()
        {
            try
            {
                DataTable dt;
                string className = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where " +
                    " ClassName='" + className + "'", dt = new DataTable(), sqlDB.connection);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {
                    dlSection.Items.Clear();                    
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.Items.Add(new ListItem("...Select...", "0"));
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                    lblGrpSection.Text = "Group";
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' " +
                        " AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add(new ListItem("...Select...", "0"));
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                    lblGrpSection.Text = "Section";
                }
            }
            catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (absentStd == null)
            {
                absentStd = new AbsentStudents();
            }
            DateTime today = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            absentStdList = null;
            if(dlBatch.SelectedValue == "All")
            {

                absentStdList = absentStd.GetEntitiesData(today.ToString(), dlShift.SelectedValue);
            }
            else
            {
                absentStdList = absentStd.GetEntitiesData(today.ToString(), dlBatch.SelectedItem.Text.Trim(), dlSection.SelectedItem.Text.Trim(), dlShift.SelectedValue);
            }
            adsentStdView.DataSource = absentStdList;
            adsentStdView.DataBind();
        }
        #endregion Absent Student List
        #region Notice Student/Emp List
        protected void dlBatchN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlBatchN.SelectedValue != "0" && dlBatchN.SelectedValue != "All")
            {
                lblGrdSectionN.Visible = true;
                dlSectionN.Visible = true;
                loadSectionClassWise1();
            }
            else
            {
                lblGrdSectionN.Visible = false;
                dlSectionN.Visible = false;
            }
        }
        private void loadSectionClassWise1()
        {
            try
            {
                DataTable dt;
                string className = new String(dlBatchN.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where " +
                    " ClassName='" + className + "'", dt = new DataTable(), sqlDB.connection);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {
                    dlSectionN.Items.Clear();
                    dlSectionN.Items.Add("Science");
                    dlSectionN.Items.Add("Commerce");
                    dlSectionN.Items.Add("Arts");
                    dlSectionN.Items.Add(new ListItem("...Select...", "0"));
                    dlSectionN.SelectedIndex = dlSection.Items.Count - 1;
                    lblGrdSectionN.Text = "Group";
                }
                else
                {
                    dlSectionN.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' " +
                        " AND SectionName<>'Arts' Order by SectionName", dlSectionN);
                    dlSectionN.Items.Add(new ListItem("...Select...", "0"));
                    dlSectionN.SelectedIndex = dlSectionN.Items.Count - 1;
                    lblGrdSectionN.Text = "Section";
                }
            }
            catch { }
        }
        protected void btnSTDSearchN_Click(object sender, EventArgs e)
        {
            if(std == null)
            {
                std = new Students();
            }
            if (dlBatchN.SelectedValue == "All")
            {
                StdList = std.GetEntitiesData(dlShiftN.SelectedValue);
            }
            else
            {
                StdList = std.GetEntitiesData(dlBatchN.SelectedItem.Text.Trim(), dlSectionN.SelectedItem.Text.Trim(), dlShiftN.SelectedValue);
            }
            noticeSTDGridPanel.Visible = true;
            noticeEmpGridPanel.Visible = false;
            noticeSTDView.DataSource = StdList;
            noticeSTDView.DataBind();
        }
        protected void btnESearchN_Click(object sender, EventArgs e)
        {
            if(emp == null)
            {
                emp = new EmployeeEntry();
            }
            string dept = (dlDeptN.SelectedValue != "All") ? dlDeptN.SelectedValue : null;
            string designation = (dlDesignationN.SelectedValue != "All") ? dlDesignationN.SelectedValue : null;
            empList = emp.GetEntitiesData(dept, designation, dlEShiftN.SelectedValue);
            noticeSTDGridPanel.Visible = false;
            noticeEmpGridPanel.Visible = true;
            noticeEmpView.DataSource = empList;
            noticeEmpView.DataBind();
        }
        #endregion
        #region Template code
        private void GetSMSTitle()
        {
            if(smsBodyTitle == null)
            {
                smsBodyTitle = new SMSBodyTitleEntry();
            }
            SmsTitleList = smsBodyTitle.GetEntitiesData();
            if(SmsTitleList != null)
            {
                dlSMSTemplate.DataTextField = "Title";
                dlSMSTemplate.DataValueField = "TitleID";
                dlSMSTemplate.DataSource = SmsTitleList;
                dlSMSTemplate.DataBind();
                dlSMSTemplate.Items.Add(new ListItem("...Select...","0"));
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
            if (smsBodyTitle == null)
            {
                smsBodyTitle = new SMSBodyTitleEntry();
            }
            if (SmsTitleList == null)
            {
                SmsTitleList = smsBodyTitle.GetEntitiesData();
            }
            txtMsgBody.Text = SmsTitleList.Find(c => c.TitleID == int.Parse(dlSMSTemplate.SelectedValue)).ToString();
            lblCharCount.Text = txtMsgBody.Text.Length.ToString();            
        }
        #endregion Template code
        #region SMS sending Zone
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if(lblHidetabIndex.Value == "0")
            {
                if (adsentStdView.Rows.Count > 0)
                {
                    int cout = 0;
                    smsList = null;
                    foreach (GridViewRow row in adsentStdView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkIndivisual") as CheckBox;
                        if (chk.Checked)
                        {
                            smsEntities = new SMSEntites();
                            ++cout;
                            smsEntities.ID = cout;
                            smsEntities.MobileNo = row.Cells[7].Text.ToString();
                            smsEntities.MessageBody = string.Format(txtMsgBody.Text + 
                                                                    ". Name: {0} Class: {1}, Roll: {2}, Section: {3}, Shift: {4}.",
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
                    if(cout == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select all or individual SMS Recipient from checkbox";
                        return;
                    }
                    sending(smsList);
                }
                else
                {
                    lblMessage.InnerText = "warning->Please Searching Absent Student before sending SMS";
                }       

            }
            if(lblHidetabIndex.Value == "1")
            {

            }
            if (lblHidetabIndex.Value == "2")
            {

            }
            if (lblHidetabIndex.Value == "3")
            {

            }
            if (lblHidetabIndex.Value == "4")
            {

            }
        }
        private void sending(List<SMSEntites> smsList)
        {
            routesmsdll.SMS sendSMS = new routesmsdll.SMS();
            sendSMS.UserName = "sumo-sumon";
            sendSMS.Password = "111111";
            sendSMS.IpAddress = "121.241.242.114";
            sendSMS.MessageType = routesmsdll.MESSAGE_TYPE.mTEXT;
            sendSMS.dlr = 1;
            foreach (var value in smsList)
            {
                sendSMS.MobileNo = value.MobileNo;
                sendSMS.Sender = "ODMES";
                sendSMS.Message = value.MessageBody;                
                string[] message = sendSMS.sendMessage().Split('|');
                value.Status = MsgStatus(int.Parse(message[0]));
                value.SMSID = message[2];                
            }
            if (smsList != null)
            {
                if (smsReport != null)
                {
                    smsReport = new SMSReportEntry();
                }
                smsReport.BulkInsert(smsList); 
                smsReportView.DataSource = smsList;
                smsReportView.DataBind();
                smsReportModal.Show();
            }
            
        }
        private string MsgStatus(int value)
        {
            string returnMsg = string.Empty;
            switch (value)
            { 
                case 1701 :
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
    }
}