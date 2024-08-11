using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class CurrentStudentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadClass(dlClass);
                    sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dlSection);
                    loadCurrentStudentInfo("");
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dlClass.Text.ToString())) return;
                loadCurrentStudentInfo("");
            }
            catch { }

        }


        private void loadCurrentStudentInfo(string sqlCmd)
        {
            try
            {
                if (dlClass.Text == "All" && dlSection.Text == "All" && dlShift.Text == "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo order by  ClassName Desc";

                else if (dlClass.Text == "All" && dlSection.Text == "All" && dlShift.Text != "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text == "All" && dlSection.Text != "All" && dlShift.Text == "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where SectionName='" + dlSection.Text + "' ";

                else if (dlClass.Text != "All" && dlSection.Text == "All" && dlShift.Text == "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where ClassName='" + dlClass.Text + "' ";

                else if (dlClass.Text != "All" && dlSection.Text != "All" && dlShift.Text != "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Text + "' and Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text == "All" && dlSection.Text != "All" && dlShift.Text != "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where SectionName='" + dlSection.Text + "'  and Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text != "All" && dlSection.Text == "All" && dlShift.Text != "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where  ClassName='" + dlClass.Text + "' and Shift='" + dlShift.Text.Trim() + "' ";

                else if (dlClass.Text != "All" && dlSection.Text != "All" && dlShift.Text == "All") sqlCmd = "select StudentId,ClassName,SectionName,Shift,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where  ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Text + "' ";


                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No student available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Class</th>";
                divInfo += "<th>Section</th>";
                divInfo += "<th>Shift</th>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Roll</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "<th class='numeric control'>View</th>";
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Shift"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editStudent(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divStudentDetails.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}