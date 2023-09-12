using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academics.Examination
{
    public partial class ExamHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //---url bind---
                    aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                    aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;

                    aExamType.HRef = "~/" + Classes.Routing.ExamTypeRouteUrl;
                    aQuestionPattern.HRef = "~/" + Classes.Routing.QuestionPatternRouteUrl;                    
                    aSubjectQuestionPattern.HRef = "~/" + Classes.Routing.SubjectQuestionPatternRouteUrl;
                    aExamInfo.HRef = "~/" + Classes.Routing.ExamInfoRouteUrl;
                    aExamRoutine.HRef = "~/" + Classes.Routing.ExamRoutineRouteUrl;
                    aExamineeSelection.HRef = "~/" + Classes.Routing.ExamineeSelectionRouteUrl;
                    aExamGrading.HRef = "~/" + Classes.Routing.ExamGradingRouteUrl;
                    aExamMarksEntry.HRef = "~/" + Classes.Routing.ExamMarksEntryRouteUrl;
                    aResultPublish.HRef = "~/" + Classes.Routing.ResultPublishRouteUrl;                    
                    //---url bind end---
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You have not any privilege for this page.Please set privilege.";
                }
                catch { }
            }
        }
    }
}