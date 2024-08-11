using DS.BLL.ControlPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Examination
{
    public partial class Result_Publish : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddExam.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
                //---url bind end---
                loadEaxmInfo();
            }
           
        }
        private void loadEaxmInfo()
        {
            dt = new DataTable();
            dt = Classes.Exam.loadExamList();
            gvExamList.DataSource = dt;
            gvExamList.DataBind();
        }

        protected void gvExamList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Publish")
            {              
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string ExInSl = gvExamList.DataKeys[rIndex].Values[0].ToString();
                if (DAL.CRUD.ExecuteQuery("update  ExamInfo set IsPublished=1 where ExInSl=" + ExInSl))
                {
                    saveToPublishLog(ExInSl, "1");
                    lblMessage.InnerText = "success->Successfully Published";
                    loadEaxmInfo();
                }
            }
            else if (e.CommandName == "Unpublish")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string ExInSl = gvExamList.DataKeys[rIndex].Values[0].ToString();
                if (DAL.CRUD.ExecuteQuery("update  ExamInfo set IsPublished=0 where ExInSl=" + ExInSl))
                {
                    saveToPublishLog(ExInSl,"0");
                    lblMessage.InnerText = "success->Successfully Unpublished";
                    loadEaxmInfo();
                }
            }
        }
        private void saveToPublishLog(string ExInSl,string IsPublished)
        {
            DAL.CRUD.ExecuteQuery(@"INSERT INTO[dbo].[Result_PublishedLog]
        ([ExInSl]
          ,[ActionTime]
          ,[IsPublished])
    VALUES
          (" + ExInSl + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+ IsPublished + ")");
        }
        
    }
   
}