using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class DueList : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(dlBatch);
                        Classes.commonTask.loadSection(dlSection);
                    }
                }
            }
            catch { }
        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlFeesCategory.Items.Clear();
                sqlDB.loadDropDownList("Select FeeCatName From FeesCategoryInfo where BatchName='" + dlBatch.Text + "'  ", dlFeesCategory);
            }
            catch { }
        }


        private void loadDueList(string sqlCmd)   //Load Due list 
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select RollNo,FullName  from v_StudentPaymentInfo where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Text + "' and  FeeCatName='" + dlFeesCategory.Text + "' and Shift='"+dlShift.Text+"' and PayStatus='False'  ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Fee Fine</div>";
                    // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divDueList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblFine' class='display'  style='width:100%;margin:0px auto; ' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th class='numeric' style='width:100px'>Roll No</th>";
                divInfo += "<th >Name</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
             
                divDueList.Controls.Add(new LiteralControl(divInfo));

                Session["__DueListReport__"] = divInfo;
                lblBatch.Text = "Due List of " + dlBatch.SelectedItem.Text;
                lblShift.Text = "Shift : " + dlShift.SelectedItem.Text;
                lblSection.Text = "Section : " + dlSection.SelectedItem.Text;
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadDueList("");
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            // ss = new String(s.Where(Char.IsLetter).ToArray());
            Session["__Class__"] = new String(dlBatch.Text.Where(Char.IsLetter).ToArray());
            Session["__Section__"] = dlSection.Text;
            Session["__FeeCate__"] = dlFeesCategory.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/DueListReport.aspx');", true);  //Open New Tab for Sever side code
        }



    }
}