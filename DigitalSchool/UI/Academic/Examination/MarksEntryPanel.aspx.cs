using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.BLL.ManagedBatch;
using DS.BLL.Examinition;
using DS.PropertyEntities.Model.Examinition;
using DS.UI.Academic.Examination;
using DS.DAL;
using DS.BLL.ControlPanel;
using System.Text;
using DS.Classes;
using DS.BLL;

namespace DS.UI.Academic.Examination
{
    public partial class MarksEntryPanel : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        string sqlCmd = "";
        bool result;
        
        Class_ClasswiseMarksheet_TotalResultProcess_Entry class_classWiseMarkSheet_Entry;
        List<Class_ClasswiseMarksheet_TotalResultProcess> SubList = new List<Class_ClasswiseMarksheet_TotalResultProcess>();
        Exam_Final_Result_Stock_Of_All_Batch_Entry exam_Final_Result_Stock_Of_All_Batch_Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        //---url bind---
                        aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                        aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                        aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
                        //---url bind end---
                        Button btnPreviewMarksheet = new Button();
                        Button btnDetailsMarks = new Button();
                        if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "MarksEntry.aspx", btnSearch, chkForCoutAsFinalResult, btnPreviewMarksheet, btnDetailsMarks, btnPrintPreview)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        ShiftEntry.GetDropDownList(ddlShift);
                        BatchEntry.GetDropdownlist(ddlBatch, true);
                    }
                }
            }
            catch { }
        }
        string tblInfo = "";
        string tblHeader = "";
        private void loadMarksEntrySheet(string getMarkSheetName)
        {
            try
            {
                bool HasMultipleOptSub = false;
                bool HasSingleOptSub = false;
                string[] getExTypeId = ddlExamId.SelectedItem.Text.Split('_');
                string ExamId = ddlExamId.SelectedValue;
                DataTable dtExId = new DataTable();

                string[] SubID = ddlSubject.SelectedValue.Split('_');
                //-----------for title display---------------- 
                MarkSheetTitle.InnerText = "Marksheet entry point for batch " + ddlBatch.SelectedItem.Text + " section " + ddlSectionName.SelectedItem.Text.Trim() + " ("
                + ddlShift.SelectedItem.Text + ")";
                //--------------------------------------------

                //----------for get class name and marksheet name -------------------
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";
                //---------------------------------------------------------------------

                DataTable dt_GroupSubjectList = new DataTable();
                //----------for load all current student of this batch----------------- 
                if (ddlGroup.Enabled == false)
                {
                    SQLOperation.selectBySetCommandInDatatable("select distinct ee.StudentId,csi.RollNo from v_ExamExaminee ee inner join CurrentStudentInfo csi on ee.StudentID=csi.StudentId and ee.BatchID=csi.BatchID inner join " + getMarkSheetName + " ms on ee.StudentID=ms.StudentId and ee.ExamId=ms.ExamId  where ee.BatchId=" + ddlBatch.SelectedValue + " AND ee.ClsSecId=" + ddlSectionName.SelectedValue + " AND ConfigId=" + ddlShift.SelectedValue + " AND ee.ClsGrpId=" + ddlGroup.SelectedValue + " and ms.ExamId='" + ExamId + "' and ms.SubId=" + SubID[0] + " and ms.CourseId=" + SubID[1] + " order by csi.RollNo", dt = new DataTable(), DAL.DbConnection.Connection);

                    //sqlDB.fillDataTable("select StudentId,SubId from OptionalSubjectInfo where StudentId in (select StudentId from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    //+ ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " order by RollNo )", dt_GroupSubjectList);
                }
                else
                {
                    sqlCmd = "select distinct ee.StudentId,csi.RollNo from v_ExamExaminee ee inner join CurrentStudentInfo csi on ee.StudentID=csi.StudentId and ee.BatchID=csi.BatchID inner join " + getMarkSheetName + " ms on ee.StudentID=ms.StudentId and ee.ExamId=ms.ExamId  where ee.BatchId=" + ddlBatch.SelectedValue + " AND ee.ClsSecId=" + ddlSectionName.SelectedValue + " AND ConfigId=" + ddlShift.SelectedValue + " AND ee.ClsGrpId=" + ddlGroup.SelectedValue + " and ms.ExamId='" + ExamId + "' and ms.SubId=" + SubID[0] + " and ms.CourseId=" + SubID[1] + " order by csi.RollNo";
                    SQLOperation.selectBySetCommandInDatatable(sqlCmd, dt = new DataTable(), DAL.DbConnection.Connection);
                    
                }
                //---------------------------------------------------------------------

                //----------for load subject question pattern by batchId and exam type----------------------
                DataTable dtSQPInfo = new DataTable();   // SQP = subject question pattern information
                string subID = "";
                if (ddlSubject.SelectedValue != "0")
                {
                    string[] value = ddlSubject.SelectedValue.Split('_');
                    subID = " and SubId="+ value[0]+ " and CourseId="+value[1];
                }
                if (ddlGroup.Enabled == false)
                {
                    SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,SubId,CourseId,SubName,QPId,QPName,CourseName,IsOptional,QMarks from "
                + "v_SubjectQuestionPattern where BatchId='" + ddlBatch.SelectedItem.Value + "' AND ExId=(select ExId from ExamInfo where ExamId ='" + ExamId + "') "+ subID + " order by QPId", dtSQPInfo, DAL.DbConnection.Connection);
                }
                else
                {
                    SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,SubId,SubName,QPId,QPName,CourseName,IsOptional,QMarks,CourseId from v_SubjectQuestionPattern where BatchId=" + ddlBatch.SelectedItem.Value + " "
                + "AND ExId=(select ExId from ExamInfo where ExInSl ='" + ExamId + "') AND ClsGrpID='" + ddlGroup.SelectedItem.Value + "' " + subID + " order by QPId", dtSQPInfo, DAL.DbConnection.Connection);    // specially for nine ten and upper from ten
                }
                //-------------------------------------------------------------------------------------------

                //---------------------------class subject ordering synchronizing------------------------------------------
                System.Data.DataColumn Ordering = new DataColumn("Ordering", typeof(int));
                Ordering.DefaultValue = 0;
                dtSQPInfo.Columns.Add(Ordering);

                
                DataTable dtGroupId = new DataTable();
                dtGroupId=CRUD.ReturnTableNull("select GroupId from Tbl_Class_Group where ClsGrpId =" + ddlGroup.SelectedValue + "");
                

                string GRPId = (ddlGroup.Enabled == false) ? "0" : dtGroupId.Rows[0]["GroupId"].ToString();
                DataTable dtClassSubOrder = new DataTable();
                dtClassSubOrder = CRUD.ReturnTableNull("select SubId,CourseId,Ordering from ClassSubject where ClassID=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedValue + ") and GroupId in ('0','" + GRPId + "') order by Ordering  ");
                
                for (sbyte b = 0; b < dtSQPInfo.Rows.Count; b++)
                {
                    if (int.Parse(dtSQPInfo.Rows[b]["CourseId"].ToString()) == 0)
                    {
                        DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " ", null);
                        dtSQPInfo.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                    }
                    else
                    {
                        DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " AND CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString().Trim() + " ", null);
                        dtSQPInfo.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "and CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString() + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                    }

                }

                DataView dv = dtSQPInfo.DefaultView;
                dv.Sort = "Ordering asc";
                dtSQPInfo = dv.ToTable();
                //----------------------------------------------End Synchronizing---------------------------------------------

                //-----------for create table of marks entry point ------------------------------------------
                tblInfo = "<Table id=tblMarkEntryPoint class=table table-striped table-bordered dt-responsive nowrap cellspacing='0' width='100%'>";

                tblHeader += "<th style='width:70px;text-align:center;'>Roll No</th>";
                //tblHeader += "<th style='width:70px;text-align:center;'>Name</th>";

                //-------------------Bellow statements for header------------------------------------------------
                DataTable dt_TotalMarksheetForReport = new DataTable();
                DataTable dt_JustMarkSheetColumnsName = new DataTable();

                for (byte b = 0; b < dtSQPInfo.Rows.Count; b++)
                {
                    string subsName = "", subsName2 = "";
                    string[] SubJectName = dtSQPInfo.Rows[b]["SubName"].ToString().Trim().Split(' ');
                    for (sbyte s = 0; s < SubJectName.Length; s++)
                    {
                        subsName += SubJectName[s] + "</br>";
                        if (s == SubJectName.Length - 1) subsName2 += SubJectName[s];
                        else subsName2 += SubJectName[s] + Environment.NewLine;
                    }
                    subsName = subsName.Substring(0, subsName.LastIndexOf('<'));


                    string getCourseName = (dtSQPInfo.Rows[b]["CourseName"].ToString().Equals("Null")) ? "" : " " + dtSQPInfo.Rows[b]["CourseName"].ToString();
                    string IsOptional = (bool.Parse(dtSQPInfo.Rows[b]["IsOptional"].ToString()).Equals(true)) ? "" : "";
                    tblHeader += "<th style='border:2px solid gray;text-align: center;'>" + subsName + getCourseName + "</br>" + dtSQPInfo.Rows[b]["QPName"].ToString().Replace(" ", "") + "</br>" + IsOptional + "[" + dtSQPInfo.Rows[b]["QMarks"].ToString() + "] </th>";
                    if (b == dtSQPInfo.Rows.Count - 1) tblInfo += tblHeader;

                    if (b == 0)
                    {
                        dt_TotalMarksheetForReport.Columns.Add("RollNo", typeof(string));
                        dt_JustMarkSheetColumnsName.Columns.Add("RollNo", typeof(string));
                    }
                    dt_TotalMarksheetForReport.Columns.Add("DataColumn" + (b + 1).ToString(), typeof(string));
                    dt_JustMarkSheetColumnsName.Columns.Add(subsName2 + getCourseName + Environment.NewLine + dtSQPInfo.Rows[b]["QPName"].ToString().Replace(" ", ""), typeof(string));

                }

                Session["__JustColumnsName__"] = dt_JustMarkSheetColumnsName;
                //--------------------------------------------------------------------------------------------------

                tblInfo += "<tr>";
                DataTable dtCreateViewForClassMarksheet = new DataTable();

                string StudentIdList = "";
                foreach (DataRow row in dt.Rows)
                    StudentIdList += "," + row["StudentId"].ToString();
                if (StudentIdList.Length>0)
                    StudentIdList = StudentIdList.Remove(0, 1);

                dtCreateViewForClassMarksheet = CRUD.ReturnTableNull("with ms as(select * from  " + getTable + " where ExamId='" + ExamId + "' and StudentId in(" + StudentIdList + ") ) "
                + "SELECT Distinct ms.ExamId,ms.StudentId,CurrentStudentInfo.FullName,ms.RollNo,ms.BatchId,ms.ClsSecId,ms.ShiftId,"
                + "ms.SubQPId,SubjectQuestionPattern.SubId,SubjectQuestionPattern.CourseId,SubjectQuestionPattern.IsOptional,v_ClassSujectListForResultProcess.SubName,"
                + " QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks,ms.ConvertToPercentage,ms.Marks,"
                + "ms.ClsGrpID,SubjectQuestionPattern.SubQPMarks,SubjectQuestionPattern.PassMarks,v_ClassSujectListForResultProcess.IsOptional,v_ClassSujectListForResultProcess.BothType,v_ClassSujectListForResultProcess.IsCommon"
                + " FROM CurrentStudentInfo INNER JOIN ms ON CurrentStudentInfo.StudentId = ms.StudentId INNER JOIN ExamInfo ON "
                + "ms.ExamId = ExamInfo.ExInSl INNER JOIN SubjectQuestionPattern ON ms.SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN v_ClassSujectListForResultProcess "
                + "ON SubjectQuestionPattern.SubId = v_ClassSujectListForResultProcess.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId AND "
                + " v_ClassSujectListForResultProcess.ClassId=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedItem.Value + ") Order By "
                + "ms.RollNo ASC,QuestionPattern.QPId  ");

                //----------------------------For Ordering Synchronizing------------------------------------------------------------------
                Ordering = new DataColumn("Ordering", typeof(int));
                Ordering.DefaultValue = 0;
                dtCreateViewForClassMarksheet.Columns.Add(Ordering);


                //for (int  b = 0; b < dtSQPInfo.Rows.Count; b++)
                //{
                //    DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " AND CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString().Trim() + " ", null);

                //    dtCreateViewForClassMarksheet.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "and CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString() + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                //}
                for (int b = 0; b < dtSQPInfo.Rows.Count; b++)
                {
                    if (int.Parse(dtSQPInfo.Rows[b]["CourseId"].ToString()) == 0)
                    {
                        DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " ", null);
                        dtCreateViewForClassMarksheet.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                    }
                    else
                    {
                        DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " AND CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString().Trim() + " ", null);
                        dtCreateViewForClassMarksheet.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "and CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString() + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                    }
                }
                dv = dtCreateViewForClassMarksheet.DefaultView;
                dv.Sort = "Ordering asc";
                dtCreateViewForClassMarksheet = dv.ToTable();

                ViewState["__getMarkSheetView__"] = dtCreateViewForClassMarksheet;
                //-------------------------------------------- End Ordering Synchronizing ---------------------------------------------------------------------



                for (int i = 0; i < dt.Rows.Count; i++)   // all student included in dt 
                {
                    try {
                        string a = "";
                    ViewState["__StudentID__"]= a = dt.Rows[i]["StudentId"].ToString();
                    if (dt.Rows[i]["StudentId"].ToString() == "164346")
                    {

                    }
                    int row = i + 1;

                    DataTable dtSubInfo = new DataTable();
                    try {
                        dtSubInfo = dtCreateViewForClassMarksheet.Select("StudentId=" + dt.Rows[i]["StudentId"].ToString() + " AND ExamId='" + ExamId + "' " + subID + " ").CopyToDataTable();
                    }
                    catch (Exception ex) { }
                    

                    dt_GroupSubjectList = new DataTable();
                    dt_GroupSubjectList = CRUD.ReturnTableNull("select SubId,MSStatus from v_StudentGroupSubSetupDetails where StudentId='" + dt.Rows[i]["StudentId"].ToString() + "' AND BatchId='" + ddlBatch.SelectedItem.Value + "'");
                    string SubIdRange = " in (";
                    for (int j = 0; j < dtSubInfo.Rows.Count; j++)
                    {
                        //------------------ if Any optional suject are exists then execute this block-----------------------------------------------
                        if (dt_GroupSubjectList.Rows.Count > 0)
                        {
                            //---------------------------For Just add common subject without others type of subject----------------------------------------------------------------------------------------------
                            if ((dtSubInfo.Rows[j]["IsCommon"].ToString().Equals("True") && dtSubInfo.Rows[j]["IsOptional"].ToString().Equals("False") && dtSubInfo.Rows[j]["BothType"].ToString().Equals("False")))
                            {

                                if (j == 0)
                                {
                                    tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input  autocomplete='off' onchange='saveData(this)' "
                                + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":"
                                + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                + "></td>";

                                    dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                }
                                else
                                {
                                    tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";
                                    dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                }
                                row += dt.Rows.Count;
                            }
                            //---------------------------------------------------------------------------------------------------------------------
                            else
                            {
                                //---------------when any subject are not exists then it will be return null.that means this subject is not occopyed for this student


                                DataRow[] dr = dt_GroupSubjectList.Select("subId=" + dtSubInfo.Rows[j]["SubId"].ToString() + "");

                                if (dr.Length > 0)
                                {
                                    if (j == 0)
                                    {
                                        tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input autocomplete='off' onchange='saveData(this)' "
                                    + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":"
                                    + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                    + "></td>";
                                        dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                    }
                                    else
                                    {
                                        tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                    + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                    ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";

                                        dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                    }
                                    row += dt.Rows.Count;
                                    HasSingleOptSub = true;
                                }
                                else
                                {
                                    if (j == 0)
                                    {
                                        tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input disabled='disabled' autocomplete='off' onchange='saveData(this)' "
                                    + "type='text' style='text-align: center; background-color:#A6A6A6; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":"
                                    + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                    + "></td>";

                                        dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                    }
                                    else
                                    {
                                        tblInfo += "<td><input disabled='disabled' onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                    + "type=text style='text-align: center;background-color:#A6A6A6; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                    ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";

                                        SubIdRange += dtSubInfo.Rows[j]["SubId"].ToString() + ",";
                                        dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                    }
                                    row += dt.Rows.Count;
                                    HasMultipleOptSub = true;
                                }
                            }
                        }
                        //----------------------------------------------------End Blocks--------------------------------------------------------------
                        else
                        {
                            if (j == 0)
                            {
                                tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input  autocomplete='off' onchange='saveData(this)' "
                               + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":"
                               + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                               + "></td>";
                                dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                            }
                            else
                            {
                                tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                            + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExamId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                            ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";


                                dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                            }

                            row += dt.Rows.Count;
                        }

                        if (j == dtSubInfo.Rows.Count - 1) tblInfo += "</tr>";
                    }


                    if (dt_GroupSubjectList.Rows.Count > 0)
                    {
                        if (HasSingleOptSub && HasMultipleOptSub)
                        {
                            try
                            {
                                SubIdRange = SubIdRange.Substring(0, SubIdRange.LastIndexOf(',')) + ")";
                                cmd = new SqlCommand("update " + getTable + " set Marks='0' where ExamId='" + ddlExamId.SelectedItem.Text + "' AND StudentId=" + dt.Rows[i]["StudentId"].ToString() + " AND SubId " + SubIdRange + "", DbConnection.Connection);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex) { }
                            
                        }
                    }
                    }
                    catch (Exception ex) { string a = ViewState["__StudentID__"].ToString(); }
                }
                tblInfo += "</table>";
                divMarksheet.Controls.Add(new LiteralControl(tblInfo));

                Session["__MarkSheet__"] = dt_TotalMarksheetForReport;
                string[] ExId = ddlExamId.SelectedItem.Text.Split('_');
                string[] Year = ExId[1].Split('-');
                Session["__lblExamName__"] = ExamId[0] + "-" + Year[2];
                Session["__lblShift__"] = ddlShift.SelectedItem.Text;
                Session["__lblBatch__"] = ddlBatch.SelectedItem.Text;
                Session["__lblSection__"] = ddlSectionName.SelectedItem.Text;
                //btnPrintPreview.Visible = true;
                //------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                string a = ViewState["__StudentID__"].ToString();
                Session["__MarkSheet__"] = ""; }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //---- check new entry student and insert to result sheet
            string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            string getMarkSheetName = "Class_" + getClass + "MarksSheet";
            EnterdEssentialStudentInfo(getMarkSheetName);
            //------------------------------------------------------
            MarkSheetTitle.Visible = true;
            divMarksheet.Visible = true;
            //Panel1.Visible = true;
            loadMarksEntrySheet(getMarkSheetName);
            chkForCoutAsFinalResult.Checked = false;
        }
       
      
    
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSectionName.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlGroup, ddlBatch.SelectedValue.ToString());

            if (ddlGroup.Items.Count == 1)
            {
                ddlGroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId(ddlSectionName, ddlBatch.SelectedValue.ToString());
                ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString());
            }
            else
            {
                ddlGroup.Enabled = true;
            }

            //ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString(),true);
            

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSectionName, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
            ExamInfoEntry.GetExamIdListWithExInSl(ddlExamId, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
        }

       

        protected void btnDetailsMarks_Click(object sender, EventArgs e)
        {
            //Panel1.Visible = true;
            createDetailsMarkList();
        }

        private void createDetailsMarkList()
        {

            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (class_classWiseMarkSheet_Entry == null)
                {
                    class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                dt = class_classWiseMarkSheet_Entry.GetExamResultDetails(getClass, ddlShift.SelectedValue,
                    ddlBatch.SelectedValue, ddlGroup.SelectedValue, ddlSectionName.SelectedValue,
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



      

        public Class_ClasswiseMarksheet_TotalResultProcess GetData(bool HasDependency, int studentId, int RollNo, int SubId, bool IsPotional, int CourseId, int QPId, float Marks, float MarksForSubject, float MarksForOnlyDependencySubQPattern, int NoOfCourse, float DependencySubPassMarks)
        {
            Class_ClasswiseMarksheet_TotalResultProcess c_ctmp = new Class_ClasswiseMarksheet_TotalResultProcess();

            c_ctmp.ExamId = ddlExamId.SelectedItem.Value;
            c_ctmp.StudentId = studentId;
            c_ctmp.RollNo = RollNo;
            c_ctmp.BatchId = int.Parse(ddlBatch.SelectedItem.Value);
            c_ctmp.ClsSecId = int.Parse(ddlSectionName.SelectedItem.Value);
            c_ctmp.ShiftId = int.Parse(ddlShift.SelectedItem.Value);
            c_ctmp.SubId = SubId;
            c_ctmp.IsOptional = IsPotional;
            c_ctmp.CourseId = CourseId;
            c_ctmp.QPId = QPId;
            c_ctmp.Marks = Marks;
            c_ctmp.ClsGrpID = (ddlGroup.Enabled) ? int.Parse(ddlGroup.SelectedItem.Value) : 0;

            // here hasDependancy Means Dependency Subject such as bangla 1st+2nd paper
            if (HasDependency)
            {

                string[] GP = Grade_And_Point(HasDependency, MarksForOnlyDependencySubQPattern, false, 0, 0).Split('|');  // GP= Grade And Point
                c_ctmp.MarksOfAllPatternBySCId = MarksForOnlyDependencySubQPattern;
                c_ctmp.GradeOfAllPatternBySCId = GP[0];
                c_ctmp.PointOfAllPatternBySCId = float.Parse(GP[1]);

                GP = Grade_And_Point(HasDependency, MarksForSubject, true, NoOfCourse, DependencySubPassMarks).Split('|');  // GP= Grade And Point .when any dependency subject are exists then it will be work

                c_ctmp.MarksOfSubject_WithAllDependencySub = MarksForSubject;
                c_ctmp.GradeOfSubject_WithAllDependencySub = GP[0];
                c_ctmp.PointOfSubject_WithAllDependencySub = float.Parse(GP[1]);
            }
            else
            {
                string[] GP = Grade_And_Point(HasDependency, MarksForSubject, false, 0, 0).Split('|');  // GP= Grade And Point
                c_ctmp.MarksOfAllPatternBySCId = MarksForSubject;
                c_ctmp.GradeOfAllPatternBySCId = GP[0];
                c_ctmp.PointOfAllPatternBySCId = float.Parse(GP[1]);

                c_ctmp.MarksOfSubject_WithAllDependencySub = MarksForSubject;
                c_ctmp.GradeOfSubject_WithAllDependencySub = GP[0];
                c_ctmp.PointOfSubject_WithAllDependencySub = float.Parse(GP[1]);

            }

            return c_ctmp;
        }

        public string Grade_And_Point(bool HasDependency, float marks, bool Marks_Grade_For_DependencySubject, int NoOfCourse, float DependencySubPassMarks)
        {
            try
            {
                // convert every marks in hundred percentage
                double PercentageMarks = 0;
                bool Has_SubjectDependency = false;
                //----Frist Time Check of the dependency subject marks for count pass or fale-------------
                if (Marks_Grade_For_DependencySubject)
                {
                    if ((marks * NoOfCourse) < DependencySubPassMarks)
                    {
                        return "F" + "|" + "0.00";

                    }
                    //  ViewState["__SubQPMarks__"] = ViewState["__DpendencySubQPMarks__"].ToString();
                    marks *= NoOfCourse;
                    //  Has_SubjectDependency = true;

                    marks = (int)marks / NoOfCourse;
                    Has_SubjectDependency = true;
                }
                //-------------------------------------------------------------------------------------------

                if (ViewState["__ExId__"] != null)  //  if any dependency exam are exists then must be this section will be execute
                {
                    if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";

                    PercentageMarks = Math.Round(marks * 100 / double.Parse(ViewState["__SubQPMarks__"].ToString()), 0);
                }
                else if (Has_SubjectDependency)  // if any dependency exam are are exists and sub has dependency then this section will be execute
                {
                    if ((marks * NoOfCourse) < DependencySubPassMarks)
                    {
                        if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";
                    }

                    PercentageMarks = Math.Round(marks * NoOfCourse * 100 / (double.Parse(ViewState["__SubQPMarks__"].ToString()) * NoOfCourse), 0);
                }
                else
                {
                    if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";

                    PercentageMarks = Math.Round(marks * 100 / double.Parse(ViewState["__SubQPMarks__"].ToString()), 0);
                }


                for (byte b = 0; b < dtG.Rows.Count; b++)
                {
                    if (Math.Floor(PercentageMarks) >= double.Parse(dtG.Rows[b]["GMarkMin"].ToString()) && Math.Floor(PercentageMarks) <= double.Parse(dtG.Rows[b]["GMarkMax"].ToString()))
                    {
                        return dtG.Rows[b]["GName"].ToString() + "|" + dtG.Rows[b]["GPointMin"].ToString();

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return null;
            }
        }


        DataTable dtG;
        private void loadGrade()  // for load grade info 
        {
            // here load grade list -----------------------------------------------------
            dtG = new DataTable();
            sqlDB.fillDataTable("select * from  Grading ", dtG);
            //---------------------------------------------------------------------------

        }


        private void Check_And_DeleteExistingResultByExamId(string getTableName)
        {
            try
            {
                // for delete result from Class wise marksheet (Class_ClassMarksSheet_TotalResultProcess).table name structure
                string sql;
                if (ddlGroup.Enabled == false)
                    sql = "delete from " + getTableName + "_TotalResultProcess where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                        + ddlSectionName.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value + " AND ExamId='" + ddlExamId.SelectedItem.Text + "'";
                else
                    sql = "delete from " + getTableName + "_TotalResultProcess where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    + ddlSectionName.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value + " AND ExamId='" + ddlExamId.SelectedItem.Text + "' AND ClsGrpID=" + ddlGroup.SelectedItem.Value + "";

                CRUD.ExecuteQuery(sql);

                int ClsGrpID = (ddlGroup.Enabled) ? int.Parse(ddlGroup.SelectedItem.Value) : 0;
                sql = "delete from Exam_Final_Result_Stock_Of_All_Batch where ExamId='" + ddlExamId.SelectedItem.Text + "' AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value.ToString() + " AND ClsSecId=" + ddlSectionName.SelectedItem.Value + " AND ClsGrpID=" + ClsGrpID + "";
                CRUD.ExecuteQuery(sql);




            }
            catch { }

        }

        private void createFinalResultSheet(string studentId, string getTableName, bool HasOptional)    // generate table for store all processing result by clase wise
        {
            try
            {

                // load all subject marks of selected student
                DataTable dtTotalMarkSheet = new DataTable();
                sqlDB.fillDataTable("select distinct SubId,MarksOfSubject_WithAllDependencySub,PointOfSubject_WithAllDependencySub,IsOptional  from " + getTableName + "_TotalResultProcess where StudentId=" + studentId + " AND ExamId='" + ddlExamId.SelectedItem.ToString() + "' AND BatchId=" + ddlBatch.SelectedItem.Value + "", dtTotalMarkSheet);

                DataTable GetFailSubjectList = new DataTable();
                try
                {
                    GetFailSubjectList = dtTotalMarkSheet.Select("PointOfSubject_WithAllDependencySub=0 AND IsOptional='False'").CopyToDataTable();
                }
                catch { }
                object getTotalMarks_WithOptionalSub = 0;
                object getTotalMarks_WithOut_OptionalSub = 0;
                string getTotalPoint_WithOptionalSub = "0.00";
                string getGrade_WithOptionalSub = "F";
                string getTotalPoint_WithOut_OptionalSub = "0.00";
                string getGrade_WithOut_OptionalSub = "F";
                object getOptionSubjetMarks = 0;

                if (GetFailSubjectList.Rows.Count == 0)
                {
                    object GetOptionalPoint;
                    if (HasOptional)
                    {
                        GetOptionalPoint = dtTotalMarkSheet.Compute("sum(PointOfSubject_WithAllDependencySub)", "IsOptional='true'");
                        if (float.Parse(GetOptionalPoint.ToString()) >= 2)
                        {
                            GetOptionalPoint = float.Parse(GetOptionalPoint.ToString()) - 2;
                            getOptionSubjetMarks = dtTotalMarkSheet.Compute("sum(MarksOfSubject_WithAllDependencySub)", "IsOptional='true'");
                            getOptionSubjetMarks = float.Parse(getOptionSubjetMarks.ToString()) - 40;
                        }
                        else GetOptionalPoint = 0;
                    }
                    else GetOptionalPoint = 0;

                    object GetTotalPointsOfExam = dtTotalMarkSheet.Compute("sum(PointOfSubject_WithAllDependencySub)", "IsOptional='false'");

                    DataRow[] dr = null;
                    dr = dtTotalMarkSheet.Select("IsOptional='false'");



                    // if has optional subject and optionalSubPoint not equqal 0 then execute this if block
                    if (HasOptional && float.Parse(GetOptionalPoint.ToString()) != 0)
                    {
                        getTotalPoint_WithOptionalSub = Math.Round((float.Parse(GetTotalPointsOfExam.ToString()) + float.Parse(GetOptionalPoint.ToString())) / dr.Length, 2).ToString();

                        if (double.Parse(getTotalPoint_WithOptionalSub) > double.Parse(dtG.Rows[0]["GPointMax"].ToString())) getTotalPoint_WithOptionalSub = dtG.Rows[0]["GPointMax"].ToString();

                        getGrade_WithOptionalSub = GetFinalResultGrade(float.Parse(getTotalPoint_WithOptionalSub));   // to get result grade for with optional subject
                    }

                    // for get total point and grade for regular subject 
                    getTotalPoint_WithOut_OptionalSub = Math.Round((float.Parse(GetTotalPointsOfExam.ToString())) / dr.Length, 2).ToString();

                    if (double.Parse(getTotalPoint_WithOut_OptionalSub) > double.Parse(dtG.Rows[0]["GPointMax"].ToString())) getTotalPoint_WithOut_OptionalSub = dtG.Rows[0]["GPointMax"].ToString();

                    getGrade_WithOut_OptionalSub = GetFinalResultGrade(float.Parse(getTotalPoint_WithOut_OptionalSub));  // to get result grade for without optional subject


                    // for get total marks of exam with optional subject and without optional subject-------
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable("select  distinct SubId,CourseId,MarksOfAllPatternBySCId,IsOptional from " + getTableName + "_TotalResultProcess where StudentId=" + studentId + " AND ExamId='" + ddlExamId.SelectedItem.ToString() + "' AND BatchId=" + ddlBatch.SelectedItem.Value + "", dt);



                    // if (float.Parse(GetOptionalPoint.ToString()) != 0) getTotalMarks_WithOptionalSub = dt.Compute("sum (MarksOfAllPatternBySCId)", null);
                    //  getTotalMarks_WithOptionalSub =float.Parse(getTotalMarks_WithOptionalSub.ToString())- float.Parse(getOptionSubjetMarks.ToString());

                    getTotalMarks_WithOut_OptionalSub = dt.Compute("sum (MarksOfAllPatternBySCId)", "IsOptional='False'");

                    getTotalMarks_WithOptionalSub = float.Parse(getTotalMarks_WithOut_OptionalSub.ToString()) + float.Parse(getOptionSubjetMarks.ToString());

                    //----------------------------------------------------------------------------------------
                }

                using (Exam_Final_Result_Stock_Of_All_Batch e_frsab = GetData(studentId, float.Parse(getTotalPoint_WithOptionalSub), getGrade_WithOptionalSub,
                     float.Parse(getTotalMarks_WithOptionalSub.ToString()), float.Parse(getTotalPoint_WithOut_OptionalSub), getGrade_WithOut_OptionalSub,
                     float.Parse(getTotalMarks_WithOut_OptionalSub.ToString())))
                {
                    if (exam_Final_Result_Stock_Of_All_Batch_Entry == null) exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                    exam_Final_Result_Stock_Of_All_Batch_Entry.SetValues = e_frsab;
                    exam_Final_Result_Stock_Of_All_Batch_Entry.Insert();

                }
            }
            catch { }
        }

        private Exam_Final_Result_Stock_Of_All_Batch GetData(string studentId, float FinalGPA_OfExam_WithOptionalSub,
            string FinalGrade_OfExam_WithOptionalSub, float FinalTotalMarks_OfExam_WithOptionalSub,
            float FinalGPA_OfExam, string FinalGrad_OfExam, float FinalTotalMarks_OfExam)
        {
            Exam_Final_Result_Stock_Of_All_Batch e_frsab = new Exam_Final_Result_Stock_Of_All_Batch();
            e_frsab.ExamId = ddlExamId.SelectedItem.Text;
            e_frsab.StudentId = int.Parse(studentId);
            e_frsab.BatchId = int.Parse(ddlBatch.SelectedValue.ToString());
            e_frsab.ShiftId = int.Parse(ddlShift.SelectedItem.Value);
            e_frsab.ClsSecId = int.Parse(ddlSectionName.SelectedValue.ToString());
            int ClsGrpID = (ddlGroup.Enabled) ? int.Parse(ddlGroup.SelectedItem.Value) : 0;
            e_frsab.ClsGrpID = ClsGrpID;
            e_frsab.FinalGPA_OfExam_WithOptionalSub = FinalGPA_OfExam_WithOptionalSub;
            e_frsab.FinalGrade_OfExam_WithOptionalSub = FinalGrade_OfExam_WithOptionalSub;
            e_frsab.FinalTotalMarks_OfExam_WithOptionalSub = FinalTotalMarks_OfExam_WithOptionalSub;
            e_frsab.FinalGPA_OfExam = FinalGPA_OfExam;
            e_frsab.FinalGrad_OfExam = FinalGrad_OfExam;
            e_frsab.FinalTotalMarks_OfExam = FinalTotalMarks_OfExam;
            e_frsab.PublishDate = TimeZoneBD.getCurrentTimeBD();
            e_frsab.IsFinalExam = (chkForCoutAsFinalResult.Checked) ? false : true;
            return e_frsab;

        }

        private string GetFinalResultGrade(float getTotalPoint)
        {
            try
            {
                for (byte g = 0; g < dtG.Rows.Count; g++)
                {

                    if (getTotalPoint >= float.Parse(dtG.Rows[g]["GPointMin"].ToString()) && getTotalPoint <= double.Parse(dtG.Rows[g]["GPointMax"].ToString()))

                        return dtG.Rows[g]["GName"].ToString();
                }
                return null;
            }
            catch { return null; }
        }

        private void displayTotalFinalResult(string getTable) //p
        {
            try
            {





            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkDependencyExam();
                Exam.getSubjects(ddlSubject,ddlBatch.SelectedValue,ddlGroup.SelectedValue, ddlGroup.Enabled, ddlExamId.SelectedValue);             
            }
            catch { }
        }
        

        private void SaveFileListRecord(string studentId, string SubQPId, string ExamId, string getMarks)  // for entered record as fail 
        {
            try
            {
                // for delete exists record as fail by exam and studentId and others 
                cmd = new SqlCommand("delete from StudentFailList where ExamId='" + ExamId + "'AND StudentId="
                + studentId + " AND SubQPId=" + SubQPId + "", DAL.DbConnection.Connection);
                cmd.ExecuteNonQuery();

                // for entered fail recod in table
                cmd = new SqlCommand("insert into StudentFailList (StudentId,ExamId,SubQpId,getMarks) values ("
                + studentId + ",'" + ExamId + "'," + SubQPId + "," + getMarks + ")", DAL.DbConnection.Connection);
                cmd.ExecuteNonQuery();

            }
            catch { }

        }

        private void checkDependencyExam()
        {
            try
            {
                result = ExamDependencyInfoEntry.checkHasDependencyExam(ddlExamId.SelectedItem.Text);

                if (result)
                {
                    
                    chkForCoutAsFinalResult.Visible = false;
                }
                else
                {
                   
                    chkForCoutAsFinalResult.Visible = true;
                }
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBatch.SelectedValue == "0" || ddlExamId.SelectedValue == "0" || ddlShift.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning-> First Search then Print"; return;
                }
                MarkSheetTitle.Visible = true;
                divMarksheet.Visible = true;
                //Panel1.Visible = true;
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getMarkSheetName = "Class_" + getClass + "MarksSheet";
                loadMarksEntrySheet(getMarkSheetName);
                if (Session["__MarkSheet__"] == null)
                {
                    lblMessage.InnerText = "warning-> Marks Sheet isn't available,Please Search again."; return;
                }
                else
                {
                    DataTable dt = (DataTable)Session["__MarkSheet__"];
                }
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                // "goToNewTab('/Report/MarksSheet.aspx');", true);  //Open New Tab for Sever side code
                string[] Exam = ddlExamId.SelectedItem.Text.Split('_');
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=MarksEntrySheet-" + ddlShift.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + ddlGroup.SelectedItem.Text + "-" + ddlSectionName.SelectedItem.Text + "-" + Exam[0] + "');", true);  //Open New Tab for Sever side code    
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

        protected void btnDeleteCurrentResult_Click(object sender, EventArgs e)
        {
            string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            string getTable = "Class_" + getClass + "MarksSheet";
            Check_And_DeleteExistingResultByExamId(getTable);
            lblMessage.InnerText = "success->Successfully Deleted";
        }



        protected void btnHideSearchOptions_Click(object sender, EventArgs e)
        {
            divSearchPanel.Visible = false;
            tblOp.Visible = false;
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
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());

                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getOptionalSub(getClass, ddlBatch.SelectedValue,
                    ddlExamId.SelectedValue, ddlShift.SelectedValue, ddlGroup.SelectedValue, ddlSectionName.SelectedValue);
                if (dt != null)
                {
                    dtgetfinalResult = new DataTable();
                    dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getExamFinalResult
                        (getClass, ddlShift.SelectedValue, ddlBatch.SelectedValue, ddlGroup.SelectedValue,
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
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        protected void btnResultProcessing_Click(object sender, EventArgs e)
        {
            ResultProcessing();
            setMeritList(ddlExamId.SelectedValue); 
        }
        private void ResultProcessing()
        {
            try {
                string className = ddlBatch.SelectedItem.Text.Trim().Remove(ddlBatch.SelectedItem.Text.Trim().Length-4,4);              
                DataTable dtMarks = new DataTable();
                dtMarks = Classes.Exam.loadMarks(className,ddlExamId.SelectedValue,ddlBatch.SelectedValue,ddlShift.SelectedValue,txtRollNo.Text.Trim());
                DataTable dtStudentInfo = new DataTable();
                dtStudentInfo = Classes.Exam.loadStudentsInfo(className,ddlExamId.SelectedValue, ddlBatch.SelectedValue, ddlShift.SelectedValue,ddlGroup.SelectedValue,ddlSectionName.SelectedValue, txtRollNo.Text.Trim());
                DataTable dtIndividualStudent;               
                for (int i=0;i< dtStudentInfo.Rows.Count;i++)//All Students
                {
                    string StudentID = dtStudentInfo.Rows[i]["StudentId"].ToString();
                    string BatchID = dtStudentInfo.Rows[i]["BatchID"].ToString();
                    string ExamID = dtStudentInfo.Rows[i]["ExamID"].ToString();
                    string ShiftID = dtStudentInfo.Rows[i]["ShiftID"].ToString();
                    string ClsGrpID = dtStudentInfo.Rows[i]["ClsGrpID"].ToString();
                    string ClsSecID = dtStudentInfo.Rows[i]["ClsSecID"].ToString();
                    if (StudentID == "213982")
                    {

                    }
                    string RollNo = dtStudentInfo.Rows[i]["RollNo"].ToString();
                    DataTable dtSubjectsList = new DataTable();
                    dtSubjectsList = Classes.Exam.loadSubjectsByStudent(ddlBatch.SelectedValue, StudentID);// Subjects of this student
                    int sCount = dtSubjectsList.Rows.Count;
                    double mGPA = 0;
                    double opGPA = 0;
                    double GPA = 0;
                    double withOutOptionalGPA = 0;
                    string Grade = "";
                    string withOutOptionalGrade = "";
                    double mMarks = 0;
                    double opMarks = 0;
                    double TotalMarks = 0;
                    double withoutOpTotalMarks = 0;
                    string FailSubjectCode = "";
                    string AbsentSubjectCode = "";
                    string FailAbsentSubjectCode = "";
                    int NumberOfFailSubject = 0;
                    int NumberOfAbsentSubject = 0;
                    bool IsPassed = true;
                    List<ExamResultFailSubject> examResultFailSubjectList = new List<ExamResultFailSubject>();
                    ExamResultFailSubject examResultFailSubject;

                     dtIndividualStudent = new DataTable();
                    dtIndividualStudent = dtMarks.Select("StudentId=" + StudentID).CopyToDataTable(); //Individual Student
                    if (dtIndividualStudent!=null && dtIndividualStudent.Rows.Count > 0)
                    {
                       
                        for (int k = 0; k < sCount; k++)
                        {
                            string _MsStatus= dtSubjectsList.Rows[k]["MsStatus"].ToString();
                         
                            DataTable dtSubMarks = new DataTable();
                            try {
                                dtSubMarks = dtIndividualStudent.Select("SubId=" + dtSubjectsList.Rows[k]["SubId"].ToString()).CopyToDataTable();
                            }
                            catch(Exception ex)
                            {
                                //Absent & failed
                                if (_MsStatus == "1")
                                {
                                    IsPassed = false;
                                    if (!dtSubjectsList.Rows[k]["SubCode"].ToString().Trim().Equals(""))
                                    {
                                        AbsentSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                        FailAbsentSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                    }
                                    NumberOfAbsentSubject++;
                                }

                                examResultFailSubject = new ExamResultFailSubject();
                                examResultFailSubject.SubID = dtSubjectsList.Rows[k]["SubID"].ToString();
                                examResultFailSubject.CourseID = dtSubjectsList.Rows[k]["CourseID"].ToString();
                                examResultFailSubject.IsAbsent = "1";
                                examResultFailSubject.IsOptionalSub = dtSubjectsList.Rows[k]["MsStatus"].ToString();
                                examResultFailSubjectList.Add(examResultFailSubject);

                            }                           
                            if (dtSubMarks != null && dtSubMarks.Rows.Count > 0)
                            {

                                if (dtSubMarks.Rows[0]["IsFailed"].ToString().Equals("0"))
                                {
                                    if (_MsStatus == "1")
                                    {
                                        mGPA += Classes.Exam.getGPA(float.Parse(dtSubMarks.Rows[0]["Marks"].ToString()), float.Parse(dtSubMarks.Rows[0]["FullMarks"].ToString()));
                                        mMarks += int.Parse(dtSubMarks.Rows[0]["Marks"].ToString());
                                    }                                        
                                    else
                                    {
                                        opGPA = Classes.Exam.getGPA(float.Parse(dtSubMarks.Rows[0]["Marks"].ToString()), float.Parse(dtSubMarks.Rows[0]["FullMarks"].ToString()));
                                        opMarks = int.Parse(dtSubMarks.Rows[0]["Marks"].ToString());
                                        if (opGPA > 2)
                                            opGPA = opGPA - 2;//                                    
                                        else
                                        {
                                            opGPA = 0;
                                            opMarks = 0;
                                        }
                                           
                                    }

                                }
                                else
                                {
                                    if (dtSubMarks.Rows[0]["Marks"].ToString().Trim().Equals(""))
                                    {
                                        //Absent & failed in this subject
                                        if (_MsStatus == "1")
                                        {
                                            IsPassed = false;
                                            if (!dtSubjectsList.Rows[k]["SubCode"].ToString().Trim().Equals(""))
                                            {
                                                AbsentSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                                FailAbsentSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                            }
                                            NumberOfAbsentSubject++;
                                        }
                                        examResultFailSubject = new ExamResultFailSubject();
                                        examResultFailSubject.SubID = dtSubjectsList.Rows[k]["SubID"].ToString();
                                        examResultFailSubject.CourseID = dtSubjectsList.Rows[k]["CourseID"].ToString();
                                        examResultFailSubject.IsAbsent = "1";
                                        examResultFailSubject.IsOptionalSub = dtSubjectsList.Rows[k]["MsStatus"].ToString();
                                        examResultFailSubjectList.Add(examResultFailSubject);
                                    }
                                    else
                                    {
                                        //failed in this subject
                                       
                                        if (_MsStatus == "1")
                                        {
                                            
                                            IsPassed = false;
                                            mMarks += int.Parse(dtSubMarks.Rows[0]["Marks"].ToString());
                                            if (!dtSubjectsList.Rows[k]["SubCode"].ToString().Trim().Equals(""))
                                            {
                                                FailSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                                FailAbsentSubjectCode += "," + dtSubjectsList.Rows[k]["SubCode"].ToString().Trim();
                                            }
                                            NumberOfFailSubject++;

                                        }
                                        else
                                        {
                                            opMarks = int.Parse(dtSubMarks.Rows[0]["Marks"].ToString());
                                        }
                                        examResultFailSubject = new ExamResultFailSubject();
                                        examResultFailSubject.SubID = dtSubjectsList.Rows[k]["SubID"].ToString();
                                        examResultFailSubject.CourseID = dtSubjectsList.Rows[k]["CourseID"].ToString();
                                        examResultFailSubject.IsAbsent = "0";
                                        examResultFailSubject.IsOptionalSub = dtSubjectsList.Rows[k]["MsStatus"].ToString();
                                        examResultFailSubjectList.Add(examResultFailSubject);
                                    }
                                    
                                }
                            }
                          

                        }
                        withoutOpTotalMarks = mMarks;
                        TotalMarks = mMarks + opMarks;
                        if (IsPassed)
                        {
                            //Passed                            
                            withOutOptionalGPA = mGPA / (sCount - 1);
                            if (withOutOptionalGPA > 5)
                                withOutOptionalGPA = 5;
                            GPA = (mGPA + opGPA) / (sCount - 1);
                            if (GPA > 5)
                                GPA = 5;
                            Grade = Classes.Exam.getGrade(GPA);
                            withOutOptionalGrade = Classes.Exam.getGrade(withOutOptionalGPA);
                        }
                        else
                        {
                            withOutOptionalGPA = 0;
                            GPA = 0;
                            Grade = "F";
                            withOutOptionalGrade = "F";
                        }
                        
                    }
                    else // No data found for this student, that why it's treat as absent
                    {
                        //absent & failed
                        IsPassed = false;
                        withOutOptionalGPA = 0;
                        GPA = 0;
                        Grade = "F";
                        withOutOptionalGrade = "F";
                      
                        for (int s=0;s<dtSubjectsList.Rows.Count;s++)
                        {
                            if (!dtSubjectsList.Rows[s]["SubCode"].ToString().Trim().Equals(""))
                            {
                                AbsentSubjectCode += "," + dtSubjectsList.Rows[s]["SubCode"].ToString().Trim();
                                FailAbsentSubjectCode += "," + dtSubjectsList.Rows[s]["SubCode"].ToString().Trim();
                                
                            }
                               
                            NumberOfAbsentSubject++;
                            examResultFailSubject = new ExamResultFailSubject();
                            examResultFailSubject.SubID = dtSubjectsList.Rows[s]["SubID"].ToString();
                            examResultFailSubject.CourseID = dtSubjectsList.Rows[s]["CourseID"].ToString();
                            examResultFailSubject.IsAbsent = "1";
                            examResultFailSubject.IsOptionalSub = dtSubjectsList.Rows[s]["MsStatus"].ToString();
                            examResultFailSubjectList.Add(examResultFailSubject);
                        }                     



                    }
                    if (AbsentSubjectCode.Length > 0)
                        AbsentSubjectCode = AbsentSubjectCode.Remove(0,1);
                    if (FailSubjectCode.Length > 0)
                        FailSubjectCode = FailSubjectCode.Remove(0, 1);

                    if (FailAbsentSubjectCode.Length > 0)
                        FailAbsentSubjectCode = FailAbsentSubjectCode.Remove(0, 1);
                    if (class_classWiseMarkSheet_Entry == null)
                    {
                        class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                    }
                    class_classWiseMarkSheet_Entry.InsertToResultSheet(ddlExamId.SelectedValue, BatchID, ShiftID, ClsGrpID, ClsSecID, StudentID, RollNo, Math.Round(GPA, 2).ToString(), Grade, Math.Round(withOutOptionalGPA, 2).ToString(), withOutOptionalGrade, TotalMarks.ToString(), withoutOpTotalMarks.ToString(),IsPassed.ToString(),FailSubjectCode,AbsentSubjectCode, FailAbsentSubjectCode, NumberOfFailSubject.ToString(),NumberOfAbsentSubject.ToString(),(NumberOfFailSubject+NumberOfAbsentSubject).ToString(), examResultFailSubjectList);

                    

                }
            }
            catch (Exception ex) { }
        }
        private void setMeritList(string ExamID)
        {
            try {
                //--------Start clear previous record------
                sqlCmd = "Delete [Exam_ResultMeritList] where ExamID="+ ExamID;
                CRUD.ExecuteQuery(sqlCmd);
                //-------- End clear previous record------

                sqlCmd = @"SELECT SL as [ResultID],[ExamID],[BatchID],[ShiftID],
 StudentID,ClsGrpID,ClsSecID,GPA,TotalMarks,RANK() OVER(PARTITION BY ShiftId

ORDER BY  GPA desc, TotalMarks desc)[ShiftRank],RANK() OVER(PARTITION BY BatchID

ORDER BY  GPA desc, TotalMarks desc)[BatchRank],
 RANK() OVER(PARTITION BY ClsGrpID

ORDER BY  GPA desc, TotalMarks desc)[GrpRank],RANK() OVER(PARTITION BY ClsSecID

ORDER BY  GPA desc, TotalMarks desc)[SecRank]
from Exam_ResultSheet where ExamID ="+ ExamID;
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sqlCmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (class_classWiseMarkSheet_Entry == null)
                        {
                            class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                        }
                        class_classWiseMarkSheet_Entry.InsertMeritlist(dt.Rows[i]["ResultID"].ToString(), dt.Rows[i]["BatchID"].ToString(), dt.Rows[i]["ExamID"].ToString(), dt.Rows[i]["BatchRank"].ToString(), dt.Rows[i]["ShiftID"].ToString(), dt.Rows[i]["ShiftRank"].ToString(), dt.Rows[i]["ClsGrpID"].ToString(), dt.Rows[i]["GrpRank"].ToString(), dt.Rows[i]["ClsSecID"].ToString(), dt.Rows[i]["SecRank"].ToString());
                    }
                    
                }
            }
            catch (Exception ex) { }
        }

        private void EnterdEssentialStudentInfo(string getMarkSheetName)   // for enterd data in marksheet table  of this class
        {
            try
            {
                
                string BatchID = ddlBatch.SelectedItem.Value.ToString();
                string ExId = "", ExamID="", ExamId =ddlExamId.SelectedValue;
                DataTable dtExInfo = new DataTable();
                dtExInfo=CRUD.ReturnTableNull("select ExInSl,ExInId,ExId from ExamInfo where ExInSl='" + ExamId + "'");
                if (dtExInfo == null || dtExInfo.Rows.Count == 0) return;
                ExId = dtExInfo.Rows[0]["ExId"].ToString();
              
                ExamID = dtExInfo.Rows[0]["ExInSl"].ToString();
                string ClsSecID = ddlSectionName.SelectedValue;
                string ClsGrpID = ddlGroup.SelectedValue;
                
                 /* load total student of this class by batch */
                 DataTable dtCS = new DataTable();  //CS =current student
                if (!ddlGroup.Visible)
                    sqlCmd = "select ee.StudentId,RollNo,ee.ClsSecId,ConfigId from ExamExaminee ee inner join CurrentStudentInfo csi on ee.StudentID=csi.StudentId and ee.BatchID=csi.BatchID  where ee.BatchId='"+ BatchID + "' and ee.ExamID="+ ExamID + " order by StudentId";
                else
                    sqlCmd = "select ee.StudentId,RollNo,ee.ClsSecId,ConfigId from ExamExaminee ee inner join CurrentStudentInfo csi on ee.StudentID=csi.StudentId and ee.BatchID=csi.BatchID where  ee.BatchID='" + BatchID + "' and ee.ClsGrpID=" + ClsGrpID + " and ee.ClsSecID=" + ClsSecID + " and ee.StudentId  not in(select Distinct StudentId from " + getMarkSheetName + " where  BatchID='" + BatchID + "' and ClsGrpID=" + ClsGrpID + " and ClsSecID=" + ClsSecID + "  and ExamID='" + ExamID + "') order by StudentId";
                dtCS = new DataTable();
                dtCS = CRUD.ReturnTableNull(sqlCmd);
                if (dtCS != null && dtCS.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCS.Rows.Count; i++)
                {
                        ///* load total subject question pattern id of this class */
                        DataTable dtSQPId = new DataTable();  // SQP=Subject 
                        if (!ddlGroup.Visible)
                            sqlCmd = "select distinct SubQPId,ConvertTo,SubId,CourseId from v_SubjectQuestionPattern where ExId=" + ExId + " AND BatchId='" + BatchID + "' and SubId in(select SubId from ClassSubject  where ClassID in (select ClassID from  BatchInfo where BatchId='" + BatchID + "') and IsCommon=1 and IsOptional=0 union all select SubId from v_StudentGroupSubSetup where StudentId = " + dtCS.Rows[i]["StudentId"].ToString() + ") order by SubQPId";
                        else
                            sqlCmd = "select distinct SubQPId,ConvertTo,SubId,CourseId from v_SubjectQuestionPattern where ExId=" + ExId + " AND BatchId='" + BatchID + "' AND ClsGrpId='" + ClsGrpID + "' and SubId in(select SubId from ClassSubject  where ClassID in (select ClassID from  BatchInfo where BatchId='" + BatchID + "') and IsCommon=1 and IsOptional=0 union all select SubId from v_StudentGroupSubSetup where StudentId = " + dtCS.Rows[i]["StudentId"].ToString() + ") order by SubQPId";
                        dtSQPId = CRUD.ReturnTableNull(sqlCmd);

                        for (byte b = 0; b < dtSQPId.Rows.Count; b++)
                    {
                        string getGroup = (ddlGroup.Visible) ? ddlGroup.SelectedItem.Value : "0";
                       
                        //--------------
                        CRUD.ExecuteQuery("insert into " + getMarkSheetName + " (ExId, ExamID, StudentId, RollNo, BatchId, ClsSecId, ShiftId, SubQPId, ConvertToPercentage, ClsGrpID, SubId, CourseId)" +
                            " values('" + ExId + "', '" + ExamID + "','" + dtCS.Rows[i]["StudentId"].ToString() + "','" + dtCS.Rows[i]["RollNo"].ToString() +
                            "','" + BatchID + "','" + dtCS.Rows[i]["ClsSecId"].ToString() + "','" + dtCS.Rows[i]["ConfigId"].ToString() + "', '" + dtSQPId.Rows[b]["SubQPId"].ToString() +
                            "','" + dtSQPId.Rows[b]["ConvertTo"].ToString() + "', '" + getGroup + "','" + dtSQPId.Rows[b]["SubId"].ToString() + "','" + dtSQPId.Rows[b]["CourseId"].ToString() + "' )");

                        //---------------
                    }
                }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}