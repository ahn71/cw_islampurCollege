using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.Examinition;
using DS.BLL.ControlPanel;
using System.Text;
using System.Web.Http.Results;
using DS.Classes;
using System.Reflection;
using DS.DAL;
using DS.BLL.SMS;

namespace DS.UI.Reports.Examination
{
    public partial class ExamReports : System.Web.UI.Page
    {
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry clsTotalResultEntry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry exam_Final_Result_Stock_Of_All_Batch_Entry;
        ClassGroupEntry clsgrpEntry;

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
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);

                if (ddlGroup.Enabled == false)
                {
                    //  ExamInfoEntry.GetExamIdListWithoutQuiz(ddlExamId, BatchClsID[0]);
                    ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, BatchClsID[0]);
                    ClassSectionEntry.GetEntitiesDataWithAll(ddlSectionName, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
                }



            }
            catch { }
        }
        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (clsTotalResultEntry == null)
            //    {
            //        clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            //    }
            //    dt = new DataTable();
            //    string className = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            //    dt = clsTotalResultEntry.LoadRoll(className, ddlShift.SelectedValue, ddlBatch.SelectedValue,
            //        ddlGroup.SelectedValue, ddlSectionName.SelectedValue, ddlExamId.SelectedItem.Text);
            //    ddlRoll.DataSource = dt;
            //    if (dt == null)
            //    {
            //        ddlRoll.Items.Clear();
            //        return;
            //    }
            //    ddlRoll.DataTextField = "RollNo";
            //    ddlRoll.DataValueField = "StudentId";
            //    ddlRoll.DataBind();
            //    ddlRoll.Items.Insert(0, new ListItem("...Select..", "0"));
        }
        protected void btnStudentWiseMarkList_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            //if (ddlSectionName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; ddlSectionName.Focus(); return; }
            //------------------------------
            GetFinalResultReport();
        }

        private void GetFinalResultReport()
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                DataTable dtgetfinalResult;
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getOptionalSub(getClass, BatchClsID[0],
                    ddlExamId.SelectedValue, ddlShift.SelectedValue, ddlGroup.SelectedValue, ddlSectionName.SelectedValue);
                if (dt != null)
                {
                    dtgetfinalResult = new DataTable();
                    dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getExamFinalResult
                        (getClass, ddlShift.SelectedValue, BatchClsID[0], ddlGroup.SelectedValue,
                        ddlSectionName.SelectedValue, ddlExamId.SelectedValue);
                    string FinalResultReportName = "";
                    if (dt.Rows.Count > 0)
                    {
                        FinalResultReportName = "FinalResultwithOptional";
                    }
                    else
                    {
                        FinalResultReportName = "FinalResultwithoutOptional";
                    }
                    if (dtgetfinalResult.Rows.Count > 0)
                    {
                        Session["__FinalResult__"] = dtgetfinalResult;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                        "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FinalResult-" + FinalResultReportName + "');", true);  //Open New Tab for Sever side code                      
                    }
                    else lblMessage.InnerText = "warning-> No Result Found.";

                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }
        protected void btnResultSummary_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            //if (ddlSectionName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; ddlSectionName.Focus(); return; }
            //------------------------------
            ResultSummary();
        }
        private void ResultSummary()
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            if (clsTotalResultEntry == null)
            {
                clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            }
            dt = new DataTable();
            dt = clsTotalResultEntry.getResultSummary(ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
            if (dt == null)
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
            if (dt.Rows.Count > 0)
            {
                Session["__ResultSummary__"] = dt;
                dt = new DataTable();
                dt = clsTotalResultEntry.getSubjectWiseResultAnalysis(ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
                Session["__SubjectWiseResultAnalysis__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ResultSummary-" + ddlSectionName.SelectedItem.Text.Trim() + "');", true);  //Open New Tab for Sever side code 
            }
            else
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
        }
        private void generatePassList()
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                dt = new DataTable();
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.GetPassList(getClass, BatchClsID[0],
                    ddlShift.SelectedValue, ddlSectionName.SelectedValue, ddlGroup.SelectedValue,
                    ddlExamId.SelectedItem.Text);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Pass Student  Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    Session["_ResultList_"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ResultList-Pass List');", true);
                }
                else
                {
                    lblMessage.InnerText = "warning->Pass Student  Not Found";
                }
            }
            catch
            {

            }
        }
        //Student Pass List
        protected void btnFailList_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            //if (ddlSectionName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; ddlSectionName.Focus(); return; }
            //------------------------------
            generateFailList();
        }
        private void generateFailList()
        {
            try
            {
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                dt = new DataTable();
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.GetFailList(getClass, BatchClsID[0],
                    ddlShift.SelectedValue, ddlSectionName.SelectedValue, ddlGroup.SelectedValue,
                    ddlExamId.SelectedItem.Text);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Fail Student Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    Session["_ResultList_"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ResultList-Fail List');", true);
                }
                else
                {
                    lblMessage.InnerText = "warning->Fail Student Not Found";
                }
            }
            catch
            {

            }
        }

        private void getTopResult() // Find Top Student List
        {
            try
            {
                //  lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;                

                int topNo = Convert.ToInt32(txtTopStudent.Text.Trim());
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet_TotalResultProcess";

                DataTable dtTopResult = new DataTable();
                sqlDB.fillDataTable("Select * From " + getTable + "  where  " + getTable + ".Grade !='F' AND " + getTable + ".Grade !=''  And " + getTable
                + ".Shift='" + ddlShift.SelectedItem.Text + "' Order By GPA DESC ", dtTopResult);

                DataTable dtName = new DataTable();

                string divInfo = "";
                divInfo = " <table id='tblTopResultList' class='display' style='width:100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='width:50px; text-align:center'>SL</th>";
                divInfo += "<th style='width:50px; text-align:center'>Roll</th>";
                divInfo += "<th style='width:190px;'>Name</th>";
                divInfo += "<th style='width:60px; text-align:center'>Grade</th>";
                divInfo += "<th style='width:60px; text-align:center'>GPA</th>";
                divInfo += "<th style='width:100px; text-align:center'>Total Marks</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";
                int sl = 0;
                for (int x = 0; x < topNo; x++)
                {

                    sqlDB.fillDataTable("Select FullName From CurrentStudentInfo Where StudentId='" + dtTopResult.Rows[x]["StudentId"] + "' ", dtName);

                    sl = sl + 1;
                    id = dtTopResult.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td class='numeric'>" + dtTopResult.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dtName.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtTopResult.Rows[x]["Grade"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtTopResult.Rows[x]["GPA"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtTopResult.Rows[x]["TotalMarks"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/ExamReportsPrint.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnPassList_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            //if (ddlSectionName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section !"; ddlSectionName.Focus(); return; }
            //------------------------------
            generatePassList();
        }

        protected void btnFindTopStudent_Click(object sender, EventArgs e)
        {
            getTopResult();
        }

        protected void btnFindGPA_Click(object sender, EventArgs e)  //Find Student GPA
        {
            try
            {
                //  lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;               

                string className = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                DataTable dtGPa = new DataTable();
                sqlDB.fillDataTable("Select GPA,TotalMarks,StudentId From Class_" + className + "MarksSheet_TotalResultProcess Where BatchName='"
                + ddlBatch.SelectedItem.Text + "' And Shift='" + ddlShift.SelectedItem.Text + "' And SectionName='" + ddlSectionName.Text
                + "' And RollNo='" + txtStudentRoll.Text.Trim() + "'  ", dtGPa);

                DataTable dtName = new DataTable();
                sqlDB.fillDataTable("Select FullName From StudentProfile Where StudentId='" + dtGPa.Rows[0]["StudentId"] + "' ", dtName);

                string divInfo = "";
                divInfo = " <table id='tblTopResultList' class='display' style='width:100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='width:50px'>Name</th>";
                divInfo += "<th style='width:50px' class='numeric'>GPA</th>";
                divInfo += "<th class='numeric'>Total Marks</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                divInfo += "<tr>";

                divInfo += "<td style='width:200px'> " + dtName.Rows[0]["FullName"] + " </td>";
                divInfo += "<td class='numeric'> " + dtGPa.Rows[0]["GPA"] + " </td>";
                divInfo += "<td class='numeric'> " + dtGPa.Rows[0]["TotalMarks"] + " </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                Session["__Reports__"] = divInfo;
            }
            catch { }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            // ExamInfoEntry.GetExamIdListWithoutQuiz(ddlExamId, BatchClsID[0], ddlGroup.SelectedValue);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, BatchClsID[0], ddlGroup.SelectedValue);
            ClassSectionEntry.GetEntitiesDataWithAll(ddlSectionName, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }

        protected void btnTabulationSheet_Click(object sender, EventArgs e)
        {
            createDetailsMarkList();
        }
        private void createDetailsMarkList()
        {

            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (clsTotalResultEntry == null)
                {
                    clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                dt = clsTotalResultEntry.GetExamResultDetails(getClass, ddlShift.SelectedValue,
                    BatchClsID[0], ddlGroup.SelectedValue, ddlSectionName.SelectedValue,
                    ddlExamId.SelectedValue);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    Session["__ExamResultDetails__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ExamResultDetails');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                }
            }

            catch { }
        }

        protected void btnprogressreport_Click(object sender, EventArgs e)
        {
            SemesterProgressReport("");
        }
        private void SemesterProgressReport(string repotType)
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (clsTotalResultEntry == null)
                {
                    clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                dt = clsTotalResultEntry.LoadSemesterProgressReport(getClass, ddlShift.SelectedValue,
                    BatchClsID[0], ddlGroup.SelectedValue, ddlSectionName.SelectedValue, ddlExamId.SelectedItem.Text,
                    ddlExamId.SelectedValue, txtRollNo.Text, cbIsFinal.Checked);

                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    string IsIndependent = dt.Rows[0]["IsIndependent"].ToString();
                    Session["__progressreportsemester__"] = dt;
                    string ExamName = "Final Exam";
                    if (!cbIsFinal.Checked)
                    {
                        //string[] xm = ddlExamId.SelectedItem.Text.Split('|');
                        //if (xm[1].Contains("Exam"))
                        //ExamName = xm[1];
                        //else
                        //    ExamName = xm[1] + " Exam";
                        ExamName = dt.Rows[0]["ExName"].ToString();
                    }
                    dt = new DataTable();
                    dt = clsTotalResultEntry.LoadGrading();
                    Session["__grading__"] = dt;
                    dt = new DataTable();
                    dt = clsTotalResultEntry.LoadMonthlyTest(getClass, ddlShift.SelectedValue,
                    BatchClsID[0], ddlGroup.SelectedValue, ddlSectionName.SelectedValue,
                    ddlExamId.SelectedItem.Text);
                    Session["__monthlytest__"] = dt;

                    //string ExamName = "Final Exam";
                    //if (!cbIsFinal.Checked)
                    //{
                    //    //string[] xm = ddlExamId.SelectedItem.Text.Split('|');

                    //    //if (xm[1].Contains("Exam"))
                    //    //ExamName = xm[1];
                    //    //else
                    //    //    ExamName = xm[1] + " Exam";
                    //    ExamName = dt.Rows[0]["ExName"].ToString();
                    //}
                    string examType = (IsIndependent == "1") ? "Independent" : cbIsFinal.Checked.ToString();
                    if (repotType == "AcademicTranscript")
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AcademicTranscript-" + ExamName + "-" + examType + "');", true);  //Open New Tab for Sever side code
                    else if (repotType == "AcademicTranscriptWithMarks")
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AcademicTranscriptWithMarks-" + ExamName + "-" + examType + "');", true);
                    else
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                      "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SemesterProgressReport-" + ExamName + "-" + examType + "');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                }
            }

            catch { }
        }

        private void OldCodeOfProgressreport()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (clsTotalResultEntry == null)
                {
                    clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                dt = clsTotalResultEntry.LoadProgressReport(getClass, ddlShift.SelectedValue,
                    BatchClsID[0], ddlGroup.SelectedValue, ddlSectionName.SelectedValue,
                    ddlExamId.SelectedValue);
                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt);
                // ds.WriteXml("E:\\progressreport.xml");
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    Session["__progressreportsemester__"] = dt;
                    dt = new DataTable();
                    dt = clsTotalResultEntry.LoadGrading();
                    Session["__grading__"] = dt;
                    dt = new DataTable();
                    dt = clsTotalResultEntry.LoadMonthlyTest(getClass, ddlShift.SelectedValue,
                    BatchClsID[0], ddlGroup.SelectedValue, ddlSectionName.SelectedValue,
                    ddlExamId.SelectedValue);
                    Session["__monthlytest__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=progressreportsemester');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                }
            }

            catch { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtRollNo.Text = "";
            txtRollNo.Focus();
        }

        protected void btnAcademicTranscript_Click(object sender, EventArgs e)
        {
            SemesterProgressReport("AcademicTranscript");
        }

        protected void btnAcademicTranscriptWithMarks_Click(object sender, EventArgs e)
        {
            SemesterProgressReport("AcademicTranscriptWithMarks");
        }



        protected void btnMeritList_Click(object sender, EventArgs e)
        {
            meritListReport();
        }
        private void meritListReport()
        {
            try
            {
                string[] BatchID = ddlBatch.SelectedValue.Split('_');
                dt = new DataTable();
                if (clsTotalResultEntry == null)
                {
                    clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                dt = clsTotalResultEntry.getMeritList(BatchID[0], ddlShift.SelectedValue, ddlGroup.SelectedValue, ddlSectionName.SelectedValue, txtRollNo.Text.Trim());
                if (dt == null || dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Data not found.";
                    return;
                }
                Session["__meritlist__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=meritlist');", true);  //Open New Tab for Sever side code
            }
            catch (Exception ex) { }
        }

        protected void btnFailedStudent_Click(object sender, EventArgs e)
        {
            //--------Validation------------
            if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group !"; ddlGroup.Focus(); return; }
            //------------------------------
            FailedStudentReport();
        }
        private void FailedStudentReport()
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            if (clsTotalResultEntry == null)
            {
                clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            }
            dt = new DataTable();
            dt = clsTotalResultEntry.getFailedStudentReport(ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
            if (dt == null)
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
            if (dt.Rows.Count > 0)
            {
                Session["__FailedStudentReport__"] = dt;
                dt = new DataTable();
                dt = clsTotalResultEntry.getFailedAccordingToNumberOfSubjects(ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
                Session["__FailedAccordingToNumberOfSubjects__"] = dt;
                dt = new DataTable();
                dt = clsTotalResultEntry.getSubjectWiseFailedStudentSummary(ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
                Session["__SubjectWiseFailedStudentSummary__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FailedStudentReport');", true);  //Open New Tab for Sever side code 
            }
            else
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
        }

        protected void btnSubjectWiseFailedStudent_Click(object sender, EventArgs e)
        {
            SubjectWiseFailedStudentReport();
        }
        private void SubjectWiseFailedStudentReport()
        {
            string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            if (clsTotalResultEntry == null)
            {
                clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            }
            dt = new DataTable();
            dt = clsTotalResultEntry.getSubjectWiseFailedStudentReport(getClass, ddlExamId.SelectedValue, ddlSectionName.SelectedValue);
            if (dt == null)
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
            if (dt.Rows.Count > 0)
            {
                Session["__SubjectWiseFailedStudentReport__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubjectWiseFailedStudentReport');", true);  //Open New Tab for Sever side code 
            }
            else
            {
                lblMessage.InnerText = "warning->No Result Found";
                return;
            }
        }


        protected void btnShowResult_Click(object sender, EventArgs e)
        {

            Session["ExamName"] = ddlExamId.SelectedItem.Text;
            Session["GroupName"] = ddlGroup.SelectedItem.Text;
            getTotalPassedInfor();
            getPassedResult();
            getFailedResult();
            string url = "ShowResult.aspx";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
        }

        private void getPassedResult()
        {
            string examId = ddlExamId.SelectedValue;
            string ClasGrpID = ddlGroup.SelectedValue;



            using (SqlConnection connection = DbConnection.Connection)
            {
                if (connection.State != null)
                    connection.Close();
                connection.Open();


                string query = "SELECT CONCAT(rs.rollno, '[', rs.GPA, ']') AS rollnumber_gpa FROM Exam_ResultSheet rs  WHERE rs.ExamID='" + examId + "' AND rs.ClsGrpID='" + ClasGrpID + "'  AND IsPassed='1 '";



                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                StringBuilder resultBuilder = new StringBuilder();
                int count = 0;
                while (reader.Read())
                {
                    string rollnumberGPA = reader["rollnumber_gpa"].ToString();
                    resultBuilder.Append(rollnumberGPA).Append(", ");

                    count++;
                }

                reader.Close();

                Session["ResultPassedData"] = resultBuilder.ToString().TrimEnd(',', ' ') + " =" + count;


            }



        }


        private void getFailedResult()
        {
            string examId = ddlExamId.SelectedValue;
            string ClasGrpID = ddlGroup.SelectedValue;



            using (SqlConnection connection = DbConnection.Connection)
            {
                if (connection.State != null)
                    connection.Close();
                connection.Open();
                string query = "SELECT CONCAT(rs.rollno, '[', rs.Grade, ']') AS rollnumber_gpa FROM Exam_ResultSheet rs  WHERE rs.ExamID='" + examId + "' AND rs.ClsGrpID='" + ClasGrpID + "'  AND IsPassed='0'";



                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                StringBuilder resultBuilder = new StringBuilder();
                int count = 0;
                while (reader.Read())
                {
                    string rollnumberGPA = reader["rollnumber_gpa"].ToString();
                    resultBuilder.Append(rollnumberGPA).Append(", ");

                    count++;
                }

                reader.Close();

                Session["ResultFailedData"] = resultBuilder.ToString().TrimEnd(',', ' ') + " =" + count;

            }


        }

        private void getTotalPassedInfor()
        {
            string examId = ddlExamId.SelectedValue;
            string ClasGrpID = ddlGroup.SelectedValue;

            using (SqlConnection connection = DbConnection.Connection)
            {
                if (connection.State != null)
                    connection.Close();
                connection.Open();
                string query = " SELECT COUNT(StudentID) AS Examinee,SUM(CASE WHEN IsPassed = 'True' THEN 1 ELSE 0 END) AS Appeared,  SUM(CASE WHEN IsPassed = 'True' THEN 1 ELSE 0 END) AS Passed, CAST(SUM(CASE WHEN IsPassed = 'True' THEN 1 ELSE 0 END) AS DECIMAL) / COUNT(StudentID) * 100 AS PassPercentage,SUM(CASE WHEN GPA = 5 THEN 1 ELSE 0 END) AS GPA5  FROM Exam_ResultSheet rs where rs.ExamID ='" + examId + "' and rs.ClsGrpID ='" + ClasGrpID + "'  group by rs.BatchID";



                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int examineCount = Convert.ToInt32(reader["Examinee"]);
                    int appearedCount = Convert.ToInt32(reader["Appeared"]);
                    int passedCount = Convert.ToInt32(reader["Passed"]);
                    double passPercentage = Convert.ToDouble(reader["PassPercentage"]);
                    int gpa5Count = Convert.ToInt32(reader["GPA5"]);

                    string output = string.Format("No. of Students: {{ Examinee: {0}, Appeared: {1}, Passed: {2}, Percentage of Pass: {3:F2}, GPA 5: {4} }}",
                                      examineCount, appearedCount, passedCount, passPercentage, gpa5Count);

                    Session["resultSatatus"] = output;

                }

                reader.Close();

                // Session["ResultFailedData"] = resultBuilder.ToString().TrimEnd(',', ' ') + " =" + count;

            }

        }

    }
}