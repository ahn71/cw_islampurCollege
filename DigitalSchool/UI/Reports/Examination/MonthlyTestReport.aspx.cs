using DS.BLL.ControlPanel;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class MonthlyTestReport : System.Web.UI.Page
    {
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry clsTotalResultEntry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry exam_Final_Result_Stock_Of_All_Batch_Entry;
        ClassGroupEntry clsgrpEntry;
        Tbl_Exam_MonthlyTestDetailsEntry monthlytestdtl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamReports.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                ShiftEntry.GetDropDownList(ddlShift);
                BatchEntry.GetDropdownlist(ddlBatch, "True");
            }
            lblMessage.InnerText = "";
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                ExamInfoEntry.GetExamIdList(ddlExamId, BatchClsID[0],false);
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
                ClassSectionEntry.GetEntitiesData(ddlSectionName, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            }
            catch { }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(ddlSectionName, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }
        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(monthlytestdtl==null)
                {
                    monthlytestdtl = new Tbl_Exam_MonthlyTestDetailsEntry();
                }
                string[] batchid = ddlBatch.SelectedValue.Split('_');               
                dt = new DataTable();
                dt = monthlytestdtl.GetMonthlyTestReportData(ddlShift.SelectedValue,batchid[0],ddlGroup.SelectedValue,ddlSectionName.SelectedValue,ddlExamId.SelectedValue);
                if(dt.Rows.Count>0)
                {
                    Session["__monthlytestreport__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                    "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=monthlytestreport');", true);  //Open New Tab for Sever side code                      
                }
                else lblMessage.InnerText = "warning-> No Result Found.";
            }
            catch { }
        }
      
    }
}