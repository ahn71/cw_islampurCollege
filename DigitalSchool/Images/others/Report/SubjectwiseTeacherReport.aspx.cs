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
    public partial class SubjectwiseTeacherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTeacherList("");
            }
        }
        private void LoadTeacherList(string sqlcmd)
        {
            try
            {
                if (Session["__Department__"].ToString() == "All")
                {
                    lblDepartmentName.Text = "DEPAETMENT WISE TEACHER & STAFF REPORT";
                }
                else if (Session["__Department__"].ToString() == "Techer List")
                {
                    lblDepartmentName.Text = "DEPAETMENT WISE TEACHER REPORT";
                }
                else if (Session["__Department__"].ToString() == "Staff List")
                {
                    lblDepartmentName.Text = "DEPAETMENT WISE Staff REPORT";
                }
                else
                {
                    if (Session["__CheckDepartment__"].ToString() == "Teacher")
                        lblDepartmentName.Text = Session["__Department__"].ToString() + "Department Teacher Report";
                    else
                        lblDepartmentName.Text = Session["__Department__"].ToString() + " Department Staff Report";
                }
                lblYear.Text = DateTime.Now.ToString("yyyy");
                if (Session["__Department__"].ToString() == "All" || Session["__Department__"].ToString() == "Techer List" || Session["__Department__"].ToString() == "Staff List")
                {
                    
                    DataSet ds = new DataSet();
                    ds = (DataSet)Session["__TeacherInfo__"];

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        int totalRows = ds.Tables[i].Rows.Count;
                        string divInfo = "";



                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Department : " + ds.Tables[i].Rows[0]["DName"].ToString() + "</div></div>";
                        }
                        divInfo += "<thead>";
                        divInfo += "<tr>";
                        divInfo += "<th style='text-align:center'>SL</th>";
                        divInfo += "<th>Name</th>";
                        divInfo += "<th>Contact No</th>";
                        divInfo += "<th>Last Degree</th>";
                        if (Session["__Image__"].ToString() == "withimage")
                        divInfo += "<th>Photo</th>";
                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";
                        string id = "";

                        for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                        {
                            int Sl = x + 1;
                            divInfo += "<tr id='r_" + id + "'>";
                            divInfo += "<td style='text-align:center' >" + Sl + "</td>";
                            divInfo += "<td style='width:170px' >" + ds.Tables[i].Rows[x]["EName"].ToString() + "</td>";
                            divInfo += "<td style='width:150px' >" + ds.Tables[i].Rows[x]["EMobile"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["ELastDegree"].ToString() + "</td>";
                            if (Session["__Image__"].ToString() == "withimage")
                                divInfo += "<td class='numeric_control' style='width:50px' >" + "<img src='/Images/teacherProfileImage/" + ds.Tables[i].Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";

                        }

                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";

                            divInfo += "</table><br>";
                            divTeacherList.Controls.Add(new LiteralControl(divInfo));
                        }

                        Session["__TeacherInfo__"] = ds;
                    }
                }
                else
                {
                    lblDepartment.Text = Session["__Department__"].ToString();
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["__TeacherInfo__"];

                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Teacher available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divTeacherList.Controls.Add(new LiteralControl(divInfo));
                        return;
                    }

                    divInfo = " <table id='tblClassList' class='display'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='text-align:center'>SL</th>";
                    divInfo += "<th>Name</th>";
                    divInfo += "<th>Contact No</th>";
                    divInfo += "<th>Last Degree</th>";
                    if (Session["__Image__"].ToString() == "withimage")
                    divInfo += "<th>Photo</th>";
                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    string id = "";

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int Sl = x + 1;
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfo += "<td style='text-align:center'>" + Sl + "</td>";
                        divInfo += "<td style='width:170px' >" + dt.Rows[x]["EName"].ToString() + "</td>";
                        divInfo += "<td style='width:150px'>" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["ELastDegree"].ToString() + "</td>";
                        if (Session["__Image__"].ToString() == "withimage")
                            divInfo += "<td class='numeric_control' style='width:50px'  >" + "<img src='/Images/teacherProfileImage/" + dt.Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    
                    //divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divTeacherList.Controls.Add(new LiteralControl(divInfo));
                }                
            }
            catch { }
        }
    }
}