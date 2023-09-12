using adviitRuntimeScripting;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class FeeCollectionReport : System.Web.UI.Page
    {
        float total = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (total < 1) { };
                lblYear.Text = System.DateTime.Now.Year.ToString();
                // loadParticularCategory("");

                divFeesCollection.Controls.Add(new LiteralControl(Session["__FeeCollectionReport__"].ToString()));

                lblStName.Text = Session["__FullName__"].ToString();
                lblStRoll.Text = Session["__Roll__"].ToString();
                lblClass.Text = Session["__Class__"].ToString();
                lblCategory.Text = Session["__FeeCategory__"].ToString();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT SchoolName,Address FROM School_Setup");
                lblSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
                lblAddress.Text = dt.Rows[0]["Address"].ToString();

                loadFineReport();
            }
            catch { }
        }

        private void loadFineReport()
        {
            try
            {
                string divInfo;
                if (Session["__storeFineInfo__"] != null)
                {
                    DataTable dtfine = (DataTable)Session["__storeFineInfo__"];
                    if (dtfine.Rows.Count == 0) return;


                    divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%; margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                    divInfo += "<th>Fine Purpose</th>";
                    divInfo += "<th class='numeric' style='width:100px;'></th>";

                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    for (int x = 0; x < dtfine.Rows.Count; x++)
                    {
                        int sl = x + 1;
                        divInfo += "<tr style='width:50px'>";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td >" + dtfine.Rows[x]["FinePurpose"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dtfine.Rows[x]["FineAmount"].ToString() + "</td>";

                        total += float.Parse(dtfine.Rows[x]["FineAmount"].ToString());

                    }
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'> </td>";
                    divInfo += "<td style='text-align:right; font-weight: bold' > Grand Total : </td>";
                    float grandTotal = total + float.Parse(Session["__Total__"].ToString());
                    divInfo += "<td class='numeric' style='font-weight: bold'>" + grandTotal + "</td>";
                    divInfo += "</tr>";



                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";

                    

                    //divInfo += "<div class='dataTables_wrapper'><div class='head' style='text-align:right;'> </div></div>";

                    divFeesCollection.Controls.Add(new LiteralControl(divInfo));

                  


                }
            }
            catch { }
        }

        private void loadParticularCategory(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select  CatPId, FeeCatName,PName, Amount from v_FeesCatDetails where FeeCatName='" + Session["__FeeCatName__"].ToString() + "'  ";

                total = 0;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
               
                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divFeesCollection.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%; margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Particulars</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";



                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dt.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Amount"].ToString() + "</td>";

                    total += float.Parse(dt.Rows[x]["Amount"].ToString());
                   
                }
                divInfo += "<tr>";
                divInfo += "<td class='numeric'> </td>";
                divInfo += "<td style='text-align:right' > Total : </td>";
                divInfo += "<td class='numeric'>" + total + "</td>";
                divInfo += "</tr>";



                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'> </div></div><br/>";

                divFeesCollection.Controls.Add(new LiteralControl(divInfo));

                lblStName.Text = Session["__FullName__"].ToString();
                lblStRoll.Text = Session["__Roll__"].ToString();
                lblClass.Text = Session["__Class__"].ToString();
                lblCategory.Text = Session["__FeeCategory__"].ToString();

               
            }
            catch { }
        }

    }
}