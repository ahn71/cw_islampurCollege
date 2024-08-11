using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adviitRuntimeScripting;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;

namespace DS.Report
{
    public partial class ExamReports : System.Web.UI.Page
    {
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlExamType.Items.Add("---Select---");
                Classes.Exam.LoadExamType(ddlExamType);
                Classes.commonTask.loadBatch(ddlBatch);
                Classes.commonTask.loadSection(ddlSectionName);
            }
        }
        
        protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExamId();           
        }

        private void loadSectionClayseWise()
        {
            try
            {
                ViewState["__getClassOrder__"] = "";
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + new String(ddlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray()) + "'", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
                {

                    ddlSectionName.Items.Clear();
                    ddlSectionName.Items.Add("...Select...");
                    ddlSectionName.Items.Add("Science");
                    ddlSectionName.Items.Add("Commerce");
                    ddlSectionName.Items.Add("Arts");

                    ddlSectionName.SelectedIndex = ddlSectionName.Items.Count - ddlSectionName.Items.Count;
                }

                else
                {

                    ddlSectionName.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", ddlSectionName);
                    ddlSectionName.Items.Add("...Select...");

                    ddlSectionName.SelectedIndex = ddlSectionName.Items.Count - 1;
                }
                ViewState["__getClassOrder__"] = dt.Rows[0]["ClassOrder"].ToString();
            }
            catch { }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExamId();
            loadSectionClayseWise();
        }

        private void loadExamId()
        {
            try
            {
                if (ddlExamType.Text != "...Select..." && ddlBatch.Text.Trim() != "...Select Batch...")
                {
                    string getClass=new String(ddlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray());
                    string getTableName = "Class_" +getClass + "MarksSheet";
                    sqlDB.fillDataTable("select distinct ExInId from "+getTableName+" where ExInId like '%"+ddlExamType.SelectedItem.Text+"%'",dt=new DataTable());
                    ddlExamId.DataTextField = "ExInId";
                    ddlExamId.DataSource = dt;
                    ddlExamId.DataBind();       
                }

            }
            catch { }
        }

        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSectionName.SelectedItem.Text.Trim() != "...Select...")
                {
                    sqlDB.fillDataTable("select RollNo From CurrentStudentInfo where ClassName='" + new String(ddlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray()) + "' AND SectionName='" + ddlSectionName.SelectedItem.Text.Trim() + "' AND BatchName='" + ddlBatch.SelectedItem.Text.Trim() + "' AND Shift='" + ddlShift.SelectedItem.Text.Trim() + "' Order By RollNo", dt = new DataTable());
                    ddlRoll.DataTextField = "RollNo";
                    ddlRoll.DataSource = dt;
                    ddlRoll.DataBind();
                }
            }
            catch { }
        }

        protected void btnStudentWiseMarkList_Click(object sender, EventArgs e)
        {
            generateStudentWiseMarkList();
        }

        private void generateStudentWiseMarkList()
        {
            try
            {
                divProgressReport.Visible = true;
                DataTable dtLoadQpattern=new DataTable ();
                sqlDB.fillDataTable("select * from v_SubjectQuestionPattern where ExId ="+ddlExamType.Text.Trim()+" order by ordering ",dtLoadQpattern);

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";

                
                DataTable dtLoadMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," + getTable + ".BatchName," + getTable + ".SectionName," + getTable + ".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks," + getTable + ".ConvertTo," + getTable + ".Marks FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " + getTable + ".ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject ON SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId", dtLoadMarksheet);

                DataTable dtGetDesireAllSubject = dtLoadMarksheet.DefaultView.ToTable(true, "ExInId", "SubName", "SubId", "Ordering");
                DataTable dtGetOrderingSubject = dtGetDesireAllSubject.Select("ExInId='" + ddlExamId.Text.Trim() + "'", "Ordering asc").CopyToDataTable();
                
                DataTable dtFileterSubjectMarksList = new DataTable();

                dtFileterSubjectMarksList = dtLoadMarksheet.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" + ddlSectionName.Text.Trim() + "'", "Ordering asc").CopyToDataTable();

                DataTable dtFilteringByStudentForMarks = dtLoadMarksheet.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" + ddlSectionName.Text.Trim() + "' AND RollNo="+ddlRoll.SelectedItem.Text+" AND Shift='"+ddlShift.SelectedItem.Text+"'", "Ordering asc").CopyToDataTable();
               
                lblProgressReport.Text = "Progress Report: " + ddlExamType.SelectedItem.Text+"-"+DateTime.Now.Year;
                lblClass.Text ="Class :"+ new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                lblName.Text ="Name :"+dtFilteringByStudentForMarks.Rows[0]["FullName"].ToString() + "("+ddlRoll.SelectedItem.Text+")";
                
                DataTable dtStudentAmount = new DataTable();
                dtStudentAmount = new DataView(dtFileterSubjectMarksList).ToTable(false, "StudentId", "QPName", "RollNo");

                DataTable dtGetDesireSA = dtStudentAmount.DefaultView.ToTable(true, "StudentId", "RollNo");  // SA= student amount
                if (dtStudentAmount.Rows.Count == 0) return;
                DataTable dtGetDesireSQP = dtStudentAmount.DefaultView.ToTable(true, "QPName");    // SQP =Subject Question Pattern
                DataTable dtML = new DataTable();  // ML = Mark List

                DataTable dtGetDetails = new DataTable();
                dtGetDetails.Columns.Add("Roll", typeof(int));
                dtGetDetails.Columns.Add("Name", typeof(string));


                string tblInfo = "<table class='display'>";
                tblInfo += "<thead>";
                tblInfo += "<tr>";
        
                tblInfo += "<th style='text-align: left;'>Subject</th>";
                for (byte b = 0; b < dtGetOrderingSubject.Rows.Count;b++ )
                {
                    if (b == 0) for (byte qp = 0; qp < dtGetDesireSQP.Rows.Count; qp++)
                        {
                          
                            tblInfo += "<th>" + dtGetDesireSQP.Rows[qp]["QPName"].ToString() + "</th>";
                            if (qp == dtGetDesireSQP.Rows.Count - 1)
                            {
                                tblInfo += "<th>Total<th><th>H.Marks</th>";
                                tblInfo += "</thead>";
                                tblInfo += "</tr>";

                            }
                        }
                  
                    DataTable dtGetMarks = dtFilteringByStudentForMarks.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SubId=" + dtGetOrderingSubject.Rows[b]["SubId"].ToString() + "").CopyToDataTable();
                    DataTable dtGetPassMarks = dtLoadQpattern.Select("SubId=" + dtGetOrderingSubject.Rows[b]["SubId"].ToString() + "").CopyToDataTable();
                    tblInfo += "<tr><td>"+dtGetOrderingSubject.Rows[b]["SubName"].ToString()+"</td>";


                    float getMarks = 0;
                    bool passStatus=true;
                    for (byte m = 0; m < dtGetDesireSQP.Rows.Count; m++)
                    {
                        try
                        {
                            getMarks = (dtGetMarks.Rows[m]["Marks"].ToString() == " " || dtGetMarks.Rows[m]["Marks"].ToString() == "0") ? 0 : float.Parse(dtGetMarks.Rows[m]["Marks"].ToString());
                            if (getMarks<float.Parse(dtGetPassMarks.Rows[m]["PassMarks"].ToString()))passStatus=false ;
                        }
                        catch { getMarks = 0; }
                        if (!passStatus) tblInfo += "<td style='color: red; font-weight: bold; font-size: 15px; text-align: center;'>"+getMarks+"</td>";
                        else tblInfo += "<td style='font-weight: bold;font-size: 15px; text-align: center;'>" + getMarks + "</td>";
                        if (m == dtGetDesireSQP.Rows.Count-1)
                        {
                            float totalMarks = 0;
                            try
                            {
                               totalMarks = float.Parse(dtGetMarks.Compute("sum (Marks)", "").ToString());
                            }
                            catch { totalMarks = 0; }
                            tblInfo += "<td style='font-weight: bold;font-size: 15px; text-align: center;'>" + totalMarks + "</td>";
                            tblInfo += "<td> </td>";
                            tblInfo += "</tr>";
                        }
                        passStatus = true;
                    }
 
                }
                tblInfo +="</table>";
                divProgressReport.Controls.Add(new  LiteralControl(tblInfo));

                Session["__Reports__"] = tblInfo;
                //Session["__getSubjectMarkList__"] = dtGetDetails;
                //Session["__getExamTitle__"] = ddlExamId.Text.Trim();
                //Session["__getSubjectDetails__"] = dtGetOrderingSubject;
                //Session["__getDetails__"] = dtLoadMarksheet;
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/ExamReportsPrint.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnFailList_Click(object sender, EventArgs e)
        {
            generateTotalFailList();
        }

        private void generateTotalFailList()   // Class and Section wise
        {
            try
            {
                divProgressReport.Visible = true;
                DataTable dtLoadQpattern = new DataTable();
                sqlDB.fillDataTable("select * from v_SubjectQuestionPattern where ExId =" + ddlExamType.Text.Trim() + " order by ordering ", dtLoadQpattern);

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";

                if (!IsSummaryReport)
                {
                    lblProgressReport.Text = "Progress Report: " + ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                    lblClass.Text = "Class :" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                    lblShift.Text = "Shift :" + ddlShift.SelectedItem.Text;
                    lblName.Text = "Fail Details List";
                }
                else
                {
                    lblProgressReport.Text =ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                    lblClass.Text = "Class :" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                    lblShift.Text = "Shift :" + ddlShift.SelectedItem.Text;
                    lblName.Text = "Result Summary";             
                }
               
                DataTable dtLoadMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," + getTable + ".BatchName," + getTable + ".SectionName," + getTable + ".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName," + getTable + ".Marks FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " + getTable + ".ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject ON SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId   ", dtLoadMarksheet);

                DataTable dtGetDesireAllSubject = dtLoadMarksheet.DefaultView.ToTable(true, "ExInId", "SubName", "SubId", "Ordering");
                DataTable dtGetOrderingSubject = dtGetDesireAllSubject.Select("ExInId='" + ddlExamId.Text.Trim() + "'", "Ordering asc").CopyToDataTable();

                DataTable dtFileterSubjectMarksList = new DataTable();

                dtFileterSubjectMarksList = dtLoadMarksheet.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" + ddlSectionName.Text.Trim() + "' AND Shift='"+ddlShift.SelectedItem.Text+"'  ", "Ordering asc").CopyToDataTable();

                DataTable dtStudentAmount = new DataTable();
                dtStudentAmount = new DataView(dtFileterSubjectMarksList).ToTable(false, "StudentId", "QPName", "RollNo");

                DataTable dtGetDesireSA = dtStudentAmount.DefaultView.ToTable(true, "StudentId", "RollNo");  // SA= student amount
                if (dtStudentAmount.Rows.Count == 0) return;
                DataTable dtGetDesireSQP = dtStudentAmount.DefaultView.ToTable(true, "QPName");    // SQP =Subject Question Pattern
                string tblInfo = "";
                byte TPS = 0, TFS = 0; //TPS=Total Pass Student && TFS=Total Fail Student
                if (!IsSummaryReport)
                {
                    tblInfo = "<table class='display' style='width:100%'>";
                    tblInfo += "<thead><tr> <th style='text-align: left;'>SL</th> <th style='text-align: center;'>Roll</th> <th style='text-align:left; width:150px' >Name</th> <th>Fail Details</th> </tr></thead>";
                }
                for (int i = 0; i < dtGetDesireSA.Rows.Count; i++)
                {
                   
                    string getFileInfo = "";
                    bool status = false;
                    

                    DataTable dtFilteringByStudentForMarks = dtLoadMarksheet.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" + ddlSectionName.Text.Trim() + "' AND Shift='"+ddlShift.SelectedItem.Text+"' AND StudentId=" +dtGetDesireSA.Rows[i]["StudentId"].ToString()+ "", "Ordering asc").CopyToDataTable();
                    
                    
                 
                    for (byte b = 0; b < dtGetOrderingSubject.Rows.Count; b++)
                    {
                        DataTable dtGetMarks = dtFilteringByStudentForMarks.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SubId=" + dtGetOrderingSubject.Rows[b]["SubId"].ToString() + "").CopyToDataTable();
                        DataTable dtGetPassMarks = dtLoadQpattern.Select(" ClassName='"+getClass+"' AND SubId=" + dtGetOrderingSubject.Rows[b]["SubId"].ToString() + "").CopyToDataTable();
                        int TempSubId=0;
                        for (byte c = 0; c < dtGetDesireSQP.Rows.Count; c++)
                        {
                            try
                            {
                                if (float.Parse(dtGetMarks.Rows[c]["Marks"].ToString()) < float.Parse(dtGetPassMarks.Rows[c]["PassMarks"].ToString()))
                                {
                                    if (TempSubId==0)getFileInfo += " "+dtGetOrderingSubject.Rows[b]["SubName"].ToString() + " " + dtGetDesireSQP.Rows[c]["QPName"].ToString() + ":" + dtGetMarks.Rows[c]["Marks"].ToString() + ".";
                                    else if (TempSubId.ToString() == dtGetOrderingSubject.Rows[b]["SubId"].ToString()) getFileInfo += dtGetDesireSQP.Rows[c]["QPName"].ToString() + ":" + dtGetMarks.Rows[c]["Marks"].ToString() + ".";
                                    status = true;
                                    TempSubId= int.Parse(dtGetOrderingSubject.Rows[b]["SubId"].ToString());

                                    if (IsSummaryReport) break;   // its for get summary report
                                }
                            }
                            catch { }
                        }

                        if (IsSummaryReport)if (status) break;   // its for get summary report
                    }

                    if (IsSummaryReport)
                    {
                        if (!status) TPS += 1;
                        else TFS += 1;
                    }
                  
                   if (!IsSummaryReport)if (status) 
                   {
                       tblInfo += "<tr><td style='text-align: center;'>" + (i + 1).ToString() + "</td><td style='text-align: center;'>" + dtFilteringByStudentForMarks.Rows[0]["RollNo"].ToString() + "</td><td style='text-align: left;'>" + dtFilteringByStudentForMarks.Rows[0]["FullName"].ToString() + "</td>";
                       tblInfo += "<td style='text-align: left;'>" + getFileInfo + " </td></tr>";
                   }
                  
                }
                if (IsSummaryReport)
                {
                    tblInfo += "<table class='display'><thead><tr> <th>Summary Staus</th> </tr></thead> <tr><td style='font-size: 21px; font-weight: bold;'>Total Pass Student= <span style='Color:green'>" + TPS.ToString() + "</span></td></tr> <tr><td style='font-size: 21px; font-weight: bold;'>Total Fail Student= <span style='Color:red'>" + TFS.ToString() + "</span></td></tr>";
                }
                
                tblInfo += "</table>";
                divProgressReport.Controls.Add(new LiteralControl(tblInfo));
                Session["__Reports__"] = tblInfo;
            }
            catch { }
        }

        private void generatePassList()
        {
            try
            {
                lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                lblClass.Text = "Class : " + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                lblShift.Text = "Shift : " + ddlShift.SelectedItem.Text;
                lblName.Text = "Pass List";

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet_TotalResultProcess";

                DataTable dtPlist = new DataTable();
                sqlDB.fillDataTable("select StudentId,Grade,GPA,RollNo from " + getTable + " where  " + getTable + ".ExInId='" + ddlExamId.SelectedItem.Text + "' AND " + getTable + ".Grade !='F' AND " + getTable + ".Grade !=''  And " + getTable + ".Shift='" + ddlShift.SelectedItem.Text + "' And " + getTable + ".SectionName='" + ddlSectionName.SelectedItem.Text + "' Order By RollNo ASC  ", dtPlist);

                DataTable dtName = new DataTable();

                string divInfo = "";
                divInfo = " <table id='tblPassList' class='display' style='width:100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px'>Roll</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th class='numeric'>Grade</th>";
                divInfo += "<th class='numeric'>PGA</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dtPlist.Rows.Count; x++)
                {
                    sqlDB.fillDataTable("Select FullName From StudentProfile Where StudentId='" + dtPlist.Rows[x]["StudentId"] + "' ", dtName);
                    id = dtPlist.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='width:50px' class='numeric'>" + dtPlist.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td style='width:190px'>" + dtName.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtPlist.Rows[x]["Grade"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtPlist.Rows[x]["GPA"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                
                divProgressReport.Controls.Add(new LiteralControl(divInfo));

                divProgressReport.Visible = true;
                Session["__Reports__"] = divInfo;
            }
            catch { }
        }  //Student Pass List

        private void getTopResult() // Find Top Student List
        {
            try
            {
                lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                lblClass.Text = "Class :" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                lblShift.Text = "Shift : " + ddlShift.SelectedItem.Text;
                lblName.Text = "Top " + txtTopStudent.Text + " Student List";

                int topNo=Convert.ToInt32(txtTopStudent.Text.Trim());
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet_TotalResultProcess";

                DataTable dtTopResult = new DataTable();
                sqlDB.fillDataTable("Select * From " + getTable + "  where  " + getTable + ".Grade !='F' AND " + getTable + ".Grade !=''  And " + getTable + ".Shift='" + ddlShift.SelectedItem.Text + "' Order By GPA DESC ", dtTopResult);

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
                for (int x = 0; x <topNo; x++)
                {

                    sqlDB.fillDataTable("Select FullName From StudentProfile Where StudentId='" + dtTopResult.Rows[x]["StudentId"] + "' ", dtName);

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

                divProgressReport.Controls.Add(new LiteralControl(divInfo));
                divProgressReport.Visible = true;
                Session["__Reports__"] = divInfo;
            }
            catch { }
        }

        bool IsSummaryReport = false;
        protected void btnResultSummary_Click(object sender, EventArgs e)
        {
            resultSummary();

           // IsSummaryReport = true;
           // generateTotalFailList();  // Class and Section wise
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Session["__lblProgress__"] = lblProgressReport.Text;
                Session["__lblName__"] = lblName.Text;
                Session["__lblClass__"] = lblClass.Text;
                Session["__lblShift__"] = lblShift.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/ExamReportsPrint.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlShift.SelectedItem.Text.Trim() != "...Select...")
                {
                    sqlDB.fillDataTable("select RollNo From CurrentStudentInfo where ClassName='" + new String(ddlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray()) + "' AND SectionName='" + ddlSectionName.SelectedItem.Text.Trim() + "' AND BatchName='" + ddlBatch.SelectedItem.Text.Trim() + "' AND Shift='" + ddlShift.SelectedItem.Text.Trim() + "'", dt = new DataTable());
                    ddlRoll.DataTextField = "RollNo";
                    ddlRoll.DataSource = dt;
                    ddlRoll.DataBind();
                }
            }
            catch { }
        }

        protected void btnPassList_Click(object sender, EventArgs e)
        {
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
                lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                lblClass.Text = "Class :" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                lblShift.Text = "Shift : " + ddlShift.SelectedItem.Text;
                lblName.Text = "Student GPA";

                string className = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                DataTable dtGPa = new DataTable();
                sqlDB.fillDataTable("Select GPA,TotalMarks,StudentId From Class_" + className + "MarksSheet_TotalResultProcess Where BatchName='" + ddlBatch.SelectedItem.Text + "' And Shift='" + ddlShift.SelectedItem.Text + "' And SectionName='" + ddlSectionName.Text + "' And RollNo='" + txtStudentRoll.Text.Trim() + "'  ", dtGPa);

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

                divProgressReport.Controls.Add(new LiteralControl(divInfo));
                divProgressReport.Visible = true;
                Session["__Reports__"] = divInfo;
            }
            catch { }
        }

        private void resultSummary()
        {
            try
            {
                lblProgressReport.Text = ddlExamType.SelectedItem.Text + "-" + DateTime.Now.Year;
                lblClass.Text = "Class :" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ")";
                lblShift.Text = "Shift : " + ddlShift.SelectedItem.Text;
                lblName.Text = "Result Summary";

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet_TotalResultProcess";

                DataTable dtPlist = new DataTable();
                sqlDB.fillDataTable("select StudentId,Grade,GPA,RollNo from " + getTable + " where  " + getTable + ".ExInId='" + ddlExamId.SelectedItem.Text + "' AND " + getTable + ".Grade !='F' AND " + getTable + ".Grade !=''  And " + getTable + ".Shift='" + ddlShift.SelectedItem.Text + "' And " + getTable + ".SectionName='" + ddlSectionName.SelectedItem.Text + "' Order By RollNo ASC  ", dtPlist);

                DataTable dtFlist = new DataTable();
                sqlDB.fillDataTable("select StudentId,Grade,GPA,RollNo from " + getTable + " where  " + getTable + ".ExInId='" + ddlExamId.SelectedItem.Text + "' AND  (" + getTable + ".Grade ='F' Or " + getTable + ".Grade ='')  And " + getTable + ".Shift='" + ddlShift.SelectedItem.Text + "' And " + getTable + ".SectionName='" + ddlSectionName.SelectedItem.Text + "' Order By RollNo ASC  ", dtFlist);

                string divInfo = "";
                divInfo = " <table id='tblTopResultList' class='display' style='width:100%' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Result Summary</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:280px; font-size:20px'> Total Pass Student : " + dtPlist.Rows.Count + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td style='width:280px; font-size:20px'> Total Fail Student : " + dtFlist.Rows.Count + "</td>";
                divInfo += "</tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divProgressReport.Controls.Add(new LiteralControl(divInfo));
                divProgressReport.Visible = true;
                Session["__Reports__"] = divInfo;
            }
            catch { }
        }
    }
}