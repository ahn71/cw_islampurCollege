using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class MarksSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                lblSchoolName.Text = dtSclInfo.Rows[0]["SchoolName"].ToString();
                lblExamName.Text = Session["__lblExamName__"].ToString();
                lblShift.Text ="Shift: "+ Session["__lblShift__"].ToString();
                lblBatch.Text = "Batch: " + Session["__lblBatch__"].ToString() + "(" + Session["__lblSection__"].ToString()+")";              
                string divInfo = Session["__MarkSheet__"].ToString();
                divProgressReport.Controls.Add(new LiteralControl(divInfo));               
            }
            catch { }
        }
    }
}