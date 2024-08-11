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
    public partial class AbsentDetailsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBatch.Text = Session["__AttendanceSheet__"].ToString();
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

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th style='text-align:center;width:80px'>Roll No</th>";
            divInfo += "<th style='width:180px'>Name</th>";
            divInfo += "<th>Days</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td style='text-align:center;width:80px' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                divInfo += "<td style='width:180px' >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["Days"].ToString() + "</td>";

            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divAbsentDetails.Controls.Add(new LiteralControl(divInfo));

        }
    }
}