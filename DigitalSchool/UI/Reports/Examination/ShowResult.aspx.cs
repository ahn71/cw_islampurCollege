using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class ShowResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ResultPassedData"] != null)
            {
                string PassedresultData = Session["ResultPassedData"].ToString();
                string FailedResultdata = Session["ResultFailedData"].ToString();
                lblShowpassResult.Text = PassedresultData;
                lblFailedData.Text = FailedResultdata;

                lblExamName.Text = Session["ExamName"].ToString();
                lblExamGroupName.Text= Session["GroupName"].ToString();

                lblResultStatus.Text = Session["resultSatatus"].ToString();


            }
        }
    }
}