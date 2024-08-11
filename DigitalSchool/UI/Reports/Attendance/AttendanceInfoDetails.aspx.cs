using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Attendance
{
    public partial class AttendanceInfoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        DataTable dt;
        DataSet ds;
        private void loadAllClassAttendance()
        {
            try
            {
                DataTable dtSheetName = new DataTable();
                sqlDB.fillDataTable("Select ASName From AttendanceSheetInfo Where Year='" + DateTime.Now.Year.ToString() + "' ", dtSheetName);


                ds = new DataSet();
                for (int j = 0; j < dtSheetName.Rows.Count; j++)
                {
                    dt = new DataTable();

                    if (string.IsNullOrEmpty("")) sqlDB.fillDataTable("Select CurrentStudentInfo.FullName,CurrentStudentInfo.RollNo, " + dtSheetName.Rows[j]["ASName"] + ".*"
                    + "from " + dtSheetName.Rows[j]["ASName"] + " , CurrentStudentInfo where " + dtSheetName.Rows[j]["ASName"] + ".StudentId=CurrentStudentInfo."
                    + "StudentId Order By CurrentStudentInfo.RollNo ", dt);
                    if (string.Equals("", "")) sqlDB.fillDataTable("Select CurrentStudentInfo.FullName,CurrentStudentInfo.RollNo, " + dtSheetName.Rows[j]["ASName"] + ".* from"
                     + "" + dtSheetName.Rows[j]["ASName"] + " , CurrentStudentInfo where " + dtSheetName.Rows[j]["ASName"] + ".StudentId=CurrentStudentInfo."
                     + "StudentId Order By CurrentStudentInfo.RollNo ", dt);

                    dt.Columns.Add("Total Present");           //Add New Columns Total Present
                    dt.Columns.Add("Total Absent");           //Add New Columns Total Absent
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int totalabsent = 0, totalpresent = 0;
                        for (int x = 0; x < dt.Columns.Count; x++)
                        {
                            if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "a".ToUpper())
                            {
                                totalabsent++;
                            }
                            else if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "p".ToUpper())
                            {
                                totalpresent++;
                            }
                        }
                        dt.Rows[i]["Total Present"] = totalpresent;     //Add New Row Total Present
                        dt.Rows[i]["Total Absent"] = totalabsent;      //Add New Row Total Absent
                    }

                    dt.Columns.Remove("StudentId");
                    DataView dv = new DataView(dt);

                    DataTable dtsummary = dv.ToTable(true, "FullName", "RollNo", "Total Present", "Total Absent");
                    ds.Tables.Add(dtsummary);
                }

                for (int y = 0; y < ds.Tables.Count; y++)
                {
                    string divInfo = "";
                    divInfo += "<h6 class='HeaderTitle'>" + dtSheetName.Rows[y]["ASName"] + "</h6>";

                    divInfo += " <table id='tblAttendanceInfo' class='display'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='width:200px'>Name</th>";
                    divInfo += "<th class='numeric' style='width:80px'>Roll No</th>";
                    divInfo += "<th class='numeric' style='width:80px'>Total Present</th>";
                    divInfo += "<th class='numeric' style='width:80px'>Total Absent</th>";

                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    for (int x = 0; x < ds.Tables[y].Rows.Count; x++)
                    {
                        divInfo += "<tr>";
                        divInfo += "<td >" + ds.Tables[y].Rows[x]["FullName"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + ds.Tables[y].Rows[x]["RollNo"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + ds.Tables[y].Rows[x]["Total Present"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + ds.Tables[y].Rows[x]["Total Absent"].ToString() + "</td>";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divAttendanceInfoDetails.Controls.Add(new LiteralControl(divInfo));
                }


            }
            catch { }
        }

        protected void btnAllClassAttendance_Click(object sender, EventArgs e)
        {
            loadAllClassAttendance();
        }

        protected void btnTodayAllClassAttendance_Click(object sender, EventArgs e)
        {
            loadAllClassAttendance();
        }
    }
}