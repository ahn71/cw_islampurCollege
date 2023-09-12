using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.Timetable;
using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class WSClassRoutine : System.Web.UI.Page
    {
        ClassEntry clsEntry;
        SubTeacherName subTeacherName;
        BuildingNameEntry buildingNameEntry;
        RoomEntry rms;
        WeeklyDaysBLL weeklyDays;
        List<WeeklyDaysEntities> daysList;
        ClsTimeSpecificationEntry clsTimeSpecification;
        ClassRoutine clsRoutine;
        List<ClassRoutineEntities> clsRoutineList;
        DataTable dtTeacherSubject;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShiftEntry.GetDropDownList(ddlShift);
                BatchEntry.GetDropdownlist(ddlBatch, true);
            }
            lblMessage.InnerText = "";
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlGroup, ddlBatch.SelectedValue.ToString());

            if (ddlGroup.Items.Count == 1)
            {
                ddlGroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
            }
            else
            {
                ddlGroup.Enabled = true;
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedValue != "0")
                GetTimeTable(false);
        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedIndex == -1) return;
            else { if (ddlSection.SelectedItem.Text == "Select") return; }
            GetTimeTable(false);
        }
        private void GetTimeTable(bool b)
        {
            int count = 0;
            List<int> isBreakNumber = new List<int>();
            List<int> timingId = new List<int>();
            string color = string.Empty;
            if (clsTimeSpecification == null)
            {
                clsTimeSpecification = new ClsTimeSpecificationEntry();
            }
            DataTable dtTimmigset = CRUD.ReturnTableNull("SELECT DISTINCT MAX(ClsTimeSetNameID) as ClsTimeSetNameID FROM Tbl_Class_Routine WHERE ShiftId='"+ddlShift.SelectedValue
                +"' and  BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlGroup.SelectedValue + "' and clsSecID='" + ddlSection.SelectedValue + "' ");
            if (dtTimmigset.Rows[0]["ClsTimeSetNameID"].ToString() == "") return;
            List<ClassTimeSpecificationEntities> clsTimingList = clsTimeSpecification.GetEntitiesData(int.Parse(dtTimmigset.Rows[0]["ClsTimeSetNameID"].ToString()));
            if (clsTimingList != null)
            {

                string tbl = "";
                //  tbl += "<table style='border: 1px solid gray' ><tr ><th><h6 >T / W</h6></th>";
                tbl += "<div class='table-responsive'>";
                tbl += "<table id='routinetable' class='table table-bordered table-hover'><thead><tr><th><h6 class='text-danger text-center'>T / W</h6></th>";
                foreach (var timing in clsTimingList)
                {
                    if (!timing.IsbreakTime)
                    {
                        tbl += string.Format("<th><h4 class='text-muted text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></th>", timing.Name, timing.StartTime, timing.EndTime);
                    }
                    else
                    {
                        tbl += string.Format("<th rowspan='8' class='rotate'><div><h4 class='text-primary text-center'>" +
                                               "{0}</h4><h6 class='text-danger text-center'>" +
                                               "{1:t}-{2:t}</h6></div></th>", timing.Name, timing.StartTime, timing.EndTime);
                        isBreakNumber.Add(count);
                    }
                    timingId.Add(timing.ClsTimeID);
                    count++;
                }
                tbl += "</tr>";
                tbl += "</thead>";
                if (daysList == null)
                {
                    if (weeklyDays == null)
                    {
                        weeklyDays = new WeeklyDaysBLL();
                    }
                    daysList = weeklyDays.GetWDaysEntities().FindAll(c => c.status == false);
                }

                if (clsRoutine == null)
                {
                    clsRoutine = new ClassRoutine();
                }
                clsRoutineList = clsRoutine.GetEntitiesData(int.Parse(dtTimmigset.Rows[0]["ClsTimeSetNameID"].ToString()));
                tbl += "<tbody>";
                foreach (var day in daysList)
                {
                    if (day.status)
                    {
                        tbl += string.Format("<tr><td><h4 class='text-muted text-center'>{0}</h4></td>", day.DayShortName);
                    }
                    else
                    {
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
                                                           &&c.Day.Id == day.Id
                                                           && c.Batch == ddlBatch.SelectedItem.Text.Trim()
                                                           && c.Shift == ddlShift.SelectedItem.Text.Trim()
                                                           && c.Section == ddlSection.SelectedItem.Text.Trim());
                            if (value != null)
                            {
                                build = true;                               
                                tbl += string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                            "<ul id='cr_" + value.ClassRoutineID + "'>" +
                                                                "<li><span  class='label label-default'>{0}<a id='{4}' class='st' href='javascript:void(0);'></a></span></li>" +
                                                                "<li><span class='label label-inverse'>{1}<a class='st' href='javascript:void(0);'></a></span></span></li>" +
                                                                "<li><span class='label label-default'>{2}<a class='br' href='javascript:void(0);'></a></span></span></li>" +
                                                                "<li><span class='label label-inverse'>{3}<a id='{5}' class='br' href='javascript:void(0);'></a></span></span></li>" +
                                                            "</ul></div></td>",
                                                            value.SubInfo.SubjectName != string.Empty ? value.SubInfo.SubjectName + " " + value.CourseInfo.CourseName : "No Suject",
                                                            value.EmpInfo.EmpName != string.Empty ? value.EmpInfo.EmpName+":"+value.EmpInfo.TCode : "No Teacher",
                                                            value.Room.BuildingName != string.Empty ? value.Room.BuildingName : "No Building",
                                                            value.Room.RoomName != string.Empty ? value.Room.RoomName : "No Room",
                                                            value.EmpInfo.EmployeeId, value.Room.RoomId);
                            }
                        }
                        if (!build)
                        {                           
                            tbl += string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                        "<ul id='cr_0' style='font-size:16px'>XXX</ul></div></td>",

                                                        "No Suject", "No Teacher", "No Building", "No Room", "0", "0");
                        }
                    }
                    tbl += "</tr>";
                }
                tbl += "</tbody>";
                tbl += "</table>";
                tbl += "</div>";
                string Group = (ddlGroup.SelectedValue == "0") ? "" : "Group : " + ddlGroup.SelectedItem.Text + " , ";
                string a = "<h4 style='margin-bottom: 4px; text-align: center;'>Shift : " + ddlShift.SelectedItem.Text + " , Batch : " + ddlBatch.SelectedItem.Text + ", " + Group + " Section : " + ddlSection.SelectedItem.Text + "</h4>" + tbl;
                divClassRoutine.Controls.Add(new LiteralControl(a.ToString()));
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();                
                if (dtSclInfo.Rows.Count == 0) return;
                hSchoolName.InnerText = dtSclInfo.Rows[0]["SchoolName"].ToString();
                aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();                
            }

        }

        protected void btnprintRoutine_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType =


 "application/pdf";

                Response.AddHeader(

                "content-disposition", "attachment;filename=TestPage.pdf");

                Response.Cache.SetCacheability(

                HttpCacheability.NoCache);

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);

                pnlRoutine.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());

                Document pdfDoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);

                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                pdfDoc.Open();

                htmlparser.Parse(sr);


                pdfDoc.Close();


                Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                throw new Exception("Error occured: " + ex);
            }
           
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}