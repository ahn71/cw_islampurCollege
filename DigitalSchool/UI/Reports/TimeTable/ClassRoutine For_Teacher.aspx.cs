using DS.DAL;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.Attendace;
using DS.BLL.Timetable;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.Timetable;

namespace DS.UI.Reports.TimeTable
{
    public partial class ClassRoutine_For_Teacher : System.Web.UI.Page
    {
        
        DataSet ds;
        DataTable dtSft;
        string totalTable;
        int clm = 0;
        public ClsTimeSpecificationEntry clsTimeSpecification;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ClassRoutine For_Teacher.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                btnPrint_D.CssClass = "";
                btnPrint_D.Enabled = false;
                ShiftEntry.GetDropDownList(ddlShift);
                Classes.commonTask.LoadDeprtmentAtttedence(ddlDepartment);
                //ForClassRoutineReport.LoadTeacherInfo(ddlTeacher,ForClassRoutineReport.GetAlllist(ddlDepartment));
                //if (ddlTeacher.Items.Count > 2)
                //{                    
                //    ddlTeacher.Items.Insert(1, new ListItem("All", "00"));
                //}
                //LoadClassRoutineForTeacher();
            }
        }

        private void LoadClassRoutineForTeacher() 
        {
            try
            {
                string divInfo = "";
                DataTable dtday = new DataTable();
                dtSft = new DataTable();
                DataTable dtTinfo = new DataTable();
                if (ddlTeacher.SelectedValue != "00")
                {
                    divInfo += "<table class='tg'><tr><th class='tg-yw4l' rowspan='2'>Teacher's Name<br></th><th class='tg-yw4l'></th>";
                    List<ClassTimeSpecificationEntities> ClsTimeSpcftnList = null;
                    if (clsTimeSpecification == null)
                    {
                        clsTimeSpecification = new ClsTimeSpecificationEntry();
                    }

                    ClsTimeSpcftnList = clsTimeSpecification.GetEntitiesData(int.Parse(ddlShift.SelectedValue));
                    for (int i = 0; i < ClsTimeSpcftnList.Count; i++)
                    {
                        if (i != ClsTimeSpcftnList.Count - 1)
                        {
                            if (ClsTimeSpcftnList[i + 1].IsbreakTime)
                            {

                                divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "<br></th>";
                            }
                            else
                            {

                                if (ClsTimeSpcftnList[i].IsbreakTime)
                                {
                                    divInfo += "<th class='tg-yw4l' rowspan='2'></th>";
                                }
                                else
                                {
                                    divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "</th>";
                                }
                            }
                        }
                        else
                        {
                            if (ClsTimeSpcftnList[i].IsbreakTime)
                            {
                                divInfo += "<th class='tg-yw4l' rowspan='2'></th>";
                            }
                            else
                            {
                                divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "</th>";
                            }
                        }


                    }
                    divInfo += "</tr><tr><td class='tg-yw4l'>Day</td>";
                    for (int i = 0; i < ClsTimeSpcftnList.Count; i++)
                    {
                        if (ClsTimeSpcftnList[i].IsbreakTime)
                        {
                            //divInfo += "<td class='tg-yw4l' colspan='2'></td>";
                        }
                        else
                        {
                            divInfo += "<td class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].StartTime.ToString("HH:MM") + "-" + ClsTimeSpcftnList[i].EndTime.ToString("HH:MM") + "</td>";
                        }

                    }
                    divInfo += "</tr>";
                    
                        dtTinfo = new DataTable();
                        dtTinfo = CRUD.ReturnTableNull("Select DayID,BatchName,SectionName,ClsTimeID,SubName from v_Tbl_Class_Routine where ShiftId='" + ddlShift.SelectedValue + "' and EId='" + ddlTeacher.SelectedValue + "' and BatchYear='" + DateTime.Now.Year + "'");
                        if (dtTinfo.Rows.Count == 0)
                        {
                            divInfo += "</table>";
                            divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                            Session["__ClassRoutine__"] = divInfo;
                            return;
                        }
                        dtday = new DataTable();
                        dtday = CRUD.ReturnTableNull("Select WDayId,ShortDayName from Tbl_Weekly_days where Status='0' order by OrderBy");
                        for (int i = 0; i < dtday.Rows.Count; i++)
                        {
                            divInfo += "<tr>";
                            if (i == 0)
                            {
                                divInfo += "<td class='tg-yw4l' rowspan='6'>" + ddlTeacher.SelectedItem.Text + "</td>";
                            }
                            divInfo += "<td class='tg-yw4l'>" + dtday.Rows[i]["ShortDayName"].ToString() + "<br></td>";
                            for (int j = 0; j < ClsTimeSpcftnList.Count; j++)
                            {
                                if (i == 0)
                                {
                                    DataRow[] dr = dtTinfo.Select("DayID='" + dtday.Rows[i]["WDayId"].ToString() + "' and ClsTimeID='" + ClsTimeSpcftnList[j].ClsTimeID + "'");
                                    if (dr.Count() > 0)
                                    {
                                        string getClass = new String(dr[0].ItemArray[1].ToString().Where(Char.IsLetter).ToArray());
                                        if (ClsTimeSpcftnList[j].IsbreakTime)
                                        {
                                            divInfo += "<td class='tg-yw4l' rowspan='6'>" + getClass + ",<br>(" + dr[0].ItemArray[2] + ")</td><td class='tg-yw4l' rowspan='6'></td><td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                        }
                                        else divInfo += "<td class='tg-yw4l' rowspan='6'>" + getClass + "<br>(" + dr[0].ItemArray[2] + ")</td><td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                    }
                                    else
                                    {
                                        if (ClsTimeSpcftnList[j].IsbreakTime)
                                        {
                                            divInfo += "<td class='tg-yw4l' rowspan='6'></td>";
                                        }
                                        else divInfo += "<td class='tg-yw4l' rowspan='6'></td><td class='tg-yw4l'></td>";
                                    }
                                }
                                else
                                {
                                    if (ClsTimeSpcftnList[j].IsbreakTime) continue;

                                    DataRow[] dr = dtTinfo.Select("DayID='" + dtday.Rows[i]["WDayId"].ToString() + "' and ClsTimeID='" + ClsTimeSpcftnList[j].ClsTimeID + "'");
                                    if (dr.Count() > 0)
                                    {
                                        divInfo += "<td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                    }
                                    else
                                    {
                                        divInfo += "<td class='tg-yw4l'></td>";
                                    }
                                }
                            }
                            divInfo += "</tr>";
                        }
                        //dtTinfo = ForClassRoutineReport.dt_TeacherIfo(ddlTeacher.Items[E].Value);
                        ////DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                        ////dtday = dtDays;
                        //dtSft = ForClassRoutineReport.dt_For_Shift(ddlTeacher.Items[E].Value);
                        //for (int s = 0; s < dtSft.Rows.Count; s++)
                        //{
                        //    dtday = ForClassRoutineReport.return_dt_for_Days(ddlTeacher.Items[E].Value, dtSft.Rows[s]["ShiftId"].ToString());
                        //    ds = new DataSet();
                        //    for (int j = 0; j < dtday.Rows.Count; j++)
                        //    {
                        //        DataTable dt = new DataTable();
                        //        dt = ForClassRoutineReport.return_dt_for_ClassInfo(ddlTeacher.Items[E].Value, dtday.Rows[j]["DayID"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
                        //        ds.Tables.Add(dt);
                        //    }

                        //    // Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                        //    int tableColumn = 0;
                        //    for (byte y = 0; y < ds.Tables.Count; y++)
                        //    {
                        //        if (ds.Tables[y].Rows.Count > tableColumn)
                        //        {
                        //            tableColumn = ds.Tables[y].Rows.Count;
                        //        }
                        //    }
                        //    string br = (s == 1) ? "" : "</br>";
                        //    string _Border_Top = (s == 1) ? "" : "border-top: 1px solid gray;";
                        //    string _Tinfo = (s == 1) ? "" : "&nbsp; Name : " + ddlTeacher.Items[E].Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "";
                        //    divInfo = "" + br + "<div Id='divMain' style='width:862px;border:1px solid gray;margin:0px auto'>" + _Tinfo + "<div style=' " + _Border_Top + " margin: 3px auto 0; width: 850px'>";
                        //    divInfo += "<div style='width:850px;margin: 0 auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
                        //    divInfo += " <table id='tblClassRoutine" + s + "' class='displayRoutine'  > ";
                        //    divInfo += "<thead>";

                        //    for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                        //    {
                        //        divInfo += "<tr>";
                        //        for (byte b = 0; b < tableColumn; b++)
                        //        {
                        //            if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["DayName"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                        //            if (ds.Tables[x].Rows.Count > clm)
                        //            {
                        //                string GroupName = (ds.Tables[x].Rows[b]["ClsGrpId"].ToString() == "0") ? "" : " | " + ds.Tables[x].Rows[b]["GroupName"] + "";
                        //                divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] +
                        //                     "<br/>" + ds.Tables[x].Rows[b]["BatchName"] + GroupName + " | " + ds.Tables[x].Rows[b]["SectionName"] +
                        //                    "<br/>" + ds.Tables[x].Rows[b]["SubName"] +
                        //                     "<br/>" + ds.Tables[x].Rows[b]["BuildingName"] + " | " + ds.Tables[x].Rows[b]["RoomName"] +
                        //                    "</th>";
                        //                clm++;
                        //            }
                        //            else divInfo += "<th> &nbsp; </th>";
                        //        }
                        //        clm = 0;
                        //        divInfo += "</tr>";
                        //    }

                        //    divInfo += "</thead>";
                        //    divInfo += "</table>";
                        //    divInfo += "</div>";
                        //    divInfo += "</div>";
                        //    divInfo += "</div>";
                        //    divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                        //    totalTable += divInfo;

                        //    // string[] classN = dlClass.SelectedItem.Text.Split('_');
                        //    // Session["__ClassName__"] = classN[0].ToString();
                        //}
                        ////string a = "</br><div style='width:862px;border:1px solid gray'>" + totalTable + "</div>";
                        //Session["__ClassRoutine__"] = totalTable;                    
                    divInfo += "</table>";
                    divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                    Session["__ClassRoutine__"] = divInfo;
                }
                else 
                {
                    divInfo += "<table class='tg'><tr><th class='tg-yw4l' rowspan='2'>Teacher's Name<br></th><th class='tg-yw4l'></th>";
                    List<ClassTimeSpecificationEntities> ClsTimeSpcftnList = null;
                    if (clsTimeSpecification == null)
                    {
                        clsTimeSpecification = new ClsTimeSpecificationEntry();
                    }
                    
                     ClsTimeSpcftnList = clsTimeSpecification.GetEntitiesData(int.Parse(ddlShift.SelectedValue));
                     for (int i = 0; i < ClsTimeSpcftnList.Count;i++ )
                     {
                         if (i != ClsTimeSpcftnList.Count-1)
                         {
                             if(ClsTimeSpcftnList[i+1].IsbreakTime)
                             {
                                 
                                     divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "<br></th>";
                             }
                             else
                             {

                                 if (ClsTimeSpcftnList[i].IsbreakTime)
                                 {
                                     divInfo += "<th class='tg-yw4l' rowspan='2'></th>";
                                 }
                                 else
                                 {
                                     divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "</th>";
                                 }
                             }
                         }
                         else
                         {
                             if (ClsTimeSpcftnList[i].IsbreakTime)
                             {
                                 divInfo += "<th class='tg-yw4l' rowspan='2'></th>";
                             }
                             else
                             {
                                 divInfo += "<th class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].Name + "</th>";
                             }
                         }
                         
                         
                     }
                     divInfo += "</tr><tr class='time'><td class='tg-yw4l'>Day</td>";
                     for (int i = 0; i < ClsTimeSpcftnList.Count; i++)
                     {
                         if (ClsTimeSpcftnList[i].IsbreakTime)
                         {
                             //divInfo += "<td class='tg-yw4l' colspan='2'></td>";
                         }
                         else
                         {
                             divInfo += "<td class='tg-yw4l' colspan='2'>" + ClsTimeSpcftnList[i].StartTime.ToString("HH:MM") + "-" + ClsTimeSpcftnList[i].EndTime.ToString("HH:MM") + "</td>";
                         }
                         
                     }
                     divInfo += "</tr>";
                     for (int E = 2; E < ddlTeacher.Items.Count; E++)
                     {
                         dtTinfo = new DataTable();
                         dtTinfo = CRUD.ReturnTableNull("Select DayID,BatchName,SectionName,ClsTimeID,SubName from v_Tbl_Class_Routine where ShiftId='" + ddlShift.SelectedValue + "' and EId='" + ddlTeacher.Items[E].Value + "' and BatchYear='"+DateTime.Now.Year+"'");
                         if(dtTinfo.Rows.Count==0)
                         {
                             continue;
                         }
                         dtday = new DataTable();
                         dtday = CRUD.ReturnTableNull("Select WDayId,ShortDayName from Tbl_Weekly_days where Status='0' order by OrderBy");
                         for(int i=0;i<dtday.Rows.Count;i++)
                         {
                             divInfo += "<tr>";
                             if (i == 0)
                             {
                                 divInfo += "<td class='tg-yw4l' rowspan='6'>" + ddlTeacher.Items[E].Text + "</td>";
                             }
                             divInfo += "<td class='tg-yw4l'>" + dtday.Rows[i]["ShortDayName"].ToString() + "<br></td>";
                             for (int j = 0; j < ClsTimeSpcftnList.Count; j++)
                             {                                 
                                 if (i == 0)
                                 {
                                     DataRow[] dr = dtTinfo.Select("DayID='" + dtday.Rows[i]["WDayId"].ToString() + "' and ClsTimeID='" + ClsTimeSpcftnList[j].ClsTimeID+ "'");
                                     if(dr.Count()>0)
                                     {
                                         string getClass = new String(dr[0].ItemArray[1].ToString().Where(Char.IsLetter).ToArray());
                                         if (ClsTimeSpcftnList[j].IsbreakTime)
                                         {
                                             divInfo += "<td class='tg-yw4l' rowspan='6'>" + getClass + "<br>(" + dr[0].ItemArray[2] + ")</td><td class='tg-yw4l' rowspan='6'></td><td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                         }
                                         else divInfo += "<td class='tg-yw4l' rowspan='6'>" + getClass + "<br>(" + dr[0].ItemArray[2] + ")</td><td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                     }
                                     else
                                     {
                                         if (ClsTimeSpcftnList[j].IsbreakTime)
                                         {
                                             divInfo += "<td class='tg-yw4l' rowspan='6'></td>";
                                         }
                                         else divInfo += "<td class='tg-yw4l' rowspan='6'></td><td class='tg-yw4l'></td>";
                                     }
                                 }
                                 else
                                 {
                                     if (ClsTimeSpcftnList[j].IsbreakTime) continue;
                                         
                                     DataRow[] dr = dtTinfo.Select("DayID='" + dtday.Rows[i]["WDayId"].ToString() + "' and ClsTimeID='" + ClsTimeSpcftnList[j].ClsTimeID + "'");
                                     if (dr.Count() > 0)
                                     {
                                         divInfo += "<td class='tg-yw4l'>" + dr[0].ItemArray[4] + "</td>";
                                     }
                                     else
                                     {
                                         divInfo += "<td class='tg-yw4l'></td>";
                                     }
                                 }
                             }
                             divInfo += "</tr>";
                         }
                         //dtTinfo = ForClassRoutineReport.dt_TeacherIfo(ddlTeacher.Items[E].Value);
                         ////DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                         ////dtday = dtDays;
                         //dtSft = ForClassRoutineReport.dt_For_Shift(ddlTeacher.Items[E].Value);
                         //for (int s = 0; s < dtSft.Rows.Count; s++)
                         //{
                         //    dtday = ForClassRoutineReport.return_dt_for_Days(ddlTeacher.Items[E].Value, dtSft.Rows[s]["ShiftId"].ToString());
                         //    ds = new DataSet();
                         //    for (int j = 0; j < dtday.Rows.Count; j++)
                         //    {
                         //        DataTable dt = new DataTable();
                         //        dt = ForClassRoutineReport.return_dt_for_ClassInfo(ddlTeacher.Items[E].Value, dtday.Rows[j]["DayID"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
                         //        ds.Tables.Add(dt);
                         //    }

                         //    // Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                         //    int tableColumn = 0;
                         //    for (byte y = 0; y < ds.Tables.Count; y++)
                         //    {
                         //        if (ds.Tables[y].Rows.Count > tableColumn)
                         //        {
                         //            tableColumn = ds.Tables[y].Rows.Count;
                         //        }
                         //    }
                         //    string br = (s == 1) ? "" : "</br>";
                         //    string _Border_Top = (s == 1) ? "" : "border-top: 1px solid gray;";
                         //    string _Tinfo = (s == 1) ? "" : "&nbsp; Name : " + ddlTeacher.Items[E].Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "";
                         //    divInfo = "" + br + "<div Id='divMain' style='width:862px;border:1px solid gray;margin:0px auto'>" + _Tinfo + "<div style=' " + _Border_Top + " margin: 3px auto 0; width: 850px'>";
                         //    divInfo += "<div style='width:850px;margin: 0 auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
                         //    divInfo += " <table id='tblClassRoutine" + s + "' class='displayRoutine'  > ";
                         //    divInfo += "<thead>";

                         //    for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                         //    {
                         //        divInfo += "<tr>";
                         //        for (byte b = 0; b < tableColumn; b++)
                         //        {
                         //            if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["DayName"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                         //            if (ds.Tables[x].Rows.Count > clm)
                         //            {
                         //                string GroupName = (ds.Tables[x].Rows[b]["ClsGrpId"].ToString() == "0") ? "" : " | " + ds.Tables[x].Rows[b]["GroupName"] + "";
                         //                divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] +
                         //                     "<br/>" + ds.Tables[x].Rows[b]["BatchName"] + GroupName + " | " + ds.Tables[x].Rows[b]["SectionName"] +
                         //                    "<br/>" + ds.Tables[x].Rows[b]["SubName"] +
                         //                     "<br/>" + ds.Tables[x].Rows[b]["BuildingName"] + " | " + ds.Tables[x].Rows[b]["RoomName"] +
                         //                    "</th>";
                         //                clm++;
                         //            }
                         //            else divInfo += "<th> &nbsp; </th>";
                         //        }
                         //        clm = 0;
                         //        divInfo += "</tr>";
                         //    }

                         //    divInfo += "</thead>";
                         //    divInfo += "</table>";
                         //    divInfo += "</div>";
                         //    divInfo += "</div>";
                         //    divInfo += "</div>";
                         //    divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                         //    totalTable += divInfo;

                         //    // string[] classN = dlClass.SelectedItem.Text.Split('_');
                         //    // Session["__ClassName__"] = classN[0].ToString();
                         //}
                         ////string a = "</br><div style='width:862px;border:1px solid gray'>" + totalTable + "</div>";
                         //Session["__ClassRoutine__"] = totalTable;
                     }
                     divInfo += "</table>";
                     divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                     Session["__ClassRoutine__"] = divInfo;
                }
            }
            catch { }
        }
        private void generateTeacherLoadReport() 
        {
            try {
                DataTable dtL = new DataTable();
                string DepartmentList = (ddlDepartment.SelectedValue == "0") ? ForClassRoutineReport.GetAlllist(ddlDepartment) : ddlDepartment.SelectedValue;
                //dtSft = new DataTable();
                dtSft = ForClassRoutineReport.dt_For_ShiftByDepartment(DepartmentList);
                dtL = ForClassRoutineReport.return_dt_for_TeacherLoadReport(ddlTeacher.SelectedValue, "0", DepartmentList);
                Session["__TeacherLoad__"] = dtL;
                string divInfo = "";
                dtL = new DataTable();
                for (byte s = 0; s < dtSft.Rows.Count; s++)
                {
                    dtL = ForClassRoutineReport.return_dt_for_TeacherLoadReport(ddlTeacher.SelectedValue, dtSft.Rows[s]["ShiftId"].ToString(), DepartmentList);
                    divInfo += "<div style='width:850px;margin: 0 auto 5px'>Shift : " + dtL.Rows[s]["ShiftName"].ToString() + "</br>";//s
                    divInfo += " <table id='tblLoadRoutine" + s + "' class='display'  > ";
                    divInfo += "<thead><tr><th>SL</th><th>Card No</th><th>Name</th><th>T-Code</th><th>Department</th><th>Designation</th><th>Total Class</th></tr></thead>";
                    for (int r = 0; r < dtL.Rows.Count; r++)
                    {
                        int SL = r + 1;
                        divInfo += "<tr><td>" + SL + "</td><td>" + dtL.Rows[r]["ECardNo"] + "</td><td>" + dtL.Rows[r]["EName"] + "</td><td>" + dtL.Rows[r]["TCodeNo"] + "</td>" +
                            "<td>" + dtL.Rows[r]["DName"] + "</td><td>" + dtL.Rows[r]["DesName"] + "</td><td>" + dtL.Rows[r]["TotalClass"] + "</td></tr>";
                    }
                    divInfo += "</table>";
                    divInfo += "</div>";
                }
                divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
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
            if(rblReportType.SelectedValue=="0")
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/TimeTable/RoutinePrint.aspx?for=TeacherClassRoutine');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=TeacherLoad');", true);
                   
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

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            ForClassRoutineReport.LoadTeacherInfoRoutine(ddlTeacher, ddlShift.SelectedValue,DateTime.Now.Year.ToString());
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
    }
}