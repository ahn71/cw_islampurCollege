
using DS.BLL.Attendace;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.HR;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.Timetable;
using DS.PropertyEntities.Model.HR;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.TimeTable
{
    public partial class ClassRoutineReport : System.Web.UI.Page
    {
        DataSet ds;
        string totalTable;
        string GroupName = "";
        int clm = 0;
        string divInfo = "";
        DataTable dtSft;
        WeeklyDaysBLL weeklyDays;
        List<WeeklyDaysEntities> daysList;
        EmployeeEntry Teacher;
        List<Employee> TeacherList;
        DS.BLL.Timetable.ClassRoutine clsRoutine;
        List<ClassRoutineEntities> ClsRoutineList;
        List<ClassRoutineEntities> CRList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ClassRoutineReport.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                btnPrint.Enabled = false;
                btnPrint.CssClass = "";
                ShiftEntry.GetDropDownList(ddlShift);
                BatchEntry.GetDropdownlist(ddlBatch, true);
                btnPrint_D.CssClass = "";
                btnPrint_D.Enabled = false;
                Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment);
                ForClassRoutineReport.LoadTeacherInfo(ddlTeacher, ForClassRoutineReport.GetAlllist(ddlDepartment));
                if (ddlTeacher.Items.Count > 2)
                {
                    ddlTeacher.Items.Insert(1, new ListItem("All", "00"));
                }
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
        private void loadClassRoutine()
        {
            try
            {
                //......................Validation........................
                if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a shift!"; ddlShift.Focus(); return; }
                if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; ddlBatch.Focus(); return; }
                if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; ddlGroup.Focus(); return; }
                if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; ddlSection.Focus(); return; } 
                //..............................................
                DataTable dtday = new DataTable();
               // sqlDB.fillDataTable("Select distinct Day, OrderNo, Shift From v_ClassRoutine where RoutineId='" + dlClass.SelectedItem.Text + "' ", dtday);
                dtday = ForClassRoutineReport.return_dt_for_Days(ddlShift.SelectedValue,ddlBatch.SelectedValue,ddlGroup.SelectedValue,ddlSection.SelectedValue);
                if (dtday.Rows.Count < 1)
                {
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";
                    divInfo = "<div style='color: red;font-size: 25px;text-align: center;width: 100%;'>Class Routine Not Available !</div>";
                    divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                else
                {
                    btnPrint.Enabled = true;
                    btnPrint.CssClass = "btn btn-success";
                }
                ds = new DataSet();

                for (int j = 0; j < dtday.Rows.Count; j++)
                {
                    DataTable dt = new DataTable();
                    dt = ForClassRoutineReport.return_dt_for_ClassInfo(ddlShift.SelectedValue, ddlBatch.SelectedValue, ddlGroup.SelectedValue, ddlSection.SelectedValue, dtday.Rows[j]["DayId"].ToString());

                    ds.Tables.Add(dt);
                }
              //  Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                int tableColumn = 0;
                for (byte y = 0; y < ds.Tables.Count; y++)
                {
                    if (ds.Tables[y].Rows.Count > tableColumn)
                    {
                        tableColumn = ds.Tables[y].Rows.Count;
                    }
                }

               
                divInfo += "<div style='width:100%'>";//s
                divInfo = " <table id='tblClassRoutine' class='displayRoutine' style=' margin: 3px auto 0'  > ";
                divInfo += "<thead>";

                for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                {
                    divInfo += "<tr>";
                    for (byte b = 0; b < tableColumn; b++)
                    {
                        if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["DayName"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                        if (ds.Tables[x].Rows.Count > clm)
                        {
                            divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] + "<br/>" + ds.Tables[x].Rows[b]["SubName"] + " <br/>(" + ds.Tables[x].Rows[b]["TCodeNo"] +
                                 ") <br/>" + ds.Tables[x].Rows[b]["BuildingName"] + " | " + ds.Tables[x].Rows[b]["RoomName"]+"</th>";
                            clm++;
                        }
                        else divInfo += "<th> &nbsp; </th>";
                    }
                    clm = 0;
                    divInfo += "</tr>";
                }

                divInfo += "</thead>";
                divInfo += "</table>";
                GroupName = (ddlGroup.SelectedValue == "0") ? "" :" Group : "+ ddlGroup.SelectedItem.Text+" , ";
                divInfo = "<div style='width:850px;margin:0px auto'>Shift : " + ddlShift.SelectedItem.Text + " , Batch : " + ddlBatch.SelectedItem.Text + " , " + GroupName + "Section : " + ddlSection.SelectedItem.Text + "</br>" + divInfo + "</div>";
                divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                totalTable += divInfo;

                Session["__ClassRoutine__"] = totalTable;          
                
            }
            catch { }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSection.SelectedValue!="0")
            loadClassRoutine();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/RoutinePrint.aspx?for=ClassRoutine- Class Routine');", true);
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedIndex==-1) return;
            else { if (ddlSection.SelectedItem.Text == "Select") return; }           
            loadClassRoutine();
        }
        // Teacher Wise Class Routine
        private void LoadClassRoutineForTeacher()
        {
            try
            {
                DataTable dtday = new DataTable();
                dtSft = new DataTable();
                DataTable dtTinfo = new DataTable();
                if (ddlTeacher.SelectedValue != "00")
                {
                    dtTinfo = ForClassRoutineReport.dt_TeacherIfo(ddlTeacher.SelectedValue);
                    //DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                    //dtday = dtDays;
                    dtSft = ForClassRoutineReport.dt_For_Shift(ddlTeacher.SelectedValue);
                    for (int s = 0; s < dtSft.Rows.Count; s++)
                    {
                        dtday = ForClassRoutineReport.return_dt_for_Days(ddlTeacher.SelectedValue, dtSft.Rows[s]["ShiftId"].ToString());
                        ds = new DataSet();
                        for (int j = 0; j < dtday.Rows.Count; j++)
                        {
                            DataTable dt = new DataTable();
                            dt = ForClassRoutineReport.return_dt_for_ClassInfo(ddlTeacher.SelectedValue, dtday.Rows[j]["DayID"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
                            ds.Tables.Add(dt);
                        }

                        // Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                        int tableColumn = 0;
                        for (byte y = 0; y < ds.Tables.Count; y++)
                        {
                            if (ds.Tables[y].Rows.Count > tableColumn)
                            {
                                tableColumn = ds.Tables[y].Rows.Count;
                            }
                        }
                        string Tinfo = (s == 1) ? "" : "&nbsp; Name : " + ddlTeacher.SelectedItem.Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "";
                        string Border_Top = (s == 1) ? "" : "border-top: 1px solid gray;";
                        string divInfo = "";
                        divInfo += Tinfo + "<div style='width:850px;" + Border_Top + " margin: 3px auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
                        // divInfo += "<div style='width:850px;margin: 0 auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
                        divInfo += " <table id='tblClassRoutine" + s + "' class='displayRoutine'  > ";
                        divInfo += "<thead>";

                        for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                        {
                            divInfo += "<tr>";
                            for (byte b = 0; b < tableColumn; b++)
                            {
                                if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["DayName"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                                if (ds.Tables[x].Rows.Count > clm)
                                {
                                    string GroupName = (ds.Tables[x].Rows[b]["ClsGrpId"].ToString() == "0") ? "" : " | " + ds.Tables[x].Rows[b]["GroupName"] + "";
                                    divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] +
                                         "<br/>" + ds.Tables[x].Rows[b]["BatchName"] + GroupName + " | " + ds.Tables[x].Rows[b]["SectionName"] +
                                        "<br/>" + ds.Tables[x].Rows[b]["SubName"] +
                                         "<br/>" + ds.Tables[x].Rows[b]["BuildingName"] + " | " + ds.Tables[x].Rows[b]["RoomName"] +
                                        "</th>";
                                    clm++;
                                }
                                else divInfo += "<th> &nbsp; </th>";
                            }
                            clm = 0;
                            divInfo += "</tr>";
                        }

                        divInfo += "</thead>";
                        divInfo += "</table>";
                        divInfo += "</div>";
                        divInfo = "<div style='border: 1px solid gray; margin: 0 auto;width: 862px'>" + divInfo + "</div>";
                        divTeacherRoutine.Controls.Add(new LiteralControl(divInfo));
                        totalTable += divInfo;
                        // Session["__ClassRoutine__"] = "&nbsp; Name : " + ddlTeacher.SelectedItem.Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "</br><div style=' border-top: 1px solid gray; margin: 3px auto 0; width: 850px'>" + totalTable + "</div>";
                        Session["__ClassRoutine__"] = "<div Id='divMain' style='width:862px;border:1px solid gray;margin:0px auto '>" + totalTable + "</div>";
                        // string[] classN = dlClass.SelectedItem.Text.Split('_');
                        // Session["__ClassName__"] = classN[0].ToString();
                    }
                }
                else
                {
                    for (int E = 2; E < ddlTeacher.Items.Count; E++)
                    {
                        dtTinfo = ForClassRoutineReport.dt_TeacherIfo(ddlTeacher.Items[E].Value);
                        //DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                        //dtday = dtDays;
                        dtSft = ForClassRoutineReport.dt_For_Shift(ddlTeacher.Items[E].Value);
                        for (int s = 0; s < dtSft.Rows.Count; s++)
                        {
                            dtday = ForClassRoutineReport.return_dt_for_Days(ddlTeacher.Items[E].Value, dtSft.Rows[s]["ShiftId"].ToString());
                            ds = new DataSet();
                            for (int j = 0; j < dtday.Rows.Count; j++)
                            {
                                DataTable dt = new DataTable();
                                dt = ForClassRoutineReport.return_dt_for_ClassInfo(ddlTeacher.Items[E].Value, dtday.Rows[j]["DayID"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
                                ds.Tables.Add(dt);
                            }

                            // Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                            int tableColumn = 0;
                            for (byte y = 0; y < ds.Tables.Count; y++)
                            {
                                if (ds.Tables[y].Rows.Count > tableColumn)
                                {
                                    tableColumn = ds.Tables[y].Rows.Count;
                                }
                            }
                            string br = (s == 1) ? "" : "</br>";
                            string _Border_Top = (s == 1) ? "" : "border-top: 1px solid gray;";
                            string _Tinfo = (s == 1) ? "" : "&nbsp; Name : " + ddlTeacher.Items[E].Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "";
                            string divInfo = "" + br + "<div Id='divMain' style='width:862px;border:1px solid gray;margin:0px auto'>" + _Tinfo + "<div style=' " + _Border_Top + " margin: 3px auto 0; width: 850px'>";
                            divInfo += "<div style='width:850px;margin: 0 auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
                            divInfo += " <table id='tblClassRoutine" + s + "' class='displayRoutine'  > ";
                            divInfo += "<thead>";

                            for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                            {
                                divInfo += "<tr>";
                                for (byte b = 0; b < tableColumn; b++)
                                {
                                    if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["DayName"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                                    if (ds.Tables[x].Rows.Count > clm)
                                    {
                                        string GroupName = (ds.Tables[x].Rows[b]["ClsGrpId"].ToString() == "0") ? "" : " | " + ds.Tables[x].Rows[b]["GroupName"] + "";
                                        divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] +
                                             "<br/>" + ds.Tables[x].Rows[b]["BatchName"] + GroupName + " | " + ds.Tables[x].Rows[b]["SectionName"] +
                                            "<br/>" + ds.Tables[x].Rows[b]["SubName"] +
                                             "<br/>" + ds.Tables[x].Rows[b]["BuildingName"] + " | " + ds.Tables[x].Rows[b]["RoomName"] +
                                            "</th>";
                                        clm++;
                                    }
                                    else divInfo += "<th> &nbsp; </th>";
                                }
                                clm = 0;
                                divInfo += "</tr>";
                            }

                            divInfo += "</thead>";
                            divInfo += "</table>";
                            divInfo += "</div>";
                            divInfo += "</div>";
                            divInfo += "</div>";
                            divTeacherRoutine.Controls.Add(new LiteralControl(divInfo));
                            totalTable += divInfo;

                            // string[] classN = dlClass.SelectedItem.Text.Split('_');
                            // Session["__ClassName__"] = classN[0].ToString();
                        }
                        //string a = "</br><div style='width:862px;border:1px solid gray'>" + totalTable + "</div>";
                        Session["__ClassRoutine__"] = totalTable;
                    }
                }
            }
            catch { }
        }
        private void generateTeacherLoadReport()
        {
            try
            {
                DataTable dtL = new DataTable();
                string DepartmentList = (ddlDepartment.SelectedValue == "0") ? ForClassRoutineReport.GetAlllist(ddlDepartment) : ddlDepartment.SelectedValue;
                //dtSft = new DataTable();
                if (daysList == null)
                {
                    if (weeklyDays == null)
                    {
                        weeklyDays = new WeeklyDaysBLL();
                    }
                    daysList = weeklyDays.GetWDaysEntities().FindAll(c => c.status == false);
                }
                if (daysList == null) return;
                string divInfo = "";
                divInfo = "<table class='table-defination table table-bordered'><tr><th><h6 class='text-danger'>Teacher / Day</h6></th>";
                foreach (var day in daysList)
                {
                    divInfo += "<th><h6 class='text-danger text-center'>"+day.DayName+"</h6></th>";
                }
                divInfo += "<th><h6 class='text-danger text-center'>Total</h6></th>";
                divInfo += "</tr>";
                if(Teacher==null)
                {
                    Teacher = new EmployeeEntry();
                }        

                if(ddlDepartment.SelectedValue=="0" && ddlTeacher.SelectedValue=="00")
                {
                    TeacherList = Teacher.getTeacher();
                }
                else if (ddlDepartment.SelectedValue != "0" && ddlTeacher.SelectedValue == "00")
                {
                    TeacherList = Teacher.getTeacher().FindAll(c=>c.DepartmentId==int.Parse(ddlDepartment.SelectedValue));
                }
                else if (ddlDepartment.SelectedValue == "0" && ddlTeacher.SelectedValue != "00")
                {
                    TeacherList = Teacher.getTeacher().FindAll(c => c.EmployeeId == int.Parse(ddlTeacher.SelectedValue));
                }
                else if (ddlDepartment.SelectedValue != "0" && ddlTeacher.SelectedValue != "00")
                {
                    TeacherList = Teacher.getTeacher().FindAll(c => c.EmployeeId == int.Parse(ddlTeacher.SelectedValue));
                }
                if (TeacherList == null) return;
                if(clsRoutine==null)
                {
                    clsRoutine = new DS.BLL.Timetable.ClassRoutine();
                }
                string getyear = new String(ddlBatch.SelectedItem.Text.Where(Char.IsNumber).ToArray());
                ClsRoutineList = clsRoutine.GetRoutine(getyear);
                if (ClsRoutineList == null) return;
                
                for (int i = 0; i < TeacherList.Count;i++ )
                {
                    divInfo += "<tr>";
                    divInfo+="<td>"+TeacherList[i].EmpName+"("+TeacherList[i].TCode+")</td>";
                    foreach (var day in daysList)
                    {
                        divInfo += "<td><h6 class='text-center'>" + ClsRoutineList.FindAll(c => c.EmpInfo.EmployeeId == TeacherList[i].EmployeeId && c.Day.Id == day.Id).Count + "</h6></td>";
                    }
                    divInfo += "<td><h6 class='text-center'>" + ClsRoutineList.FindAll(c => c.EmpInfo.EmployeeId == TeacherList[i].EmployeeId).Count + "</h6></td>";
                    divInfo += "</tr>";
                }
               
                divInfo += "</table>";
                Session["__LoadReport__"] = divInfo;
                divTeacherRoutine.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }

        }
        protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);

            if (ddlTeacher.SelectedValue != "0")
            {
                btnPrint_D.CssClass = "btn btn-success ";
                btnPrint_D.Enabled = true;
                if (rblReportType.SelectedValue == "0")
                    LoadClassRoutineForTeacher();
                else
                    generateTeacherLoadReport();
            }
            else
            {
                btnPrint_D.CssClass = "";
                btnPrint_D.Enabled = false;
            }
        }



        protected void btnPrint_D_Click(object sender, EventArgs e)
        {
            if (rblReportType.SelectedValue == "0")
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/RoutinePrint.aspx?for=TeacherClassRoutine');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/LoadReportPrint.aspx');", true);

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string DepartmentList = (ddlDepartment.SelectedValue == "0") ? ForClassRoutineReport.GetAlllist(ddlDepartment) : ddlDepartment.SelectedValue;
            ForClassRoutineReport.LoadTeacherInfo(ddlTeacher, DepartmentList);
            if (ddlTeacher.Items.Count > 2)
            {
                ddlTeacher.Items.Insert(1, new ListItem("All", "00"));
            }
            if (ddlTeacher.SelectedValue != "0")
            {
                btnPrint_D.CssClass = "btn btn-success ";
                btnPrint_D.Enabled = true;
                LoadClassRoutineForTeacher();
            }
            else
            {
                btnPrint_D.CssClass = "";
                btnPrint_D.Enabled = false;
            }
        }

        protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTeacher.SelectedValue = "0";
        } 
    }
}