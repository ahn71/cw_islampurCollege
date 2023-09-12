using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.UI.HtmlControls;
using System.Data;
using DS.DAL;
using DS.DAL.AdviitDAL;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.BLL.Timetable;
using System.Text;
using DS.PropertyEntities.Model.Timetable;
using System.Data.SqlClient;
using System.Text;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable
{
    public partial class SetExamTimings : System.Web.UI.Page
    {
        tbl_Exam_TimeSettings_InfoEntry tetsiE;
        tbl_Exam_TimeSettings_Details_Entry tetsdE;
        bool result = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "SetExamTimings.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                GetBatch();
                GetExamTimeSetName();
                ViewState["__Count__"] = "0";
            //    createRoutineTimeLine();
                ViewState["__RoutineText__"] = "";
                ViewState["__IsNewRow__"] = "Yes";
                ViewState["__pIndex__"] = "No";

                
            }
        }

        private void GetBatch()
        {
            try
            {
                BatchEntry batchEntry = new BatchEntry();
                List<BatchEntities> batchList = batchEntry.GetEntitiesData().FindAll(c => c.IsUsed == true).ToList();
                dlBatchName.DataTextField = "BatchName";
                dlBatchName.DataValueField = "BatchId";
                dlBatchName.DataSource = batchList;
                dlBatchName.DataBind();
                dlBatchName.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }

        private void GetExamTimeSetName()
        {
            DataTable dt=new DataTable ();
            sqlDB.fillDataTable("select ExamTimeSetNameId,Name from Tbl_Exam_Time_SetName",dt);
            ddlTimingSet.DataTextField = "Name";
            ddlTimingSet.DataValueField = "ExamTimeSetNameId";
            ddlTimingSet.DataSource = dt;
            ddlTimingSet.DataBind();
            ddlTimingSet.Items.Insert(0, new ListItem("Select Exam Time Line", "0"));
        }

        private void LoadExamStartTime()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select ExamTimeId ,Convert(varchar(9),StartTime,100) as StartTime from Tbl_ExamTime_Specification where ExamTimeSetNameId =" + ddlTimingSet.SelectedItem.Value + "", dt);
                rblPeriodList.DataTextField = "StartTime";
                rblPeriodList.DataValueField = "StartTime";
                rblPeriodList.DataSource = dt;
                rblPeriodList.DataBind();
            }
            catch { }
        }
        DataTable dtExamSchedule=new DataTable ();
        private void createRoutineTimeLine()
        {
            try
            {
               dtExamSchedule = new DataTable();

               
                dtExamSchedule.Columns.Add("Date & Time", typeof(string));
              
                DataTable dt =new DataTable ();
                sqlDB.fillDataTable("select ExamTimeId ,Convert(varchar(9),StartTime,100) as StartTime,Convert(varchar(9),EndTime,100) as EndTime from Tbl_ExamTime_Specification where ExamTimeSetNameId ="+ddlTimingSet.SelectedItem.Value+"",dt);

                for (byte b = 0; b < dt.Rows.Count; b++)
                {
                    dtExamSchedule.Columns.Add(dt.Rows[b]["StartTime"].ToString() + "-" + dt.Rows[b]["EndTime"].ToString(), typeof(string));
                }

                if (dt.Rows.Count == 0)
                {
                    gvExamSchedule.DataSource = null;
                    return;
                }
                dtExamSchedule.Rows.Add("","","");
                

                gvExamSchedule.DataSource = dtExamSchedule;
                gvExamSchedule.DataBind();
                
                Session["__ExamSchedule__"] = dtExamSchedule;



                for (byte b = 0; b < dtExamSchedule.Columns.Count; b++)
                {
                    if (b == 0 || b == 1) gvExamSchedule.Columns[b].ItemStyle.Width = 40;
                    else gvExamSchedule.Columns[b].ItemStyle.Width = 150;
                    gvExamSchedule.Columns[b].ItemStyle.Wrap = true;
                }

            }
            catch { }
        }

        protected void ddlTimingSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (ddlTimingSet.SelectedItem.Value == "0")
            {
                lblMessage.InnerText = "warning->Please select a time line";
                gvExamSchedule.DataSource = null;
                gvExamSchedule.DataBind();
            }
            LoadExamStartTime();  // for load exam start time in radib button list
            if (CheckAndLoadRoutineFromDB()) return;
            createRoutineTimeLine();    // create for table diagram of routine
            
        }

        private bool  CheckAndLoadRoutineFromDB()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from tbl_Exam_TimeSettings_Info where ExamTimeSetNameId="+ddlTimingSet.SelectedItem.Value+"",dt);
                byte NoOfPeriod=0;
                if (dt.Rows.Count > 0)
                {
                     NoOfPeriod = byte.Parse(dt.Rows[0]["NoOfPeriod"].ToString());

                     if (NoOfPeriod == 1) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                     else if (NoOfPeriod == 2) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                     else if (NoOfPeriod == 3) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                     else if (NoOfPeriod == 4) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                     else if (NoOfPeriod == 5) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());

                     dtExamSchedule = new DataTable();


                     dtExamSchedule.Columns.Add("Date & Time", typeof(string));
                     if (NoOfPeriod == 1)
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration1"].ToString(), typeof(string));
                     else if (NoOfPeriod == 2)
                     {
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration1"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration2"].ToString(), typeof(string));
                     }
                     else if (NoOfPeriod == 3)
                     {
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration1"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration2"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration3"].ToString(), typeof(string));
                     }
                     else if (NoOfPeriod == 4)
                     {
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration1"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration2"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration3"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration4"].ToString(), typeof(string));

                     }
                     else if (NoOfPeriod == 5)
                     {
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration1"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration2"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration3"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration4"].ToString(), typeof(string));
                         dtExamSchedule.Columns.Add(dt.Rows[0]["ExamTimeDuration5"].ToString(), typeof(string));
                     }

                     
                    if (NoOfPeriod == 1)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                             dt.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString());
                     
                    }
                    
                    else if (NoOfPeriod == 2)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString());
                    }
                    else if (NoOfPeriod == 3)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString());
                    }
                    else if (NoOfPeriod == 4)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString(), dt.Rows[b]["Details4"].ToString());
                    }
                    else if (NoOfPeriod == 5)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString(), dt.Rows[b]["Details4"].ToString(), dt.Rows[b]["Details5"].ToString());
                    }
                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    Session["__ExamSchedule__"] = dtExamSchedule;
                    ViewState["__IsNewRow__"] = "No";
                    return true;
                }
                gvExamSchedule.DataSource = null;
                gvExamSchedule.DataBind();
                return false;
            }
            catch { return false; }
        }

        protected void dlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                if (dlBatchName.SelectedItem.Value != "0")
                {
                    if (ddlClsGrpId.Enabled == true && ddlClsGrpId.SelectedValue == "0")
                        Tbl_Exam_Time_SetNameEntry.getSubject_CourseList(dlBatchName.SelectedItem.Value, ddlSubject, "0");
                    else if (ddlClsGrpId.Enabled == true && ddlClsGrpId.SelectedValue != "0")
                    {
                        DataTable dtGrpId = new DataTable();
                        sqlDB.fillDataTable("select GroupId from Tbl_Class_Group where ClsGrpID =" + ddlClsGrpId.SelectedValue + "", dtGrpId);
                        Tbl_Exam_Time_SetNameEntry.getSubject_CourseList(dlBatchName.SelectedItem.Value, ddlSubject, dtGrpId.Rows[0]["GroupId"].ToString());
                    }
                    else Tbl_Exam_Time_SetNameEntry.getSubject_CourseList(dlBatchName.SelectedItem.Value, ddlSubject, "0");
                }

                BatchEntry.loadGroupByBatchId(ddlClsGrpId, dlBatchName.SelectedValue.ToString());
                if (ddlClsGrpId.Items.Count > 1)
                {
                    ddlClsGrpId.Enabled = true;
                    divGroup.Visible = true;
                }

                else
                {
                    ddlClsGrpId.Enabled = false;
                    divGroup.Visible = false;
                }
            }
            catch { }
        }
        protected void ddlClsGrpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtGrpId = new DataTable();
                sqlDB.fillDataTable("select GroupId from Tbl_Class_Group where ClsGrpID =" + ddlClsGrpId.SelectedValue + "", dtGrpId);
                Tbl_Exam_Time_SetNameEntry.getSubject_CourseList(dlBatchName.SelectedItem.Value, ddlSubject, dtGrpId.Rows[0]["GroupId"].ToString());
            }
            catch { }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                if (ValidationBasket())
                {
                    if (!validationBasketOFGridview()) return;
                }
                else return;
               // if (ViewState["__IsNewRow__"].ToString() == "Yes") 
                if (checkDateIsExists()) return; // Validation For Date and Subject
                dtExamSchedule = (DataTable)Session["__ExamSchedule__"];
                if (ViewState["__Count__"].ToString() == "0")
                {
                    //dtExamSchedule.Rows.RemoveAt(0);
                    
                }
                byte r;
                for ( r = 0; r < gvExamSchedule.Rows.Count; r++)
                {
                    CheckBox chk =(CheckBox) gvExamSchedule.Rows[r].Cells[0].FindControl("ChkChosen");
                    if (chk.Checked) break;
                }

               
                int getIndex = rblPeriodList.SelectedIndex;


                if (getIndex == 0)
                {
                    ViewState["__RoutineText__"] = gvExamSchedule.Rows[r].Cells[4].Text;
                    if (ViewState["__RoutineText__"].ToString() == "&nbsp;")
                    {
                       
                        ViewState["__RoutineText__"] = "";
                        ViewState["__RoutineText__"] += checkAnd_Replace(r);
                    }
                    else
                    {
                        ViewState["__HasExists_?__"] = checkAnd_Replace(r);
                        if (ViewState["__HasExists_?__"] == null)
                            if (ddlClsGrpId.Enabled)ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text+" "+ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                            else ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                        
                    }
                    dtExamSchedule.Rows[r][0] = txtExamDate.Text.Trim();
                    dtExamSchedule.Rows[r][1] = ViewState["__RoutineText__"].ToString();

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    Session["__dtExamSchedule__"] = dtExamSchedule;
                }
                else if (getIndex == 1)
                {
                    ViewState["__RoutineText__"] = gvExamSchedule.Rows[r].Cells[5].Text;
                    if (ViewState["__RoutineText__"].ToString() == "&nbsp;")
                    {
                        ViewState["__RoutineText__"] = "";
                        ViewState["__RoutineText__"] += checkAnd_Replace(r);
                    }
                    else
                    {
                        ViewState["__HasExists_?__"] = checkAnd_Replace(r);
                        if (ViewState["__HasExists_?__"] == null)
                            if (ddlClsGrpId.Enabled) ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                            else ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                    }
                    dtExamSchedule.Rows[r][0] = txtExamDate.Text.Trim();
                    dtExamSchedule.Rows[r][2] = ViewState["__RoutineText__"];

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    Session["__dtExamSchedule__"] = dtExamSchedule;
                }
                else if (getIndex == 2)
                {
                    ViewState["__RoutineText__"] = gvExamSchedule.Rows[r].Cells[6].Text;
                    if (ViewState["__RoutineText__"].ToString() == "&nbsp;")
                    {
                        ViewState["__RoutineText__"] = "";
                        ViewState["__RoutineText__"] += checkAnd_Replace(r);
                    }
                    else
                    {
                        ViewState["__HasExists_?__"] = checkAnd_Replace(r);
                        if (ViewState["__HasExists_?__"] == null)
                            if (ddlClsGrpId.Enabled) ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                            else ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                    }
                    dtExamSchedule.Rows[r][0] = txtExamDate.Text.Trim();
                    dtExamSchedule.Rows[r][3] = ViewState["__RoutineText__"];

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();

                }
                else if (getIndex == 3)
                {
                    ViewState["__RoutineText__"] = gvExamSchedule.Rows[r].Cells[7].Text;
                    if (ViewState["__RoutineText__"].ToString() == "&nbsp;")
                    {
                        ViewState["__RoutineText__"] = "";
                        ViewState["__RoutineText__"] += checkAnd_Replace(r);
                    }
                    else
                    {
                        ViewState["__HasExists_?__"] = checkAnd_Replace(r);
                        if (ViewState["__HasExists_?__"] == null)
                            if (ddlClsGrpId.Enabled) ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                            else ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                    }
                    dtExamSchedule.Rows[r][0] = txtExamDate.Text.Trim();
                    dtExamSchedule.Rows[r][4] = ViewState["__RoutineText__"];

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();

                }
                else if (getIndex == 4)
                {
                    ViewState["__RoutineText__"] = gvExamSchedule.Rows[r].Cells[8].Text;
                    if (ViewState["__RoutineText__"].ToString() == "&nbsp;")
                    {
                        ViewState["__RoutineText__"] = "";
                        ViewState["__RoutineText__"] += checkAnd_Replace(r);
                    }
                    else
                    {
                        ViewState["__HasExists_?__"] = checkAnd_Replace(r);
                        if (ViewState["__HasExists_?__"] == null)
                            if (ddlClsGrpId.Enabled) ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                            else ViewState["__RoutineText__"] += "," + dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                    }
                    dtExamSchedule.Rows[r][0] = txtExamDate.Text.Trim();
                    dtExamSchedule.Rows[r][5] = ViewState["__RoutineText__"];

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();

                }

                CheckBox chks = (CheckBox)gvExamSchedule.Rows[r].Cells[0].FindControl("ChkChosen");
                chks.Checked = true;
                Session["__ExamTimeTable__"] = dtExamSchedule;
                Session["__ExamSchedule__"] = dtExamSchedule;
                ViewState["__Count__"] = "1";
                ViewState["__IsNewRow__"] = "No";
            }
            catch { }
        }

        private bool checkAlreadyBatchAndSibjectIsAssigned(byte r)
        {
            try {

                for (byte b = 0; b < gvExamSchedule.Rows.Count; b++)
                {
                    if (b != r)
                    {
                        if (rblPeriodList.Items.Count == 1)
                        {
                            if (gvExamSchedule.Rows[b].Cells[2].Text.Trim().Contains(dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text))
                            {
                                lblMessage.InnerText = "error->"+dlBatchName.SelectedItem.Text+" "+ddlSubject.SelectedItem.Text+" already assigned at "+gvExamSchedule.Columns[2].HeaderText.ToString();
                                return true;
                            }
                        }
                    }
                }
                return false;

            }
            catch { return false; }
        }
        private string checkAnd_Replace(byte r)
        {
            try
            {
                byte TimeDurationIndex =(byte) rblPeriodList.SelectedIndex;
                
                StringBuilder sb = new System.Text.StringBuilder();
                string[] getCurrentList;
               
                CheckBox chk = (CheckBox)gvExamSchedule.Rows[r].Cells[0].FindControl("ChkChosen");

                if (chk.Checked)
                {
                    if (gvExamSchedule.Rows[r].Cells[TimeDurationIndex + 4].Text.Trim() == " " || gvExamSchedule.Rows[r].Cells[TimeDurationIndex + 4].Text.Trim() == "&nbsp;")
                    {
                        if (ddlClsGrpId.Enabled)
                        return dlBatchName.SelectedItem.Text +" "+ddlClsGrpId.SelectedItem.Text+" " + ddlSubject.SelectedItem.Text;
                        else return dlBatchName.SelectedItem.Text +" " + ddlSubject.SelectedItem.Text;
                    } 
                    getCurrentList = gvExamSchedule.Rows[r].Cells[TimeDurationIndex + 4].Text.Trim().Split(',');
                    bool status = false;
                    for (byte b = 0; b < getCurrentList.Length; b++)
                    {
                        if (ddlClsGrpId.Enabled)
                        {
                            if (getCurrentList[b].Contains(dlBatchName.SelectedItem.Text) && getCurrentList[b].Contains(ddlClsGrpId.SelectedItem.Text))
                            {
                                getCurrentList[b] = dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                                status = true;
                                break;
                            }
                        }
                        else
                        {
                            if (getCurrentList[b].Contains(dlBatchName.SelectedItem.Text))
                            {
                                getCurrentList[b] = dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                                status = true;
                                break;
                            }
                        }
                    }

                    if (status)
                    {
                        for (byte b = 0; b < getCurrentList.Length; b++)
                        {
                            sb.Append(getCurrentList[b] + ",");
                        }
                        ViewState["__RoutineText__"] = sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')).ToString();
                        return ViewState["__RoutineText__"].ToString();
                    }
                    return null;
                }                  
              
                return null;
            }
            catch { return null; }
        }
        private bool ValidationBasket()
        {
            try
            {
                if (ddlTimingSet.SelectedValue.ToString() == "0")
                {
                    lblMessage.InnerText = "warning->Please select a time line";
                    ddlTimingSet.Focus();
                    return false;
                }
                else if (txtExamDate.Text.Trim().Length < 10)
                {
                    lblMessage.InnerText = "warning->Please select a valid date";
                    txtExamDate.Focus();
                    return false;
                
                }
                else if (dlBatchName.SelectedItem.Value == "0")
                {
                    lblMessage.InnerText = "warning->Please select a valid batch";
                    dlBatchName.Focus();
                    return false;
                }
                else if (divGroup.Visible == true && ddlClsGrpId.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning->Please select a valid group";
                    dlBatchName.Focus();
                    return false;
                }
                else if (ddlSubject.SelectedItem.Value == "0")
                {
                    lblMessage.InnerText = "warning->Please select a sbuject";
                    ddlSubject.Focus();
                    return false;
                }
                else if (rblPeriodList.SelectedIndex == -1)
                {
                    lblMessage.InnerText = "warning->Please select a time duration";
                    rblPeriodList.Focus();
                    return false;
                }              
                return true;
            }
            catch { return false; }
        }

        private bool validationBasketOFGridview()
        {
            try
            {
                bool status = false;
                for (byte b = 0; b < gvExamSchedule.Rows.Count; b++)
                {
                    CheckBox chk = (CheckBox)gvExamSchedule.Rows[b].Cells[0].FindControl("ChkChosen");
                  
                    if (chk.Checked)
                    {
                        status = true;
                        return true;
                    }
                }
                if (!status)
                {
                    lblMessage.InnerText = "warning->Please select row";
                    gvExamSchedule.Focus();
                    return false;
                }
                return true;
            }
            catch { return false; }
        }

        private bool checkDateIsExists() // Validation For Date and Subject
        {
            try
            {
                if (!ViewState["__pIndex__"].ToString().Equals("No")) {
                    gvExamSchedule.Rows[int.Parse(ViewState["__pIndex__"].ToString())].Cells[int.Parse(ViewState["__cIndex__"].ToString())].Style.Add("background-color", "");
                  //gvExamSchedule.Rows[int.Parse(ViewState["__pIndex__"].ToString())].Style.Add("background-color", "");
                    
                }
                string SubjectText = (divGroup.Visible == false) ? dlBatchName.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text : dlBatchName.SelectedItem.Text + " " + ddlClsGrpId.SelectedItem.Text + " " + ddlSubject.SelectedItem.Text;
                for (byte b = 0; b < gvExamSchedule.Rows.Count; b++)
                {
                    CheckBox chk = (CheckBox)gvExamSchedule.Rows[b].Cells[0].FindControl("ChkChosen");
                    if (chk.Checked==false && txtExamDate.Text.Trim().Equals(gvExamSchedule.Rows[b].Cells[3].Text))
                    {
                        lblMessage.InnerText = "warning->Sorry,This date already assigned";
                        gvExamSchedule.Rows[b].Cells[3].Style.Add("background-color", "yellow");//DefaultCellStyle.BackColor = Color.Red; 
                        ViewState["__pIndex__"] = b.ToString();
                        ViewState["__cIndex__"] = "3";
                        return true;
                    }
                    if (rblPeriodList.Items.Count == 1) { 
                    if (gvExamSchedule.Rows[b].Cells[4].Text.Contains(SubjectText)) 
                    {
                        lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                        return true;
                    }
                    }
                    else if (rblPeriodList.Items.Count == 2)
                    {
                        if (gvExamSchedule.Rows[b].Cells[4].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[4].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "4";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[5].Text.Contains(SubjectText))
                        {
                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[5].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "5";
                            return true;
                        }
                    }
                    else if (rblPeriodList.Items.Count == 3)
                    {
                        if (gvExamSchedule.Rows[b].Cells[4].Text.Contains(SubjectText))
                        {
                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[4].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "4";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[5].Text.Contains(SubjectText))
                        {
                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[5].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "5";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[6].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[6].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "6";
                            return true;
                        }                      
                    }
                    else if (rblPeriodList.Items.Count == 4)
                    {
                        if (gvExamSchedule.Rows[b].Cells[4].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[4].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "4";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[5].Text.Contains(SubjectText))
                        {
                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[5].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "5";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[6].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[6].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "6";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[7].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[7].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "7";
                            return true;
                        } 
                    }
                    else if (rblPeriodList.Items.Count == 5)
                    {
                        if (gvExamSchedule.Rows[b].Cells[4].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[4].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "4";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[5].Text.Contains(SubjectText))
                        {
                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[5].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "5";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[6].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[6].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "6";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[7].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[7].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "7";
                            return true;
                        }
                        if (gvExamSchedule.Rows[b].Cells[8].Text.Contains(SubjectText))
                        {

                            lblMessage.InnerText = "warning->Sorry,This Subject is already assigned";
                            gvExamSchedule.Rows[b].Cells[8].Style.Add("background-color", "yellow");
                            ViewState["__pIndex__"] = b.ToString();
                            ViewState["__cIndex__"] = "8";
                            return true;
                        }
                    }
                }        
                return false;
            }
            catch { return false; }
        }

        protected void btnNewDate_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                dtExamSchedule = (DataTable)Session["__ExamSchedule__"];
                dtExamSchedule.Rows.Add("", "", "");
                gvExamSchedule.DataSource = dtExamSchedule;
                gvExamSchedule.DataBind();
                CheckBox chk = (CheckBox)gvExamSchedule.Rows[gvExamSchedule.Rows.Count - 1].Cells[0].FindControl("ChkChosen");
                chk.Checked = true;
                ViewState["__IsNewRow__"] = "Yes";
            }
            catch { }
        }

        protected void rblPeriodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                ViewState["__RoutineText__"] = "";
            }
            catch { }
        }

        private tbl_Exam_TimeSettings_Info GetData()
        {
            try
            {
                tbl_Exam_TimeSettings_Info t_etsi = new tbl_Exam_TimeSettings_Info();
                t_etsi.ExamTimeSetNameId =int.Parse(ddlTimingSet.SelectedItem.Value);
                t_etsi.Year = int.Parse(DateTime.Now.ToString("yyyy"));

                return t_etsi;
            }
            catch { return null; }
               
        }


        private tbl_Exam_TimeSettings_Details GetDetailsData(byte r)
        {
            try
            {
                dtExamSchedule = (DataTable)Session["__ExamTimeTable__"];
                tbl_Exam_TimeSettings_Details tetsd = new tbl_Exam_TimeSettings_Details();
                tetsd.ExamDate = DateTime.ParseExact(gvExamSchedule.Rows[r].Cells[3].Text, "dd-MM-yyyy", null);

                tetsd.ExamTimeDuration1 = dtExamSchedule.Columns[1].ColumnName.ToString();
                tetsd.Details1 = gvExamSchedule.Rows[r].Cells[4].Text;
                
                if (dtExamSchedule.Columns.Count == 3)// 3=4 columns of table
                {
                    tetsd.ExamTimeDuration2 = dtExamSchedule.Columns[2].ColumnName.ToString();
                    tetsd.Details2 = gvExamSchedule.Rows[r].Cells[5].Text;

                    tetsd.ExamTimeDuration3 = "";
                    tetsd.Details3 = "";

                    tetsd.ExamTimeDuration4 = "";
                    tetsd.Details4 = "";

                    tetsd.ExamTimeDuration5 = "";
                    tetsd.Details5 = "";

                   
                }

                else if (dtExamSchedule.Columns.Count == 4)// 4=5 columns of table
                {
                    tetsd.ExamTimeDuration2 = dtExamSchedule.Columns[2].ColumnName.ToString();
                    tetsd.Details2 = gvExamSchedule.Rows[r].Cells[5].Text;

                    tetsd.ExamTimeDuration3 = dtExamSchedule.Columns[3].ColumnName.ToString();
                    tetsd.Details3 = gvExamSchedule.Rows[r].Cells[6].Text;

                    tetsd.ExamTimeDuration4 = "";
                    tetsd.Details4 = "";

                    tetsd.ExamTimeDuration5 = "";
                    tetsd.Details5 = "";
                    
                }

                else if (dtExamSchedule.Columns.Count == 5)// 5=6 columns of table
                {
                    tetsd.ExamTimeDuration2 = dtExamSchedule.Columns[2].ColumnName.ToString();
                    tetsd.Details2 = gvExamSchedule.Rows[r].Cells[5].Text;

                    tetsd.ExamTimeDuration3 = dtExamSchedule.Columns[3].ColumnName.ToString();
                    tetsd.Details3 = gvExamSchedule.Rows[r].Cells[6].Text;

                    tetsd.ExamTimeDuration4 = dtExamSchedule.Columns[4].ColumnName.ToString();
                    tetsd.Details4 = gvExamSchedule.Rows[r].Cells[7].Text;

                    tetsd.ExamTimeDuration5 = "";
                    tetsd.Details5 = "";

                   
                }
                else if (dtExamSchedule.Columns.Count == 6)// 6=7 columns of table
                {
                    tetsd.ExamTimeDuration2 = dtExamSchedule.Columns[2].ColumnName.ToString();
                    tetsd.Details2 = gvExamSchedule.Rows[r].Cells[5].Text;

                    tetsd.ExamTimeDuration3 = dtExamSchedule.Columns[3].ColumnName.ToString();
                    tetsd.Details3 = gvExamSchedule.Rows[r].Cells[6].Text;

                    tetsd.ExamTimeDuration4 = dtExamSchedule.Columns[4].ColumnName.ToString();
                    tetsd.Details4 = gvExamSchedule.Rows[r].Cells[7].Text;

                    tetsd.ExamTimeDuration5 = dtExamSchedule.Columns[5].ColumnName.ToString();
                    tetsd.Details5 = gvExamSchedule.Rows[r].Cells[8].Text;

                    
                }

                return tetsd;
            }
            
            catch { return null; }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                using (tbl_Exam_TimeSettings_Info tetsi = GetData())
                {
                    DeleteExistsRoutine();

                    if (tetsiE == null) tetsiE = new tbl_Exam_TimeSettings_InfoEntry();
                    tetsiE.setValues=tetsi;
                    if (tetsiE.Insert())
                    {
                        for (byte r = 0; r < gvExamSchedule.Rows.Count; r++)
                        {
                            if (r == 0) tbl_Exam_TimeSettings_Details_Entry.SetNoOfPeriod(rblPeriodList.Items.Count.ToString(),ddlTimingSet.SelectedItem.Value);
                            using (tbl_Exam_TimeSettings_Details tetsd = GetDetailsData(r))
                            {
                                if (tetsdE == null) tetsdE = new tbl_Exam_TimeSettings_Details_Entry();
                                tetsdE.SetValues = tetsd;
                                result=tetsdE.Insert();
                            }
                      
                        }
                    
                    }
                    
                }

                if (result) lblMessage.InnerText = "success->Successfully exam routine created.";

               
            }
            catch { }
        }

        private void DeleteExistsRoutine()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from tbl_Exam_TimeSettings_Info where ExamTimeSetNameId =" + ddlTimingSet.SelectedItem.Value + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        protected void gvExamSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                


            }
            catch { }
        }

        protected void gvExamSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());            
            if (e.CommandName == "Clear") 
            {
                if (rblPeriodList.SelectedIndex < 0) 
                {
                    lblMessage.InnerText = "warning-> Please select any time duration then clear."; return;
                }             

               // DataTable dtExamSchedule = (DataTable)Session["__dtExamSchedule__"];
                int cIndex = rblPeriodList.SelectedIndex + 1;
                DataTable dt = (DataTable)Session["__ExamSchedule__"];
                dt.Rows[rIndex][cIndex] = "";
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
                Session["__ExamSchedule__"] = dt;
               // gvExamSchedule.Rows[rIndex].Cells[cIndex].Text = "";
            }
            else if (e.CommandName == "Delete") 
            {
                DataTable dt = (DataTable)Session["__ExamSchedule__"];               
                dt.Rows.RemoveAt(rIndex);
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
                Session["__ExamSchedule__"] = dt;
            }
        }

        protected void chkChosen_Checked(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk;
                foreach(GridViewRow gvr in gvExamSchedule.Rows)
                {
                    chk = (CheckBox)gvr.Cells[0].FindControl("chkChosen");
                    if (chk.Checked)
                    {
                        txtExamDate.Text = gvr.Cells[3].Text;

                        for(byte b=(byte)(gvr.RowIndex+1);b<gvExamSchedule.Rows.Count;b++)
                        {
                            chk = (CheckBox)gvExamSchedule.Rows[b].Cells[0].FindControl("chkChosen");
                            chk.Checked = false;
                        }
                        return;
                    }
                    else
                    {
                        txtExamDate.Text = "";
                        chk.Checked = false;
                    }
                }
            }
            catch { }
        }
        protected void gvExamSchedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

    }
}