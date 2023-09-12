using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class IndividualStudentAbsentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["__AbsentDetails__"];
                loadDesignationList(dt);
            }
            catch { }
        }
        private void loadDesignationList(DataTable dt)
        {
            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Absent available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divAbsentDetails.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                divInfo = " <table id='tblStudentInfo' class='display' > ";
                divInfo += "<thead>";

                divInfo += "<tbody>";

                string id = "";

                //id = dtDis.Rows[x]["StudentId"].ToString();

                divInfo += "<tr>";
                divInfo += "<td>Roll No</td>";
                divInfo += "<td >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td>Name</td>";
                divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td>Days</td>";
                divInfo += "<td >" + dt.Rows[x]["Days"].ToString() + "</td>";
                divInfo += "</tr>";

                divInfo += "<tr>";
                divInfo += "<td>Total Days</td>";
                divInfo += "<td >" + dt.Rows[x]["Total Days"].ToString() + "</td>";
                divInfo += "</tr>";

                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divAbsentDetails.Controls.Add(new LiteralControl(divInfo));

            }

        }
    }
}