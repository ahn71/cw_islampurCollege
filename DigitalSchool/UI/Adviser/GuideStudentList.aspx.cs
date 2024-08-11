using DS.BLL.StudentGuide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class GuideStudentList : System.Web.UI.Page
    {
        StudentGuideEntry stdgdEntry;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if(!IsPostBack)
                {
                 LoadStudentList(Session["__EID__"].ToString());
                }
        }
        private void LoadStudentList(string EID)
        {
            try
            {
                if (stdgdEntry == null)
                {
                    stdgdEntry = new StudentGuideEntry();
                }
                dt = new DataTable();
                dt = stdgdEntry.LoadStudentList(EID);
                gvStudentList.DataSource = dt;
                gvStudentList.DataBind();
            }
            catch { }
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Result")
            {
                string StudentId = e.CommandArgument.ToString();
                Response.Redirect("/UI/Adviser/StudentResult.aspx?ID=Std-" + StudentId + "");
            }
            else if (e.CommandName == "Attendance")
            {
                string StudentId = e.CommandArgument.ToString();
                Response.Redirect("/UI/Adviser/AdviserWiseStdAttDetails.aspx?ID=Std-" + StudentId + "");
            }
        }
    }
}