using DS.BLL.ControlPanel;
using DS.BLL.ManagedBatch;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.ManagedBatch;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.TimeTable
{
    public partial class ExamRoutine : System.Web.UI.Page
    {
        string Details1 = "";
        string Details2 = "";
        string Details3 = "";
        string Details4 = "";
        string Details5 = "";
        DataTable dtExamSchedule;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamRoutine.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                ddlBatch.Enabled = false;
                btnPrint.Enabled = false;
                btnPrint.CssClass = "";
                GetExamTimeSetName();
                GetBatch();
            }
        }
        private void GetExamTimeSetName()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("select ExamTimeSetNameId,Name from Tbl_Exam_Time_SetName", dt);
            ddlExam.DataTextField = "Name";
            ddlExam.DataValueField = "ExamTimeSetNameId";
            ddlExam.DataSource = dt;
            ddlExam.DataBind();
            ddlExam.Items.Insert(0, new ListItem("Select Exam Time Line", "0"));
        }
        private void GetBatch()
        {
            try
            {
                BatchEntry batchEntry = new BatchEntry();
                List<BatchEntities> batchList = batchEntry.GetEntitiesData().FindAll(c => c.IsUsed == true).ToList();
                ddlBatch.DataTextField = "BatchName";
                ddlBatch.DataValueField = "BatchId";
                ddlBatch.DataSource = batchList;
                ddlBatch.DataBind();
                ddlBatch.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        private void CheckAndLoadRoutineFromDB()
        {
            try
            {
                //---------Validation---------------
                if ( ddlExam.SelectedValue == "0") 
                { 
                    lblMessage.InnerText = "warning->Please select exam time line!"; 
                    ddlExam.Focus();
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";
                    gvExamSchedule.DataSource = null;
                    gvExamSchedule.DataBind();                    
                    return;
                }
                if (rblReportType.SelectedValue != "0" && ddlBatch.SelectedValue == "0") 
                {
                    lblMessage.InnerText = "warning->Please select a Batch!";
                    ddlBatch.Focus();
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";
                    gvExamSchedule.DataSource = null;
                    gvExamSchedule.DataBind();   
                    return;
                }
                //----------------------------------
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from tbl_Exam_TimeSettings_Info where ExamTimeSetNameId=" + ddlExam.SelectedItem.Value + "", dt);
                byte NoOfPeriod = 0;
                if (dt.Rows.Count > 0)
                {
                    NoOfPeriod = byte.Parse(dt.Rows[0]["NoOfPeriod"].ToString());
                    if (rblReportType.SelectedValue == "0")
                    {
                        if (NoOfPeriod == 1) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                        else if (NoOfPeriod == 2) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                        else if (NoOfPeriod == 3) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                        else if (NoOfPeriod == 4) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                        else if (NoOfPeriod == 5) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + "", dt = new DataTable());
                    }
                    else
                    {
                        if (NoOfPeriod == 1) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and Details1 Like '%" + ddlBatch.SelectedItem.Text + "%'", dt = new DataTable());
                        else if (NoOfPeriod == 2) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%')", dt = new DataTable());
                        else if (NoOfPeriod == 3) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%')", dt = new DataTable());
                        else if (NoOfPeriod == 4) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details4 Like '%" + ddlBatch.SelectedItem.Text + "%')", dt = new DataTable());
                        else if (NoOfPeriod == 5) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details4 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details5 Like '%" + ddlBatch.SelectedItem.Text + "%')", dt = new DataTable());
                    }
                    if (dt.Rows.Count < 1)
                    {
                        btnPrint.Enabled = false;
                        btnPrint.CssClass = "";
                        gvExamSchedule.DataSource = null;
                        gvExamSchedule.DataBind();
                        lblMessage.InnerText = "warning-> Routine Not Available!";
                        return;
                    }
                    btnPrint.Enabled = true;
                    btnPrint.CssClass = "btn btn-success";
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

                    //if (rblReportType.SelectedValue !="0")
                    //{
                    //    if (NoOfPeriod == 1)
                    //    {
                    //        for (byte b = 0; b < dt.Rows.Count; b++)
                    //            dt.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString());

                    //    }

                    //    else if (NoOfPeriod == 2)
                    //    {

                    //        for (byte b = 0; b < dt.Rows.Count; b++)
                    //        {
                    //            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString());
                    //        }
                    //    }
                    //    else if (NoOfPeriod == 3)
                    //    {
                    //        for (byte b = 0; b < dt.Rows.Count; b++)
                    //            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString());
                    //    }
                    //    else if (NoOfPeriod == 4)
                    //    {
                    //        for (byte b = 0; b < dt.Rows.Count; b++)
                    //            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString(), dt.Rows[b]["Details4"].ToString());
                    //    }
                    //    else if (NoOfPeriod == 5)
                    //    {
                    //        for (byte b = 0; b < dt.Rows.Count; b++)
                    //            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), dt.Rows[b]["Details1"].ToString(), dt.Rows[b]["Details2"].ToString(), dt.Rows[b]["Details3"].ToString(), dt.Rows[b]["Details4"].ToString(), dt.Rows[b]["Details5"].ToString());
                    //    }
                    //}
                    //else 
                    //{
                    if (NoOfPeriod == 1)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString());
                            dt.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1);
                        }

                    }

                    else if (NoOfPeriod == 2)
                    {

                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2);
                        }
                    }
                    else if (NoOfPeriod == 3)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3);
                        }
                    }
                    else if (NoOfPeriod == 4)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString());
                            Details4 = getDetails(dt.Rows[b]["Details4"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3, Details4);
                        }
                    }
                    else if (NoOfPeriod == 5)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString());
                            Details4 = getDetails(dt.Rows[b]["Details4"].ToString());
                            Details5 = getDetails(dt.Rows[b]["Details5"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3, Details4, Details5);
                        }
                    }
                    //}
                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    var sb = new StringBuilder();
                    divGv.RenderControl(new HtmlTextWriter(new StringWriter(sb)));

                    string s = sb.ToString();
                    Session["__ExamSchedule__"] = s;
                    ViewState["__IsNewRow__"] = "No";

                }
                else
                {
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";
                    gvExamSchedule.DataSource = null;
                    gvExamSchedule.DataBind();
                    lblMessage.InnerText = "warning-> Routine Not Available!";                   
                }
                
            }
            catch {  }
        }

        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndLoadRoutineFromDB();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {   if(rblReportType.SelectedValue=="1")         
            CheckAndLoadRoutineFromDB();
        }
        private string  getDetails(string line) 
        {
            string Details = "";
            string Dtls = "";
            if (!line.Contains(ddlBatch.SelectedItem.Text)&& rblReportType.SelectedValue !="0") 
            {
                return Details = "XXX";
            }
          //  string Line2 = "TestFive2015 Bangla_1st Paper";
           // string cLine = "TestFive2015";
          //  string cNewLine = "TestFive2015 Math";

           // int a = line.LastIndexOf(',');
           // string creatLine = "";
            
          //  string[] getLine2 = Line2.Trim().Split(',');
            //string getNewText = line.Substring(cLine.Length, line.IndexOf(','));
            string[] getBatchInfo = line.Trim().Split(',');
            if (rblReportType.SelectedValue != "0")
            {
                byte b;
                for (b = 0; b < getBatchInfo.Length; b++)
                {
                    if (getBatchInfo[b].Contains(ddlBatch.SelectedItem.Text))
                    {
                        Details = getBatchInfo[b].ToString();
                        string[] stringLine = Details.Split(' ');
                        Details = "";
                        for (byte k = 1; k < stringLine.Length; k++) Details += " " + stringLine[k];
                        Dtls += ","+Details;
                       // break;
                    }
                }
                Dtls = Dtls.Remove(0, 1);
                
            }
            else
            {
                byte b;
                for (b = 0; b < getBatchInfo.Length; b++)
                {
                        string[] stringLine = getBatchInfo[b].Split(' ');
                        Details +=", "+ new String(stringLine[0].Where(Char.IsLetter).ToArray());
                        for (byte k = 1; k < stringLine.Length; k++) Details += " " + stringLine[k];
                    
                }
                Dtls = Details.Remove(0, 1);
            }
            //for (int r = 0; r < getBatchInfo.Length; r++)
            //{
            //    creatLine += getBatchInfo[r] + ",";
            //}
           // creatLine = creatLine.Substring(0, creatLine.LastIndexOf(','));
            
           
            //string NewString = stringLine[0].Remove(stringLine[0].Length - 4, 4);
           // string NewString = new String(stringLine[0].Where(Char.IsLetter).ToArray());
           
            
              //  Details = NewString + " " + stringLine[1];
            return Details = (Dtls == " ") ? "XXX" : Dtls;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/ExamRoutinePrint.aspx?For="+ddlExam.SelectedItem.Text+"-"+ddlBatch.SelectedItem.Text+"-"+rblReportType.SelectedValue+"');", true);
        }

        protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType.SelectedValue != "0") ddlBatch.Enabled = true; else ddlBatch.Enabled = false;
            CheckAndLoadRoutineFromDB();
        }
    }
}