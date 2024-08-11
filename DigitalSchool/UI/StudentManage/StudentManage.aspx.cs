using adviitRuntimeScripting;
using DS.BLL.Admission;
using DS.BLL.ManagedClass;
using DS.BLL.Timetable;
using DS.DAL;
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
    public partial class StudentManage1 : System.Web.UI.Page
    {
        CurrentStdEntry crntStd;
        DataTable dt;
        ClassSubjectEntry class_subjectEntry;
        ClsTimeSpecificationEntry clsTimeSpecification;
        WeeklyDaysBLL weeklyDays;
        List<WeeklyDaysEntities> daysList;
        ClassRoutine clsRoutine;
        List<ClassRoutineEntities> clsRoutineList;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    LoadStudentInfo();
                }
        }
        private void LoadStudentInfo()
        {
            try
            {
                if (crntStd == null)
                {
                    crntStd = new CurrentStdEntry();
                }
                dt = crntStd.GetLoginStudentInfo(Session["__StudentId__"].ToString());
                lblPicName.Text = dt.Rows[0]["FullName"].ToString();
                lblShift.Text = dt.Rows[0]["ShiftName"].ToString();
                lblBatch.Text = dt.Rows[0]["BatchName"].ToString();
                if (dt.Rows[0]["GroupName"].ToString() == "")
                {
                    lblGroup.Text = "No Group";
                }
                else
                {
                    lblGroup.Text = dt.Rows[0]["GroupName"].ToString();
                }
                lblSection.Text = dt.Rows[0]["SectionName"].ToString();
                lblRoll.Text = dt.Rows[0]["RollNo"].ToString();
                string url = "";
                if (dt.Rows[0]["ImageName"].ToString() != string.Empty)
                {
                    url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                }
                else
                {
                    url = "http://www.placehold.it/300x300/EFEFEF/999&text=no+image";
                }
                stImage.ImageUrl = url;
            }
            catch { }
        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {
            string sqlCmd = "";
            sqlCmd = "Select  v.StudentId,asi.AdmissionNo, "
           + "convert(varchar(11),asi.AdmissionDate,106) as  AdmissionDate, v.ClassName, v.SectionName, v.RollNo, v.FullName, v.Gender, convert(varchar(11),v.DateOfBirth,106) as  "
           + "DateOfBirth, v.BloodGroup, v.Mobile, v.ImageName, v.FathersName, v.FathersProfession, v.FathersYearlyIncome, v.MothersName, v.MothersProfession, v.MothersYearlyIncome, "
           + "v.FathersMobile, v.MothersMoible, v.HomePhone, v.PAVillage, v.PAPostOffice, v.PAThana, v.PADistrict, v.TAViIlage, v.TAPostOffice, v.TAThana, v.TADistrict, v.GuardianName, "
           + "v.GuardianRelation, v.GuardianMobileNo, v.GuardianAddress, v.MotherTongue, v.Nationality, v.PreviousSchoolName, v.TransferCertifiedNo, v.CertifiedDate, v.PreferredClass,"
           + "v.PSCGPA, v.PSCRollNo, v.PSCBoard, v.PSCPassingYear from v_CurrentStudentInfo v INNER JOIN  TBL_STD_Admission_INFO asi ON v.StudentId = asi.StudentID " +
           "where v.StudentId=" + Session["__StudentId__"].ToString() + " ";
            DataTable dt = new DataTable();            
            dt= CRUD.ReturnTableNull(sqlCmd);
            Session["_StudentProfile_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentProfile');", true);  //Open New Tab for Sever side code
        }

        protected void A7_ServerClick(object sender, EventArgs e)
        {
            LoadSubjectList();
        }
        private void LoadSubjectList()
        {
            DataTable dt = new DataTable();
            if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
            DataTable dtClsID = new DataTable();
            dtClsID = CRUD.ReturnTableNull("SELECT ClassID FROM CurrentStudentInfo WHERE StudentId='" + Session["__StudentId__"].ToString() + "'");
            dt = class_subjectEntry.GetDataTable(dtClsID.Rows[0]["ClassID"].ToString());
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning-> No Taught List available"; return;
            }
            Session["_SubjectPattern_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubjectPattern');", true);  //Open New Tab for Sever side code
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

         protected void A4_ServerClick(object sender, EventArgs e)
         {
             GetTimeTable(false);
         }
    }
}