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
    public partial class ParentsInformationListReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadStudentInfo("");
        }

        private void loadStudentInfo(string sqlCmd) //for load student Guardian information
        {
            try
            {
                lblBatch.Text = Session["__Batch__"].ToString();
                if (Session["__Section__"].ToString() == "All")
                {
                    DataSet ds = (DataSet)Session["__ParentsInfo__"];

                    Session["__ParentsInfo__"] = ds;
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {

                        int totalRows = ds.Tables[i].Rows.Count;
                        string divInfo = "";

                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto' > ";
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:15px; margin-top:5px'>" + " Section : " + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                        }
                        divInfo += "<thead>";

                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:40px;'>SL</th>";
                        divInfo += "<th style='width:190px'>Name</th>";
                        divInfo += "<th class='numeric' style='width:120px'>Roll</th>";
                        divInfo += "<th style='width:190px'>Father's Name</th>";
                        divInfo += "<th style='width:100px'>Contact No</th>";
                        divInfo += "<th style='width:190px'>Profession </th>";

                        divInfo += "<th style='width:190px'>Mother's Name</th>";
                        divInfo += "<th style='width:100px'>Contact No</th>";
                        divInfo += "<th style='width:190px'>Profession</th>";

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
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["FathersName"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["FathersMobile"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["FathersProfession"].ToString() + "</td>";

                            divInfo += "<td >" + ds.Tables[i].Rows[x]["MothersName"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["MothersMoible"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["MothersProfession"].ToString() + "</td>";

                        }

                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";

                            divInfo += "</table><br>";
                            divParentsInfoList.Controls.Add(new LiteralControl(divInfo));
                        }
                    }
                }
                else
                {
                    DataTable dt = (DataTable)Session["__ParentsInfo__"];
                    Session["__ParentsInfo__"] = dt;
                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Guardian available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divParentsInfoList.Controls.Add(new LiteralControl(divInfo));
                        return;
                    }

                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";

                    divInfo += "<tr>";

                    divInfo += "<th class='numeric' style='width:40px;'>SL</th>";
                    divInfo += "<th style='width:190px'>Name</th>";
                    divInfo += "<th class='numeric' style='width:120px'>Roll</th>";
                    divInfo += "<th style='width:190px'>Father's Name</th>";
                    divInfo += "<th style='width:100px'>Contact No</th>";
                    divInfo += "<th style='width:190px'>Profession </th>";

                    divInfo += "<th style='width:190px'>Mother's Name</th>";
                    divInfo += "<th style='width:100px'>Contact No</th>";
                    divInfo += "<th style='width:190px'>Profession</th>";

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
                        divInfo += "<td >" + dt.Rows[x]["FathersName"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FathersMobile"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FathersProfession"].ToString() + "</td>";

                        divInfo += "<td >" + dt.Rows[x]["MothersName"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["MothersMoible"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["MothersProfession"].ToString() + "</td>";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divParentsInfoList.Controls.Add(new LiteralControl(divInfo));
                }
            }
            catch { }
        }


    }
}