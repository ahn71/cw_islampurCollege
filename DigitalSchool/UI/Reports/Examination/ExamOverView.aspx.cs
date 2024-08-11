using DS.BLL.ControlPanel;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class ExamOverView : System.Web.UI.Page
    {
        Class_ClasswiseMarksheet_TotalResultProcess_Entry clstotalResultEntry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry ExamFinal;
        DataTable dt;
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamOverView.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        ShiftEntry.GetDropDownList(dlShift);
                        BatchEntry.GetDropdownlist(ddlBatch, "True");
                    }
            }
            catch { }
        }        
        protected void btnFailSubjectWise_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            if (dlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; dlSection.Focus(); return; }
            //------------------------------
            failListSubjectWise();           
        }
      
        private void failListSubjectWise()
        {
           try
           {
               string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
               if (clstotalResultEntry == null)
               {
                   clstotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
               }
               dt = new DataTable();
               string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
               dt = clstotalResultEntry.LoadFailSubject(getClass,dlShift.SelectedValue,
                   BatchClsID[0], ddlGroup.SelectedValue, dlSection.SelectedValue, ddlExamId.SelectedValue);
               if (dt == null || dt.Rows.Count==0)
               {
                   lblMessage.InnerText = "warning->Fail Subject Not Found";
                   return;
               }
               if (dt.Rows.Count > 0)
               {
                   Session["__FailSubject__"] = dt;
                   ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FailSubject');", true);  //Open New Tab for Sever side code
               }
             
           }
            catch { }
        }      

        int totalAPlus = 0;
        int totalA = 0;
        int totalAminus = 0;
        int totalB = 0;
        int totalC = 0;
        int totalD = 0;
        int totalF = 0;
        protected void btnGrateInfo_Click(object sender, EventArgs e)
        {
            try
            {
                //--------Validation------------
                if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
                if (dlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; dlSection.Focus(); return; }
                //------------------------------
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (ExamFinal == null)
                {
                    ExamFinal = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                dt = new DataTable();
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = ExamFinal.GetExamOverView(getClass, BatchClsID[0],
                    dlShift.SelectedValue,dlSection.SelectedValue,ddlGroup.SelectedValue, ddlExamId.SelectedValue);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Exam OverView Not Found";
                    return;
                }
                string Grade = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    if (dt.Rows[j]["FinalGrade_OfExam_WithOptionalSub"].ToString() == "F ")
                    {
                        Grade = dt.Rows[j]["FinalGrad_OfExam"].ToString();
                    }
                    else
                    {
                        Grade = dt.Rows[j]["FinalGrade_OfExam_WithOptionalSub"].ToString();
                    }
                    GetGrade(Grade);
                }              
                if (dt.Rows.Count > 0)
                {
                      DataTable dtEOV = new DataTable();
                      dtEOV.Columns.Add("TotalStudent");
                      dtEOV.Columns.Add("TotalPass");
                      dtEOV.Columns.Add("APlus");
                      dtEOV.Columns.Add("A");
                      dtEOV.Columns.Add("AMinus");
                      dtEOV.Columns.Add("B");
                      dtEOV.Columns.Add("C");
                      dtEOV.Columns.Add("D");
                      dtEOV.Columns.Add("F");
                      dtEOV.Columns.Add("GroupName");
                      dtEOV.Columns.Add("SectionName");
                      dtEOV.Columns.Add("BatchName");
                      dtEOV.Columns.Add("ExName");
                      dtEOV.Columns.Add("ShiftName");
                      dtEOV.Columns.Add("PublishDate");
                      dtEOV.Rows.Add(dt.Rows.Count,totalAPlus+totalA+totalAminus+totalB+totalC+totalD,
                          totalAPlus,totalA,totalAminus,totalB,totalC,totalD,totalF,dt.Rows[0]["GroupName"].ToString(),
                          dt.Rows[0]["SectionName"].ToString(), dt.Rows[0]["BatchName"].ToString(),
                          dt.Rows[0]["ExName"].ToString(), dt.Rows[0]["ShiftName"].ToString(),
                          dt.Rows[0]["PublishDate"].ToString());
                   
                    Session["__ExamOverView__"] = dtEOV;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ExamOverView');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Exam OverView Not Found";
                }    
          }
            catch { }
        }
        private void GetGrade(string Grade)
        {
            try
            {
                switch(Grade)
                {
                    case "A+":
                        totalAPlus += 1;
                        break;
                    case "A ":
                        totalA += 1;
                        break;
                    case "A-":
                        totalAminus += 1;
                        break;
                    case "B ":
                        totalB += 1;
                        break;
                    case "C ":
                        totalC += 1;
                        break;
                    case "D ":
                        totalD += 1;
                        break;
                    case "F ":
                        totalF += 1;
                        break;                        
                }
            }
            catch { }
        }


        int MtotalAPlus = 0;
        int MtotalA = 0;
        int MtotalAminus = 0;
        int MtotalB = 0;
        int MtotalC = 0;
        int MtotalD = 0;
        int MtotalF = 0;
        int FtotalAPlus = 0;
        int FtotalA = 0;
        int FtotalAminus = 0;
        int FtotalB = 0;
        int FtotalC = 0;
        int FtotalD = 0;
        int FtotalF = 0;
        protected void btnResultGenderWise_Click(object sender, EventArgs e)
        {
            try
            {
                //--------Validation------------
                if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
                if (dlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; dlSection.Focus(); return; }
                //------------------------------
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (ExamFinal == null)
                {
                    ExamFinal = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                dt = new DataTable();
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = ExamFinal.GetExamOverView(getClass, BatchClsID[0],
                    dlShift.SelectedValue, dlSection.SelectedValue, ddlGroup.SelectedValue, ddlExamId.SelectedValue);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Gender Wise Result  Not Found";
                    return;
                }
                string Grade = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string Gender = dt.Rows[j]["Gender"].ToString();

                    if (dt.Rows[j]["FinalGrade_OfExam_WithOptionalSub"].ToString() == "F ")
                    {
                        Grade = dt.Rows[j]["FinalGrad_OfExam"].ToString();
                    }
                    else
                    {
                        Grade = dt.Rows[j]["FinalGrade_OfExam_WithOptionalSub"].ToString();
                    }
                    GetGrade(Grade,Gender);
                }
                if (dt.Rows.Count > 0)
                {
                    DataTable dtEOV = new DataTable();
                    dtEOV.Columns.Add("TotalStudent");
                    dtEOV.Columns.Add("Male");
                    dtEOV.Columns.Add("Female");
                    dtEOV.Columns.Add("MAPlus");
                    dtEOV.Columns.Add("MA");
                    dtEOV.Columns.Add("MAMinus");
                    dtEOV.Columns.Add("MB");
                    dtEOV.Columns.Add("MC");
                    dtEOV.Columns.Add("MD");
                    dtEOV.Columns.Add("MF");
                    dtEOV.Columns.Add("FAPlus");
                    dtEOV.Columns.Add("FA");
                    dtEOV.Columns.Add("FAMinus");
                    dtEOV.Columns.Add("FB");
                    dtEOV.Columns.Add("FC");
                    dtEOV.Columns.Add("FD");
                    dtEOV.Columns.Add("FF");
                    dtEOV.Columns.Add("GroupName");
                    dtEOV.Columns.Add("SectionName");
                    dtEOV.Columns.Add("BatchName");
                    dtEOV.Columns.Add("ExName");
                    dtEOV.Columns.Add("ShiftName");
                    dtEOV.Columns.Add("PublishDate");
                    dtEOV.Rows.Add(dt.Rows.Count, MtotalAPlus + MtotalA + MtotalAminus + MtotalB + MtotalC + MtotalD+MtotalF,
                        FtotalAPlus + FtotalA + FtotalAminus + FtotalB + FtotalC + FtotalD+FtotalF,
                        MtotalAPlus, MtotalA, MtotalAminus, MtotalB, MtotalC, MtotalD, MtotalF,
                        FtotalAPlus, FtotalA, FtotalAminus, FtotalB, FtotalC, FtotalD, FtotalF,dt.Rows[0]["GroupName"].ToString(),
                        dt.Rows[0]["SectionName"].ToString(), dt.Rows[0]["BatchName"].ToString(),
                        dt.Rows[0]["ExName"].ToString(), dt.Rows[0]["ShiftName"].ToString(),
                        dt.Rows[0]["PublishDate"].ToString());

                    Session["__GenderWiseOverView__"] = dtEOV;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=GenderWiseOverView');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Gender Wise Result  Not Found";
                }
            }
            catch { }
        }
        private void GetGrade(string Grade,string Gender)
        {
            try
            {
                switch (Grade)
                {
                    case "A+":
                        if(Gender=="Male")
                        MtotalAPlus += 1;
                        else
                            FtotalAPlus += 1;
                        break;
                    case "A ":
                        if(Gender=="Male")
                        MtotalA += 1;
                        else
                            FtotalA += 1;
                        break;
                    case "A-":
                        if (Gender == "Male")
                        MtotalAminus += 1;
                        else
                            FtotalAminus += 1;
                        break;
                    case "B ":
                        if (Gender == "Male")
                        MtotalB += 1;
                        else
                            FtotalB += 1;
                        break;
                    case "C ":
                        if (Gender == "Male")
                        MtotalC += 1;
                        else
                            FtotalC += 1;
                        break;
                    case "D ":
                        if (Gender == "Male")
                        MtotalD += 1;
                        else
                            FtotalD += 1;
                        break;
                    case "F ":
                        if (Gender == "Male")
                        MtotalF += 1;
                        else
                            FtotalF += 1;
                        break;
                }
            }
            catch { }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                ExamInfoEntry.GetExamIdList(ddlExamId, BatchClsID[0],true);
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
                ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            }
            catch { }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }
    }
}