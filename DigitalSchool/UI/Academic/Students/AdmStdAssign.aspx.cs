using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedClass;
using DS.BLL.GeneralSettings;
using DS.BLL.Admission;
using DS.PropertyEntities.Model.Admission;
using DS.BLL.ManagedBatch;
using DS.BLL.ControlPanel;
using System.Data;
using DS.DAL;
using System.Data.SqlClient;
using iTextSharp.text;
using Org.BouncyCastle.Utilities.Collections;

namespace DS.UI.Academic.Students
{   
    public partial class AdmStdAssign : System.Web.UI.Page
    {
        AdmStdInfoEntry admStdInfoEntry;
        List<AdmStdInfoEntities> admStdInfoList;
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if(!Page.IsPostBack)
            {
              if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "AdmStdAssign.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");             
                GetBlankTable(string.Empty);                
                GetAllClss();
                GetAllShift();                
            }
        }
        private void GetAllClss()
        {
            ClassEntry.GetEntitiesData(dlClass);
        }
        private void GetAllShift()
        {
            ShiftEntry.GetDropDownList(dlShift);
        }
        private void GetBlankTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please class wise search new students for assigning Batch, Roll And Section";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Admission No</th>";
            divInfo += "<th>Admission Date</th>";
            divInfo += "<th>Student Name</th>";           
            divInfo += "<th>Roll</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='7'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            admStdAssignPanel.Controls.Add(new LiteralControl(divInfo));
        }        
        private void SearchNewStd(int searchClsId, int searchShiftId,int clsgrpID)
        {
            try
            {
                if (admStdInfoEntry == null)
                {
                    admStdInfoEntry = new AdmStdInfoEntry();
                }
                if (admStdInfoList == null)
                {
                    admStdInfoList = admStdInfoEntry.GetEntitiesData();
                }
                if (admStdInfoList != null)
                {
                    if (searchClsId == 0 && searchShiftId == 0)
                    {
                        admStdInfoList = admStdInfoList.FindAll(c => c.ClsSecID == 0 &&
                                                        c.RollNo == 0 &&
                                                        c.StartBatchID == 0 &&
                                                        c.StdStatus == true);
                    }
                    else
                    {
                        admStdInfoList = admStdInfoList.FindAll(c => c.ClsSecID == 0 &&
                                                        c.RollNo == 0 &&
                                                        c.StartBatchID == 0 &&
                                                        c.StdStatus == true &&
                                                        c.Student.ClassID == searchClsId &&
                                                        c.Student.ConfigId == searchShiftId &&
                                                        c.Student.ClsGrpID == clsgrpID);
                    }
                }
                if (admStdInfoList != null && admStdInfoList.Count > 0)
                {
                    int maxRoll = 0;
                    int clsId = 0;
                    var NewList = new List<AdmStdInfoEntities>();
                    foreach (var newVal in admStdInfoList)
                    {
                        if (clsId != newVal.Student.ClassID)
                        {
                            clsId = newVal.Student.ClassID;
                            maxRoll = AdmStdInfoEntry.GetLastRoll(clsId);
                        }
                        NewList.Add(new AdmStdInfoEntities()
                        {
                            AdmissionNo = newVal.AdmissionNo,
                            AdmissionDate = newVal.AdmissionDate,
                            Student = new CurrentStdEntities()
                            {
                                StudentID = newVal.Student.StudentID,
                                FullName = newVal.Student.FullName,
                                ClassName = newVal.Student.ClassName,
                                Shift = newVal.Student.Shift
                            },
                            ClassID = newVal.Student.ClassID,
                            RollNo = maxRoll
                        });
                        maxRoll++;
                    }
                    admStdAssignView.DataSource = NewList;
                    admStdAssignView.DataBind();
                    lbltotal.Text = "Total " + NewList.Count;
                }
                else
                {
                    admStdAssignView.DataSource = null;
                    admStdAssignView.DataBind();
                    lbltotal.Text = "";
                    GetBlankTable("New Student not found");
                }
            }
            catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(dlClass.SelectedValue != "0" && dlShift.SelectedValue != "0")
            {
                SearchNewStd(int.Parse(dlClass.SelectedValue), int.Parse(dlShift.SelectedValue),int.Parse(ddlsearchGroup.SelectedValue));             
            }
            else
            {
                lblMessage.InnerText = "warning-> Please Select Class And Shift before searching";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            if(dlClass.SelectedValue != "0" && dlShift.SelectedValue != "0")
            {
                if(admStdAssignView.Rows.Count > 0)
                {
                    var saveList = new List<AdmStdInfoEntities>();
                    int count = 0;
                    int totalstd = 0;
                    
                    foreach(GridViewRow row in admStdAssignView.Rows)
                    {
                        CheckBox chk = row.FindControl("chkStatus") as CheckBox;
                        if (chk.Checked == true)
                        {
                            HiddenField stdId = row.FindControl("hideStdId") as HiddenField;

                            TextBox rollNo = row.FindControl("txtRoll") as TextBox;
                            if (ddlBatch.SelectedValue != "0" && ddlSection.SelectedValue != "0" && rollNo.Text != string.Empty)
                            {
                                if (RollValidation(ddlBatch.SelectedValue, ddlsearchGroup.SelectedValue, ddlSection.SelectedValue, rollNo.Text) == false)
                                {
                                    lblMessage.InnerText = "warning->" + rollNo.Text + " is Duplicated Roll";
                                    return;
                                }
                                saveList.Add(new AdmStdInfoEntities()
                                {
                                    ClsSecID = int.Parse(ddlSection.SelectedValue),
                                    RollNo = int.Parse(rollNo.Text.Trim()),
                                    StartBatchID = int.Parse(ddlBatch.SelectedValue),
                                    SpendYear = 1,
                                    Student = new CurrentStdEntities()
                                    {
                                        StudentID = int.Parse(stdId.Value.Trim()),
                                        SectionName = ddlSection.SelectedItem.Text.Trim(),
                                        BatchName = ddlBatch.SelectedItem.Text.Trim()
                                    }
                                });
                                count++;
                            }
                        }
                    }
                    if(count > 0)
                    {
                        if (admStdInfoEntry == null)
                        {
                            admStdInfoEntry = new AdmStdInfoEntry();
                        }
                        //...........................
                        //Check Duplicate Roll No
                        var duplicateroll = new List<AdmStdInfoEntities>(); 
                        foreach (var value in saveList)
                        {
                            duplicateroll = saveList.FindAll(c=>c.RollNo==value.RollNo);
                            if (duplicateroll.Count > 1)
                            {
                                lblMessage.InnerText = "warning->" + value.RollNo + " is Duplicate Roll. DisAllow Duplicate Roll";
                                return;
                            }
                        }
                        //.......................................

                        bool result = admStdInfoEntry.Update(saveList);
                        if (result)
                        {
                            SetExamInfoForLaterAdmission(); // for later student admission
                            SearchNewStd(int.Parse(dlClass.SelectedValue), int.Parse(dlShift.SelectedValue),int.Parse(ddlsearchGroup.SelectedValue));                           
                            lblMessage.InnerText = "success-> Saved successfully";
                            return;
                        }
                        lblMessage.InnerText = "error-> Unable to save";
                        return;
                    }
                    lblMessage.InnerText = "warning-> Please Select Batch, Section And Roll No before Saved";
                    return;
                }
            }
            lblMessage.InnerText = "warning-> Please Select Class And Shift before searching";
        }
        private Boolean RollValidation(string batchId,string clsgrpID,string clsSecID,string rollNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT RollNo FROM CurrentStudentInfo WHERE BatchID='"
                    + batchId + "' AND ClsGrpID='" + clsgrpID + "' AND ClsSecID='" + clsSecID + "' AND RollNo='"+rollNo+"'");
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }      
        protected void btnClear_Click(object sender, EventArgs e)
        {
            admStdAssignView.DataSource = null;
            admStdAssignView.DataBind();
            dlClass.SelectedValue = "0";
            dlShift.SelectedValue = "0";
            GetBlankTable(string.Empty);            
        }

        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(dlClass.SelectedValue), ddlsearchGroup);
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(dlClass.SelectedValue), ddlsearchGroup.SelectedValue);
            BatchEntry.GetBatchForNewStdAssign(ddlBatch,int.Parse(dlClass.SelectedValue));
        }

        protected void ddlsearchGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(dlClass.SelectedValue), ddlsearchGroup.SelectedValue);
        }
      

        void SetExamInfoForLaterAdmission()  // whose student are later admited,that means after any exam,then this method r entered essential record in exam info table
        {
            try
            {
                int MaxVal = admStdAssignView.Rows.Count;
                foreach(GridViewRow row in admStdAssignView.Rows)
                {
                    CheckBox chk = row.FindControl("chkStatus") as CheckBox;
                    if (chk.Checked == true)
                    {
                        HiddenField hfStudentId = row.FindControl("hideStdId") as HiddenField;
                        TextBox txtRoll = row.FindControl("txtRoll") as TextBox;

                        Label lblAdmDate = row.FindControl("lblAdmDate") as Label;

                        string[] DMY = lblAdmDate.Text.Split('-');

                        DataTable dt = CRUD.ReturnTableNull("select ExInSl,ExInId,Format(ExInDate,'dd-MM-yyyy') as ExInDate,ExId from ExamInfo where ExInDate <='" + DMY[2].Substring(0, 4) + "-" + DMY[1] + "-" + DMY[0] + "' AND  BatchId=" + ddlBatch.SelectedItem.Value + "");

                        string GetMarksheetTable = "Class_" + dlClass.SelectedItem.Text + "MarksSheet";
                        for (byte c = 0; c < dt.Rows.Count; c++)
                        {
                            DataTable dtSubQPInfo = new DataTable();

                            string clsGroupId = (!ddlsearchGroup.Enabled) ? "0" : ddlsearchGroup.SelectedItem.Value;

                            dtSubQPInfo = CRUD.ReturnTableNull("Select SubQPId,ConvertTo from v_SubjectQuestionPattern where ExId=" + dt.Rows[c]["ExId"].ToString() + " AND BatchId='" + ddlBatch.SelectedItem.Value.ToString() + "' AND ClsGrpId='" + clsGroupId + "' order by SubQPId");

                            foreach (DataRow dr in dtSubQPInfo.Rows)
                            {
                                SqlCommand cmd = new SqlCommand("insert into " + GetMarksheetTable + " (ExId,ExInId,StudentId,RollNo,BatchId,ClsSecId,ShiftId,SubQPId,Marks,ConvertToPercentage,ConvertMarks,ClsGrpID)" +
                               " values('" + dt.Rows[c]["ExId"].ToString() + "','" + dt.Rows[c]["ExInId"].ToString() + "','" + hfStudentId.Value.ToString() + "','" + txtRoll.Text + "','" + ddlBatch.SelectedItem.Value + "','" + ddlSection.SelectedItem.Value + "','" + dlShift.SelectedItem.Value + "','" + dr["SubQPId"].ToString() + "','0','" + dr["ConvertTo"].ToString() + "','0','" + clsGroupId + "')", DbConnection.Connection);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        protected void hdChk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)admStdAssignView.HeaderRow.FindControl("hdChk");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in admStdAssignView.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkStatus");
                        chk.Checked = true;

                    }
                }
                else
                {
                    foreach (GridViewRow row in admStdAssignView.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkStatus");
                        chk.Checked = false;

                    }
                }


            }
            catch { }
        }
        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                CheckBox chk = (CheckBox)admStdAssignView.Rows[index_row].Cells[4].FindControl("chkStatus");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(4, "chkStatus", out  checkedRowsAmount);
                chk = (CheckBox)admStdAssignView.HeaderRow.FindControl("hdChk");

                if (checkedRowsAmount == admStdAssignView.Rows.Count)
                {

                    chk.Checked = true;
                }
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        private void CheckedRowsAmount(byte cIndex, string ControlName, out byte checkedRowsAmount)
        {
            try
            {
                byte i = 0;
                foreach (GridViewRow gvr in admStdAssignView.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }

        protected void admStdAssignView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                }
            }
            catch { }
        }
    }
}