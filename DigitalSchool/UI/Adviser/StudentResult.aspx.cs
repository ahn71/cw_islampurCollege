using DS.BLL.Examinition;
using DS.BLL.ManagedClass;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class StudentResult : System.Web.UI.Page
    {
        DataTable dt;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry class_classWiseMarkSheet_Entry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry exam_Final_Result_Stock_Of_All_Batch_Entry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry FinalResultEntry;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry clstotalResultEntry;
        ClassSubjectEntry class_subjectEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    string[] query = Request.QueryString["ID"].ToString().Split('-');
                    ViewState["__ReportStatus__"] = query[0];
                    ViewState["__ID__"] = query[1];
                    if (query[0].Equals("EID"))
                    {
                        if (FinalResultEntry == null)
                        {
                            FinalResultEntry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                        }
                        FinalResultEntry.GetExamID(ddlExamType);
                    }
                    else
                    {
                        divacademictranscript.Visible = true;
                        ExamInfoEntry.GetStudentWiseExamIdList(ddlExamType,query[1]);
                        if (ddlExamType.Items.Count > 0)
                        {
                            ddlExamType.SelectedIndex = 1;
                        }
                        LoadFinalResultReport(query[1]);
                    }
            }
        }
        private void LoadFinalResultReport(string stdID)
        {
            try
            {
                DataTable dtgetfinalResult;
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string[] batch = ddlExamType.SelectedItem.Text.Split('_');
                string getClass = new String(batch[2].Where(Char.IsLetter).ToArray());
                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getOptionalSub(getClass, ddlExamType.SelectedItem.Text, stdID);
                if (dt != null)
                {
                    dtgetfinalResult = new DataTable();
                    dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getExamFinalResult
                        (getClass, ddlExamType.SelectedItem.Text, stdID);
                    string divInfo = "";
                    if (dtgetfinalResult.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            //With Optional Subject
                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            divInfo += "<thead>";
                            divInfo += "<tr>";
                            divInfo += "<th colspan='3' style='text-align:center'>Without Optional Subject</th>";
                            divInfo += "<th colspan='3' style='text-align:center'>With Optional Subject</th>";
                            divInfo += "</tr>";
                            divInfo += "</thead>";
                            divInfo += "<thead>";
                            divInfo += "<tr>";
                            divInfo += "<th>Letter Grade</th>";
                            divInfo += "<th>Grade Point</th>";
                            divInfo += "<th>Total Marks</th>";
                            divInfo += "<th>Letter Grade</th>";
                            divInfo += "<th>Grade Point</th>";
                            divInfo += "<th>Total Marks</th>";
                            divInfo += "</tr>";
                            divInfo += "</thead>";
                            divInfo += "<tbody>";
                            for (int x = 0; x < dtgetfinalResult.Rows.Count; x++)
                            {
                                int sl = x + 1;
                                divInfo += "<tr style='text-align:center'>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGrad_OfExam"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGPA_OfExam"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalTotalMarks_OfExam"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGrade_OfExam_WithOptionalSub"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGPA_OfExam_WithOptionalSub"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalTotalMarks_OfExam_WithOptionalSub"].ToString() + "</td>";
                            }
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";
                            divInfo += "</table>";
                            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                            divResultSummary.Controls.Add(new LiteralControl(divInfo));
                        }
                        else
                        {
                            //without Optional Subject                        
                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            divInfo += "<thead>";
                            divInfo += "<tr>";
                            divInfo += "<th class='numeric'>Letter Grade</th>";
                            divInfo += "<th>Grade Point</th>";
                            divInfo += "<th>Total Marks</th>";
                            divInfo += "</tr>";
                            divInfo += "</thead>";
                            divInfo += "<tbody>";
                            for (int x = 0; x < dtgetfinalResult.Rows.Count; x++)
                            {
                                int sl = x + 1;
                                divInfo += "<tr style='text-align:center'>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGrad_OfExam"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalGPA_OfExam"].ToString() + "</td>";
                                divInfo += "<td >" + dtgetfinalResult.Rows[x]["FinalTotalMarks_OfExam"].ToString() + "</td>";
                            }
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";
                            divInfo += "</table>";
                            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                            divResultSummary.Controls.Add(new LiteralControl(divInfo));
                        }
                    }
                    else
                    {
                        divInfo = "<div class='noData'>Final Result Not Processing</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divResultSummary.Controls.Add(new LiteralControl(divInfo));
                    }


                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string[] query = Request.QueryString["ID"].ToString().Split('-');
            if (query[0].Equals("EID"))
            {

            }
            else
            {
                LoadFinalResultReport(query[1]);
            }
        } 
        protected void A2_ServerClick(object sender, EventArgs e)
        {
            createDetailsMarkList();
        }
        private void createDetailsMarkList()
        {

            try
            {
                string[] batch = ddlExamType.SelectedItem.Text.Split('_');
                string getClass = new String(batch[2].Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (class_classWiseMarkSheet_Entry == null)
                {
                    class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = class_classWiseMarkSheet_Entry.GetExamResultDetails(getClass, ViewState["__ID__"].ToString(), ddlExamType.SelectedItem.Text);
                }
                else
                {
                    dt = class_classWiseMarkSheet_Entry.GetGuideStudentResultDetails(getClass, ViewState["__ID__"].ToString(), ddlExamType.SelectedItem.Text);
                }
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
        protected void A3_ServerClick(object sender, EventArgs e)
        {
            GetFinalResultReport();
        }
        private void GetFinalResultReport()
        {
            try
            {
                DataTable dtgetfinalResult;
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string[] batch = ddlExamType.SelectedItem.Text.Split('_');
                string getClass = new String(batch[2].Where(Char.IsLetter).ToArray());
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getOptionalSub(getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                }
                else
                {
                    dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getGuideStudentOptionalSub(getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                }
                if (dt != null)
                {
                    dtgetfinalResult = new DataTable();
                    if (ViewState["__ReportStatus__"].ToString() == "Std")
                    {
                        dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getExamFinalResult
                            (getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                    }
                    else
                    {
                        dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getGuideStudentFinalResult
                           (getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                    }
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

        protected void A4_ServerClick(object sender, EventArgs e)
        {
            loadAcademicTranscript("");
        }
        private void loadAcademicTranscript(string sqlCmd)   // generate studentfine information if his already fined
        {
            try
            {
                //--------Validation------------

                DataTable dtgpa = new DataTable();
                dtgpa = CRUD.ReturnTableNull("select GName,GMarkMin,GMarkMax,GPointMin,GPointMax from Grading Order By GPointMin DESC ");

                DataTable dtMarkSheet = new DataTable();

                if (FinalResultEntry == null)
                {
                    FinalResultEntry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string[] batch = ddlExamType.SelectedItem.Text.Split('_');
                string className = new String(batch[2].Where(Char.IsLetter).ToArray());
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dtMarkSheet = FinalResultEntry.GetAcademicTranscript(className, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                }
                else
                {

                }
                if (dtMarkSheet == null)
                {
                    lblMessage.InnerText = "warning->Academic Transcript Not Found";
                    return;
                }
                else if (dtMarkSheet.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning->Academic Transcript Not Found";
                    return;
                }
                string divInfo = "";
                DataTable schoolInfo = new DataTable();
                schoolInfo = CRUD.ReturnTableNull("Select SchoolName,Address From School_Setup");
                divInfo += "<div style='text-align:center;'>";
                divInfo += "<h1 style='font-weight:bold;font-size: 12px; '>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + ","
                + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + " </h1>";
                divInfo += "<h2 style='font-weight: bold; padding: 0px; font-size: 10px;'>BANGLADESH <br> " + dtMarkSheet.Rows[0]["ExName"].ToString() + "-"
                + dtMarkSheet.Rows[0]["ExInDate"].ToString() + " </h2>";

                divInfo += "</div>";
                divInfo += "<div style='height:auto; width:99%; margin-top:0px;'>";
                divInfo += "<div style='height:140px; width:69%; float:left;'>"; //div1 st
                divInfo += "<table id='tblLogo' style='height:100px; font-size:10px; width:100%; float:left ' > ";
                divInfo += "<thead'>";
                divInfo += "<tr>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td> </td>";
                divInfo += "<td> </td>";
                divInfo += "<td style='text-align:center'> " + "<img src='/Images/logo.png' style='height:100px; width:100px ; margin: 0 0 0 170px;' '  </td>";
                divInfo += "<tr>";
                divInfo += "<td> </td>";
                divInfo += "<td> </td>";
                divInfo += "<td style='text-align:center; '> <h4 style='margin: 0 0 0 170px;font-size:10px;'>ACADEMIC TRANSCRIPT </h4></td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div>"; //div1 end
                divInfo += "<div style='height:auto; width:30%; float:right; border:1px solid gray; '>";
                divInfo += " <table id='tblFine' class='display ac_transcript'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;height: 15px;'>Grade</th>";
                divInfo += "<th class='numeric' style='height: 15px;'>Marks</th>";
                divInfo += "<th class='numeric' style='width:100px;height: 15px;'>Point</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (byte b = 0; b < dtgpa.Rows.Count; b++)
                {
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric' style='padding:0px;;  font-size: 10px;'>" + dtgpa.Rows[b]["GName"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GMarkMin"].ToString() + "-"
                    + dtgpa.Rows[b]["GMarkMax"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GPointMin"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";

                divInfo += "</div>";
                divInfo += "</div>";

                divInfo += "<div>"; //div main start
                divInfo += " <table id='tblStinfo' class='ac_transcript'  style='height:auto; width:99%; float: left;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:150px;font-size: 12px'> Name of Student </td>";
                divInfo += "<td style='width:10px;'> : </td>";
                divInfo += "<td style='width:220px; font-size: 12px; font-family: Lucida Console;'> " + dtMarkSheet.Rows[0]["FullName"].ToString() + " </td>";
                divInfo += "<td style='width:120px;font-size: 12px;'>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td style='width:80px;'>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Father's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'> " + dtMarkSheet.Rows[0]["FathersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Mother's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'> " + dtMarkSheet.Rows[0]["MothersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Roll No </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'>  " + dtMarkSheet.Rows[0]["RollNo"].ToString() + "  </td>";
                //if (classN.Equals("Nine") || classN.Equals("Ten"))
                //{
                //    divInfo += "<td style='width:160px;'>  Registration No </td>";
                //    divInfo += "<td> : </td>";
                //    divInfo += "<td> </td>";
                //}
                //else
                //{
                //    divInfo += "<td style='width:160px;'>  </td>";
                //    divInfo += "<td>  </td>";
                //    divInfo += "<td> </td>";
                //}

                divInfo += "<tr>";

                divInfo += "<td> Group </td>";
                divInfo += "<td> : </td>";
                if (dtMarkSheet.Rows[0]["GroupName"].ToString() == "")
                {
                    divInfo += "<td ></td>";
                }
                else
                {
                    divInfo += "<td >" + dtMarkSheet.Rows[0]["GroupName"].ToString() + "</td>";
                }

                divInfo += "<td> Section </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td > " + dtMarkSheet.Rows[0]["SectionName"].ToString() + " </td>";

                divInfo += "<td style='width:160px'></td>";
                divInfo += "<td></td>";
                divInfo += "<td>  </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div><br/><br/>"; //div main end

                divInfo += "<div>"; //div marks sheet start
                divInfo += "<table id='tblMarksList' class='display ac_transcript'  style='height:auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>SL</th>";
                divInfo += "<th style='background-color:black;color:white;'>Name of Subjects</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Letter Grade</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Grade Point</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Grade Point Average (GPA)</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                DataTable dtSub = new DataTable();
                DataTable dtOpSub = new DataTable();
                try
                {
                    dtSub = dtMarkSheet.Select("IsOptional='False'").CopyToDataTable();
                }
                catch { }
                try
                {
                    dtOpSub = dtMarkSheet.Select("IsOptional='True'").CopyToDataTable();
                }
                catch { }

                int sl = 0;
                for (int i = 0; i < dtSub.Rows.Count; i++)
                {

                    sl = i + 1;

                    divInfo += "<tr>";
                    divInfo += "<td style='width:30px;' class='numeric'> " + sl + " </td>";
                    divInfo += "<td style='width:200px;' >" + dtSub.Rows[i]["SubName"].ToString() + " </td>";
                    divInfo += "<td style='width:80px;' class='numeric'> " + dtSub.Rows[i]["GradeOfSubject_WithAllDependencySub"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtSub.Rows[i]["PointOfSubject_WithAllDependencySub"].ToString() + " </td>";
                    if (i == 0) divInfo += "<td style='width:100px; ' rowspan='" + (dtSub.Rows.Count) + "'> <h4 style='text-align:center'> "
                    + dtSub.Rows[0]["FinalGPA_OfExam"].ToString() + " </h4>  </td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div>"; //div marks sheet end

                if (dtOpSub.Rows.Count > 0)
                {

                    divInfo += "<h5>Additional Subject</h5>";
                    divInfo += "<table id='tblAdditionalSub' class='display ac_transcript'  style='height:auto; border:1px solid #D5D5D5; margin-left: 0; width: 478px;margin-bottom:15px' > ";
                    divInfo += "<thead>";
                    divInfo += "</thead>";
                    divInfo += "<td style='width:30px;' class='numeric' > " + (sl + 1) + " </td>";
                    divInfo += "<td style='width:200px;'> " + dtOpSub.Rows[0]["SubName"] + " </td>";
                    divInfo += "<td style='width:80px;'> " + dtOpSub.Rows[0]["GradeOfSubject_WithAllDependencySub"] + " </td>";
                    divInfo += "<td style='width:100px;'> " + dtOpSub.Rows[0]["FinalGPA_OfExam_WithOptionalSub"] + " </td>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                }
                divInfo += "<h5 style='width:330px; float:left; font-family: sans-serif; font-size: 11px;'>Date of Publication of Result : "
                + dtSub.Rows[0]["PublishDate"] + "</h5> <h5 style='float:right; width:200px; font-family: sans-serif; font-size: 11px;'>Controller of "
                + "Examinations </h5><br/><br/>";

                Session["__AcademicTranscript__"] = divInfo;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "print report", "goToNewTab('/Report/AcademicTranscriptPrint.aspx');", true);
            }
            catch { }
        }

        protected void A6_ServerClick(object sender, EventArgs e)
        {
            failListSubjectWise();
        }
        private void failListSubjectWise()
        {
            try
            {
                if (clstotalResultEntry == null)
                {
                    clstotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                dt = new DataTable();
                string[] batch = ddlExamType.SelectedItem.Text.Split('_');
                string getClass = new String(batch[2].Where(Char.IsLetter).ToArray());
                if (ViewState["__ReportStatus__"].ToString() == "Std")
                {
                    dt = clstotalResultEntry.LoadFailSubject(getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                }
                else
                {
                    dt = clstotalResultEntry.LoadGuideStudentFailSubject(getClass, ddlExamType.SelectedItem.Text, ViewState["__ID__"].ToString());
                }
                if (dt == null || dt.Rows.Count == 0)
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
      
    }
}