using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DS.UI.Reports.TimeTable
{
    public partial class ExamRoutinePrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] query = Request.QueryString["for"].ToString().Split('-');
            DataTable dtSclInfo = new DataTable();            
            dtSclInfo = Classes.commonTask.LoadShoolInfo();
            hSchoolName.InnerText = dtSclInfo.Rows[0]["SchoolName"].ToString();
            aAddress.InnerText = dtSclInfo.Rows[0]["Address"].ToString();
            pExamTitle.InnerText = query[0].ToString();
            if (query[2].ToString() != "0")
            {
                pBatch.InnerText = "Class : " + new String(query[1].ToString().Where(Char.IsLetter).ToArray()); ;
            }
            string divInfo = Session["__ExamSchedule__"].ToString();
            divClassRoutine.Controls.Add(new LiteralControl(divInfo));
        }
    }
}