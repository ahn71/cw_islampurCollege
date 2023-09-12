using DS.BLL.Admission;
using DS.BLL.Timetable;
using DS.DAL;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.StudentManage
{
    public partial class StudentTimetable : System.Web.UI.Page
    {
        ClsTimeSpecificationEntry clsTimeSpecification;
        WeeklyDaysBLL weeklyDays;
        List<WeeklyDaysEntities> daysList;
        ClassRoutine clsRoutine;
        List<ClassRoutineEntities> clsRoutineList;
        CurrentStdEntry crntStd;
        DataTable dt;
        DataTable dtExamSchedule;
        string Details1 = "";
        string Details2 = "";
        string Details3 = "";
        string Details4 = "";
        string Details5 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    lblName.Text = Session["__FullName__"].ToString() + "'s DashBoard";
                    GetExamTimeSetName();
                }
        }
        private void GetExamTimeSetName()
        {
            DataTable dt = new DataTable();
            dt=CRUD.ReturnTableNull("select ExamTimeSetNameId,Name from Tbl_Exam_Time_SetName");
            ddlExam.DataTextField = "Name";
            ddlExam.DataValueField = "ExamTimeSetNameId";
            ddlExam.DataSource = dt;
            ddlExam.DataBind();
            ddlExam.Items.Insert(0, new ListItem("Select Exam Time Line", "0"));
        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {
            GetTimeTable(false);
        }
        private void GetTimeTable(bool b)
        {
            if (crntStd == null)
            {
                crntStd = new CurrentStdEntry();
            }
            dt = crntStd.GetLoginStudentInfo(Session["__StudentId__"].ToString());
            int count = 0;
            List<int> isBreakNumber = new List<int>();
            List<int> timingId = new List<int>();
            string color = string.Empty;
            if (clsTimeSpecification == null)
            {
                clsTimeSpecification = new ClsTimeSpecificationEntry();
            }
            List<ClassTimeSpecificationEntities> clsTimingList = clsTimeSpecification.GetEntitiesData(int.Parse(dt.Rows[0]["ConfigId"].ToString()));
            if (clsTimingList != null)
            {

                string tbl = "";
                StringBuilder DivSB = new StringBuilder();
                DivSB.Append("<table class='table-defination table table-bordered'><tr><th><h6 class='text-danger text-center'>T / W</h6></th>");
                //  tbl += "<table style='border: 1px solid gray' ><tr ><th><h6 >T / W</h6></th>";
                tbl += "<table class='table-defination table table-bordered'><tr><th><h6 class='text-danger text-center'>T / W</h6></th>";
                foreach (var timing in clsTimingList)
                {
                    if (!timing.IsbreakTime)
                    {
                        DivSB.Append(string.Format("<th><h4 class='text-muted text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></th>", timing.Name, timing.StartTime, timing.EndTime));
                        tbl += string.Format("<th><h4 class='text-muted text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></th>", timing.Name, timing.StartTime, timing.EndTime);
                    }
                    else
                    {
                        DivSB.Append(string.Format("<th rowspan='8' class='rotate'><div><h4 class='text-primary text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></div></th>", timing.Name, timing.StartTime, timing.EndTime));
                        tbl += string.Format("<th rowspan='8' class='rotate'><div><h4 class='text-primary text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></div></th>", timing.Name, timing.StartTime, timing.EndTime);
                        isBreakNumber.Add(count);
                    }
                    timingId.Add(timing.ClsTimeID);
                    count++;
                }
                DivSB.Append("</tr>");
                tbl += "</tr>";
                if (daysList == null)
                {
                    if (weeklyDays == null)
                    {
                        weeklyDays = new WeeklyDaysBLL();
                    }
                    daysList = weeklyDays.GetWDaysEntities();
                }

                if (clsRoutine == null)
                {
                    clsRoutine = new ClassRoutine();
                }
                clsRoutineList = clsRoutine.GetEntitiesData(int.Parse(dt.Rows[0]["ConfigId"].ToString()));

                foreach (var day in daysList)
                {
                    if (day.status)
                    {
                        DivSB.Append(string.Format("<tr><td><h4 class='text-muted text-center'>{0}</h4></td>", day.DayShortName));
                        tbl += string.Format("<tr><td><h4 class='text-muted text-center'>{0}</h4></td>", day.DayShortName);
                    }
                    else
                    {
                        DivSB.Append(string.Format("<tr><td><h4 class='text-danger text-center'>{0}</h4></td>", day.DayShortName));
                        tbl += string.Format("<tr><td><h4 class='text-danger text-center'>{0}</h4></td>", day.DayShortName);
                        color = "#ff6c60";
                    }
                    for (int i = 0; i < count; i++)
                    {
                        if (isBreakNumber.Contains(i))
                        {
                            continue;
                        }
                        bool build = false;
                        if (clsRoutineList != null)
                        {
                            var value = clsRoutineList.Find(c => c.ClassTime.ClsTimeID == timingId[i]
                                                           && c.Day.Id == day.Id
                                                           && c.Batch == dt.Rows[0]["BatchName"].ToString()
                                                           && c.Shift == dt.Rows[0]["ShiftName"].ToString()
                                                           && c.Section == dt.Rows[0]["SectionName"].ToString());
                            if (value != null)
                            {
                                build = true;
                                DivSB.Append(string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                            "<ul id='cr_" + value.ClassRoutineID + "'>" +
                                                                "<li><span  class='label label-default'>{0}<a id='{4}' class='st' href='javascript:void(0);'>x</a></span></li>" +
                                                                "<li><span class='label label-inverse'>{1}<a class='st' href='javascript:void(0);'>x</a></span></span></li>" +
                                                                "<li><span class='label label-default'>{2}<a class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                                "<li><span class='label label-inverse'>{3}<a id='{5}' class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                            "</ul></div></td>",
                                                            value.SubInfo.SubjectName != string.Empty ? value.SubInfo.SubjectName : "No Suject",
                                                            value.EmpInfo.EmpName != string.Empty ? value.EmpInfo.EmpName : "No Teacher",
                                                            value.Room.BuildingName != string.Empty ? value.Room.BuildingName : "No Building",
                                                            value.Room.RoomName != string.Empty ? value.Room.RoomName : "No Room",
                                                            value.EmpInfo.EmployeeId, value.Room.RoomId));
                                tbl += string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                            "<ul id='cr_" + value.ClassRoutineID + "'>" +
                                                                "<li><span  class='label label-default'>{0}<a id='{4}' class='st' href='javascript:void(0);'></a></span></li>" +
                                                                "<li><span class='label label-inverse'>{1}<a class='st' href='javascript:void(0);'></a></span></span></li>" +
                                                                "<li><span class='label label-default'>{2}<a class='br' href='javascript:void(0);'></a></span></span></li>" +
                                                                "<li><span class='label label-inverse'>{3}<a id='{5}' class='br' href='javascript:void(0);'></a></span></span></li>" +
                                                            "</ul></div></td>",
                                                            value.SubInfo.SubjectName != string.Empty ? value.SubInfo.SubjectName : "No Suject",
                                                            value.EmpInfo.EmpName != string.Empty ? value.EmpInfo.EmpName : "No Teacher",
                                                            value.Room.BuildingName != string.Empty ? value.Room.BuildingName : "No Building",
                                                            value.Room.RoomName != string.Empty ? value.Room.RoomName : "No Room",
                                                            value.EmpInfo.EmployeeId, value.Room.RoomId);
                            }
                        }
                        if (!build)
                        {
                            DivSB.Append(string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                        "<ul id='cr_0'>" +
                                                            "<li><span  class='label label-default'>{0}<a id='{4}' class='st' href='javascript:void(0);'>x</a></span></li>" +
                                                            "<li><span class='label label-inverse'>{1}<a class='st' href='javascript:void(0);'>x</a></span></span></li>" +
                                                            "<li><span class='label label-default'>{2}<a class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                            "<li><span class='label label-inverse'>{3}<a id='{5}' class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                        "</ul></div></td>",
                                                        "No Suject", "No Teacher", "No Building", "No Room", "0", "0"));
                            tbl += string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                        "<ul id='cr_0' style='font-size:16px'>XXX</ul></div></td>",

                                                        "No Suject", "No Teacher", "No Building", "No Room", "0", "0");
                        }
                    }
                    DivSB.Append("</tr>");
                    tbl += "</tr>";
                }
                DivSB.Append("</table>");
                tbl += "</table>";
                if (b)
                {
                    Session["_ClassRoutine_"] = DivSB.ToString();
                    return;
                }
                string Group = dt.Rows[0]["GroupName"].ToString();
                string a = "<h4 style='margin-bottom: 4px; text-align: center;'>Shift : " + dt.Rows[0]["ShiftName"].ToString() + " , Batch : " + dt.Rows[0]["BatchName"].ToString() + ", " + Group + " Section : " + dt.Rows[0]["SectionName"].ToString() + "</h4>" + tbl;
                Session["_ClassRoutine_"] = a;
            }
            else
            {

                lblMessage.InnerText = "warning-> The Class timing is empty. Please create first.";
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Academic/Timetable/SetClassTiming_Report.aspx');", true);
        }

        protected void A2_ServerClick(object sender, EventArgs e)
        {
            CheckAndLoadRoutineFromDB();
        }
        private void CheckAndLoadRoutineFromDB()
        {
            try
            {              
                //----------------------------------
                divGv.Visible = true;
                if (crntStd == null)
                {
                    crntStd = new CurrentStdEntry();
                }
               DataTable dtstdInfo = crntStd.GetLoginStudentInfo(Session["__StudentId__"].ToString());
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from tbl_Exam_TimeSettings_Info where ExamTimeSetNameId=" + ddlExam.SelectedItem.Value + "", dt);
                byte NoOfPeriod = 0;
                if (dt.Rows.Count > 0)
                {
                         NoOfPeriod = byte.Parse(dt.Rows[0]["NoOfPeriod"].ToString());

                         if (NoOfPeriod == 1) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and Details1 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%'", dt = new DataTable());
                         else if (NoOfPeriod == 2) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details2 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%')", dt = new DataTable());
                         else if (NoOfPeriod == 3) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details2 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details3 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%')", dt = new DataTable());
                         else if (NoOfPeriod == 4) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details2 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details3 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details4 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%')", dt = new DataTable());
                         else if (NoOfPeriod == 5) sqlDB.fillDataTable("select format(ExamDate,'dd-MM-yyyy') as ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5 from tbl_Exam_TimeSettings_Details where ExScId=" + dt.Rows[0]["ExScId"].ToString() + " and (Details1 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details2 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details3 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details4 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%' or Details5 Like '%" + dtstdInfo.Rows[0]["BatchName"].ToString() + "%')", dt = new DataTable());
                   
                    if (dt.Rows.Count < 1)
                    {                       
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
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            dt.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1);
                        }

                    }

                    else if (NoOfPeriod == 2)
                    {

                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2);
                        }
                    }
                    else if (NoOfPeriod == 3)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3);
                        }
                    }
                    else if (NoOfPeriod == 4)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details4 = getDetails(dt.Rows[b]["Details4"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3, Details4);
                        }
                    }
                    else if (NoOfPeriod == 5)
                    {
                        for (byte b = 0; b < dt.Rows.Count; b++)
                        {
                            Details1 = getDetails(dt.Rows[b]["Details1"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details2 = getDetails(dt.Rows[b]["Details2"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details3 = getDetails(dt.Rows[b]["Details3"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details4 = getDetails(dt.Rows[b]["Details4"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            Details5 = getDetails(dt.Rows[b]["Details5"].ToString(), dtstdInfo.Rows[0]["BatchName"].ToString());
                            dtExamSchedule.Rows.Add(dt.Rows[b]["ExamDate"].ToString(), Details1, Details2, Details3, Details4, Details5);
                        }
                    }
                    //}

                    gvExamSchedule.DataSource = dtExamSchedule;
                    gvExamSchedule.DataBind();
                    var sb = new StringBuilder();
                    divGv.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                    string s = sb.ToString();
                    divGv.Visible = false;
                    Session["__ExamSchedule__"] = s;
                    ViewState["__IsNewRow__"] = "No";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/ExamRoutinePrint.aspx?For=" + ddlExam.SelectedItem.Text + "-" + dtstdInfo.Rows[0]["BatchName"].ToString() + "-" + 1 + "');", true);

                }

            }
            catch { }
        }
        private string getDetails(string line,string batch)
        {
            string Details = "";
            if (!line.Contains(batch))
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
           
                byte b;
                for (b = 0; b < getBatchInfo.Length; b++)
                {
                    if (getBatchInfo[b].Contains(batch))
                    {
                        Details = getBatchInfo[b].ToString();
                        break;
                    }
                }
                string[] stringLine = Details.Split(' ');
                Details = "";
                for (byte k = 1; k < stringLine.Length; k++) Details += " " + stringLine[k];
             
            
            return Details = (Details == " ") ? "XXX" : Details;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
    }
}