using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using DS.BLL;

namespace DS.Forms
{
    public partial class MarksEntry : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(ddlBatch);
                        ddlBatch.SelectedIndex = ddlBatch.Items.Count - 1;
                        Classes.Exam.loadExamId(ddlExamId);       
                        ddlSectionName.Items.Add("---Select---");
                        ddlSectionName.SelectedIndex = ddlSectionName.Items.Count - ddlSectionName.Items.Count;
                    }
                }
            }
            catch { }
        }
        string tblInfo = "";
        string tblHeader = "";
        private void loadMarksEntrySheet()
        {
            try
            {
                string [] getExTypeId = ddlExamId.SelectedItem.Text.Split('_');
                DataTable dtExId = new DataTable();
                MarkSheetTitle.InnerText = "Marksheet entry point for batch " + ddlBatch.SelectedItem.Text + " section " + ddlSectionName.Text.Trim() + " (" 
                + ddlShift.SelectedItem.Text + ")";
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable="Class_"+getClass+"MarksSheet";               
                // load all student of this batch 
                SQLOperation.selectBySetCommandInDatatable("select StudentId from CurrentStudentInfo where BatchName='" + ddlBatch.Text.Trim() + "' AND SectionName='" 
                + ddlSectionName.SelectedItem.Text.Trim() + "' AND Shift='" + ddlShift.SelectedItem.Text.Trim() + "' order by RollNo", dt = new DataTable(), sqlDB.connection);
                DataTable dtSQPInfo = new DataTable();   // SQP = subject question pattern information
                if (byte.Parse(ViewState["__getClassOrder__"].ToString()) < 9) SQLOperation.selectBySetCommandInDatatable("select SubName,Ordering,QPId,QPName from "
                +"v_SubjectQuestionPattern where ClassName='" + getClass + "' AND ExName='" + getExTypeId[0] + "' order by Ordering", dtSQPInfo, sqlDB.connection);
                else SQLOperation.selectBySetCommandInDatatable("select SubName,Ordering,QPId,QPName from v_SubjectQuestionPattern where ClassName='" + getClass + "' "
                +"AND ExName='" + getExTypeId[0] + "' AND GroupName='"+ddlSectionName.SelectedItem.Text.Trim()+"' order by Ordering", dtSQPInfo, sqlDB.connection);    // specially for nine ten and upper from ten
                
                tblInfo = "<Table id=tblMarkEntryPoint>";
                tblHeader+="<th>Roll No</th>";
                for (byte b = 0; b < dtSQPInfo.Rows.Count; b++) 
                {
                    tblHeader += "<th>" + dtSQPInfo.Rows[b]["SubName"].ToString().Replace(" ","") + "_" + dtSQPInfo.Rows[b]["QPName"].ToString().Replace(" ","")+" </th>";
                    if (b == dtSQPInfo.Rows.Count - 1) tblInfo += tblHeader;
                }

                tblInfo+="<tr>";
                DataTable dtCreateViewForClassMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," 
                + getTable + ".BatchName," + getTable + ".SectionName," + getTable + ".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,"
                +"NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks," + getTable + ".ConvertTo," + getTable + ".Marks," 
                + getTable + ".GroupName FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " 
                + getTable + ".ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject "
                +"ON SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId Order By " 
                + getTable + ".RollNo ASC ", dtCreateViewForClassMarksheet);
                ViewState["__getMarkSheetView__"] = dtCreateViewForClassMarksheet;     
                for (int i = 0; i < dt.Rows.Count; i++)   // all student included in dt 
                    {
                        int row =i+1;

                        DataTable dtSubInfo = new DataTable();
                        dtSubInfo = dtCreateViewForClassMarksheet.Select("StudentId=" + dt.Rows[i]["StudentId"].ToString() + " AND ExInId='" + ddlExamId.Text.Trim() + "' ").CopyToDataTable();
                        for (int j = 0; j < dtSubInfo.Rows.Count; j++)
                        {
                            if (j == 0) tblInfo += "<td>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input  autocomplete='off' onchange='saveData(this)' "
                            +"type='text' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" 
                            + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + "'value=" + dtSubInfo.Rows[j]["Marks"].ToString() 
                            + "></td>";
                            else tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                            +"type=text tabindex=" + row + " id='"+getTable+":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                            ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + "' value="+dtSubInfo.Rows[j]["Marks"].ToString()+"></td>";
                            row += dt.Rows.Count;
                            if (j == dtSubInfo.Rows.Count - 1) tblInfo += "</tr>";
                        } 
                    }
                    tblInfo += "</table>";
                    divMarksheet.Controls.Add(new LiteralControl(tblInfo));
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MarkSheetTitle.Visible = true;
            divMarksheet.Visible = true;
            loadMarksEntrySheet();
        }

        protected void btnPreviewMarksheet_Click(object sender, EventArgs e)
        {
            createSubjectWiseMarkList();
        }
        private void createSubjectWiseMarkList()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";
                DataTable dtLoadMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," 
                    + getTable + ".BatchName," + getTable + ".SectionName," + getTable + ".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,"
                +"NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks," + getTable + ".ConvertTo," + getTable 
                + ".Marks FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " + getTable 
                + ".ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject ON "
                +"SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId", dtLoadMarksheet);
                
                DataTable dtFileterSubjectMarksList = new DataTable();
                dtFileterSubjectMarksList = dtLoadMarksheet.Select("SubId=" + ddlsubjectName.SelectedValue.ToString() + " AND ExInId='" + ddlExamId.Text.Trim() + "' "
                +"AND SectionName='" + ddlSectionName.Text.Trim() + "'").CopyToDataTable();
                DataTable dtStudentAmount = new DataTable();
                dtStudentAmount = new DataView(dtFileterSubjectMarksList).ToTable(false,"StudentId","QPName","RollNo");

                DataTable dtGetDesireSA = dtStudentAmount.DefaultView.ToTable(true, "StudentId","RollNo");  // SA= student amount
                DataTable dtGetDesireSQP = dtStudentAmount.DefaultView.ToTable(true,"QPName");    // SQP =Subject Question Pattern
                DataTable dtML = new DataTable();  // ML = Mark List
                dtML.Columns.Add("Roll",typeof(int));
                for (byte b = 0; b < dtGetDesireSQP.Rows.Count; b++)
                {
                    dtML.Columns.Add(dtGetDesireSQP.Rows[b]["QPName"].ToString(), typeof(string));
                }
                for (int i = 0; i < dtGetDesireSA.Rows.Count; i++)
                {
                    DataTable dt=new DataTable ();
                    dt=dtFileterSubjectMarksList.Select("StudentId="+dtGetDesireSA.Rows[i]["StudentId"].ToString()+"").CopyToDataTable();
                    dtML.Rows.Add(dtGetDesireSA.Rows[i]["RollNo"].ToString());

                    for (byte b = 0; b <dt.Rows.Count; b++)
                    {
                        string getMarks = (dt.Rows[b]["Marks"].ToString().Trim() == " " || dt.Rows[b]["Marks"].ToString().Trim()=="") ? "0" : dt.Rows[b]["Marks"].
                        ToString().Trim();
                        
                        if (b == dt.Rows.Count - 1) dtML.Rows[i][b+1] = getMarks;
                        else dtML.Rows[i][b+1] =getMarks;
                    }

                    Session["__Status__"] = "MarkList";
                    Session["__getSubjectMarkList__"] = dtML;
                    Session["__getExamTitle__"] = ddlExamId.Text.Trim();
                    Session["__getSubjectName__"] = ddlsubjectName.SelectedItem.Text;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/SubjectWiseMarksList.aspx');", true);  //Open New Tab for Sever side code
                }

            }
            catch { }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubject(); loadSectionClayseWise(); loadExamIdClassAndBatchWise();
        }

        private void loadExamIdClassAndBatchWise()
        {
            try
            {
                DataTable dt = new DataTable();
                SQLOperation.selectBySetCommandInDatatable("select ExInId from ExamInfo where ExInId Like '%"+ddlBatch.SelectedItem.Text.Trim()+"%' order by ExInSl desc", dt, sqlDB.connection);
                ddlExamId.Items.Clear();
                ddlExamId.Items.Add("Select Exam Id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlExamId.Items.Add(dt.Rows[i]["ExInId"].ToString());
                }
                ddlExamId.SelectedIndex = ddlExamId.Items.Count - ddlExamId.Items.Count;
            }
            catch { }
        }

        private void LoadSubject()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName From V_ClasswiseSubject where ClassName='" +getClass+ "' order by Ordering", dt);
                ddlsubjectName.DataSource = dt;
                ddlsubjectName.DataTextField = "SubName";
                ddlsubjectName.DataValueField = "SubId";
                ddlsubjectName.DataBind();
                ddlsubjectName.Items.Add("...Select Subject...");
                ddlsubjectName.SelectedIndex = ddlsubjectName.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        
        private void loadSectionClayseWise()
        {
            try
            {
                ViewState["__getClassOrder__"] = "";
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='"+new String (ddlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray())+"'",dt=new DataTable (),sqlDB.connection);
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

        protected void btnDetailsMarks_Click(object sender, EventArgs e)
        {
            createDetailsMarkList();
        }

        private void createDetailsMarkList()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";
                
                DataTable dtLoadMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," 
                    + getTable + ".BatchName," + getTable + ".SectionName," + getTable + ".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,"
                +"NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks," + getTable + ".ConvertTo," + getTable 
                + ".Marks FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " + getTable 
                + ".ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject ON "
                +"SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId Order By " + getTable 
                + ".RollNo", dtLoadMarksheet);

                DataTable dtGetDesireAllSubject = dtLoadMarksheet.DefaultView.ToTable(true, "ExInId", "SubName", "SubId", "Ordering");
                DataTable dtGetOrderingSubject = dtGetDesireAllSubject.Select("ExInId='" + ddlExamId.Text.Trim() + "'", "Ordering asc").CopyToDataTable();

                DataTable dtFileterSubjectMarksList = new DataTable();

                dtFileterSubjectMarksList = dtLoadMarksheet.Select("ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" + ddlSectionName.Text.Trim() + "' AND Shift='"
                +ddlShift.SelectedItem.Text+"'", "Ordering asc").CopyToDataTable();
                
                DataTable dtStudentAmount = new DataTable();
                dtStudentAmount = new DataView(dtFileterSubjectMarksList).ToTable(false, "StudentId", "QPName", "RollNo");

                DataTable dtGetDesireSA = dtStudentAmount.DefaultView.ToTable(true, "StudentId", "RollNo");  // SA= student amount
                if (dtStudentAmount.Rows.Count == 0) return;
                DataTable dtGetDesireSQP = dtStudentAmount.DefaultView.ToTable(true, "QPName");    // SQP =Subject Question Pattern
                DataTable dtML = new DataTable();  // ML = Mark List

                DataTable dtGetDetails = new DataTable();
                dtGetDetails.Columns.Add("Roll",typeof(int));
                dtGetDetails.Columns.Add("Name", typeof(string));
                for (int i = 0; i < dtGetDesireSA.Rows.Count; i++)
                {
  
                dt = new DataTable();
                dt = dtLoadMarksheet.Select("StudentId=" + dtGetDesireSA.Rows[i]["StudentId"].ToString() + " AND ExInId='" + ddlExamId.Text.Trim() + "' AND SectionName='" 
                + ddlSectionName.Text.Trim() + "'", "Ordering asc").CopyToDataTable();
                if (i == 0) for (byte b = 0; b < dt.Rows.Count; b++) dtGetDetails.Columns.Add(dt.Rows[b]["SubName"].ToString().Replace(" ", "") + "_" 
                + dt.Rows[b]["QPName"].ToString().Replace(" ", ""), typeof(string));
                for (byte b = 0; b < dt.Rows.Count; b++)
                {
                    if (b == 0) dtGetDetails.Rows.Add(dt.Rows[0]["RollNo"].ToString(), dt.Rows[0]["FullName"].ToString());
                    dtGetDetails.Rows[i][b + 2] = dt.Rows[b]["Marks"].ToString();
                }


                }

                Session["__Status__"] = "Details";
                Session["__getSubjectMarkList__"] = dtGetDetails;
                Session["__getExamTitle__"] = ddlExamId.Text.Trim();
                Session["__getSubjectDetails__"] = dtGetOrderingSubject;
                Session["__getDetails__"] = dtLoadMarksheet;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/SubjectWiseMarksList.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnTotalResultProcess_Click(object sender, EventArgs e)
        {
            TotalResultProcess();
        }

        int sr = 0; // sr =subject ranking
        Boolean getResult = false;
        private void TotalResultProcess()
        {
            try
            {
               
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";
                Session["__ClassName__"] = getClass;
                displayTotalFinalResult(getTable);       // display final result or processing result       
                if (getResult)
                {
                    
                    DataTable dtFinalResult = new DataTable();
                    sqlDB.fillDataTable("select RollNo,FullName,SubName,SubId,Marks,SGrade,SPoint,GPA,Grade,TotalMarks from v_FinlaResultLog where BatchName='" 
                    + ddlBatch.SelectedItem.Text + "' and Shift='" + ddlShift.SelectedItem.Text + "' and SectionName='" + ddlSectionName.SelectedItem.Text + "' and "
                    +"ExInId='" + ddlExamId.SelectedItem.Text + "' Order By RollNo   ", dtFinalResult);

                    DataTable dtClsId = new DataTable();
                    sqlDB.fillDataTable("select ClassId from v_ClasswiseSubject where  ClassName='" + Session["__ClassName__"].ToString() + "' ", dtClsId);

                    DataTable dtDependencySubId = new DataTable();
                    sqlDB.fillDataTable("select * from v_ClassSubject where ClassID=" + dtClsId.Rows[0]["ClassId"] + " and DependencySubId not in (0) ", dtDependencySubId);

                    Session["__FainalResult__"] = dtFinalResult;
                    Session["__DependencySubId__"] = dtDependencySubId;
                    Session["__ExamId__"] = ddlExamId.SelectedItem.Text;

                    if (dtFinalResult.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/StudentFinalResultReport.aspx');", true);  //Open New Tab for Sever side code
                    }
                    return;
                }

                DataTable dtLoadGread = new DataTable();

                SQLOperation.selectBySetCommandInDatatable("select * from  Grading ",dtLoadGread,sqlDB.connection);
               
                DataTable dtLoadMarksheet = new DataTable();
                sqlDB.fillDataTable("SELECT " + getTable + ".MarksSL," + getTable + ".ExInId," + getTable + ".StudentId,StudentProfile.FullName," + getTable + ".RollNo," 
                + getTable + ".BatchName," + getTable + ".SectionName,"+getTable+".Shift," + getTable + ".SubQPId,SubjectQuestionPattern.SubId,NewSubject.SubName,"
                +"NewSubject.Ordering,QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks,"+getTable+".ConvertTo," + getTable + ".Marks FROM "
                +"StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " + getTable + ".StudentId INNER JOIN ExamInfo ON " + getTable + ".ExInId = "
                +"ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON " + getTable + ".SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN NewSubject ON "
                +"SubjectQuestionPattern.SubId = NewSubject.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId", dtLoadMarksheet);


                sqlDB.fillDataTable("select ExInDependency from ExamInfo where ExInId ='" + ddlExamId.SelectedItem.Text.Trim() + "' AND ExInDependency <>'0'", dt = new DataTable());
                DataSet ds=new DataSet ();

                DataTable dtSectionWiseStudent = dtLoadMarksheet.Select("ExInId ='" + ddlExamId.SelectedItem.Text + "' AND SectionName='" + ddlSectionName.SelectedItem.Text + "' AND Shift ='"+ddlShift.SelectedItem.Text+"'", "ordering asc").CopyToDataTable();

                DataTable dtTotalStudent = dtSectionWiseStudent.DefaultView.ToTable(true, "RollNo");

                float[] convertTo1;
                float[] convertTo2;
                
                for (byte b = 0; b < dt.Rows.Count; b++)
                {
                    DataTable dtGetdepensExmDetails = new DataTable();
                  //  SQLOperation.selectBySetCommandInDatatable("select * from " + getTable + " where ExInId ='" + dt.Rows[b]["ExInDependency"].ToString() + "'", dtGetdepensExmDetails, sqlDB.connection);
                    
                    dtGetdepensExmDetails=dtLoadMarksheet.Select("ExInId ='"+dt.Rows[b]["ExInDependency"].ToString()+"' AND SectionName='"+ddlSectionName.SelectedItem.Text+"'","ordering asc").CopyToDataTable();

                    ds.Tables.Add(dtGetdepensExmDetails);
                }

                //----------------for calculating convert marks---------------------


                

                for (int i = 0; i < dtTotalStudent.Rows.Count; i++)
                {
                    DataTable dtOldSIdSmarks = new DataTable();
                    dtOldSIdSmarks.Columns.Add("SubQPId", typeof(int));
                    dtOldSIdSmarks.Columns.Add("QPName", typeof(string));
                    dtOldSIdSmarks.Columns.Add("SubId", typeof(int));
                    dtOldSIdSmarks.Columns.Add("SubName", typeof(string));
                    dtOldSIdSmarks.Columns.Add("Marks", typeof(float));


                    DataTable dtNewSIdSmarks = new DataTable();
                    dtNewSIdSmarks.Columns.Add("SubQPId", typeof(int));
                    dtNewSIdSmarks.Columns.Add("QPName", typeof(string));
                    dtNewSIdSmarks.Columns.Add("SubId", typeof(int));
                    dtNewSIdSmarks.Columns.Add("SubName", typeof(string));
                    dtNewSIdSmarks.Columns.Add("Marks", typeof(float));
                    DataTable dtDetails=ds.Tables[0];

                    DataTable dtDESDetails = dtDetails.Select("RollNo=" + dtTotalStudent.Rows[i]["RollNo"].ToString() + " AND Shift='" + ddlShift.SelectedItem.Text.Trim() 
                    + "' AND SectionName='"+ddlSectionName.SelectedItem.Text.Trim()+"'").CopyToDataTable();   // DESDetails=Dependency Exam Subjects Details
                    convertTo1 = new float[dtDESDetails.Rows.Count];
                    convertTo2 = new float[dtDESDetails.Rows.Count];
                    DataTable dtCESDetails = dtLoadMarksheet.Select("RollNo=" + dtTotalStudent.Rows[i]["RollNo"].ToString() + " AND Shift='" + ddlShift.SelectedItem.Text.Trim() + "' AND ExInId ='" + ddlExamId.SelectedItem.Text + "' AND SectionName='" + ddlSectionName.SelectedItem.Text + "'", "ordering asc").CopyToDataTable();  // CESDetails=Current Exam Subjects Details
                    if (i == 0) if (dtDESDetails.Rows.Count>0) createFinalMarkSheet(dtDESDetails,getTable);   // create final Mark sheet
                    for (byte b = 0; b < dtDESDetails.Rows.Count; b++)
                    {
                        try
                        {
                            dtOldSIdSmarks.Rows.Add(dtDESDetails.Rows[b]["SubQPId"].ToString(), dtDESDetails.Rows[b]["QPName"].ToString(), dtDESDetails.Rows[b]["SubId"].ToString(), dtDESDetails.Rows[b]["SubName"].ToString());
                            convertTo1[b] = (float.Parse(dtDESDetails.Rows[b]["Marks"].ToString()) * float.Parse(dtDESDetails.Rows[b]["ConvertTo"].ToString())) / float.Parse(dtDESDetails.Rows[b]["QMarks"].ToString());
                            dtOldSIdSmarks.Rows[b]["Marks"] = convertTo1[b];

                            convertTo2[b] = (float.Parse(dtCESDetails.Rows[b]["Marks"].ToString()) * float.Parse(dtCESDetails.Rows[b]["ConvertTo"].ToString())) / float.Parse(dtCESDetails.Rows[b]["QMarks"].ToString());
                            dtNewSIdSmarks.Rows.Add(dtCESDetails.Rows[b]["SubQPId"].ToString(), dtCESDetails.Rows[b]["QPName"].ToString(), dtCESDetails.Rows[b]["SubId"].ToString(), dtCESDetails.Rows[b]["SubName"].ToString());
                            dtNewSIdSmarks.Rows[b]["Marks"] = convertTo2[b];
                        }
                        catch { }
                        if (b == dtDESDetails.Rows.Count - 1) setStudentDetailsWithGradePoint(getTable, int.Parse(dtDESDetails.Rows[0]["StudentId"].ToString()), int.Parse(dtTotalStudent.Rows[i]["RollNo"].ToString()), ddlBatch.SelectedItem.Text, ddlSectionName.SelectedItem.Text, dtLoadGread, (byte)i, dtOldSIdSmarks, dtNewSIdSmarks);
                    } 

                    
                }
                displayTotalFinalResult(getTable);       // display final result or processing result      
                //------------------------------------------------------------------


               
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->" + ex.Message;
            }
        }

        DataTable dtResultInfo;
        DataTable dtFinalResultLog;
        DataTable dtDependency;
        DataTable dtDependencyMarks;
        private void setStudentDetailsWithGradePoint(string getTable, int StudentId, int rollNo, string BatchName, string SectionName, DataTable dtG, byte ProcessStart, DataTable dtOldSIdSmarks, DataTable dtNewSIdSmarks)
        {

            try
            {
                double TotalMarks = 0,TotalGPA=0;
                Boolean FailStatus = false;
                bool isOptionalSub = false;
                DataTable dtOpSubInfo=new DataTable ();
                
                byte TFSA = 0;   //TFSA= Total Fail Student Amount
                //_____________________________________________________________________For Create Columns_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________ 
                if (ProcessStart == 0)
                {
                    dtDependency = new DataTable();
                    dtDependency.Columns.Add("SubId",typeof(int));
                    dtDependency.Columns.Add("DependencySubId", typeof(int));

                    dtDependencyMarks = new DataTable();
                    dtDependencyMarks.Columns.Add("SubId", typeof(int));
                    dtDependencyMarks.Columns.Add("DependencySubId", typeof(int));
                    dtDependencyMarks.Columns.Add("DependencyMarks", typeof(float));

                    DataTable dtDS = new DataTable();
                    SQLOperation.selectBySetCommandInDatatable("Select SubId,DependencySubId from ClassSubject where ClassId=(select ClassId from Classes where ClassName='" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "') AND DependencySubId not in (0)", dtDS, sqlDB.connection);
                    if (dtDS.Rows.Count > 0)
                    {
                        for(byte b=0;b<dtDS.Rows.Count;b++)
                        {
                            dtDependency.Rows.Add(int.Parse(dtDS.Rows[b]["SubId"].ToString()),int.Parse(dtDS.Rows[b]["DependencySubId"].ToString()));
                            
                        }
                    }

                    dtFinalResultLog = new DataTable();
                    dtFinalResultLog.Columns.Add("ExInId",typeof(string));
                    dtFinalResultLog.Columns.Add("StudentId", typeof(int));
                    dtFinalResultLog.Columns.Add("SubId", typeof(int));
                    dtFinalResultLog.Columns.Add("DependencySubId", typeof(string));
                    dtFinalResultLog.Columns.Add("Marks", typeof(float));
                    dtFinalResultLog.Columns.Add("SGrade", typeof(string));
                    dtFinalResultLog.Columns.Add("SPoint", typeof(float));
                    dtFinalResultLog.Columns.Add("GPA", typeof(float));
                    dtFinalResultLog.Columns.Add("Grade", typeof(string));
                    dtFinalResultLog.Columns.Add("TotalMarks", typeof(float));

                  //  dtFinalResultProcess = new dtFinalResultProcess();
                    dtResultInfo = new DataTable();
                    DataTable dtGetColumns = new DataTable();
                    string cmd;
                    if (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9) cmd = "select * from " + getTable + "_" + ddlSectionName.SelectedItem.Text.Trim().Replace(" ", "") + "TotalResultProcess";
                    else cmd = "select * from " + getTable + "_TotalResultProcess";
                    SQLOperation.selectBySetCommandInDatatable(cmd, dtGetColumns, sqlDB.connection);

                    for (byte c = 1; c < dtGetColumns.Columns.Count; c++)
                    {
                        if ((dtGetColumns.Columns[c].ToString().Equals("ExInId")) || (dtGetColumns.Columns[c].ToString().Equals("BatchName")) || (dtGetColumns.Columns[c].ToString().Equals("SectionName")) || (dtGetColumns.Columns[c].ToString().Contains("Grade")) || (dtGetColumns.Columns[c].ToString().Equals("Shift"))) dtResultInfo.Columns.Add(dtGetColumns.Columns[c].ToString(), typeof(string));
                        else if (dtGetColumns.Columns[c].ToString().Equals("StudentId") || dtGetColumns.Columns[c].ToString().Equals("RollNo")) dtResultInfo.Columns.Add(dtGetColumns.Columns[c].ToString(), typeof(int));
                        else dtResultInfo.Columns.Add(dtGetColumns.Columns[c].ToString(), typeof(float));
                    }

                }

                //_____________________________________________________________________End____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________|

                if (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9)  //______Check is optional____
                {
                    sqlDB.fillDataTable("select SubId from OptionalSubjectInfo where StudentId=" + StudentId + "", dtOpSubInfo);
                }
                dtResultInfo.Rows.Add(ddlExamId.SelectedItem.Text, StudentId, rollNo, ddlBatch.SelectedItem.Text, ddlSectionName.SelectedItem.Text, ddlShift.SelectedItem.Text);  // data enterd as result processing

                double marks;
                string GName="";
                float Gpoint =0;
                DataTable dtgetOrderdSubject = dtOldSIdSmarks.DefaultView.ToTable(true,"SubId");

                for (byte m = 0; m < dtgetOrderdSubject.Rows.Count; m++)
                {
                    marks = 0;

                    DataTable dtOldCertainSubject = dtOldSIdSmarks.Select("SubId=" + dtgetOrderdSubject.Rows[m]["SubId"].ToString() + "").CopyToDataTable();
                    DataTable dtNewCertainSubject = dtNewSIdSmarks.Select("SubId=" + dtgetOrderdSubject.Rows[m]["SubId"].ToString() + "").CopyToDataTable();

                    DataTable dtQPId = dtOldSIdSmarks.Select("SubId=" + dtgetOrderdSubject.Rows[m]["SubId"].ToString() + "").CopyToDataTable();

                    float[] esm = new float[dtNewCertainSubject.Rows.Count]; // esm=each subject marks 
                    string []columnName=new string [dtQPId.Rows.Count];

                    for (byte x = 0; x < dtOldCertainSubject.Rows.Count; x++)  // count all pattern marks
                    {
                        esm[x]= float.Parse(dtOldCertainSubject.Rows[x]["Marks"].ToString()) + float.Parse(dtNewCertainSubject.Rows[x]["Marks"].ToString());
                        columnName[x] = dtOldCertainSubject.Rows[x]["SubName"].ToString().Replace(" ","")+"_"+dtOldCertainSubject.Rows[x]["QPName"].ToString().Replace(" ","");
                    }

                    for (byte b = 0; b < esm.Length; b++)
                    {
                        marks += esm[b];
                    }

                    //_______________________________For dependency subject marks_________________________________________
                    for (byte b = 0; b < dtDependency.Rows.Count; b++)
                    {
                        if ((dtgetOrderdSubject.Rows[m]["SubId"].ToString().Equals(dtDependency.Rows[b]["SubId"].ToString())) || (dtgetOrderdSubject.Rows[m]["SubId"].ToString().Equals(dtDependency.Rows[b]["DependencySubId"].ToString())))
                        {
                            dtDependencyMarks.Rows.Add(int.Parse(dtgetOrderdSubject.Rows[m]["SubId"].ToString()), int.Parse(dtDependency.Rows[b]["DependencySubId"].ToString()), marks);

                        }
                    }



                    //_____________________________________________________________________Find Out Grade Subject Grade & Subject Point_______________________________________________________________________________________________
                    bool status = false;
                    for (byte b = 0; b < dtG.Rows.Count; b++)
                    {
                        if (Math.Floor(marks) >= double.Parse(dtG.Rows[b]["GMarkMin"].ToString()) && Math.Floor(marks) <= double.Parse(dtG.Rows[b]["GMarkMax"].ToString()))
                        {
                            
                            for (byte y =(byte)sr; y < dtResultInfo.Columns.Count; y++)
                            {
                                
                                if (dtResultInfo.Columns[y].ToString().Equals(columnName[0]))
                                {
                                    for (byte z = 0; z <esm.Length; z++)
                                    {
                                        dtResultInfo.Rows[0][y] =Math.Floor(esm[z]); y++;
                                        
                                        if (z == esm.Length - 1)
                                        {
                                           
                                            dtResultInfo.Rows[0][y] =Math.Round(marks,2);
                                            dtResultInfo.Rows[0][y + 1] =GName = dtG.Rows[b]["GName"].ToString();


                                            //______Find Optional Subject For student of calss nine and upper class____

                                            if (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9)
                                            {
                                                if (dtOpSubInfo.Rows[0]["SubId"].ToString().Equals(dtOldCertainSubject.Rows[0]["SubId"].ToString()))
                                                {
                                                    isOptionalSub = true;
                                                }
                                            }
                                            //_____End__________________________________________________________________

                                            if ("F" == dtG.Rows[b]["GName"].ToString())
                                            {
                                                if (!isOptionalSub)    // for check it's isoptional subject ?
                                                {
                                                    FailStatus = true;
                                                    TFSA++;
                                                }
                                            }

                                            else
                                            {
                                                TotalMarks += Math.Floor(marks);
                                                if (isOptionalSub)
                                                {
                                                    TotalGPA += (float.Parse(dtG.Rows[b]["GPointMin"].ToString()) - 2);
                                                    Gpoint = float.Parse(dtG.Rows[b]["GPointMin"].ToString()) - 2;
                                                }
                                                else TotalGPA += float.Parse(dtG.Rows[b]["GPointMin"].ToString());
                                            }
                                            dtResultInfo.Rows[0][y + 2] = dtG.Rows[b]["GPointMin"].ToString();
                                            Gpoint = float.Parse(dtG.Rows[b]["GPointMin"].ToString());

                                            sr = (y + 3);
                                            status = true; break;

                                        }
                                    }

                                }

                                if (status) break;
                            }
   
                        }
 
                        if (status) break;
                    }
                    //________________________________________________________________________________End_____________________________________________________________________________________________________________________________ 

                    //_______________________Enterd Marks For Final result log__________________________________

                    dtFinalResultLog.Rows.Add(ddlExamId.SelectedItem.Text.Trim(), StudentId,dtgetOrderdSubject.Rows[m]["SubId"].ToString());

                    if (dtDependencyMarks.Rows.Count > 0)
                    {
                        for (byte c = 0; c < dtDependency.Rows.Count; c++)  // for get main subject row index whose will be update
                        {
                            if ((dtgetOrderdSubject.Rows[m]["SubId"].ToString().Equals(dtDependency.Rows[c]["SubId"].ToString())) || (dtgetOrderdSubject.Rows[m]["SubId"].ToString().Equals(dtDependency.Rows[c]["DependencySubId"].ToString())))
                            {
                                dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["DependencySubId"] = dtDependency.Rows[c]["DependencySubId"].ToString();
                                dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["Marks"] = Math.Round(marks);
                                dtDependencyMarks.Rows.Clear();
                            }
                        }
                    }

                    else
                    {
                        string IsOpotional_Or_dependency=" ";
                        if (dtOpSubInfo.Rows.Count>0)
                        {
                            if (dtOpSubInfo.Rows[0]["SubId"].ToString().Equals(dtgetOrderdSubject.Rows[m]["SubId"].ToString()))
                                IsOpotional_Or_dependency = "IsOptional";

                        }
                        
                        dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["DependencySubId"] = IsOpotional_Or_dependency;
                        dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["Marks"] = Math.Round(marks);
                        dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["SGrade"] = GName;
                        dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["SPoint"] = Gpoint;
                        

                    }
                    //________________________________________________End_____________________________________________________________________

                }

                if (dtgetOrderdSubject.Rows.Count>0)
                {
                    //_________________________________________Start Calculation For Find Out Final Grade & Point______________________________________________________________________________________________________________________

                    if (!FailStatus)
                    {
                        if (isOptionalSub) dtResultInfo.Rows[0]["GPA"] = Math.Round((TotalGPA / (dtgetOrderdSubject.Rows.Count - 1)), 2);
                        else dtResultInfo.Rows[0]["GPA"] = Math.Round((TotalGPA / dtgetOrderdSubject.Rows.Count), 2);
                    }
                    else
                    {
                        if (isOptionalSub) dtResultInfo.Rows[0]["GPA"] = Math.Round((TotalGPA / (dtgetOrderdSubject.Rows.Count - TFSA-1)), 2);    //TFSA= Total Fail Student Amount
                        else dtResultInfo.Rows[0]["GPA"] = Math.Round((TotalGPA / dtgetOrderdSubject.Rows.Count - TFSA), 2);
                        

                    }
                    for (byte b = 0; b < dtG.Rows.Count; b++)
                    {
                        if (float.Parse(dtResultInfo.Rows[0]["GPA"].ToString()) >= float.Parse(dtG.Rows[b]["GPointMin"].ToString()) && float.Parse(dtResultInfo.Rows[0]["GPA"].ToString()) <= float.Parse(dtG.Rows[b]["GPointMax"].ToString()))
                        {
                            dtResultInfo.Rows[0]["Grade"] = dtG.Rows[b]["GName"].ToString();
                            dtResultInfo.Rows[0]["TotalMarks"] =Math.Round(TotalMarks,2);
                            break;
                        }
                        else if ((double.Parse(dtResultInfo.Rows[0]["GPA"].ToString()))<0 || FailStatus)
                        {
                            dtResultInfo.Rows[0]["Grade"] = "F";
                            dtResultInfo.Rows[0]["TotalMarks"] =Math.Round(TotalMarks,2);
                            break;
                        }

                    }
                    
                    //_____________________________________________________________End_________________________________________________________________________________________________________________________________________________
               


                    //____________________________________________________________________Now enter every student result in database table_____________________________________________________________________________________________

                    byte cell=0;
                    if (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9) getTable = getTable + "_" + ddlSectionName.SelectedItem.Text.Trim().Replace(" ", "") + "TotalResultProcess";
                    else getTable = getTable + "_TotalResultProcess";
                    while(cell < dtResultInfo.Columns.Count-1)
                    {
                        sr = 0;
                        try
                        {
                            if (cell == 0)
                            {
                                cmd = new SqlCommand("insert into  " + getTable + " (ExInId,StudentId,RollNo,BatchName,SectionName,Shift,PublistDate) values ('" 
                                + ddlExamId.SelectedItem.Text.Trim() + "'," + StudentId + "," + rollNo + ",'" + ddlBatch.SelectedItem.Text.Trim() + "','" 
                                + ddlSectionName.SelectedItem.Text + "','" + ddlShift.SelectedItem.Text + "','" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "')", sqlDB.connection);
                                cmd.ExecuteNonQuery(); cell +=6;
                            }

                            else
                            {
                                string value = dtResultInfo.Rows[0].ItemArray[cell].ToString();
                                cmd = new SqlCommand("update " + getTable + " set " + dtResultInfo.Columns[cell].ToString() + "='" + dtResultInfo.Rows[0].ItemArray[cell].
                                ToString() + "' where ExInId='" + ddlExamId.SelectedItem.Text.Trim() + "' AND StudentId=" + StudentId + "", sqlDB.connection);
                                cmd.ExecuteNonQuery(); cell++;
                            }
                        }
                        catch (Exception ex)
                        {
                           
                        }
                        
                    }

                  //  DataTable dtFinalResultProcess=dtFinalResultLog.Select("").CopyToDataTable();
                 //   dtFinalResultLog.Rows.Clear();
                    try
                    {
                        for (byte b = 0; b < dtDependency.Rows.Count; b++)
                        {
                            DataTable dtfrp = dtFinalResultLog.Select("DependencySubId ='" + dtDependency.Rows[b]["DependencySubId"].ToString() + "'").CopyToDataTable();
                            var getTotalDependencyMarks = dtfrp.Compute("sum (Marks)","");
                            getTotalDependencyMarks = float.Parse(getTotalDependencyMarks.ToString()) / dtfrp.Rows.Count;

                            for (byte g = 0; g < dtG.Rows.Count; g++)
                            {
                                if (Math.Floor(double.Parse(getTotalDependencyMarks.ToString())) >= double.Parse(dtG.Rows[g]["GMarkMin"].ToString()) && Math.Floor(double.Parse(getTotalDependencyMarks.ToString())) <= double.Parse(dtG.Rows[g]["GMarkMax"].ToString()))
                                {
                                    dtfrp.Rows[0]["SGrade"] = dtG.Rows[g]["GName"].ToString();
                                    dtfrp.Rows[0]["SPoint"] = dtG.Rows[g]["GPointMin"].ToString();
                                    break;
                                }
                            }

                            
                            DataRow[] dr = dtFinalResultLog.Select(" DependencySubId ='" + dtfrp.Rows[0]["DependencySubId"].ToString() + "'") ;

                            for (byte r = 0; r < dr.Length; r++)
                            {
                                dtFinalResultLog.Rows.Remove(dr[r]);
                                if (r == dtfrp.Rows.Count - 1) dtFinalResultLog.Rows.Add(ddlExamId.SelectedItem.Text.Trim(), StudentId, dtfrp.Rows[0]["DependencySubId"].ToString(), " ", getTotalDependencyMarks, dtfrp.Rows[0]["SGrade"].ToString(), dtfrp.Rows[0]["SPoint"].ToString());
                            }
                        }

                        //_________________________Final Result Log Calcultation Is Start________________________________________________________________

                        dtFinalResultLog = dtFinalResultLog.Select("", "SubId asc").CopyToDataTable();   // for ordering subject id wise

                        var getTotalPoint = dtFinalResultLog.Compute("sum (SPoint)", "");
                        var getTotalMarks = dtFinalResultLog.Compute("sum (Marks)","");

                        DataRow [] drIspotionalPoint = dtFinalResultLog.Select(" DependencySubId ='IsOptional'");

                        DataRow[] drFailSubject = dtFinalResultLog.Select("SGrade ='F'");

                        if (drIspotionalPoint.Length > 0)
                        {
                            getTotalPoint = float.Parse(getTotalPoint.ToString()) - float.Parse(drIspotionalPoint[0]["SPoint"].ToString());
                            float isOptionalPoint=float.Parse(drIspotionalPoint[0]["SPoint"].ToString())-2;
                            {
                                if (isOptionalPoint > 0) getTotalPoint = float.Parse(getTotalPoint.ToString()) + isOptionalPoint;
                            }
                        }

                        getTotalPoint = Math.Round((float.Parse(getTotalPoint.ToString()) / (dtFinalResultLog.Rows.Count - (drFailSubject.Length+drIspotionalPoint.Length))), 2);
                        
                        dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["GPA"] = getTotalPoint;
                       

                        if (drFailSubject.Length == 0)
                        {
                            for (byte g = 0; g < dtG.Rows.Count; g++)
                            {

                                if ((double.Parse(getTotalPoint.ToString()) >= double.Parse(dtG.Rows[g]["GPointMin"].ToString()) && (double.Parse(getTotalPoint.ToString())) <= double.Parse(dtG.Rows[g]["GPointMax"].ToString())))
                                {
                                    dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["Grade"] = dtG.Rows[g]["GName"].ToString();
                                    break;
                                }
                            }
                        }
                        else dtFinalResultLog.Rows[dtFinalResultLog.Rows.Count - 1]["Grade"] = "F";
                    }
                    catch { }
                    //   ___________________________________________Final Result Calculation Is End________________________________________________________

                    //____________________________________________Final Result Log Data Enter In Database _____________________________________________________
                    for (byte b = 0; b < dtFinalResultLog.Rows.Count; b++)
                    {
                        try
                        {

                            string getTotalMarks = (b == dtFinalResultLog.Rows.Count - 1) ? TotalMarks.ToString() : " ";
                            string[] getColumns = { "ExInId", "StudentId", "SubId", "Marks", "SGrade", "SPoint", "GPA", "Grade", "TotalMarks","RollNo","Shift", "SectionName", "BatchName" };
                            string[] getValues = { ddlExamId.SelectedItem.Text.Trim(), StudentId.ToString(), dtFinalResultLog.Rows[b]["SubId"].ToString(), dtFinalResultLog.Rows[b]["Marks"].ToString(), dtFinalResultLog.Rows[b]["SGrade"].ToString(), dtFinalResultLog.Rows[b]["SPoint"].ToString(), dtFinalResultLog.Rows[b]["GPA"].ToString(), dtFinalResultLog.Rows[b]["Grade"].ToString(),TotalMarks.ToString(),rollNo.ToString(),ddlShift.SelectedItem.Text.Trim(),ddlSectionName.SelectedItem.Text.Trim(),ddlBatch.SelectedItem.Text.Trim() };
                            SQLOperation.forSaveValue("FinalResultLog", getColumns, getValues, sqlDB.connection);
                            
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        } 
                    }

                    dtResultInfo.Rows.Clear();
                    dtFinalResultLog.Rows.Clear();
                    //___________________________________________________________________________End___________________________________________________________________________________________________________________________________
                }
   
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        private void createFinalMarkSheet(DataTable dtSubInfo,string getTableName)    // generate table for store all processing result by clase wise
        {
            try
            {
                string getGrouopName = (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9) ? ddlSectionName.SelectedItem.Text.Trim() : " ";
                string generateFields = "create table " + getTableName + "_"+getGrouopName.Replace(" ","")+"TotalResultProcess (Sl bigint identity primary key,ExInId varchar(50) foreign key references ExamInfo(ExInId), StudentId bigint foreign key references StudentProfile(StudentId),RollNo bigint ,BatchName varchar(20),SectionName varchar(10),Shift varchar(7),";

                DataTable dtgetSubUId = dtSubInfo.DefaultView.ToTable(true, "SubId");

                for (byte b = 0; b < dtgetSubUId.Rows.Count; b++)
                {

                    DataTable dtPairSubPattern = dtSubInfo.Select("SubId=" + dtgetSubUId.Rows[b]["SubId"].ToString() + "").CopyToDataTable();
                    for (byte c = 0; c <dtPairSubPattern.Rows.Count; c++)
                    {
                        if (c == dtPairSubPattern.Rows.Count - 1) generateFields += dtPairSubPattern.Rows[c]["SubName"].ToString().Replace(" ", "") + "_" 
                        + dtPairSubPattern.Rows[c]["QPName"].ToString().Replace(" ", "") + " float," + dtPairSubPattern.Rows[c]["SubName"].ToString().Replace(" ", "") 
                        + "_TMarks float," + dtPairSubPattern.Rows[c]["SubName"].ToString().Replace(" ", "") + "_Grade varchar(2)," + dtPairSubPattern.Rows[c]["SubName"].ToString().Replace(" ", "") + "_Point float,";
                        else generateFields += dtPairSubPattern.Rows[c]["SubName"].ToString().Replace(" ","") + "_" + dtPairSubPattern.Rows[c]["QPName"].ToString().Replace(" ","") +" float,"; 
 
                    }

                    if (b == dtgetSubUId.Rows.Count - 1)
                    {
                        generateFields += "GPA float,Grade varchar(2),TotalMarks float,PublistDate date)";
                        cmd = new SqlCommand(generateFields, sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        private void displayTotalFinalResult(string getTable) //p
        {
            try
            {
                DataTable dtTRP = new DataTable();
                
                if (byte.Parse(ViewState["__getClassOrder__"].ToString()) >= 9) getTable = getTable + "_" + ddlSectionName.SelectedItem.Text.Trim().Replace(" ", "") 
                + "TotalResultProcess";
                else getTable = getTable + "_TotalResultProcess";

                string getCmd = "SELECT StudentProfile.FullName," + getTable + ".* FROM StudentProfile INNER JOIN " + getTable + " ON StudentProfile.StudentId = " 
                + getTable + ".StudentId";
                sqlDB.fillDataTable(getCmd, dtTRP); //

                if (dtTRP.Rows.Count > 0)
                {
                    dtTRP = dtTRP.Select("ExInId='" + ddlExamId.SelectedItem.Text.Trim() + "'").CopyToDataTable();
                    dtTRP.Columns.Remove("Sl");
                    dtTRP.Columns.Remove("StudentId");
                    dtTRP.Columns.Remove("BatchName");
                    dtTRP.Columns.Remove("SectionName");
                    dtTRP.Columns.Remove("PublistDate");
                    dtTRP.Columns.Remove("ExInId");
                    dtTRP.Columns.Remove("Shift");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dtTRP.Rows.Count));
                    if (dtTRP.Rows.Count > 0)
                    {
                        string[] getExam = ddlExamId.SelectedItem.Text.Split('_');
                        lblClassTitle.Text = getExam[0] + "-" + TimeZoneBD.getCurrentTimeBD().Year.ToString();
                        lblShiftTitle.Text = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ") " 
                        + ddlShift.SelectedItem.Text + "-" + new String(ddlBatch.SelectedItem.Text.Where(Char.IsNumber).ToArray());
                        gvDisplayTotalFinalResult.DataSource = dtTRP;
                        gvDisplayTotalFinalResult.DataBind();
                        Session["__Status__"] = "FinalResult";
                        Session["__FinalResult__"] = dtTRP;
                        Session["__BatchInfo__"] ="Batch : "+ ddlBatch.Text + "(" + ddlSectionName.Text + ")";
                        getResult = true;

                        Session["__ClassTitle__"] = lblClassTitle.Text;
                        Session["__ShiftTitle__"] = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "(" + ddlSectionName.SelectedItem.Text + ") " 
                        + ddlShift.SelectedItem.Text ;
                    }

                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText ="Warning->"+ ex.Message;
            }
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkDependencyExam();
            }
            catch { }
        }

        private void checkDependencyExam()
        {
            try
            {
                sqlDB.fillDataTable("select ExInDependency from ExamInfo where ExInId='" + ddlExamId.SelectedItem.Text.Trim() + "' AND ExInDependency<>'0'", dt = new DataTable());
                if (dt.Rows.Count > 0) btnTotalResultProcess.Visible = true;
                else btnTotalResultProcess.Visible = false;
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/SubjectWiseMarksList.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch { }
        }       
 
    }
}