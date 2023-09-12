using DS.BLL.ControlPanel;
using DS.BLL.Timetable;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class ExamTimeSpecification : System.Web.UI.Page
    {
        public Tbl_ExamTime_SpecificationEntry Exam_TimeSpecification;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamTimeSpecification.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");                
                GetClsTimeSetName();
                DataBindToGridView(null);
            }            
        }

        private void GetClsTimeSetName()
        {
            Tbl_Exam_Time_SetNameEntry clsTimeNameEntry = new Tbl_Exam_Time_SetNameEntry();
            List<Tbl_Exam_Time_SetName> clsTimeSetNameList = clsTimeNameEntry.GetEntitiesData();
            DrpExamTimeSetName.DataTextField = "Name";
            DrpExamTimeSetName.DataValueField = "ExamTimeSetNameId";
            DrpExamTimeSetName.DataSource = clsTimeSetNameList;
            DrpExamTimeSetName.DataBind();
            DrpExamTimeSetName.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        private void TimeNumberCreator(DropDownList DrpHList, DropDownList DrpMList)
        {
            for (byte h = 1; h <= 12; h++)
            {
                if (h < 10) DrpHList.Items.Add("0" + h.ToString());
                else DrpHList.Items.Add(h.ToString());
            }
            DrpHList.Text = "07";
            for (byte m = 0; m < 60; m++)
            {
                if (m < 10) DrpMList.Items.Add("0" + m.ToString());
                else DrpMList.Items.Add(m.ToString());
            }
        }

        private Tbl_ExamTime_Specification GetFormData()
        {
            Tbl_ExamTime_Specification ExamTimeSpcificationEntities = new Tbl_ExamTime_Specification();
            ExamTimeSpcificationEntities.ExamTimeId = int.Parse(lblExamTimeId.Value);
            ExamTimeSpcificationEntities.Name = TxtName.Text.Trim();
            string[] stT = txtstartTime.Text.Split(' ');
            string stTime = stT[0] + ":00" + " " + stT[1];
            string[] ET = txtEndTime.Text.Split(' ');
            string EnTime = ET[0] + ":00" + " " + ET[1];
            ExamTimeSpcificationEntities.StartTime = DateTime.Parse(stTime);
            ExamTimeSpcificationEntities.EndTime = DateTime.Parse(EnTime);
            ExamTimeSpcificationEntities.OrderBy = int.Parse(txtOrderBy.Text);
            ExamTimeSpcificationEntities.IsBreakTime = ChkBrkTime.Checked;
            ExamTimeSpcificationEntities.ExamTimeSetNameId = int.Parse(DrpExamTimeSetName.SelectedValue);
            return ExamTimeSpcificationEntities;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblExamTimeId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindToGridView(int.Parse(DrpExamTimeSetName.SelectedValue)); return; }
                lblExamTimeId.Value = "0";
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
                using (Tbl_ExamTime_Specification entities = GetFormData())
                {
                    if (Exam_TimeSpecification == null)
                    {
                        Exam_TimeSpecification = new Tbl_ExamTime_SpecificationEntry();
                    }
                    Exam_TimeSpecification.AddEntities = entities;
                    bool result = Exam_TimeSpecification.Insert();
                    DataBindToGridView(entities.ExamTimeSetNameId);
                    lblExamTimeId.Value = string.Empty;
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
                using (Tbl_ExamTime_Specification entities = GetFormData())
                {
                    if (Exam_TimeSpecification == null)
                    {
                        Exam_TimeSpecification = new Tbl_ExamTime_SpecificationEntry();
                    }
                    Exam_TimeSpecification.AddEntities = entities;
                    bool result = Exam_TimeSpecification.Update();
                    lblExamTimeId.Value = string.Empty;
                    DataBindToGridView(entities.ExamTimeSetNameId);
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

        protected void DrpExamTimeSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
           DataBindToGridView(int.Parse(DrpExamTimeSetName.SelectedValue));
        }
        
        private void DataBindToGridView(int? ClsTimeSetNameID)
        {
            try
            {
                string divInfo = string.Empty;
                List<Tbl_ExamTime_Specification> ExamTimeSpcftnList = null;
                if (Exam_TimeSpecification == null)
                {
                    Exam_TimeSpecification = new Tbl_ExamTime_SpecificationEntry();
                }
                if (ClsTimeSetNameID != null)
                {
                    ExamTimeSpcftnList = Exam_TimeSpecification.GetEntitiesData(ClsTimeSetNameID);
                }
                divInfo = " <table id='tblClassList' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Serial No</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th>Start Time</th>";
                divInfo += "<th>End Time</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (ExamTimeSpcftnList == null)
                {
                    divInfo += "<tr></tr>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                    divList.Controls.Add(new LiteralControl(divInfo));
                    lblMessage.InnerText = "warning->No Class Time Specification available. Please Select Class Time Set Name.";
                    return;
                }
                string id = string.Empty;
                string ClsTimeSetNameId = string.Empty;
                string brkTime = string.Empty;
                for (int x = 0; x < ExamTimeSpcftnList.Count; x++)
                {
                    id = ExamTimeSpcftnList[x].ExamTimeId.ToString();
                    ClsTimeSetNameId = ExamTimeSpcftnList[x].ExamTimeSetNameId.ToString();
                    brkTime = ExamTimeSpcftnList[x].IsBreakTime.ToString().ToLower();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td id=order" + id + ">" + (x + 1) + "</td>";
                    divInfo += "<td> " + ExamTimeSpcftnList[x].Name.ToString() + "</td>";
                    divInfo += "<td>" + String.Format("{0:t}", ExamTimeSpcftnList[x].StartTime) + "</td>";
                    divInfo += "<td>" + String.Format("{0:t}", ExamTimeSpcftnList[x].EndTime) + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                        divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRow(" + id + "," + ExamTimeSpcftnList[x].ExamTimeSetNameId + "," + ExamTimeSpcftnList[x].OrderBy + ");'/>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void ClearField()
        {
            DrpExamTimeSetName.SelectedValue = "0";
            TxtName.Text = string.Empty;           
            txtOrderBy.Text = string.Empty;
            ChkBrkTime.Checked = false;
        }

       
    }
}