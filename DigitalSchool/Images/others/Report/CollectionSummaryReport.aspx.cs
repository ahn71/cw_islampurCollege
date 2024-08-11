using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class CollectionSummaryReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__FeeCatName__"].ToString() == "All")
                {
                    DataTable rpt = (DataTable)Session["__SummaryReport__"];
                    for (int i = 0; i < rpt.Rows.Count; i++)
                    {
                        string divInfo = rpt.Rows[i]["htmlCode"].ToString();
                        divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                    }
                    
                    if (Session["__DateFt__"] != null) lblYear.Text = DateTime.Now.Year.ToString() + " (Today)";
                    else
                    {
                        Session["__DateFrom__"].ToString();
                        Session["__DateTo__"].ToString();
                        lblFromDate.Text = Session["__DateFrom__"].ToString();
                        lblToDate.Text = Session["__DateTo__"].ToString();
                    }
                   
                    lblYear.Text = DateTime.Now.Year.ToString();
                }
                else
                {
                   // loadTotalCollectionSummary("");
                    string divInfo = Session["__SummaryReport__"].ToString();
                    divCollectionSummary.Controls.Add(new LiteralControl(divInfo));

                    if (Session["__DateFt__"]!=null) lblYear.Text = DateTime.Now.Year.ToString() + " (Today)";
                    else
                    {
                        Session["__DateFrom__"].ToString();
                        Session["__DateTo__"].ToString();
                        lblFromDate.Text = Session["__DateFrom__"].ToString();
                        lblToDate.Text = Session["__DateTo__"].ToString();
                    }
                   
                }
              
            }
            catch { }
        }


        private void loadTotalCollectionSummary(string sqlCmd)
        {
            try
            {

                if (string.IsNullOrEmpty(sqlCmd) && Session["__FeeCat__"].ToString() != "All") sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" + Session["__Batch__"] + "' and FeeCatId=" + Session["__FeeCat__"] + " and SectionName='" + Session["__Section__"] + "'  and DateOfPayment between '" + Session["__FromDate__"] + "'  and  '" + Session["__ToDate__"] + "' ";
                if (Session["__FeeCat__"].ToString() == "All") sqlCmd = "Select Sum(AmountPaid) as AmountPaid from StudentPayment where BatchName='" + Session["__Batch__"] + "' and SectionName='" + Session["__Section__"] + "'  and DateOfPayment between '" + Session["__FromDate__"] + "'  and  '" + Session["__ToDate__"] + "' ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";
                lblFromDate.Text ="From : "+ Session["__FromDate__"].ToString();
                lblToDate.Text ="To : "+ Session["__ToDate__"].ToString();
                lblClass.Text = "Class : " + new String(Session["__Batch__"].ToString().Where(Char.IsLetter).ToArray()) + " ( " + Session["__Section__"].ToString() + " )";
             
                if (dt.Rows[0]["AmountPaid"].ToString() == "" || dt.Rows[0]["AmountPaid"].ToString() == "0")
                {

                    divInfo = "<div class='noData'>No Fee Collection Information</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0 auto;'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th > " + Session["__FeeCatName__"] + " </th>";
                divInfo += "<th class='numeric'>Amount</th>";


                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";


                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    divInfo += "<tr></tr>";

                    divInfo += "<td > Total Amount Collected  </td>";

                    divInfo += "<td class='numeric'>" + dt.Rows[x]["AmountPaid"].ToString() + "</td>";

                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        private void loalCollectionAllFeeCat(string sqlCmd)
        {
            try
            {

                string[,] array = (string[,])Session["__array__"];
            
                Session["__array__"] = array;
          
                string divInfo = "";

                lblClass.Text = "Class : " + new String(Session["__Batch__"].ToString().Where(Char.IsLetter).ToArray()) + " ( " + Session["__Section__"].ToString() + " )";
   
                for (int j = 0; j < array.Length / 2; j++)
                {
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                  
                    divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin-right:10px;'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th>" + array[j, 0] + "</th>";
                    divInfo += "<th class='numeric'>Amount</th>";


                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";


                    divInfo += "<tr></tr>";

                    divInfo += "<td > Total Amount Collected  </td>";

                    divInfo += "<td class='numeric'>" + array[j, 1] + "</td>";


                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head' style='height:14px'></div></div>";

                    divCollectionSummary.Controls.Add(new LiteralControl(divInfo));
                }

                divInfo += "<div'><div style='background-color:green; height:20px;'></div></div>";

                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin-right:10px;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th> Grand Total  </th>";
                divInfo += "<th class='numeric'>" + Session["__GrandTotal__"].ToString() + "</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divCollectionSummary.Controls.Add(new LiteralControl(divInfo));



            }
            catch { }

        }



    }
}