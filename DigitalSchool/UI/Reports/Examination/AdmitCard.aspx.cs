using DS.BLL.ControlPanel;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class AdmitCard : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamReports.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    ShiftEntry.GetDropDownList(dlShift);
                    BatchEntry.GetDropdownlist(ddlBatch, "True");
                }
            }
            catch { }
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }                
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
                ClassSectionEntry.GetEntitiesDataWithAll(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
                ExamInfoEntry.GetExamIdListWithExInSl(ddlExamList, BatchClsID[0]);

            }
            catch { }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesDataWithAll(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamList, BatchClsID[0], ddlGroup.SelectedItem.Value);
            
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            DataTable dt = new DataTable();
            dt = ExamCommon.getAdmitCard(BatchClsID[0], BatchClsID[1],ddlExamList.SelectedValue,dlSection.SelectedValue,txtRollNo.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                Session["__AdmitCard__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AdmitCard');", true);  //Open New Tab for Sever side code
            }
            else
                lblMessage.InnerText = "warning-> Data not found.";

        }
    }
}