using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.Admission;
using DS.BLL.ControlPanel;
using System.Data;
using DS.DAL;

namespace DS.UI.Academic.Students
{
    public partial class StdPromotion : System.Web.UI.Page
    {
        List<BatchPromotionEntities> BatchPromList;
        List<BatchPromotionEntities> BatchPromListAll;
        List<ClassSectionEntities> ClsSecList;
        List<ClassEntities> ClassList;
        BatchPromotionEntities batcent;
        BatchPromotionEntry batchPrmEntry;
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                //---url bind end---
                if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "StdPromotion.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");              
                BatchEntry.GetDropdownlist(dlPreviousBatch,"True");                
                ShiftEntry.GetDropDownList(dlShift);
                GetBlankTable(string.Empty);
            }
        }
        private void GetBlankTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please search for student promotion";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Student Name</th>";
            divInfo += "<th>Crnt. Class</th>";
            divInfo += "<th>Crnt. Roll</th>";
            divInfo += "<th>Crnt. Section</th>";
            divInfo += "<th>Crnt. Group</th>";
            divInfo += "<th>Exam GPA</th>";
            divInfo += "<th>  </th>";
            divInfo += "<th>New. Roll</th>";
            divInfo += "<th>New. Class</th>";
            divInfo += "<th>New. Batch</th>";
            divInfo += "<th>New. Group</th>";
            divInfo += "<th>New. Section</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='10'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            admStdAssignPanel.Controls.Add(new LiteralControl(divInfo));
        }
        protected void rdblStudentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvstdlist.DataSource = null;
            gvstdlist.DataBind();
            GetBlankTable(string.Empty);           
            dlCurrentBatch.Items.Clear();
            dlPreviousBatch.SelectedValue = "0";
          
        }     
        protected void dlPreviousBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = dlPreviousBatch.SelectedValue.Split('_');
                if (rdblStudentStatus.SelectedValue == "00")
                {
                    BatchEntry.GetSameClassBatch(dlCurrentBatch, BatchClsID[1], BatchClsID[0]);
                }
                else
                {
                    BatchEntry.GetUpperClassBatch(dlCurrentBatch, BatchClsID[1]);
                }
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlMainGroup);
                ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlMainGroup.SelectedValue);
                if (ddlMainGroup.Enabled == true)
                {
                    divGroup.Visible = true;
                }
                else
                {
                    divGroup.Visible = false;
                }
            }
            catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPromotionStudent();
        }
        private void SearchPromotionStudent()
        {
            try
            {
                if (dlPreviousBatch.SelectedValue != "0" && dlShift.SelectedValue != "0" && dlCurrentBatch.SelectedValue != "0")
                {
                    string gradeCondition = "";
                    if (rdblStudentStatus.SelectedValue == "1")
                    {
                        gradeCondition = "!=";
                    }
                    else
                    {
                        gradeCondition = "=";
                    }
                    string[] PBatchClsID = dlPreviousBatch.SelectedValue.Split('_');
                    string[] NBatchClsID = dlCurrentBatch.SelectedValue.Split('_');
                    BatchPromotionEntry btcprm = new BatchPromotionEntry();
                    BatchPromList = btcprm.GetEntitiesData(PBatchClsID[0],
                        dlShift.SelectedValue, gradeCondition, ddlMainGroup.SelectedValue, ddlSection.SelectedValue);
                    if (BatchPromList == null)
                    {
                        gvstdlist.DataSource = null;
                        gvstdlist.DataBind();
                        GetBlankTable("Students not found");
                        lblMessage.InnerText = "warning-> Students not found";
                        return;
                    }
                    int count = 0;
                    string clsName = new String(dlCurrentBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                    ClassEntry cls = new ClassEntry();
                    ClassList = cls.GetEntitiesData().FindAll(c => c.ClassName == clsName);
                    BatchPromListAll = new List<BatchPromotionEntities>();
                    for (int i = 0; i < BatchPromList.Count; i++)
                    {
                        count++;
                        batcent = new BatchPromotionEntities();
                        batcent.Student = new CurrentStdEntities()
                        {
                            StudentID = BatchPromList[i].Student.StudentID,
                            FullName = BatchPromList[i].Student.FullName,
                            ClassName = BatchPromList[i].Student.ClassName,
                            SectionName = BatchPromList[i].Student.SectionName,
                            RollNo = BatchPromList[i].Student.RollNo,
                            ClsGrpID = BatchPromList[i].Student.ClsGrpID,
                            ClsSecID = BatchPromList[i].Student.ClsSecID
                        };
                        batcent.Group = new GroupEntities()
                        {
                            GroupName = BatchPromList[i].Group.GroupName == "" ? "No Group" : BatchPromList[i].Group.GroupName
                        };
                        batcent.GPA = decimal.Parse(BatchPromList[i].GPA.ToString());
                        batcent.NewRoll = BatchPromList[i].Student.RollNo;
                        batcent.NewBatchID = int.Parse(NBatchClsID[0]);
                        batcent.NewBatchName = dlCurrentBatch.SelectedItem.Text.Trim();
                        batcent.NewClassID = ClassList[0].ClassID;
                        batcent.NewClassName = ClassList[0].ClassName.Trim();
                        BatchPromListAll.Add(batcent);
                    }
                    gvstdlist.DataSource = BatchPromListAll;
                    gvstdlist.DataBind();
                    return;
                }
                lblMessage.InnerText = "warning-> Please Select Shift, Previous Batch and Promotion Batch before searching";
            }
            catch { }
        }
        public List<BatchPromotionEntities> GetEntitiesData(string batchName, string shiftID)
        {
            List<BatchPromotionEntities> ListEntities = new List<BatchPromotionEntities>();
            return ListEntities;
        }
        protected void gvstdlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the DropDownList in the Row
                    string[] batchClasId = dlCurrentBatch.SelectedValue.Split('_');
                    DropDownList ddlGroup = (e.Row.FindControl("ddlGroup") as DropDownList);
                    if (clsgrpEntry == null)
                    {
                        clsgrpEntry = new ClassGroupEntry();
                    }
                    clsgrpEntry.GetDropDownListClsGrpId(int.Parse(batchClasId[1]), ddlGroup);
                    Label Group = e.Row.FindControl("lblPreGroup") as Label;
                    try
                    {
                        ddlGroup.Items.FindByText(Group.Text).Selected = true;
                    }
                    catch { }
                    //if (ddlGroup.Enabled==false)
                    //{
                        DropDownList ddlSection = (e.Row.FindControl("ddlSection") as DropDownList);
                        ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(batchClasId[1]), ddlGroup.SelectedValue);
                        Label Section = e.Row.FindControl("lblPreSection") as Label;
                        ddlSection.Items.FindByText(Section.Text.Trim()).Selected = true;
                    //}
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
            catch { }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClasId = dlCurrentBatch.SelectedValue.Split('_');
            DropDownList ddlGroup = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlGroup.NamingContainer;
            DropDownList ddlSection = (DropDownList)row.FindControl("ddlSection");
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(batchClasId[1]), ddlGroup.SelectedValue);
        }

        protected void btnUpdateStd_Click(object sender, EventArgs e)
        {
            try
            {
                if(dlPreviousBatch.SelectedValue != "0" && dlShift.SelectedValue != "0" && dlCurrentBatch.SelectedValue != "0")
                {
                    if (gvstdlist.Rows.Count > 0)
                    {
                        var saveList = new List<BatchPromotionEntities>();
                        int count = 0;
                        foreach (GridViewRow row in gvstdlist.Rows)
                        {
                            CheckBox chkStatus = row.FindControl("chkStatus") as CheckBox;
                            if (chkStatus.Checked == true)
                            {
                                HiddenField stdId = row.FindControl("stdID") as HiddenField;
                                Label newBatchName = row.FindControl("lblPromoBatch") as Label;
                                Label newClassName = row.FindControl("lblPromoClassName") as Label;
                                TextBox newRoll = row.FindControl("txtNewRollNo") as TextBox;
                                DropDownList newGroup = row.FindControl("ddlGroup") as DropDownList;
                                DropDownList newSection = row.FindControl("ddlSection") as DropDownList;
                                //........Check Duplicate Roll No From Database.........

                                if (RollValidation(dlShift.SelectedValue,gvstdlist.DataKeys[row.RowIndex].Values[0].ToString(),
                                    newGroup.SelectedValue, newSection.SelectedValue, newRoll.Text) == false)
                                {
                                    lblMessage.InnerText = "warning->" + newRoll.Text + " is Duplicated Roll";
                                    return;
                                }
                                //.........End...............

                                saveList.Add(new BatchPromotionEntities()
                                {
                                    Student = new CurrentStdEntities()
                                    {
                                        StudentID = int.Parse(stdId.Value.Trim())
                                    },
                                    NewBatchID = int.Parse(gvstdlist.DataKeys[row.RowIndex].Values[0].ToString()),
                                    NewBatchName = newBatchName.Text.Trim(),
                                    NewClassID = int.Parse(gvstdlist.DataKeys[row.RowIndex].Values[1].ToString()),
                                    NewClassName = newClassName.Text.Trim(),
                                    NewRoll = int.Parse(newRoll.Text.Trim()),
                                    NewClsgrpID = int.Parse(newGroup.SelectedValue),
                                    NewClsSecID = int.Parse(newSection.SelectedValue),
                                    NewSectionName = newSection.SelectedItem.Text.Trim()
                                });
                                count++;
                            }
                        }
                        if (count > 0)
                        {
                            if (batchPrmEntry == null)
                            {
                                batchPrmEntry = new BatchPromotionEntry();
                            }
                            //....Check Duplicate Roll Number From Gridview
                            var duplicateroll = new List<BatchPromotionEntities>();
                            foreach (var value in saveList)
                            {
                                duplicateroll = saveList.FindAll(c => c.NewRoll == value.NewRoll);
                                if (duplicateroll.Count > 1)
                                {
                                    lblMessage.InnerText = "warning->" + value.NewRoll + " is Duplicate Roll. DisAllow Duplicate Roll";
                                    return;
                                }
                            }
                            //....................End....................

                            bool result = batchPrmEntry.Update(saveList,rdblStudentStatus.SelectedValue);
                            if (result)
                            {
                                SearchPromotionStudent();
                                lblMessage.InnerText = "success-> Saved successfully";
                                return;
                            }
                            lblMessage.InnerText = "error-> Unable to save";
                            return;
                        }
                        lblMessage.InnerText = "warning-> Please Roll No before Saved";
                        return;
                    }
                }
                lblMessage.InnerText = "warning-> Please Select Shift, Previous Batch and Promotion Batch before Saved";
            }
            catch
            { }
        }
        private Boolean RollValidation(string shiftID, string batchId, string clsgrpID, string clsSecID, string rollNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT RollNo FROM CurrentStudentInfo WHERE ConfigId='"+shiftID+"' AND BatchID='"
                    + batchId + "' AND ClsGrpID='" + clsgrpID + "' AND ClsSecID='" + clsSecID + "' AND RollNo='" + rollNo + "'");
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
            gvstdlist.DataSource = null;
            gvstdlist.DataBind();
            dlShift.SelectedValue = "0";
            dlPreviousBatch.SelectedValue = "0";
            dlCurrentBatch.SelectedValue = "0";
            GetBlankTable(string.Empty);
        }    

        protected void ddlMainGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID =dlPreviousBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlMainGroup.SelectedValue);
        }
        protected void hdChk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvstdlist.HeaderRow.FindControl("hdChk");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvstdlist.Rows)
                    {
                        chk = (CheckBox)row.Cells[12].FindControl("chkStatus");
                        chk.Checked = true;

                    }
                }
                else
                {
                    foreach (GridViewRow row in gvstdlist.Rows)
                    {
                        chk = (CheckBox)row.Cells[12].FindControl("chkStatus");
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

                CheckBox chk = (CheckBox)gvstdlist.Rows[index_row].Cells[12].FindControl("chkStatus");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(12, "chkStatus", out  checkedRowsAmount);
                chk = (CheckBox)gvstdlist.HeaderRow.FindControl("hdChk");

                if (checkedRowsAmount == gvstdlist.Rows.Count)
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
                foreach (GridViewRow gvr in gvstdlist.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }
    }
}