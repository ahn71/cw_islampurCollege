using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Timetable
{
    public partial class SetClassTiming_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                DataTable dtSclInfo = new DataTable();                
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                hSchoolName.InnerText = dtSclInfo.Rows[0]["SchoolName"].ToString();
                aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();
                divClassRoutine.Controls.Add(new LiteralControl(Session["_ClassRoutine_"].ToString()));
            }
            catch { }
        }
    }
}