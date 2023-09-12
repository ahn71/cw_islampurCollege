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
    public partial class SelarySheetReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                divSalaryDetailsInfo.Controls.Add(new LiteralControl(Session["__SalarySheet__"].ToString()));
                lblYear.Text ="Salary Report - "+ System.DateTime.Now.Year.ToString();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT SchoolName FROM School_Setup");
                if(dt.Rows.Count>0)
                lblSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
            }
            catch { }
        }
    }
}