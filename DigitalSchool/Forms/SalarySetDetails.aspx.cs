using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;

namespace DS.Forms
{
    public partial class SalarySetDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    loadSalarySetDetails("");
                }
            }
            catch { }
        }

        private void loadSalarySetDetails(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Employee available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divSalarySetInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblEmployeeSalary' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Name</th>";
                divInfo += "<th>Department </th>";
                divInfo += "<th>Designation </th>";
                divInfo += "<th class='numeric'>Basic</th>";
                divInfo += "<th class='numeric' >School</th>";
                divInfo += "<th class='numeric' >Total</th>";
                divInfo += "<th >Staus</th>";
                divInfo += "<th class='control' style='max-width:30px;'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DesName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["SaGovtOrBasic"].ToString() + "</td>";
                    divInfo += "<td class='numeric' >" + dt.Rows[x]["SaSchool"].ToString() + "</td>";
                    divInfo += "<td class='numeric' >" + dt.Rows[x]["SaTotal"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["SaStaus"].ToString() + "</td>";
                    divInfo += "<td style='max-width:30px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editSalary(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSalarySetInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}