using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.Accounts
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string []query= Request.QueryString["for"].ToString().Split('|');
                
                DataTable dtSclInfo = new DataTable();
                DataTable dt=new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                hSchoolName.InnerText = dtSclInfo.Rows[0]["SchoolName"].ToString();
                aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();
                pTitle.InnerText = query[0] + " Report"; ;
                pTitle2.InnerText = query[1];
                dt = (DataTable)Session["__ReportData__"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch { }
        }
    }
}