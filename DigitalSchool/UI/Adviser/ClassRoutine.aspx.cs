using DS.BLL.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class ClassRoutine : System.Web.UI.Page
    {
        DataSet ds;
        string totalTable;
        int clm = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    LoadClassRoutineForTeacher();
                }
        }
        private void LoadClassRoutineForTeacher()
        {
            try
            {
                DataTable dtday = new DataTable();
                DataTable dtSft = new DataTable();
                DataTable dtTinfo = new DataTable();

                dtTinfo = ForClassRoutineReport.dt_TeacherIfo(Session["__EID__"].ToString());
                    //DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                    //dtday = dtDays;
                dtSft = ForClassRoutineReport.dt_For_Shift(Session["__EID__"].ToString());
                    for (int s = 0; s < dtSft.Rows.Count; s++)
                    {
                        dtday = ForClassRoutineReport.return_dt_for_Days(Session["__EID__"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
                        ds = new DataSet();
                        for (int j = 0; j < dtday.Rows.Count; j++)
                        {
                            DataTable dt = new DataTable();
                            dt = ForClassRoutineReport.return_dt_for_ClassInfo(Session["__EID__"].ToString(), dtday.Rows[j]["DayID"].ToString(), dtSft.Rows[s]["ShiftId"].ToString());
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
                       // string Tinfo = (s == 1) ? "" : "&nbsp; Name : " + ddlTeacher.SelectedItem.Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "";
                       // string Border_Top = (s == 1) ? "" : "border-top: 1px solid gray;";
                        string divInfo = "";
                       // divInfo += Tinfo + "<div style='width:850px;" + Border_Top + " margin: 3px auto 5px'>Shift : " + dtSft.Rows[s]["ShiftName"].ToString() + "</br>";//s
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
                        divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                        totalTable += divInfo;
                        // Session["__ClassRoutine__"] = "&nbsp; Name : " + ddlTeacher.SelectedItem.Text + " | " + dtTinfo.Rows[0]["DName"].ToString() + " | " + dtTinfo.Rows[0]["DesName"].ToString() + " | " + dtTinfo.Rows[0]["ECardNo"].ToString() + "</br><div style=' border-top: 1px solid gray; margin: 3px auto 0; width: 850px'>" + totalTable + "</div>";
                        Session["__ClassRoutine__"] = "<div Id='divMain' style='width:862px;border:1px solid gray;margin:0px auto '>" + totalTable + "</div>";
                        // string[] classN = dlClass.SelectedItem.Text.Split('_');
                        // Session["__ClassName__"] = classN[0].ToString();
                    }
              
            }
            catch { }
        }
    }
}