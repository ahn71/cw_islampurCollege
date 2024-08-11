using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.Timetable;
using DS.PropertyEntities.Model.Timetable;
using DS.BLL.ManagedBatch;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI.HtmlControls;
using System.Data;
using DS.DAL;
using DS.DAL.AdviitDAL;
using DS.BLL.GeneralSettings;
using DS.BLL.ControlPanel;
using DS.BLL.HR;

namespace DS.UI.Academic.Timetable
{
    public partial class SetClassTimings : System.Web.UI.Page
    {
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
            lblMessage.InnerText = string.Empty;
            if(!Page.IsPostBack)
            {               
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SetClassTimings.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                btnPrint.Enabled = false;
                btnPrint.CssClass = "";
                ShiftEntry.GetDropDownList(ddlShift);
                GetBatch();
                DepartmentEntry.GetDepartmentlist(ddlDepartment);
                GetBuildingName();
            }
        }
        private void GetBatch()
        {
            BatchEntry batchEntry = new BatchEntry();
            if (batchEntry.GetEntitiesData() != null) { 
            List<BatchEntities> batchList = batchEntry.GetEntitiesData().FindAll(c => c.IsUsed == true).ToList();
            dlBatchName.DataTextField = "BatchName";
            dlBatchName.DataValueField = "BatchId";
            dlBatchName.DataSource = batchList;
            dlBatchName.DataBind();
            dlBatchName.Items.Insert(0, new ListItem("...Select...", "0"));
            }
        }       
        protected void dlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClsSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlClsGroup, dlBatchName.SelectedValue.ToString());

            if (ddlClsGroup.Items.Count == 1)
            {
                ddlClsGroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlClsSection, dlBatchName.SelectedValue.ToString(), ddlClsGroup.SelectedItem.Value);
            }
            else
            {
                ddlClsGroup.Enabled = true;

            }
        }
        protected void ddlClsGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlClsSection, dlBatchName.SelectedValue.ToString(), ddlClsGroup.SelectedItem.Value);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //......................Validation........................
            if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a shift!"; ddlShift.Focus(); return; }
            if (dlBatchName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; dlBatchName.Focus(); return; }
            if (ddlClsGroup.Enabled == true && ddlClsGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; ddlClsGroup.Focus(); return; }
            if (ddlClsSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; ddlClsSection.Focus(); return; }           
            //..............................................    
            if(ddlDepartment.SelectedValue!="0")
            {
                DptWiseTeacher();
            }
            SubjectList();  // to get subject and teacher.thta is already setuped in work allotment
           
                GetTimeTable(false);                
                TimeTablePanel.Visible = true;
                if (dtTeacherSubject.Rows.Count > 0 && Session["__View__"].ToString().Equals("true"))
                {
                    btnPrint.Enabled = true;
                    btnPrint.CssClass = "btn btn-warning";
                }
                else
                {
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";
                }
            
        }
        private void SubjectList()
        {
            dtTeacherSubject = new DataTable();
            dtTeacherSubject = CRUD.ReturnTableNull("SELECT SubjectId,CourseId,case courseId when 0 then SubjectName else "
            +"SubjectName+' '+ CourseName end as SubjectName FROM [dbo].[v_Class_Subject_Course_List] where BatchId="
            + dlBatchName.SelectedItem.Value + " AND GroupId=" + GetGroupId() + " UNION SELECT SubjectId,"
            +"CourseId,case courseId when 0 then SubjectName else SubjectName+' '+ CourseName end as SubjectName FROM [dbo]."
            +"[v_Class_Subject_Course_List] where BatchId=" + dlBatchName.SelectedItem.Value + " and GroupId='0'  ");
            if (Session["__SaveUpdate__"].ToString().Equals("true"))
            {
                if (dtTeacherSubject.Rows.Count > 0)
                {
                    StringBuilder DivSB = new StringBuilder();
                    DivSB.Append("<h4 class='drg-event-title'>Subject List</h4>");
                    DivSB.Append("<div id='subject'>");
                    foreach (DataRow dr in dtTeacherSubject.Rows)
                    {
                        DivSB.Append(string.Format("<div id='s_{1}' class='external-event label label-primary'>{0}</div>", dr["SubjectName"].ToString(), dr["SubjectId"].ToString() + "_" + dr["CourseId"].ToString()));
                    }
                    DivSB.Append("</div>");
                    SubjectPanel.Controls.Add(new LiteralControl(DivSB.ToString()));
                }
                else
                {
                    lblMessage.InnerText = "warning-> Please Entry Class Wise Subject for batch " + dlBatchName.SelectedItem.Text + ". Otherwise you cannot create Routine.";
                }
            }
            else lblMessage.InnerText = "warning-> You don't have permission to save/update!";
            
        }
        private int GetGroupId()
        {
            try
            {
                ClassGroupEntry clsgrpEntry = new ClassGroupEntry();
                List<ClassGroupEntities> ClassGroupList = new List<ClassGroupEntities>();
                ClassGroupList = clsgrpEntry.GetEntitiesData().FindAll(d => d.ClsGrpID == int.Parse(ddlClsGroup.SelectedValue));
                int GroupId = ClassGroupList[0].GroupID;
                return GroupId;
            }
            catch { return 0; }
           
        }
        
        private void GetTimeTable(bool b)
        {
            int count = 0;
            List<int> isBreakNumber = new List<int>();
            List<int> timingId = new List<int>();
            string color = string.Empty;
            if(clsTimeSpecification == null)
            {
                clsTimeSpecification = new ClsTimeSpecificationEntry();
            }
            List<ClassTimeSpecificationEntities> clsTimingList = clsTimeSpecification.GetEntitiesData(int.Parse(ddlShift.SelectedValue));
            if(clsTimingList != null)
            {
               
                string tbl = "";
                StringBuilder DivSB = new StringBuilder();
                DivSB.Append("<table class='table-defination table table-bordered'><tr><th><h6 class='text-danger text-center'>T / W</h6></th>");
              //  tbl += "<table style='border: 1px solid gray' ><tr ><th><h6 >T / W</h6></th>";
                tbl += "<table class='table-defination table table-bordered'><tr><th><h6 class='text-danger text-center'>T / W</h6></th>";
                foreach (var timing in clsTimingList)
                {
                    if(!timing.IsbreakTime)
                    {
                        DivSB.Append(string.Format("<th>" +
                                               "<h6 class='text-danger text-center'>" +
                                               "{0:t}-{1:t}</h6></th>",timing.StartTime, timing.EndTime));
                        tbl += string.Format("<th>" +
                                               "<h6 class='text-danger text-center'>" +
                                               "{0:t}-{1:t}</h6></th>",timing.StartTime, timing.EndTime);
                    }
                    else
                    {
                        DivSB.Append(string.Format("<th rowspan='8' class='rotate'><div><h6 class='text-danger text-center'>" +
                                               "{0:t}-{1:t}</h6></div></th>",timing.StartTime, timing.EndTime));
                        tbl += string.Format("<th rowspan='8' class='rotate'><div><h6 class='text-danger text-center'>" +
                                               "{0:t}-{1:t}</h6></div></th>",timing.StartTime, timing.EndTime);
                        isBreakNumber.Add(count);
                    }                    
                    timingId.Add(timing.ClsTimeID); 
                    count++;
                }
                DivSB.Append("</tr>");
                tbl += "</tr>";
                if(daysList == null)
                {
                    if (weeklyDays == null)
                    {
                        weeklyDays = new WeeklyDaysBLL();
                    }
                    daysList = weeklyDays.GetWDaysEntities().FindAll(c=>c.status==false);
                } 
               
                if(clsRoutine == null)
                {
                    clsRoutine = new ClassRoutine();
                }
                clsRoutineList = clsRoutine.GetEntitiesData(int.Parse(ddlShift.SelectedValue));
                       
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
                        if(isBreakNumber.Contains(i))
                        {
                            continue;
                        }
                        bool build = false;
                        if (clsRoutineList != null)
                        {
                            var value = clsRoutineList.Find(c => c.ClassTime.ClsTimeID == timingId[i]
                                                           && c.Day.Id == day.Id
                                                           && c.Batch == dlBatchName.SelectedItem.Text.Trim()
                                                           && c.Shift == ddlShift.SelectedItem.Text.Trim()
                                                           && c.ClsGroup ==ddlClsGroup.SelectedValue
                                                           && c.Section == ddlClsSection.SelectedItem.Text.Trim());
                            if (value != null)
                            {
                                build = true;
                                DivSB.Append(string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                            "<ul id='cr_"+ value.ClassRoutineID +"'>" +
                                                                "<li><span  class='label label-default'>{0}<a id='{4}' class='st' href='javascript:void(0);'>x</a></span></li>" +
                                                                "<li><span class='label label-inverse'>{1}<a class='st' href='javascript:void(0);'>x</a></span></span></li>" +
                                                                "<li><span class='label label-default'>{2}<a class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                                "<li><span class='label label-inverse'>{3}<a id='{5}' class='br' href='javascript:void(0);'>x</a></span></span></li>" +
                                                            "</ul></div></td>",
                                                            value.SubInfo.SubjectName != string.Empty ? value.SubInfo.SubjectName + " " + value.CourseInfo.CourseName : "No Suject",
                                                            value.EmpInfo.EmpName != string.Empty ? value.EmpInfo.EmpName+":"+value.EmpInfo.TCode : "No Teacher",
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
                                                            value.SubInfo.SubjectName != string.Empty ? value.SubInfo.SubjectName + " " + value.CourseInfo.CourseName : "No Suject",
                                                            value.EmpInfo.EmpName != string.Empty ? value.EmpInfo.EmpName + ":" + value.EmpInfo.TCode : "No Teacher",
                                                            value.Room.BuildingName != string.Empty ? value.Room.BuildingName : "No Building",
                                                            value.Room.RoomName != string.Empty ? value.Room.RoomName : "No Room",
                                                            value.EmpInfo.EmployeeId, value.Room.RoomId);
                            }
                        } 
                        if(!build)                        
                        {
                            DivSB.Append(string.Format("<td id='dt_" + day.Id + "_" + timingId[i] + "'><div id='droppable' class='dropped'> " +
                                                        "<ul id='cr_0'>" +
                                                            "<li><span  class='label label-default'>{0}<a id='{4}' class='s' href='javascript:void(0);'>x</a></span></li>" +
                                                            "<li><span class='label label-inverse'>{1}<a class='t' href='javascript:void(0);'>x</a></span></span></li>" +
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
                if(b)
                {
                    Session["_ClassRoutine_"] = DivSB.ToString();
                    return;
                }                
                DayTimePanel.Controls.Add(new LiteralControl(DivSB.ToString()));
                string Group = (ddlClsGroup.SelectedValue == "0") ? "" : "Group : " + ddlClsGroup.SelectedItem.Text + " , ";
                string a = "<h4 style='margin-bottom: 4px; text-align: center;'>Shift : " + ddlShift.SelectedItem.Text + " , Batch : " + dlBatchName.SelectedItem.Text + ", " + Group + " Section : " + ddlClsSection.SelectedItem.Text + "</h4>" + tbl;
                Session["_ClassRoutine_"] = a;
            }
            else
            {

                lblMessage.InnerText = "warning-> The Class timing set of " + ddlShift.SelectedItem.Text + " is empty. Please create first.";
            }            
        }
        
        private void GetBuildingName()
        {
            if(buildingNameEntry == null)
            {
                buildingNameEntry = new BuildingNameEntry();
            }
            List<BuildingNameEntities> buildingList = buildingNameEntry.GetEntitiesData();
            dlBuildingName.DataTextField = "BuildingName";
            dlBuildingName.DataValueField = "BuildingId";
            dlBuildingName.DataSource = buildingList;
            dlBuildingName.DataBind();
            dlBuildingName.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        

        public static DataTable dt = new DataTable();
        private static bool ValidationBasket(string ForRoom,string EId, string roomId, string DayId, string ClassTimeId,string SubId,string courseId,string batchName,string shiftId,string groupid)
        {
            try
            {
                dt = new DataTable();
               //if (ForRoom=="s")
               //dt=CRUD.ReturnTableNull("select StartTime,EndTime,DayID,TCodeNo,EName,RoomName,RoomID,SubName,CourseName,BatchName,SectionName from v_Tbl_Class_Routine where " +
               //     "DayID= "+DayId+" AND "+
               //     "StartTime>=(select StartTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND " +
               //     "EndTime<=(select EndTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND BatchYear=" + new String(batchName.Where(Char.IsNumber).ToArray()) + " AND EId=" + EId + " AND ShiftId=" + shiftId + "");

               if(ForRoom=="t")
                   if(int.Parse(groupid)>0)
                   {
                       dt = CRUD.ReturnTableNull("select StartTime,EndTime,DayID,TCodeNo,EName,RoomName,RoomID,SubName,CourseName,BatchName,SectionName from v_Tbl_Class_Routine where " +
                   "DayID= " + DayId + " AND " +
                   "StartTime>=(select StartTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND " +
                   "EndTime<=(select EndTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND BatchYear=" + new String(batchName.Where(Char.IsNumber).ToArray()) + " AND EId=" + EId + " AND ShiftId=" + shiftId + " and ClsGrpId='" + groupid + "'");
                   }
                       else
                   {
                       dt = CRUD.ReturnTableNull("select StartTime,EndTime,DayID,TCodeNo,EName,RoomName,RoomID,SubName,CourseName,BatchName,SectionName from v_Tbl_Class_Routine where " +
                   "DayID= " + DayId + " AND " +
                   "StartTime>=(select StartTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND " +
                   "EndTime<=(select EndTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND BatchYear=" + new String(batchName.Where(Char.IsNumber).ToArray()) + " AND EId=" + EId + " AND ShiftId=" + shiftId + " ");
                   }
                   

                else if(ForRoom=="br")
                   if (int.Parse(groupid) > 0)
                   {
                       dt = CRUD.ReturnTableNull("select StartTime,EndTime,DayID,TCodeNo,EName,RoomName,RoomID,SubName,CourseName,BatchName,SectionName from v_Tbl_Class_Routine where " +
                       "DayID= " + DayId + " AND " +
                       "StartTime>=(select StartTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND " +
                       "EndTime<=(select EndTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND BatchYear=" + new String(batchName.Where(Char.IsNumber).ToArray()) + " AND RoomID=" + roomId + " AND ShiftId=" + shiftId + " and ClsGrpId='" + groupid + "'");
                   }
                else
                   {
                       dt = CRUD.ReturnTableNull("select StartTime,EndTime,DayID,TCodeNo,EName,RoomName,RoomID,SubName,CourseName,BatchName,SectionName from v_Tbl_Class_Routine where " +
                      "DayID= " + DayId + " AND " +
                      "StartTime>=(select StartTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND " +
                      "EndTime<=(select EndTime from Tbl_ClassTime_Specification where ClsTimeID=" + ClassTimeId + ") AND BatchYear=" + new String(batchName.Where(Char.IsNumber).ToArray()) + " AND RoomID=" + roomId + " AND ShiftId=" + shiftId + "");
                   }

                if (dt.Rows.Count > 0)
                {
                   
                    return true;
                }
                else return false;   
            }
            catch { return false; }
        }

        protected void dlBuildingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dlBuildingName.SelectedValue != "0")
            {
                BuildingClassRM(int.Parse(dlBuildingName.SelectedValue));
            }
        }     
        private void BuildingClassRM(int buildingId)
        {
            if(rms == null)
            {
                rms = new RoomEntry();
            }
            List<RoomEntities> rmList = rms.GetEntitiesData(buildingId);
            if(rmList != null)
            {
                StringBuilder DivSB = new StringBuilder();
                foreach (var rm in rmList)
                {
                    DivSB.Append(string.Format("<div id='br_{2}' class='external-event label label-inverse'>{0}({1})</div>", rm.RoomName, rm.RoomCapacity, rm.RoomId));
                }
                buildings.Controls.Add(new LiteralControl(DivSB.ToString()));
            }
            else
            {
                lblMessage.InnerText = "warning-> " + dlBuildingName.SelectedItem.Text + " is not defined class room yet. please define first";
            }                     
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string chkAndInsertClsRoutine(string clsRoutineId, string EId, string dayId,string SubId, string courseId,string timeid,     
                                                    string batch, string section, string shift, string rmId, string flag,string groupId,string batchName)
        {
            string msg = "failed";
            bool result = false;
            int clsRId = 0;
            ClassRoutine clsRoutine = new ClassRoutine();
            var value = new ClassRoutineEntities();
            ClassRoutineEntities clsRoutineEntities = new ClassRoutineEntities();
            clsRoutineEntities.ClassRoutineID = int.Parse(clsRoutineId.Trim());
            clsRoutineEntities.Day = new WeeklyDaysEntities()
            {
                Id = int.Parse(dayId.Trim())
            };            
            //clsRoutineEntities.Batch = batch.Trim();
            //clsRoutineEntities.Section = section.Trim();
            //clsRoutineEntities.Shift = shift.Trim();
            clsRoutineEntities.BatchId = batch.Trim();
            clsRoutineEntities.ClasGrpId = groupId.Trim();
            clsRoutineEntities.ClsSecId = section.Trim();
            clsRoutineEntities.ShiftId = shift.Trim();
            clsRoutineEntities.BatchYear = new String(batchName.Where(Char.IsNumber).ToArray()).ToString();
            clsRoutineEntities.Room = new RoomEntities()
            {
                RoomId = int.Parse(rmId != string.Empty ? rmId.Trim() : "0")
            };
            clsRoutineEntities.EmpInfo = new PropertyEntities.Model.HR.Employee()
            {
                EmployeeId = int.Parse(EId != string.Empty ? EId.Trim() : "0")
            };
            clsRoutineEntities.SubInfo = new PropertyEntities.Model.ManagedSubject.SubjectEntities()
            {
                SubjectId = int.Parse(SubId != string.Empty ? SubId.Trim() : "0")
            };
            clsRoutineEntities.CourseInfo = new PropertyEntities.Model.ManagedSubject.CourseEntity()
            {
                CourseId = int.Parse(courseId != string.Empty ? courseId.Trim() : "0")
            };
            clsRoutineEntities.ClassTime = new ClassTimeSpecificationEntities()
            {
                ClsTimeID = int.Parse(timeid != string.Empty ? timeid.Trim() : "0")
            };
            clsRoutine.AddEntities = clsRoutineEntities;
            List<ClassRoutineEntities> clsRoutineList = clsRoutine.GetEntitiesData(int.Parse(shift.Trim()));            
            if (clsRoutineList != null)
            {
                if (flag == "br")
                {
                    if (int.Parse(groupId) > 0)
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                               && c.Day.Id == int.Parse(dayId.Trim())
                                                               && c.Room.RoomId == int.Parse(rmId.Trim()));
                    }
                    else
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                               && c.Day.Id == int.Parse(dayId.Trim())
                                                               &&c.ClsGroup==groupId
                                                               && c.Room.RoomId == int.Parse(rmId.Trim()));
                    }
                }
                else if(flag=="s")
                {
                    if (int.Parse(groupId) > 0)
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                              && c.Day.Id == int.Parse(dayId.Trim())
                                                              && c.SubInfo.SubjectId == int.Parse(SubId)
                                                              &&c.ClsGroup==groupId
                                                              && c.CourseInfo.CourseId == int.Parse(courseId));
                    }
                    else
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                              && c.Day.Id == int.Parse(dayId.Trim())
                                                              && c.SubInfo.SubjectId == int.Parse(SubId)
                                                              && c.CourseInfo.CourseId == int.Parse(courseId));
                    }
                }
                else
                {
                    if(int.Parse(groupId)>0)
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                          && c.Day.Id == int.Parse(dayId.Trim())
                                                          &&c.ClsGroup==groupId
                                                          && c.EmpInfo.EmployeeId == int.Parse(EId));
                    }
                    else
                    {
                        value = clsRoutineList.Find(c => c.ClassTime.ShiftId == int.Parse(shift.Trim())
                                                          && c.Day.Id == int.Parse(dayId.Trim())
                                                          && c.EmpInfo.EmployeeId == int.Parse(EId));
                    }
                    
                }
                if (value != null)
                {
                    if (flag == "br")
                        return msg = "RM-conflict";
                    else if (flag == "s")
                        return msg = "s-conflict";
                    else
                        return msg = "t-conflict";
                }
            }                                    
            if (clsRoutineId == "0")
            {
                

                if (ValidationBasket(flag, EId, rmId, dayId,timeid,SubId,courseId, batchName,shift,groupId))
                {
                   msg = "occupied-t_" + dt.Rows[0]["SubName"] + " " + dt.Rows[0]["CourseName"].ToString() + "_" + dt.Rows[0]["BatchName"].ToString() + "(" + dt.Rows[0]["SectionName"].ToString() + ")";
                   
                   return msg;
                }
                
                               
                else result = clsRoutine.Insert(out clsRId);
            }
            else
            {
                clsRId = int.Parse(clsRoutineId.Trim());                
              
                if (ValidationBasket(flag, EId, rmId, dayId,timeid,SubId,courseId, batchName,shift,groupId))
                {
                    msg = "occupied-t_" + dt.Rows[0]["SubName"] + " " + dt.Rows[0]["CourseName"].ToString() + "_" + dt.Rows[0]["BatchName"].ToString() + "(" + dt.Rows[0]["SectionName"].ToString() + ")";
                    return msg;
                }
                else result = clsRoutine.Update(flag);         
            }
            if (result)
            {
                if (flag == "br")
                    msg = "RM_" + clsRId;
                else if (flag == "s")
                    msg = "s_" + clsRId;
                else
                    msg = "t_" + clsRId;
            }                
            return msg;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string chkDelete(string clsRoutineId, string subTeacherId, string rmId)
        {
            string msg = "failed";
            bool result = false;
            if (clsRoutineId!= string.Empty)
            {
                ClassRoutine clsRoutine = new ClassRoutine();
                result = clsRoutine.Update(int.Parse(clsRoutineId.Trim()),
                    int.Parse(subTeacherId != string.Empty ? subTeacherId.Trim() : "0"),
                    int.Parse(rmId != string.Empty ? rmId.Trim() : "0"));
                if(result)
                {
                    msg = "ok";
                }
            }
            return msg;
        }
       
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            GetTimeTable(false);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Academic/Timetable/SetClassTiming_Report.aspx');", true);
            //GetTimeTable(true);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
            //"goToNewTab('/UI/Reports/TimeTable/ClassRoutine.aspx?Batch="+dlBatchName.SelectedItem.Text
            //+"("+dlSection.SelectedItem.Text+")&Shift="+dlShift.SelectedItem.Text+"');", true);  //Open New Tab for Sever side code
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DptWiseTeacher();
        }
        private void DptWiseTeacher()
        {
            try
            {
                dtTeacherSubject = new DataTable();
                dtTeacherSubject = CRUD.ReturnTableNull("select TCodeNo,EName,EId from EmployeeInfo where IsFaculty='True' AND DId=" + ddlDepartment.SelectedValue + " ");

                if (Session["__SaveUpdate__"].ToString().Equals("true"))
                {
                    if (dtTeacherSubject.Rows.Count > 0)
                    {
                        if (clsRoutine == null)
                        {
                            clsRoutine = new DS.BLL.Timetable.ClassRoutine();
                        }
                        string getyear = new String(dlBatchName.SelectedItem.Text.Where(Char.IsNumber).ToArray());
                        clsRoutineList = clsRoutine.GetRoutine(getyear);
                        StringBuilder DivSB = new StringBuilder();
                       // DivSB.Append("<h4 class='drg-event-title'>Teacher List</h4>");
                        DivSB.Append("<div id='teacher'>");
                        foreach (DataRow dr in dtTeacherSubject.Rows)
                        {
                            DivSB.Append(string.Format("<div id='t_{2}' class='external-event label label-inverse'><a href='#' data-toggle='tooltip' data-placement='top' title='" + 0 + " Class Assign'>{0}: {1}</a></div>", dr["EName"].ToString(), dr["TCodeNo"], dr["EId"].ToString()));
                        }
                        DivSB.Append("</div>");
                        TeacherPanel.Controls.Add(new LiteralControl(DivSB.ToString()));
                    }
                    else
                    {
                        lblMessage.InnerText = "warning-> Please Assign Teacher for batch " + dlBatchName.SelectedItem.Text + ". Otherwise you cannot create Routine.";
                    }
                }
                else lblMessage.InnerText = "warning-> You don't have permission to save/update!";
            }
            catch { }

        }
    }
}