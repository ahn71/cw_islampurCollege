using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class CourseWiseStudentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadCourseWiseStudentList("");
        }

        private void loadCourseWiseStudentList(string sqlCmd) // for Course Wise Student Report
        {
            try
            {
                lblBatch.Text = Session["__Batch__"].ToString();
                if (Session["__Section__"].ToString() == "All")
                {
                    DataSet ds = (DataSet)Session["__CourseWiseStudent__"];
                    if (Session["__Image__"].ToString() == "With image")
                    {
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            int totalRows = ds.Tables[i].Rows.Count;
                            string divInfo = "";

                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Section : " + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                            }
                            divInfo += "<thead>";
                            divInfo += "<tr>";

                            divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                            divInfo += "<th style='width:340px'>Name</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                            divInfo += "<th style='width:90px' >Gender</th>";
                            divInfo += "<th style='width:120px'>Contact</th>";
                            divInfo += "<th class='numeric' style='width:45px' >Photo</th>";
                            divInfo += "</tr>";

                            divInfo += "</thead>";

                            divInfo += "<tbody>";

                            for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                            {
                                int sl = x + 1;

                                divInfo += "<tr >";
                                divInfo += "<td class='numeric'>" + sl + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["FullName"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Gender"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Mobile"].ToString() + "</td>";

                                //divInfo += "<td class='numeric' >" + "<img src='/Images/profileImages/" + ds.Tables[i].Rows[x]["ImageName"].ToString() + "' style='width:45px; height:50px;text-align:center' />";
                                divInfo += "<td class='numeric' >" + "<img src='http://www.placehold.it/291x170/EFEFEF/AAAAAA&text=no+image' style='width:45px; height:50px;text-align:center' />";
                            }

                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "</tbody>";
                                divInfo += "<tfoot>";

                                divInfo += "</table><br>";
                                divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            int totalRows = ds.Tables[i].Rows.Count;
                            string divInfo = "";

                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Section : " + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                            }
                            divInfo += "<thead>";
                            divInfo += "<tr>";

                            divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                            divInfo += "<th style='width:340px'>Name</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                            divInfo += "<th style='width:90px' >Gender</th>";
                            divInfo += "<th style='width:120px'>Contact</th>";

                            divInfo += "</tr>";

                            divInfo += "</thead>";

                            divInfo += "<tbody>";

                            for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                            {
                                int sl = x + 1;

                                divInfo += "<tr >";
                                divInfo += "<td class='numeric'>" + sl + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["FullName"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Gender"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Mobile"].ToString() + "</td>";
                            }

                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "</tbody>";
                                divInfo += "<tfoot>";

                                divInfo += "</table><br>";
                                divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                            }
                        }
                    }
                } //End All Section
                else
                {
                    if (Session["__Image__"].ToString() == "With image")
                    {
                        DataTable dt = (DataTable)Session["__CourseWistSt__"];

                        int totalRows = dt.Rows.Count;
                        string divInfo = "";

                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                        divInfo += "<th style='width:340px'>Name</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                        divInfo += "<th style='width:90px' >Gender</th>";
                        divInfo += "<th style='width:120px'>Contact</th>";
                        divInfo += "<th class='numeric' style='width:45px' >Photo</th>";
                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int sl = x + 1;

                            divInfo += "<tr >";
                            divInfo += "<td class='numeric'>" + sl + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";

                            //divInfo += "<td class='numeric' >" + "<img src='/Images/profileImages/" + dt.Rows[x]["ImageName"].ToString() + "' style='width:45px; height:50px; text-align:center'/>";
                            divInfo += "<td class='numeric' >" + "<img src='http://www.placehold.it/291x170/EFEFEF/AAAAAA&text=no+image' style='width:45px; height:50px; text-align:center'/>";

                        }

                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                        divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                    }
                    else
                    {
                        DataTable dt = (DataTable)Session["__CourseWistSt__"];

                        int totalRows = dt.Rows.Count;
                        string divInfo = "";

                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                        divInfo += "<th style='width:340px'>Name</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                        divInfo += "<th style='width:90px' >Gender</th>";
                        divInfo += "<th style='width:120px'>Contact</th>";
                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int sl = x + 1;

                            divInfo += "<tr >";
                            divInfo += "<td class='numeric'>" + sl + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                        }

                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                        divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                    }
                }

            }
            catch { }
        }

    }
}