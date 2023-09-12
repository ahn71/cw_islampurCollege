using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class StudentContactReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadStudentContactListReport();
        }
        private void loadStudentContactListReport() // for load student  Contact List 
        {
            try
            {
               string divInfos= Session["__StContractRpt__"].ToString();
               divStudentContactListReport.Controls.Add(new LiteralControl(divInfos));
               lblBatch.Text = Session["__Batch__"].ToString();
            }
            catch { }
        }
    }
}