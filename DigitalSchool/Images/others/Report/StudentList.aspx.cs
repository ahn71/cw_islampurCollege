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
    public partial class StudentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            lblYear.Text = System.DateTime.Now.Year.ToString();
            DataTable dt =(DataTable)Session["_StudentList_"];
            loadStudentInfo(dt);
        }


        private void loadStudentInfo(DataTable dt)
        {
            try
            {
  
                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    studentList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Name</th>";
                divInfo += "<th style='text-align:center'>Roll No</th>";

                divInfo += "<th style='text-align:center'>Class</th>";
                divInfo += "<th style='text-align:center'>Section</th>";
                divInfo += "<th style='text-align:center'>Gender</th>";
                divInfo += "<th>Mobile</th>";



                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td  >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:70px'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";

                    divInfo += "<td style='text-align:center;width:80px'>" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:70px'>" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:55px'>" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                studentList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }



    }
}