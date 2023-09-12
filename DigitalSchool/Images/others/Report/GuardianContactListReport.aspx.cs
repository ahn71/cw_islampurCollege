using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class GuardianContactListReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGuardinContactList();
        }


        private void loadGuardinContactList() // for load student Guardian Contact List 
        {
            try
            {
                lblBatch.Text = Session["__Batch__"].ToString();
                if (Session["__Section__"].ToString() == "All")
                {
                    DataSet ds = (DataSet)Session["__GuardianInfo__"];
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

                        divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                        divInfo += "<th style='width:295px'>Student Name</th>";
                        divInfo += "<th style='width:80px; text-align:center'>Roll</th>";
                        divInfo += "<th>Guardian Mobile</th>";

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
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                        }

                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";

                            divInfo += "</table>";
                            divGuardianContactList.Controls.Add(new LiteralControl(divInfo));
                        }
                    }
                }
                else
                {
                    DataTable dt = (DataTable)Session["__GuardianInfoWithFixtSection__"];
                    lblBatch.Text = Session["__Batch__"].ToString();
                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Guardian available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divGuardianContactList.Controls.Add(new LiteralControl(divInfo));
                        return;
                    }

                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                    divInfo += "<th style='width:295px'>Student Name</th>";
                    divInfo += "<th style='width:80px; text-align:center'>Roll</th>";
                    divInfo += "<th>Guardian Mobile</th>";

                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int sl = x + 1;

                        divInfo += "<tr >";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        divInfo += "<td class='numeric' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divGuardianContactList.Controls.Add(new LiteralControl(divInfo));
                }

            }
            catch { }
        }

    }
}