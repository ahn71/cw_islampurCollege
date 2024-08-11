using DS.BLL.ManagedBatch;
using DS.DAL;
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

namespace DS.UI.DSWS
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
                GetExamTimeSetName();
                GetBatch();
            }
        }
        private void GetExamTimeSetName()
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select ExamTimeSetNameId,Name from Tbl_Exam_Time_SetName");
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
                if (ddlExam.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning->Please select exam time line!";
                    ddlExam.Focus();
                    gvExamSchedule.DataSource = null;
                    gvExamSchedule.DataBind();
                    return;
                }
                if ( ddlBatch.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning->Please select a Batch!";
                    ddlBatch.Focus();
                    gvExamSchedule.DataSource = null;
                    gvExamSchedule.DataBind();
                    return;
                }
                //----------------------------------
                DataTable dtExam = new DataTable();
                dtExam = CRUD.ReturnTableNull("select * from tbl_Exam_TimeSettings_Info where ExamTimeSetNameId=" + ddlExam.SelectedItem.Value + "");
                byte NoOfPeriod = 0;
                if (dtExam.Rows.Count > 0)
                {
                    NoOfPeriod = byte.Parse(dtExam.Rows[0]["NoOfPeriod"].ToString());
                    DataTable dt = new DataTable();
                    if (NoOfPeriod == 1) dt = CRUD.ReturnTableNull("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1 from tbl_Exam_TimeSettings_Details where ExScId=" + dtExam.Rows[0]["ExScId"].ToString() + " and Details1 Like '%" + ddlBatch.SelectedItem.Text + "%'");
                    else if (NoOfPeriod == 2) dt = CRUD.ReturnTableNull("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2 from tbl_Exam_TimeSettings_Details where ExScId=" + dtExam.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%')");
                    else if (NoOfPeriod == 3) dt = CRUD.ReturnTableNull("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3 from tbl_Exam_TimeSettings_Details where ExScId=" + dtExam.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%')");
                    else if (NoOfPeriod == 4) dt = CRUD.ReturnTableNull("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4 from tbl_Exam_TimeSettings_Details where ExScId=" + dtExam.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details4 Like '%" + ddlBatch.SelectedItem.Text + "%')");
                    else if (NoOfPeriod == 5) dt = CRUD.ReturnTableNull("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5 from tbl_Exam_TimeSettings_Details where ExScId=" + dtExam.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details2 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details3 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details4 Like '%" + ddlBatch.SelectedItem.Text + "%' or Details5 Like '%" + ddlBatch.SelectedItem.Text + "%')");
                 
                    if (dt.Rows.Count < 1)
                    {
                        dtExamSchedule = new DataTable();
                        gvExamSchedule.DataSource = dtExamSchedule;
                        gvExamSchedule.DataBind();
                        lblMessage.InnerText = "warning-> Routine Not Available!";
                        return;
                    }
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
                    dtExamSchedule = new DataTable();
                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    lblMessage.InnerText = "warning-> Routine Not Available!";
                }

            }
            catch { }
        }

        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndLoadRoutineFromDB();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
         CheckAndLoadRoutineFromDB();
        }
        private string getDetails(string line)
        {
            string Details = "";
            string Dtls = "";
            if (!line.Contains(ddlBatch.SelectedItem.Text))
            {
                return Details = "XXX";
            }
            string[] getBatchInfo = line.Trim().Split(',');
          
            byte b;
            for (b = 0; b < getBatchInfo.Length; b++)
            {
                if (getBatchInfo[b].Contains(ddlBatch.SelectedItem.Text))
                {
                    Details = getBatchInfo[b].ToString();
                    string[] stringLine = Details.Split(' ');
                    Details = "";
                    for (byte k = 1; k < stringLine.Length; k++) Details += " " + stringLine[k];
                    Dtls += "," + Details;
                    // break;
                }
            }
            Dtls = Dtls.Remove(0, 1);
           
            return Details = (Dtls == " ") ? "XXX" : Dtls;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
    }
}