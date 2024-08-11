using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using adviitRuntimeScripting;


namespace DS.Report
{
    public partial class StudentFineList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadBatch(dlBatch);
                    Classes.commonTask.loadSection(dlSection);
                    loadFinePurpose(dlFinePurpose);
                }
            }
            catch { }
        }

        private void loadFinePurpose(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.loadDropDownList("Select distinct FinePurpose From v_getFineStudentInfo", dl);
            }
            catch { }
        }

        private void loadStudentFinelist()
        {
            try
            {
                DataTable dt = new DataTable();
                if (dlFinePurpose.SelectedItem.Text == "All")
                {
                    if (chkToday.Checked) sqlDB.fillDataTable("Select FullName, RollNo, FinePurpose, FineAmount, convert(varchar(11), PayDate,106) as PayDate From v_getFineStudentInfo  where BatchName='" + dlBatch.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and DateOfPayment = '" + DateTime.Now.ToShortDateString() + "' and PayDate is not null   Order By RollNo ASC ", dt);
                    else sqlDB.fillDataTable("Select FullName, RollNo, FinePurpose, FineAmount, convert(varchar(11), PayDate,106) as PayDate From v_getFineStudentInfo  where BatchName='" + dlBatch.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and DateOfPayment Between '" + txtFromDate.Text.Trim() + "' and '" + txtToDate.Text.Trim() + "' and PayDate is not null Order By RollNo ASC ", dt);
                }
                else
                {
                    if (chkToday.Checked) sqlDB.fillDataTable("Select FullName, RollNo, FinePurpose, FineAmount, convert(varchar(11), PayDate,106) as PayDate From v_getFineStudentInfo  where BatchName='" + dlBatch.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and FinePurpose='" + dlFinePurpose.SelectedItem.Text + "' and DateOfPayment = '" + DateTime.Now.ToShortDateString() + "' and PayDate is not null   Order By RollNo ASC ", dt);
                    else sqlDB.fillDataTable("Select FullName, RollNo, FinePurpose, FineAmount, convert(varchar(11), PayDate,106) as PayDate From v_getFineStudentInfo  where BatchName='" + dlBatch.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and FinePurpose='" + dlFinePurpose.SelectedItem.Text + "' and DateOfPayment Between '" + txtFromDate.Text.Trim() + "' and '" + txtToDate.Text.Trim() + "' and PayDate is not null Order By RollNo ASC ", dt);
                }
                string divInfo = "";

                divInfo = "<table id='tblBatch' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Name</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th>Fine Purpose</th>";
                divInfo += "<th class='numeric' style='width:100px'>Fine Amount</th>";
                divInfo += "<th style='width:100px'>Collection Date</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                int id = 0;
                double total = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id++;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["FineAmount"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["PayDate"].ToString() + "</td>";
                    total += double.Parse(dt.Rows[x]["FineAmount"].ToString());
                }

                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='border-left:none;'></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divStudentFine.Controls.Add(new LiteralControl(divInfo));
                Session["__reportType__"] = "Fine Collection Report - ";
                Session["__batchName__"] = "Batch : " + dlBatch.SelectedItem.Text + " ("+ dlShift.SelectedItem.Text + ")";
                Session["__allCDR__"] = divInfo;
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadStudentFinelist();
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/CollectionDetailsReport.aspx');", true);  //Open New Tab for Sever side code
        }

    }
}