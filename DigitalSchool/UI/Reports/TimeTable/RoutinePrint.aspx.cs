using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class RoutinePrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] query = Request.QueryString["for"].ToString().Split('-');
                //lblBatch.Text ="Batch Name : " + Session["__ClassName__"].ToString();
                //lblShit.Text = "Shift Name : "+ Session["__Shift__"].ToString();
                DataTable dtSclInfo = new DataTable();
                if (query[0] == "TeacherClassRoutine")
                    pTitle.InnerText ="Teacher's Class Routine";
                else if (query[0] == "ClassRoutine")
                    pTitle.InnerText = query[1];               
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                hSchoolName.InnerText =dtSclInfo.Rows[0]["SchoolName"].ToString();
                aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();
                divClassRoutine.Controls.Add(new LiteralControl(Session["__ClassRoutine__"].ToString()));
            }
            catch { }
        }
    }
}