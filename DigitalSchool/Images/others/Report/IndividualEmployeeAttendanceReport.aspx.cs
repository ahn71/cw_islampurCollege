using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class IndividualEmployeeAttendanceReport : System.Web.UI.Page
    {

        int TP = 0;
        int TA = 0;
        int totalActiveDays = 0;
        int totalPresent = 0;
        int totalAbsent = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblDepartment.Text = Session["__Department__"].ToString();
                lblDesignation.Text = Session["Designation"].ToString();

                if (Session["__DateRange__"] != null) generateAttendanceSheetByDateRange();
                else employeAttendance();
            }
            catch { }
        }

        private void employeAttendance()
        {
            try
            {
                DataTable dt = (DataTable)Session["__dt__"];

                int TP = 0;
                int TA = 0;
                for (byte k = 0; k < dt.Columns.Count; k++)
                {
                    if (dt.Rows[0][dt.Columns[k].ToString()].ToString() == "a")
                    {
                        TA += 1;
                    }
                    else if (dt.Rows[0][dt.Columns[k].ToString()].ToString() == "p")
                    {
                        TP += 1;
                    }
                }


                string divInfo;

                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ; padding-left:5px' >Month </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + new String(Session["__MonthName__"].ToString().Where(Char.IsLetter).ToArray()) + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Active Days </td>";
                divInfo += "<td style='width:15% ;  padding-left:5px' > " + (TP + TA) + " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ; padding-left:5px' >Attendance </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + TP+ " </td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td style='width:5% ;  padding-left:5px' >Absent </td>";
                divInfo += "<td style='width:15% ; padding-left:5px' > " + TA + " </td>";
                divInfo += "</tr>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divIndividualEmployeeAttendace.Controls.Add(new LiteralControl(divInfo));

            }
            catch { }
        }


        private void generateAttendanceSheetByDateRange()
        {
            try
            {
                DataSet ds = (DataSet)Session["__DateRange__"];
                DataTable dtMonth = (DataTable)Session["__MonthName__"];
               
                string divInfo;

                divInfo = "<h4 style=' margin: 0px;'>Attendance Information</h4>";
                divInfo += " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th >Month</th>";
                divInfo += "<th style='text-align:center'>Active Days</th>";
                divInfo += "<th style='text-align:center'>Attendance</th>";
                divInfo += "<th style='text-align:center'>Absent</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";

                divInfo += "<tbody>";                                               

                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (ds.Tables[j].Rows.Count == 0) goto Outer;

                    DataTable dt = ds.Tables[j].Select("").CopyToDataTable();
                    string emId = Request.QueryString["employeeId"].ToString();
                    DataTable dtCertainEmp = dt.Select("EID=" + Request.QueryString["employeeId"].ToString() + "").CopyToDataTable();

                    lblName.Text = "Name : " + dtCertainEmp.Rows[0]["EName"].ToString();

                    for (byte k = 0; k < dtCertainEmp.Columns.Count; k++)
                    {
                        if (dtCertainEmp.Rows[0][k].ToString() == "a")
                        {
                            TA += 1;
                        }
                        else if (dtCertainEmp.Rows[0][k].ToString() == "p")
                        {
                            TP += 1;
                        }
                    }

                    for (int x = 0; x < dtCertainEmp.Rows.Count; x++)
                    {
                        divInfo += "<tr>";
                        divInfo += "<td style='width:5% ; padding-left:5px' > " + dtMonth.Rows[j]["MonthName"] + " </td>";
                        divInfo += "<td style='width:15% ; text-align:center; padding-left:5px' > " + (TP + TA) + "</td>";
                        divInfo += "<td style='width:5% ; text-align:center; padding-left:5px' > " + TP + " </td>";
                        divInfo += "<td style='width:15% ; text-align:center; padding-left:5px' > " + TA + " </td>";
                    }
                    totalPresent += TP;
                    totalAbsent += TA;
                    totalActiveDays += TP + TA;
                    TP = 0;
                    TA = 0;

                Outer:
                    continue;
                }
              
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div><br/>";


                divInfo += "<table class='display' style='width:595px;margin:0px auto; border:1px solid #DDD9D9' >";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:45px ; padding-left:5px' > Total : </td>";
                divInfo += "<td style='width:102px ; text-align:center; padding-left:5px' > " + totalActiveDays + "</td>";
                divInfo += "<td style='width:49px ; text-align:center; padding-left:5px' > " + totalPresent + " </td>";
                divInfo += "<td style='width:100px ; text-align:center; padding-left:5px' > " + totalAbsent + " </td>";
                divInfo += "</tr>";
                divInfo += "</tbody>";
                divInfo += "</table>";
                divIndividualEmployeeAttendace.Controls.Add(new LiteralControl(divInfo));
                Session["__DateRange__"] = null;
            }
            catch { }
        }


    }
}